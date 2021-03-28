using Microsoft.EntityFrameworkCore.Migrations;

namespace WebsiteShoe.Migrations
{
    public partial class updateTbaleAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Admins",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Admins");
        }
    }
}
