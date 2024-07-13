using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspnetcore.ntier.DAL.Migrations
{
    public partial class AlpacaTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlpacaTransactions",
                columns: table => new
                {
                    Tr_Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    User_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Activity_type = table.Column<string>(type: "TEXT", nullable: false),
                    Cum_qty = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Leaves_Qty = table.Column<string>(type: "TEXT", nullable: false),
                    Order_Id = table.Column<string>(type: "TEXT", nullable: false),
                    Order_Status = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<string>(type: "TEXT", nullable: false),
                    Qty = table.Column<string>(type: "TEXT", nullable: false),
                    Side = table.Column<string>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    Transaction_Time = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlpacaTransactions", x => x.Tr_Id);
                    table.ForeignKey(
                        name: "FK_AlpacaTransactions_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ballance", "ConcurrencyStamp" },
                values: new object[] { "2000", "0bd87629-dd15-4eb4-a11a-ac8bf1e26d95" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ballance", "ConcurrencyStamp" },
                values: new object[] { "2000", "a696c63d-82b3-4123-9d19-3a3859af7f30" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "8077abac-a4ba-49ea-a1d8-ac269751e061");

            migrationBuilder.CreateIndex(
                name: "IX_AlpacaTransactions_User_Id",
                table: "AlpacaTransactions",
                column: "User_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlpacaTransactions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Ballance", "ConcurrencyStamp" },
                values: new object[] { "10000", "7100cd9d-93b7-4dfe-a827-047eacfdcdd7" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Ballance", "ConcurrencyStamp" },
                values: new object[] { "10000", "28c01359-2803-4339-bad1-947bc7a79594" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "638c53cb-040c-4642-9bb1-7f3d51fb61f1");
        }
    }
}
