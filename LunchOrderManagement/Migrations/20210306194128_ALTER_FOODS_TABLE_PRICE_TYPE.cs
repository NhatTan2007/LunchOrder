using Microsoft.EntityFrameworkCore.Migrations;

namespace LunchOrderManagement.Migrations
{
    public partial class ALTER_FOODS_TABLE_PRICE_TYPE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: "29d075b9-d345-44c9-b2b5-5fc416a6fef1");

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: "49da7c2d-251e-43ff-83eb-027ee657098d");

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: "49e24f8d-5cf6-4727-a40d-42220be7d6b4");

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "FoodId",
                keyValue: "0a584914-9cc5-475a-b723-1c1c2a074237");

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "FoodId",
                keyValue: "23fbe179-d9bc-4c11-9720-e0501da87da2");

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "FoodId",
                keyValue: "a12cd2e6-f3a9-4470-b8ef-32f80ada738e");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Foods",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassId", "ClassName" },
                values: new object[,]
                {
                    { "e5933481-d982-407b-99d3-57d174b4b955", "C1020K1" },
                    { "834fd798-a5e6-400a-8364-71fd46aac4ef", "C0920G1" },
                    { "7ed1a59e-39c9-40db-9581-0263479aa37e", "C0221H1" }
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "FoodId", "IsActive", "Name", "Price" },
                values: new object[,]
                {
                    { "88c4ff49-4e20-4a4c-8aa7-60e487814742", true, "Cơm gà xối mỡ", 20000 },
                    { "4e9f69be-dbb8-4fdc-a7b9-3eee1180b01d", true, "Cơm sườn", 15000 },
                    { "21b81039-21f2-4e0d-abd7-7f072fee0e04", true, "Cơm sườn non", 25000 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: "7ed1a59e-39c9-40db-9581-0263479aa37e");

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: "834fd798-a5e6-400a-8364-71fd46aac4ef");

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassId",
                keyValue: "e5933481-d982-407b-99d3-57d174b4b955");

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "FoodId",
                keyValue: "21b81039-21f2-4e0d-abd7-7f072fee0e04");

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "FoodId",
                keyValue: "4e9f69be-dbb8-4fdc-a7b9-3eee1180b01d");

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "FoodId",
                keyValue: "88c4ff49-4e20-4a4c-8aa7-60e487814742");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Foods",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassId", "ClassName" },
                values: new object[,]
                {
                    { "29d075b9-d345-44c9-b2b5-5fc416a6fef1", "C1020K1" },
                    { "49da7c2d-251e-43ff-83eb-027ee657098d", "C0920G1" },
                    { "49e24f8d-5cf6-4727-a40d-42220be7d6b4", "C0221H1" }
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "FoodId", "IsActive", "Name", "Price" },
                values: new object[,]
                {
                    { "23fbe179-d9bc-4c11-9720-e0501da87da2", true, "Cơm gà xối mỡ", 20000m },
                    { "0a584914-9cc5-475a-b723-1c1c2a074237", true, "Cơm sườn", 15000m },
                    { "a12cd2e6-f3a9-4470-b8ef-32f80ada738e", true, "Cơm sườn non", 25000m }
                });
        }
    }
}
