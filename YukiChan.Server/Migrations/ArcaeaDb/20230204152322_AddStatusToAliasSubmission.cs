using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YukiChan.Server.Migrations.ArcaeaDb
{
    /// <inheritdoc />
    public partial class AddStatusToAliasSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "arcaea_alias_submissions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "arcaea_alias_submissions");
        }
    }
}
