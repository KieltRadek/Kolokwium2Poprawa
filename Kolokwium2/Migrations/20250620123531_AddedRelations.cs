using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kolokwium2.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Race_Participation",
                table: "Race_Participation");

            migrationBuilder.DropIndex(
                name: "IX_Race_Participation_RacerId",
                table: "Race_Participation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Race_Participation",
                table: "Race_Participation",
                columns: new[] { "RacerId", "TrackRaceId" });

            migrationBuilder.CreateIndex(
                name: "IX_Race_Participation_TrackRaceId",
                table: "Race_Participation",
                column: "TrackRaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Race_Participation",
                table: "Race_Participation");

            migrationBuilder.DropIndex(
                name: "IX_Race_Participation_TrackRaceId",
                table: "Race_Participation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Race_Participation",
                table: "Race_Participation",
                columns: new[] { "TrackRaceId", "RacerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Race_Participation_RacerId",
                table: "Race_Participation",
                column: "RacerId");
        }
    }
}
