using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YukiChan.Server.Migrations.UsersDb
{
    /// <inheritdoc />
    public partial class InitUsersDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    platform = table.Column<string>(type: "TEXT", nullable: false),
                    userid = table.Column<string>(name: "user_id", type: "TEXT", nullable: false),
                    userauthority = table.Column<int>(name: "user_authority", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
