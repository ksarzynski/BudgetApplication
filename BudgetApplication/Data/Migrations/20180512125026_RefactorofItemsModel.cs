using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BudgetApplication.Data.Migrations
{
    public partial class RefactorofItemsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryID",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Subcategories_SubcategoryId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CategoryID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "SubcategoryId",
                table: "Items",
                newName: "SubcategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Items_SubcategoryId",
                table: "Items",
                newName: "IX_Items_SubcategoryID");

            migrationBuilder.AlterColumn<int>(
                name: "SubcategoryID",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Subcategories_SubcategoryID",
                table: "Items",
                column: "SubcategoryID",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Subcategories_SubcategoryID",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "SubcategoryID",
                table: "Items",
                newName: "SubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_SubcategoryID",
                table: "Items",
                newName: "IX_Items_SubcategoryId");

            migrationBuilder.AlterColumn<int>(
                name: "SubcategoryId",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryID",
                table: "Items",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryID",
                table: "Items",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Subcategories_SubcategoryId",
                table: "Items",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
