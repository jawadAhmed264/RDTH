using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RDTH.Data.Migrations
{
    public partial class addnewCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetBoxes_Carts_CartId",
                table: "SetBoxes");

            migrationBuilder.DropIndex(
                name: "IX_SetBoxes_CartId",
                table: "SetBoxes");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "SetBoxes");

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonName",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CartId = table.Column<int>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: true),
                    Qty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartItems_SetBoxes_ProductId",
                        column: x => x.ProductId,
                        principalTable: "SetBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PersonName",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PersonName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "SetBoxes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SetBoxes_CartId",
                table: "SetBoxes",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_SetBoxes_Carts_CartId",
                table: "SetBoxes",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
