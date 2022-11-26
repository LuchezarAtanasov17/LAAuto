using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAAuto.Entities.Migrations
{
    public partial class updateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("62448744-4356-44dc-a005-0bfb6ba9e8b2"),
                column: "ConcurrencyStamp",
                value: "e8d44782-291f-430d-a980-04c2feb0f41f");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("62448744-4356-44dc-a005-0bfb6ba9e8b2"),
                column: "ConcurrencyStamp",
                value: "5a537c61-897f-4c74-80bf-9407c498100a");
        }
    }
}
