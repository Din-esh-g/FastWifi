using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fastwifi.Migrations
{
    /// <inheritdoc />
    public partial class progressnotemodule2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgressNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsumerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPSWCounty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PathClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Participants = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorizedRep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignatureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterventionSummary",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ALS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgressNoteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterventionSummary", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InterventionSummary_ProgressNotes_ProgressNoteId",
                        column: x => x.ProgressNoteId,
                        principalTable: "ProgressNotes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceDetail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ServiceStopTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ITTStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ITTStopTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    MilesTraveled = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Locations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgressNoteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ServiceDetail_ProgressNotes_ProgressNoteId",
                        column: x => x.ProgressNoteId,
                        principalTable: "ProgressNotes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterventionSummary_ProgressNoteId",
                table: "InterventionSummary",
                column: "ProgressNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDetail_ProgressNoteId",
                table: "ServiceDetail",
                column: "ProgressNoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterventionSummary");

            migrationBuilder.DropTable(
                name: "ServiceDetail");

            migrationBuilder.DropTable(
                name: "ProgressNotes");
        }
    }
}
