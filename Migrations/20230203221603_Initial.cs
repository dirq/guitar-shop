using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuitarShop.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Musicians",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guitars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    MusicianId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guitars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guitars_Musicians_MusicianId",
                        column: x => x.MusicianId,
                        principalTable: "Musicians",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guitars_MusicianId",
                table: "Guitars",
                column: "MusicianId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guitars");

            migrationBuilder.DropTable(
                name: "Musicians");
        }
    }
}
