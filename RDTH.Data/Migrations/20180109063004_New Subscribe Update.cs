using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RDTH.Data.Migrations
{
    public partial class NewSubscribeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "NewSubscribes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SetBoxId",
                table: "NewSubscribes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewSubscribes_PackageId",
                table: "NewSubscribes",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_NewSubscribes_SetBoxId",
                table: "NewSubscribes",
                column: "SetBoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewSubscribes_Packages_PackageId",
                table: "NewSubscribes",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewSubscribes_SetBoxes_SetBoxId",
                table: "NewSubscribes",
                column: "SetBoxId",
                principalTable: "SetBoxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewSubscribes_Packages_PackageId",
                table: "NewSubscribes");

            migrationBuilder.DropForeignKey(
                name: "FK_NewSubscribes_SetBoxes_SetBoxId",
                table: "NewSubscribes");

            migrationBuilder.DropIndex(
                name: "IX_NewSubscribes_PackageId",
                table: "NewSubscribes");

            migrationBuilder.DropIndex(
                name: "IX_NewSubscribes_SetBoxId",
                table: "NewSubscribes");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "NewSubscribes");

            migrationBuilder.DropColumn(
                name: "SetBoxId",
                table: "NewSubscribes");
        }
    }
}
