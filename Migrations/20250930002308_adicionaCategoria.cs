using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalog.Migrations
{
    /// <inheritdoc />
    public partial class adicionaCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categories (Name, ImageUrl) VALUES ('Substantivo', 'https://www.beveragedaily.com/resizer/v2/QSRDJ7WC7RP6XAPDJSS4ZS5F2A.jpg?auth=db930caaa79100ecb2af1a5b95f3b92843cf33846a1df0f391c58a77f79d0b7f')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categories WHERE Name = 'Substantivo'");
        }
    }
}