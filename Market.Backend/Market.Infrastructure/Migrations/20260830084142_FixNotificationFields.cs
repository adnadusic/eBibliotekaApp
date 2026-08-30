using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixNotificationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VezanoZaId",
                table: "Obavijesti");

            migrationBuilder.DropColumn(
                name: "VezanoZaTip",
                table: "Obavijesti");

            migrationBuilder.AlterColumn<bool>(
                name: "Procitano",
                table: "Obavijesti",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Procitano",
                table: "Obavijesti",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "VezanoZaId",
                table: "Obavijesti",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VezanoZaTip",
                table: "Obavijesti",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
