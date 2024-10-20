using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fastwifi.Migrations
{
    /// <inheritdoc />
    public partial class progressnotemodule3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterventionSummary_ProgressNotes_ProgressNoteId",
                table: "InterventionSummary");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceDetail_ProgressNotes_ProgressNoteId",
                table: "ServiceDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceDetail",
                table: "ServiceDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterventionSummary",
                table: "InterventionSummary");

            migrationBuilder.RenameTable(
                name: "ServiceDetail",
                newName: "ServiceDetails");

            migrationBuilder.RenameTable(
                name: "InterventionSummary",
                newName: "InterventionSummaries");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceDetail_ProgressNoteId",
                table: "ServiceDetails",
                newName: "IX_ServiceDetails_ProgressNoteId");

            migrationBuilder.RenameIndex(
                name: "IX_InterventionSummary_ProgressNoteId",
                table: "InterventionSummaries",
                newName: "IX_InterventionSummaries_ProgressNoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceDetails",
                table: "ServiceDetails",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterventionSummaries",
                table: "InterventionSummaries",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_InterventionSummaries_ProgressNotes_ProgressNoteId",
                table: "InterventionSummaries",
                column: "ProgressNoteId",
                principalTable: "ProgressNotes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceDetails_ProgressNotes_ProgressNoteId",
                table: "ServiceDetails",
                column: "ProgressNoteId",
                principalTable: "ProgressNotes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterventionSummaries_ProgressNotes_ProgressNoteId",
                table: "InterventionSummaries");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceDetails_ProgressNotes_ProgressNoteId",
                table: "ServiceDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceDetails",
                table: "ServiceDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterventionSummaries",
                table: "InterventionSummaries");

            migrationBuilder.RenameTable(
                name: "ServiceDetails",
                newName: "ServiceDetail");

            migrationBuilder.RenameTable(
                name: "InterventionSummaries",
                newName: "InterventionSummary");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceDetails_ProgressNoteId",
                table: "ServiceDetail",
                newName: "IX_ServiceDetail_ProgressNoteId");

            migrationBuilder.RenameIndex(
                name: "IX_InterventionSummaries_ProgressNoteId",
                table: "InterventionSummary",
                newName: "IX_InterventionSummary_ProgressNoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceDetail",
                table: "ServiceDetail",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterventionSummary",
                table: "InterventionSummary",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_InterventionSummary_ProgressNotes_ProgressNoteId",
                table: "InterventionSummary",
                column: "ProgressNoteId",
                principalTable: "ProgressNotes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceDetail_ProgressNotes_ProgressNoteId",
                table: "ServiceDetail",
                column: "ProgressNoteId",
                principalTable: "ProgressNotes",
                principalColumn: "Id");
        }
    }
}
