using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fastwifi.Migrations
{
    /// <inheritdoc />
    public partial class addNewModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    providedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProgressNoteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comments_ProgressNotes_ProgressNoteId",
                        column: x => x.ProgressNoteId,
                        principalTable: "ProgressNotes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SuscribeLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuscribeLists", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProgressNoteId",
                table: "Comments",
                column: "ProgressNoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "SuscribeLists");
        }
    }
}
