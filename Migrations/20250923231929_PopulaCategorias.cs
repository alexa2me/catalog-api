using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalog.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories (Name, ImageUrl) VALUES ('Bebidas', 'https://www.beveragedaily.com/resizer/v2/QSRDJ7WC7RP6XAPDJSS4ZS5F2A.jpg?auth=db930caaa79100ecb2af1a5b95f3b92843cf33846a1df0f391c58a77f79d0b7f')");
            migrationBuilder.Sql("INSERT INTO Categories (Name, ImageUrl) VALUES ('Lanches', 'https://img.freepik.com/fotos-gratis/vista-superior-alimentos-e-lanches-nao-saudaveis_23-2148541053.jpg')");
            migrationBuilder.Sql("INSERT INTO Categories (Name, ImageUrl) VALUES ('Sobremesas', 'https://diariodonordeste.verdesmares.com.br/image/contentid/policy:1.3315739:1671798630/Sobremesas-Ano-novo.jpg?f=default&$p$f=ad1d6c0')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories");
        }
    }
}