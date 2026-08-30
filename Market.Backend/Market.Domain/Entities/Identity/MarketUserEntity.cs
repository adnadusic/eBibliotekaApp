using Market.Domain.Common;
using Market.Domain.Entities.Catalog;

namespace Market.Domain.Entities.Identity;

public sealed class MarketUserEntity : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    public int? CityId { get; set; }
    public City? City { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public bool IsAdmin { get; set; }
    public bool IsManager { get; set; }
    public bool IsEmployee { get; set; }

    public int TokenVersion { get; set; } = 0; // For global revocation
    public bool IsEnabled { get; set; }

    public ICollection<RefreshTokenEntity> RefreshTokens { get; private set; }
        = new List<RefreshTokenEntity>();
}