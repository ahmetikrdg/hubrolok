using Microsoft.EntityFrameworkCore.Migrations;

namespace Halic.Data.Migrations
{
    public partial class CreateTwitterColumnInAuthorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Linkedin",
                table: "Authors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Order",
                table: "Authors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Authors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Linkedin",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Authors");
        }
    }
}
