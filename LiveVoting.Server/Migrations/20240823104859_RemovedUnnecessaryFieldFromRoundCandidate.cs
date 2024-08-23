using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveVoting.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUnnecessaryFieldFromRoundCandidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalCandidateId",
                table: "RoundCandidate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FinalCandidateId",
                table: "RoundCandidate",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
