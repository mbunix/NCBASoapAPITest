using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NCBASoapAPICountryServices.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CapitalCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryFlag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryISOCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyISOCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryPhoneCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContinentCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
