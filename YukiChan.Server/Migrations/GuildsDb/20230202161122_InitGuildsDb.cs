#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace YukiChan.Server.Migrations.GuildsDb;

/// <inheritdoc />
public partial class InitGuildsDb : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "guilds",
            table => new
            {
                id = table.Column<int>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                platform = table.Column<string>("TEXT", nullable: false),
                guildid = table.Column<string>(name: "guild_id", type: "TEXT", nullable: false),
                assignee = table.Column<string>("TEXT", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_guilds", x => x.id); });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "guilds");
    }
}