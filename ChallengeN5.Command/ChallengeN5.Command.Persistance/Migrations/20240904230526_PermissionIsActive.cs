using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChallengeN5.Command.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class PermissionIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Permission",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Permission");
        }
    }
}
