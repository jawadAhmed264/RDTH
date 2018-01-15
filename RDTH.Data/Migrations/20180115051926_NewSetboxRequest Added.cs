using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RDTH.Data.Migrations
{
    public partial class NewSetboxRequestAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Customers_CustomerId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CustomerId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "NewSetBoxRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CardId = table.Column<int>(nullable: true),
                    SetboxId = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewSetBoxRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewSetBoxRequest_CustomerCards_CardId",
                        column: x => x.CardId,
                        principalTable: "CustomerCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewSetBoxRequest_SetBoxes_SetboxId",
                        column: x => x.SetboxId,
                        principalTable: "SetBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewSetBoxRequest_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewSetBoxRequest_CardId",
                table: "NewSetBoxRequest",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_NewSetBoxRequest_SetboxId",
                table: "NewSetBoxRequest",
                column: "SetboxId");

            migrationBuilder.CreateIndex(
                name: "IX_NewSetBoxRequest_StatusId",
                table: "NewSetBoxRequest",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewSetBoxRequest");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CustomerId",
                table: "Payments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Customers_CustomerId",
                table: "Payments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
