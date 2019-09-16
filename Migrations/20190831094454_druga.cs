using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Migrations
{
    public partial class druga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserKey",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserKey",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserKey",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserKey",
                table: "Accounts");
        }
    }
}
