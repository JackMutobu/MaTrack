using Microsoft.EntityFrameworkCore.Migrations;

namespace MatrackApi.Migrations
{
    public partial class AddEnumStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Drivers");

            migrationBuilder.AddColumn<int>(
                name: "DriverStatus",
                table: "Drivers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverStatus",
                table: "Drivers");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Drivers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
