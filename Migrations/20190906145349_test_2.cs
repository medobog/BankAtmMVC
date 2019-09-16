using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Migrations
{
    public partial class test_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Used",
                table: "Vouchers",
                nullable: false,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Used",
                table: "Vouchers",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
