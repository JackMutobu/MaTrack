using Microsoft.EntityFrameworkCore.Migrations;

namespace MatrackApi.Migrations
{
    public partial class UpdateTripEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Stages_DestinationStageId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_DestinationStageId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DestinationStageId",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "DepartureStageId",
                table: "Trips",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DepartureStageId",
                table: "Trips",
                column: "DepartureStageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Stages_DepartureStageId",
                table: "Trips",
                column: "DepartureStageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Stages_DepartureStageId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_DepartureStageId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DepartureStageId",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "DestinationStageId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationStageId",
                table: "Trips",
                column: "DestinationStageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Stages_DestinationStageId",
                table: "Trips",
                column: "DestinationStageId",
                principalTable: "Stages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
