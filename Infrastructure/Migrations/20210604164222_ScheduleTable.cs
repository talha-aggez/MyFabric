using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ScheduleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AppUsers_AppUserID",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserID",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(nullable: false),
                    WorkCenterID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false),
                    WorkCenterOperationID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Schedules_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_WorkCenters_WorkCenterID",
                        column: x => x.WorkCenterID,
                        principalTable: "WorkCenters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_WorkCenterOperations_WorkCenterOperationID",
                        column: x => x.WorkCenterOperationID,
                        principalTable: "WorkCenterOperations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_OrderID",
                table: "Schedules",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ProductID",
                table: "Schedules",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_WorkCenterID",
                table: "Schedules",
                column: "WorkCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_WorkCenterOperationID",
                table: "Schedules",
                column: "WorkCenterOperationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AppUsers_AppUserID",
                table: "Orders",
                column: "AppUserID",
                principalTable: "AppUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AppUsers_AppUserID",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserID",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AppUsers_AppUserID",
                table: "Orders",
                column: "AppUserID",
                principalTable: "AppUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
