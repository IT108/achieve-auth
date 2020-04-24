using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthServer.Infrastructure.Migrations
{
    public partial class surname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19ea6199-e4a0-4f5f-a36f-574459f36254");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "595ef0f4-a039-494a-ab9b-9702408956fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "caad53dd-1f67-4533-8c27-c698a993e79e");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3dac9326-14b5-41bc-8f65-6f246eae64e5", "7f4db30b-5be3-4fdc-942d-9496682d73b7", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "588b3993-6d0a-4e93-87e5-80dcf7223081", "339c05f0-2518-4a70-8d74-b93e6b210361", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fbd0f84e-f415-4f00-b48c-5b92bd832bc7", "44c65189-0c5c-4df9-993a-c9327f032232", "Teacher", "TEACHER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3dac9326-14b5-41bc-8f65-6f246eae64e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "588b3993-6d0a-4e93-87e5-80dcf7223081");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fbd0f84e-f415-4f00-b48c-5b92bd832bc7");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "19ea6199-e4a0-4f5f-a36f-574459f36254", "ba314d04-0ef0-4179-9776-6477adde900a", "Student", "STUDENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "595ef0f4-a039-494a-ab9b-9702408956fb", "1edd893a-d892-405c-acd6-b20c3bc002b3", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "caad53dd-1f67-4533-8c27-c698a993e79e", "75b4d3c6-fdab-48ab-802e-8ea3e4834ff2", "Teacher", "TEACHER" });
        }
    }
}
