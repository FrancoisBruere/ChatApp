using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class RemoveStatusFromUserContactsAddStatusToUserActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnlineOfflineStatus",
                table: "UserContacts");

            migrationBuilder.AddColumn<string>(
                name: "OnlineOfflineStatus",
                table: "UserActivities",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnlineOfflineStatus",
                table: "UserActivities");

            migrationBuilder.AddColumn<string>(
                name: "OnlineOfflineStatus",
                table: "UserContacts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
