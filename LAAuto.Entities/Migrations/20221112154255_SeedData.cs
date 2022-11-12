using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAAuto.Entities.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0c1237b8-2fe4-43f7-b6dc-2a0a4ef0713d"), "Смяна на масло" },
                    { new Guid("7294f257-a657-4797-8fce-272319ade2f9"), "Преглед" },
                    { new Guid("e5dea772-dfa1-43ab-a89f-0a91df10123b"), "Диагностика" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("62448744-4356-44dc-a005-0bfb6ba9e8b2"), 0, "4c0068b9-15b8-458b-afa3-4f9bd17e59a6", "client@mail.com", false, "Pesho", "Peshov", false, null, "CLIENT@MAIL.COM", "CLIENT", null, null, false, null, false, "Client" });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("95a9873f-d6f4-4496-bf75-62be88716460"), 0, "ecb0a22a-5143-434d-bc54-5e804eeca3b8", "owner@mail.com", false, "Ivan", "Ivanov", false, null, "OWNER@MAIL.COM", "OWNER", null, null, false, null, false, "Owner" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CloseTime", "Description", "Location", "Name", "OpenTime", "OwnerId" },
                values: new object[] { new Guid("9226a3f4-35aa-4817-adcd-1c033cf739ad"), new TimeSpan(0, 18, 0, 0, 0), null, "Гр.Пловдив, кв.Кичука, ул.Македония 12", "Autox", new TimeSpan(0, 8, 0, 0, 0), new Guid("95a9873f-d6f4-4496-bf75-62be88716460") });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CloseTime", "Description", "Location", "Name", "OpenTime", "OwnerId" },
                values: new object[] { new Guid("e17b327f-eee6-4011-9905-bc8360cd5e66"), new TimeSpan(0, 18, 0, 0, 0), null, "Гр.София, кв.Надежда, ул.Стамболийски 36", "Carx", new TimeSpan(0, 9, 0, 0, 0), new Guid("95a9873f-d6f4-4496-bf75-62be88716460") });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CloseTime", "Description", "Location", "Name", "OpenTime", "OwnerId" },
                values: new object[] { new Guid("fce201d7-e941-4f41-b3be-0c265798ede9"), new TimeSpan(0, 20, 0, 0, 0), null, "Гр.Varna, кв.Владиславово, ул.Георги Минков 3", "CarKing", new TimeSpan(0, 10, 0, 0, 0), new Guid("95a9873f-d6f4-4496-bf75-62be88716460") });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "CategoryId", "ClientId", "Description", "EndDate", "ServiceId", "StartDate" },
                values: new object[] { new Guid("9d8afaca-f28c-4fce-bc14-5c3363633323"), new Guid("7294f257-a657-4797-8fce-272319ade2f9"), new Guid("62448744-4356-44dc-a005-0bfb6ba9e8b2"), null, new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e17b327f-eee6-4011-9905-bc8360cd5e66"), new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "ClientId", "ServiceId", "Value" },
                values: new object[] { new Guid("eecdc117-7fbe-4c46-bbf9-8507b45c0d88"), new Guid("62448744-4356-44dc-a005-0bfb6ba9e8b2"), new Guid("e17b327f-eee6-4011-9905-bc8360cd5e66"), 4 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: new Guid("9d8afaca-f28c-4fce-bc14-5c3363633323"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0c1237b8-2fe4-43f7-b6dc-2a0a4ef0713d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e5dea772-dfa1-43ab-a89f-0a91df10123b"));

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: new Guid("eecdc117-7fbe-4c46-bbf9-8507b45c0d88"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("9226a3f4-35aa-4817-adcd-1c033cf739ad"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("fce201d7-e941-4f41-b3be-0c265798ede9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7294f257-a657-4797-8fce-272319ade2f9"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("62448744-4356-44dc-a005-0bfb6ba9e8b2"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("e17b327f-eee6-4011-9905-bc8360cd5e66"));

            migrationBuilder.DeleteData(
                table: "Owners",
                keyColumn: "Id",
                keyValue: new Guid("95a9873f-d6f4-4496-bf75-62be88716460"));
        }
    }
}
