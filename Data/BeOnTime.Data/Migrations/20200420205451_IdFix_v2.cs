using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreTemplate.Data.Migrations
{
    public partial class IdFix_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_AspNetUsers_OrganiserId1",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_OrganiserId1",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "OrganiserId1",
                table: "Meetings");

            migrationBuilder.AlterColumn<string>(
                name: "OrganiserId",
                table: "Meetings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_OrganiserId",
                table: "Meetings",
                column: "OrganiserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_AspNetUsers_OrganiserId",
                table: "Meetings",
                column: "OrganiserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_AspNetUsers_OrganiserId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_OrganiserId",
                table: "Meetings");

            migrationBuilder.AlterColumn<int>(
                name: "OrganiserId",
                table: "Meetings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganiserId1",
                table: "Meetings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_OrganiserId1",
                table: "Meetings",
                column: "OrganiserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_AspNetUsers_OrganiserId1",
                table: "Meetings",
                column: "OrganiserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
