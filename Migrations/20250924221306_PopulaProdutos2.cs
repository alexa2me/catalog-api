using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalog.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Inventory, RegistrationDate, CategoryId) " +
                "VALUES ('Suco de uva', 'Suco de uva natural', 5.00, 'suco_uva.jpg', 100, NOW(), (SELECT Id FROM Categories WHERE Name = 'Bebidas'))");

            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Inventory, RegistrationDate, CategoryId) " +
                "VALUES ('Abacatada', 'Vitamina de abacate', 2.50, 'abacatada.jpg', 100, NOW(), (SELECT Id FROM Categories WHERE Name = 'Bebidas'))");

            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Inventory, RegistrationDate, CategoryId) " +
                "VALUES ('Bolo de banana', 'Bolo de banana com chocolate', 10.00, 'bolo_banana.jpg', 50, NOW(), (SELECT Id FROM Categories WHERE Name = 'Sobremesas'))");

            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Inventory, RegistrationDate, CategoryId) " +
                "VALUES ('Mousse de maracujá', 'Mousse de maracujá com chocolate', 15.00, 'mousse_maracuja.jpg', 80, NOW(), (SELECT Id FROM Categories WHERE Name = 'Sobremesas'))");

            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Inventory, RegistrationDate, CategoryId) " +
                "VALUES ('Batata frita', 'Batata frita crocante', 12.00, 'batata_frita.jpg', 150, NOW(), (SELECT Id FROM Categories WHERE Name = 'Lanches'))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}
