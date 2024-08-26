using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveVoting.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddingDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionRound_Election_ElectionId",
                table: "ElectionRound");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundCandidate_Candidate_CandidateId",
                table: "RoundCandidate");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundCandidate_ElectionRound_ElectionRoundId",
                table: "RoundCandidate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoundCandidate",
                table: "RoundCandidate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectionRound",
                table: "ElectionRound");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Election",
                table: "Election");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Candidate",
                table: "Candidate");

            migrationBuilder.RenameTable(
                name: "RoundCandidate",
                newName: "RoundCandidates");

            migrationBuilder.RenameTable(
                name: "ElectionRound",
                newName: "ElectionRounds");

            migrationBuilder.RenameTable(
                name: "Election",
                newName: "Elections");

            migrationBuilder.RenameTable(
                name: "Candidate",
                newName: "Candidates");

            migrationBuilder.RenameIndex(
                name: "IX_RoundCandidate_ElectionRoundId",
                table: "RoundCandidates",
                newName: "IX_RoundCandidates_ElectionRoundId");

            migrationBuilder.RenameIndex(
                name: "IX_RoundCandidate_CandidateId",
                table: "RoundCandidates",
                newName: "IX_RoundCandidates_CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionRound_ElectionId",
                table: "ElectionRounds",
                newName: "IX_ElectionRounds_ElectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoundCandidates",
                table: "RoundCandidates",
                column: "RoundCandidateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectionRounds",
                table: "ElectionRounds",
                column: "ElectionRoundId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Elections",
                table: "Elections",
                column: "ElectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionRounds_Elections_ElectionId",
                table: "ElectionRounds",
                column: "ElectionId",
                principalTable: "Elections",
                principalColumn: "ElectionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundCandidates_Candidates_CandidateId",
                table: "RoundCandidates",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "CandidateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundCandidates_ElectionRounds_ElectionRoundId",
                table: "RoundCandidates",
                column: "ElectionRoundId",
                principalTable: "ElectionRounds",
                principalColumn: "ElectionRoundId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectionRounds_Elections_ElectionId",
                table: "ElectionRounds");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundCandidates_Candidates_CandidateId",
                table: "RoundCandidates");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundCandidates_ElectionRounds_ElectionRoundId",
                table: "RoundCandidates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoundCandidates",
                table: "RoundCandidates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Elections",
                table: "Elections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElectionRounds",
                table: "ElectionRounds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates");

            migrationBuilder.RenameTable(
                name: "RoundCandidates",
                newName: "RoundCandidate");

            migrationBuilder.RenameTable(
                name: "Elections",
                newName: "Election");

            migrationBuilder.RenameTable(
                name: "ElectionRounds",
                newName: "ElectionRound");

            migrationBuilder.RenameTable(
                name: "Candidates",
                newName: "Candidate");

            migrationBuilder.RenameIndex(
                name: "IX_RoundCandidates_ElectionRoundId",
                table: "RoundCandidate",
                newName: "IX_RoundCandidate_ElectionRoundId");

            migrationBuilder.RenameIndex(
                name: "IX_RoundCandidates_CandidateId",
                table: "RoundCandidate",
                newName: "IX_RoundCandidate_CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_ElectionRounds_ElectionId",
                table: "ElectionRound",
                newName: "IX_ElectionRound_ElectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoundCandidate",
                table: "RoundCandidate",
                column: "RoundCandidateId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Election",
                table: "Election",
                column: "ElectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElectionRound",
                table: "ElectionRound",
                column: "ElectionRoundId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Candidate",
                table: "Candidate",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectionRound_Election_ElectionId",
                table: "ElectionRound",
                column: "ElectionId",
                principalTable: "Election",
                principalColumn: "ElectionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundCandidate_Candidate_CandidateId",
                table: "RoundCandidate",
                column: "CandidateId",
                principalTable: "Candidate",
                principalColumn: "CandidateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundCandidate_ElectionRound_ElectionRoundId",
                table: "RoundCandidate",
                column: "ElectionRoundId",
                principalTable: "ElectionRound",
                principalColumn: "ElectionRoundId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
