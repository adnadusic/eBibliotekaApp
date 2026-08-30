using Market.Domain.Common;
using Market.Domain.Entities.Catalog;
using Market.Domain.Entities.Identity;
using Market.Infrastructure.Database.Seeders;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Text.Json;

namespace Market.Infrastructure.Database;

public partial class DatabaseContext
{
    private static readonly HashSet<string> IgnoredAuditProperties =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "Id",
            "CreatedAtUtc",
            "ModifiedAtUtc",
            "PasswordHash",
            "TokenHash",
            "Fingerprint"
        };

    private DateTime UtcNow => _clock.GetUtcNow().UtcDateTime;

    private void ApplyAuditAndSoftDelete()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAtUtc = UtcNow;
                    entry.Entity.ModifiedAtUtc = null;
                    entry.Entity.IsDeleted = false;
                    break;

                case EntityState.Modified:
                    entry.Entity.ModifiedAtUtc = UtcNow;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.ModifiedAtUtc = UtcNow;
                    break;
            }
        }
    }

    private List<PendingAuditLog> PrepareAuditLogs()
    {
        ChangeTracker.DetectChanges();

        var pendingAuditLogs = new List<PendingAuditLog>();

        var entries = ChangeTracker
            .Entries<BaseEntity>()
            .Where(entry =>
                entry.State == EntityState.Added ||
                entry.State == EntityState.Modified ||
                entry.State == EntityState.Deleted)
            .ToList();

        foreach (var entry in entries)
        {
            if (entry.Entity is RefreshTokenEntity)
            {
                continue;
            }

            var isSoftDelete =
                entry.State == EntityState.Modified &&
                entry.Entity.IsDeleted &&
                entry.Property(nameof(BaseEntity.IsDeleted)).IsModified &&
                Equals(
                    entry.Property(nameof(BaseEntity.IsDeleted)).OriginalValue,
                    false);

            var isDelete =
                entry.State == EntityState.Deleted ||
                isSoftDelete;

            var action = isDelete
                ? "Delete"
                : entry.State switch
                {
                    EntityState.Added => "Create",
                    EntityState.Modified => "Update",
                    _ => string.Empty
                };

            var oldValues = new Dictionary<string, object?>();
            var newValues = new Dictionary<string, object?>();

            foreach (var property in entry.Properties)
            {
                var propertyName = property.Metadata.Name;

                if (IgnoredAuditProperties.Contains(propertyName))
                {
                    continue;
                }

                if (isDelete)
                {
                    oldValues[propertyName] = property.OriginalValue;
                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        newValues[propertyName] = property.CurrentValue;
                        break;

                    case EntityState.Modified:
                        if (!property.IsModified)
                        {
                            continue;
                        }

                        oldValues[propertyName] = property.OriginalValue;
                        newValues[propertyName] = property.CurrentValue;
                        break;
                }
            }

            if (action == "Update" && newValues.Count == 0)
            {
                continue;
            }

            var auditLog = new AuditLog
            {
                UserId = _currentUser.UserId,
                UserEmail = _currentUser.Email,
                EntityName = entry.Metadata.ClrType.Name,
                EntityId = entry.State == EntityState.Added
                    ? null
                    : GetEntityId(entry),
                Action = action,
                OldValues = oldValues.Count == 0
                    ? null
                    : JsonSerializer.Serialize(oldValues),
                NewValues = newValues.Count == 0
                    ? null
                    : JsonSerializer.Serialize(newValues),
                ChangedAtUtc = UtcNow
            };

            pendingAuditLogs.Add(
                new PendingAuditLog(
                    entry,
                    auditLog,
                    entry.State == EntityState.Added));
        }

        return pendingAuditLogs;
    }

    private static string? GetEntityId(
        EntityEntry<BaseEntity> entry)
    {
        var primaryKey = entry.Metadata.FindPrimaryKey();

        if (primaryKey is null)
        {
            return null;
        }

        var keyValues = primaryKey.Properties
            .Select(property =>
                entry.Property(property.Name)
                    .CurrentValue?
                    .ToString())
            .Where(value => !string.IsNullOrWhiteSpace(value));

        var result = string.Join(",", keyValues);

        return string.IsNullOrWhiteSpace(result)
            ? null
            : result;
    }

    private void SaveAuditLogs(
        List<PendingAuditLog> pendingAuditLogs)
    {
        if (pendingAuditLogs.Count == 0)
        {
            return;
        }

        foreach (var pendingAuditLog in pendingAuditLogs)
        {
            if (pendingAuditLog.NeedsEntityId)
            {
                pendingAuditLog.AuditLog.EntityId =
                    GetEntityId(pendingAuditLog.Entry);
            }
        }

        AuditLogs.AddRange(
            pendingAuditLogs.Select(x => x.AuditLog));
    }

    protected override void ConfigureConventions(
        ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<decimal>()
            .HavePrecision(18, 2);

        configurationBuilder
            .Properties<decimal?>()
            .HavePrecision(18, 2);
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(DatabaseContext).Assembly);

        ApplyGlobalFielters(modelBuilder);

        StaticDataSeeder.Seed(modelBuilder);
    }

    private void ApplyGlobalFielters(
        ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter =
                    Expression.Parameter(entityType.ClrType, "e");

                var prop =
                    Expression.Property(
                        parameter,
                        nameof(BaseEntity.IsDeleted));

                var compare =
                    Expression.Equal(
                        prop,
                        Expression.Constant(false));

                var lambda =
                    Expression.Lambda(compare, parameter);

                modelBuilder
                    .Entity(entityType.ClrType)
                    .HasQueryFilter(lambda);
            }
        }
    }

    public override int SaveChanges()
    {
        var pendingAuditLogs = PrepareAuditLogs();

        ApplyAuditAndSoftDelete();

        var ownsTransaction = Database.CurrentTransaction is null;
        using var transaction = ownsTransaction
            ? Database.BeginTransaction()
            : null;

        try
        {
            var result = base.SaveChanges();

            if (pendingAuditLogs.Count > 0)
            {
                SaveAuditLogs(pendingAuditLogs);
                base.SaveChanges();
            }

            transaction?.Commit();

            return result;
        }
        catch
        {
            transaction?.Rollback();
            throw;
        }
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var pendingAuditLogs = PrepareAuditLogs();

        ApplyAuditAndSoftDelete();

        var ownsTransaction = Database.CurrentTransaction is null;
        await using var transaction = ownsTransaction
            ? await Database.BeginTransactionAsync(cancellationToken)
            : null;

        try
        {
            var result =
                await base.SaveChangesAsync(cancellationToken);

            if (pendingAuditLogs.Count > 0)
            {
                SaveAuditLogs(pendingAuditLogs);

                await base.SaveChangesAsync(
                    cancellationToken);
            }

            if (transaction is not null)
            {
                await transaction.CommitAsync(cancellationToken);
            }

            return result;
        }
        catch
        {
            if (transaction is not null)
            {
                await transaction.RollbackAsync(
                    CancellationToken.None);
            }

            throw;
        }
    }

    private sealed record PendingAuditLog(
        EntityEntry<BaseEntity> Entry,
        AuditLog AuditLog,
        bool NeedsEntityId);
}