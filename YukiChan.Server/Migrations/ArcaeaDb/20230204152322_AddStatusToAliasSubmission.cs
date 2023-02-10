#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace YukiChan.Server.Migrations.ArcaeaDb;

/// <inheritdoc />
public partial class AddStatusToAliasSubmission : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            "status",
            "arcaea_alias_submissions",
            "INTEGER",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "status",
            "arcaea_alias_submissions");
    }
}