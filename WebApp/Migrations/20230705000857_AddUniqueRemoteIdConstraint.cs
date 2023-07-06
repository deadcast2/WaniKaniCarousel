using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueRemoteIdConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subjects_RemoteId",
                table: "Subjects");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_RemoteId",
                table: "Subjects",
                column: "RemoteId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subjects_RemoteId",
                table: "Subjects");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_RemoteId",
                table: "Subjects",
                column: "RemoteId");
        }
    }
}
