using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infra.ORM.Migrations
{
    /// <inheritdoc />
    public partial class SeedCustomerData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Customer (Name, Email, Phone) VALUES ('John Doe', 'john.doe@example.com', '559999999999');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM Customer");
        }
    }
}