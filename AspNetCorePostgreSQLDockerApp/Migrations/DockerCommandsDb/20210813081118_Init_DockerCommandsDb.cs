using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AspNetCorePostgreSQLDockerApp.Migrations.DockerCommandsDb
{
    public partial class Init_DockerCommandsDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "DockerCommands",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Command = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_DockerCommands", x => x.Id); });

            migrationBuilder.CreateTable(
                "DockerCommandExample",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Example = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DockerCommandId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DockerCommandExample", x => x.Id);
                    table.ForeignKey(
                        "FK_DockerCommandExample_DockerCommands_DockerCommandId",
                        x => x.DockerCommandId,
                        "DockerCommands",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_DockerCommandExample_DockerCommandId",
                "DockerCommandExample",
                "DockerCommandId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "DockerCommandExample");

            migrationBuilder.DropTable(
                "DockerCommands");
        }
    }
}