using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YukiChan.Server.Migrations.ArcaeaDb
{
    /// <inheritdoc />
    public partial class InitArcaeaDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "arcaea_alias_submissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    platform = table.Column<string>(type: "TEXT", nullable: false),
                    userid = table.Column<string>(name: "user_id", type: "TEXT", nullable: false),
                    songid = table.Column<string>(name: "song_id", type: "TEXT", nullable: false),
                    alias = table.Column<string>(type: "TEXT", nullable: false),
                    submittime = table.Column<DateTime>(name: "submit_time", type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_arcaea_alias_submissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "arcaea_preferences",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    platform = table.Column<string>(type: "TEXT", nullable: false),
                    userid = table.Column<string>(name: "user_id", type: "TEXT", nullable: false),
                    dark = table.Column<bool>(type: "INTEGER", nullable: false),
                    nya = table.Column<bool>(type: "INTEGER", nullable: false),
                    singledynamicbg = table.Column<bool>(name: "single_dynamic_bg", type: "INTEGER", nullable: false),
                    b30showgrade = table.Column<bool>(name: "b30_show_grade", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_arcaea_preferences", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "arcaea_users",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    platform = table.Column<string>(type: "TEXT", nullable: false),
                    userid = table.Column<string>(name: "user_id", type: "TEXT", nullable: false),
                    arcaeaid = table.Column<string>(name: "arcaea_id", type: "TEXT", nullable: false),
                    arcaeaname = table.Column<string>(name: "arcaea_name", type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_arcaea_users", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "arcaea_alias_submissions");

            migrationBuilder.DropTable(
                name: "arcaea_preferences");

            migrationBuilder.DropTable(
                name: "arcaea_users");
        }
    }
}
