using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_Voting.Data.Migrations
{
    /// <inheritdoc />
    public partial class PremiereMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BureauVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomBureauVote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreInscrits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BureauVotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Citoyens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prénom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BureauVoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citoyens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Citoyens_BureauVotes_BureauVoteId",
                        column: x => x.BureauVoteId,
                        principalTable: "BureauVotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Citoyens_BureauVoteId",
                table: "Citoyens",
                column: "BureauVoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citoyens");

            migrationBuilder.DropTable(
                name: "BureauVotes");
        }
    }
}
