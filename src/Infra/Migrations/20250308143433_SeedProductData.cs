using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infra.ORM.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO Product(Name, Price) VALUES ('Bicicleta MTB KRW Aro 20 com câmbio 7 velocidades', 836.00), 
                                                                           ('Notebook Dell Inspiron I15', 3219), 
                                                                           ('Calculadora Financeira HP 12C', 306.25)");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM Product");
        }
    }
}
