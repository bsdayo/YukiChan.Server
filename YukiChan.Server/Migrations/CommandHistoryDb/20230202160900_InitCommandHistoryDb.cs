#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace YukiChan.Server.Migrations.CommandHistoryDb;

/// <inheritdoc />
public partial class InitCommandHistoryDb : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "command_history",
            table => new
            {
                id = table.Column<int>("INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                time = table.Column<DateTime>("TEXT", nullable: false),
                platform = table.Column<string>("TEXT", nullable: false),
                guildid = table.Column<string>(name: "guild_id", type: "TEXT", nullable: true),
                channelid = table.Column<string>(name: "channel_id", type: "TEXT", nullable: true),
                userid = table.Column<string>(name: "user_id", type: "TEXT", nullable: false),
                userauthority = table.Column<int>(name: "user_authority", type: "INTEGER", nullable: false),
                assigneeid = table.Column<string>(name: "assignee_id", type: "TEXT", nullable: false),
                environment = table.Column<int>("INTEGER", nullable: false),
                command = table.Column<string>("TEXT", nullable: false),
                commandtext = table.Column<string>(name: "command_text", type: "TEXT", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_command_history", x => x.id); });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "command_history");
    }
}