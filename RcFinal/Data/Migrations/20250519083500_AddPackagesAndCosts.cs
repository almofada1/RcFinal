using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RcFinal.Migrations
{
    /// <inheritdoc />
    public partial class AddPackagesAndCosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "Reservas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PricePerNightPerGuest = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_PackageId",
                table: "Reservas",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Packages_PackageId",
                table: "Reservas",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "PackageId", "Name", "PricePerNightPerGuest" },
                values: new object[,]
                {
                    { 1, "Room Only",           0.00m   }, // no meals included
                    { 2, "Bed & Breakfast",    20.00m   }, // typical extra charge for full breakfast :contentReference[oaicite:0]{index=0}
                    { 3, "Half Board",         45.00m   }, // breakfast + one other meal (e.g. dinner) :contentReference[oaicite:1]{index=1}
                    { 4, "All Inclusive",     167.00m   }, // includes all meals & drinks (Ikos Dassia starts at €334/double) :contentReference[oaicite:2]{index=2}
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Packages_PackageId",
                table: "Reservas");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_PackageId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "Reservas");
        }
    }
}
