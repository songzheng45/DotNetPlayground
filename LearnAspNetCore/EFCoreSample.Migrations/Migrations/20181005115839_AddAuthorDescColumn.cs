using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreSample.Migrations.Migrations
{
    public partial class AddAuthorDescColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Authors",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "PostDate", "UpdateTime" },
                values: new object[] { new DateTime(2018, 10, 5, 19, 58, 39, 194, DateTimeKind.Local).AddTicks(5771), new DateTime(2018, 10, 5, 19, 58, 39, 195, DateTimeKind.Local).AddTicks(130) });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "PostDate", "UpdateTime" },
                values: new object[] { new DateTime(2018, 10, 5, 19, 58, 39, 195, DateTimeKind.Local).AddTicks(650), new DateTime(2018, 10, 5, 19, 58, 39, 195, DateTimeKind.Local).AddTicks(656) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Authors");

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "PostDate", "UpdateTime" },
                values: new object[] { new DateTime(2018, 10, 5, 19, 35, 21, 949, DateTimeKind.Local).AddTicks(5595), new DateTime(2018, 10, 5, 19, 35, 21, 949, DateTimeKind.Local).AddTicks(9814) });

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "PostDate", "UpdateTime" },
                values: new object[] { new DateTime(2018, 10, 5, 19, 35, 21, 950, DateTimeKind.Local).AddTicks(215), new DateTime(2018, 10, 5, 19, 35, 21, 950, DateTimeKind.Local).AddTicks(222) });
        }
    }
}
