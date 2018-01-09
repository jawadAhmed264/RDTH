using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RDTH.Data.Migrations
{
    public partial class statusupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPackages_ViewStatus_StatusId",
                table: "CustomerPackages");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedBacks_ViewStatus_StatusId",
                table: "FeedBacks");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesOnDemand_ViewStatus_StatusId",
                table: "MoviesOnDemand");

            migrationBuilder.DropForeignKey(
                name: "FK_NewSubscribes_ViewStatus_StatusId",
                table: "NewSubscribes");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ViewStatus_StatusId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ViewStatus",
                table: "ViewStatus");

            migrationBuilder.RenameTable(
                name: "ViewStatus",
                newName: "Status");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPackages_Status_StatusId",
                table: "CustomerPackages",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedBacks_Status_StatusId",
                table: "FeedBacks",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesOnDemand_Status_StatusId",
                table: "MoviesOnDemand",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewSubscribes_Status_StatusId",
                table: "NewSubscribes",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Status_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerPackages_Status_StatusId",
                table: "CustomerPackages");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedBacks_Status_StatusId",
                table: "FeedBacks");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesOnDemand_Status_StatusId",
                table: "MoviesOnDemand");

            migrationBuilder.DropForeignKey(
                name: "FK_NewSubscribes_Status_StatusId",
                table: "NewSubscribes");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Status_StatusId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "ViewStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ViewStatus",
                table: "ViewStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerPackages_ViewStatus_StatusId",
                table: "CustomerPackages",
                column: "StatusId",
                principalTable: "ViewStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedBacks_ViewStatus_StatusId",
                table: "FeedBacks",
                column: "StatusId",
                principalTable: "ViewStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesOnDemand_ViewStatus_StatusId",
                table: "MoviesOnDemand",
                column: "StatusId",
                principalTable: "ViewStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewSubscribes_ViewStatus_StatusId",
                table: "NewSubscribes",
                column: "StatusId",
                principalTable: "ViewStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ViewStatus_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "ViewStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
