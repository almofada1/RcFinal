using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RcFinal.Migrations
{
    /// <inheritdoc />
    public partial class AddReservasLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quartos",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quartos", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Guests = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Quartos_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Quartos",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Quartos",
                columns: new[] { "RoomId", "Capacidade" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 3 },
                    { 6, 3 },
                    { 7, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_RoomId",
                table: "Reservas",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Quartos");
        }
    }
}
