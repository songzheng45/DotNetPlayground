using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreSample.Migrations.Migrations
{
    public partial class AddSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Blogs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "ProfilePhoto", "UserName" },
                values: new object[] { 100, "", "Joe" });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "AuthorId", "PostDate", "Title", "UpdateTime" },
                values: new object[] { 100, 100, new DateTime(2018, 10, 5, 19, 35, 21, 949, DateTimeKind.Local).AddTicks(5595), "HOw to drive care", new DateTime(2018, 10, 5, 19, 35, 21, 949, DateTimeKind.Local).AddTicks(9814) });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "AuthorId", "PostDate", "Title", "UpdateTime" },
                values: new object[] { 101, 100, new DateTime(2018, 10, 5, 19, 35, 21, 950, DateTimeKind.Local).AddTicks(215), "Let's go to Thailand", new DateTime(2018, 10, 5, 19, 35, 21, 950, DateTimeKind.Local).AddTicks(222) });

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs");

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Blogs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Authors_AuthorId",
                table: "Blogs",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
