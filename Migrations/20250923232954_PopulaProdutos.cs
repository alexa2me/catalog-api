using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalog.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Inventory, RegistrationDate, CategoryId) " +
                "VALUES ('Suco de laranja', 'Suco de laranja natural', 5.00, 'suco_laranja.jpg', 100, NOW(), 1)");

            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Inventory, RegistrationDate, CategoryId) " +
                "VALUES ('Abalarançananaxi', 'Suco especial do Castelo Rá-Tim-Bum', 2.50, 'abalarancananaxi.jpg', 100, NOW(), 1)");

            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Inventory, RegistrationDate, CategoryId) " +
                "VALUES ('Brownie', 'Brownie de chocolate', 10.00, 'brownie.jpg', 50, NOW(), 2)");

            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Inventory, RegistrationDate, CategoryId) " +
                "VALUES ('Pudim', 'Pudim de leite condensado', 15.00, 'pudim.jpg', 80, NOW(), 1)");

            migrationBuilder.Sql("INSERT INTO Products (Name, Description, Price, ImageUrl, Inventory, RegistrationDate, CategoryId) " +
                "VALUES ('Sanduíche veg', 'Sanduíche vegano com abacate', 12.00, 'sandwich_veg.jpg', 150, NOW(), 1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}
