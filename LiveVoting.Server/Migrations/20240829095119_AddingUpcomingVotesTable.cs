using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveVoting.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddingUpcomingVotesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UpcomingVotes",
                columns: table => new
                {
                    UpcomingVoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpcomingCandidateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpcomingVotes", x => x.UpcomingVoteId);
                    table.ForeignKey(
                        name: "FK_UpcomingVotes_UpcomingCandidates_UpcomingCandidateId",
                        column: x => x.UpcomingCandidateId,
                        principalTable: "UpcomingCandidates",
                        principalColumn: "UpcomingCandidateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UpcomingVotes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpcomingVotes_UpcomingCandidateId",
                table: "UpcomingVotes",
                column: "UpcomingCandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_UpcomingVotes_UserId",
                table: "UpcomingVotes",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpcomingVotes");
        }
    }
}
