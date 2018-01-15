using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RDTH.Data.Migrations
{
    public partial class customerPackageUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerCardId",
                table: "CustomerPackages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPackages_CustomerCardId",
                table: "CustomerPackages",
                column: "CustomerCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPackages_CustomerCards_CustomerCardId",
                table: "CustomerPackages",
                column: "CustomerCardId",
                principalTable: "CustomerCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPackages_CustomerCards_CustomerCardId",
                table: "CustomerPackages");

            migrationBuilder.DropIndex(
                name: "IX_CustomerPackages_CustomerCardId",
                table: "CustomerPackages");

            migrationBuilder.DropColumn(
                name: "CustomerCardId",
                table: "CustomerPackages");
        }
    }
}
