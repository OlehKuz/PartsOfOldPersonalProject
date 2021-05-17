using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Persistance.Migrations
{
    public partial class OrderPaymentInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    PaymentMadeDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientsId = table.Column<long>(nullable: false),
                    StartHour = table.Column<DateTime>(nullable: false),
                    EndHour = table.Column<DateTime>(nullable: false),
                    ServicesId = table.Column<long>(nullable: false),
                    Customerconfirmed = table.Column<bool>(nullable: false),
                    UnpayedOrderExpirationTime = table.Column<DateTime>(nullable: false),
                    Canceled = table.Column<bool>(nullable: false),
                    ReasonDeclined = table.Column<string>(nullable: true),
                    Masterconfirmed = table.Column<bool>(nullable: false),
                    PaymentsId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Payments_PaymentsId",
                        column: x => x.PaymentsId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientsId",
                table: "Orders",
                column: "ClientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentsId",
                table: "Orders",
                column: "PaymentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ServicesId",
                table: "Orders",
                column: "ServicesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
