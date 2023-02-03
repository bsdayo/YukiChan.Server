using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YukiChan.Server.Migrations.GuildsDb
{
    /// <inheritdoc />
    public partial class InitGuildsDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "guilds",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    platform = table.Column<string>(type: "TEXT", nullable: false),
                    guildid = table.Column<string>(name: "guild_id", type: "TEXT", nullable: false),
                    assignee = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guilds", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guilds");
        }
    }
}
