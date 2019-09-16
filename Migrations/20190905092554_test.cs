using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_Accounts_AccountID",
                table: "Voucher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Voucher",
                table: "Voucher");

            migrationBuilder.RenameTable(
                name: "Voucher",
                newName: "Vouchers");

            migrationBuilder.RenameIndex(
                name: "IX_Voucher_AccountID",
                table: "Vouchers",
                newName: "IX_Vouchers_AccountID");

            migrationBuilder.AlterColumn<float>(
                name: "Duration",
                table: "Vouchers",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers",
                column: "VoucherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_Accounts_AccountID",
                table: "Vouchers",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_Accounts_AccountID",
                table: "Vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers");

            migrationBuilder.RenameTable(
                name: "Vouchers",
                newName: "Voucher");

            migrationBuilder.RenameIndex(
                name: "IX_Vouchers_AccountID",
                table: "Voucher",
                newName: "IX_Voucher_AccountID");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Voucher",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Voucher",
                table: "Voucher",
                column: "VoucherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_Accounts_AccountID",
                table: "Voucher",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
