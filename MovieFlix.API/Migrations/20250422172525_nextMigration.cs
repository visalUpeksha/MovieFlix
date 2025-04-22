using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieFlix.API.Migrations
{
    /// <inheritdoc />
    public partial class nextMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieRentals",
                table: "MovieRentals");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MovieRentals",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieRentals",
                table: "MovieRentals",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MovieRentals_RentalId",
                table: "MovieRentals",
                column: "RentalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieRentals",
                table: "MovieRentals");

            migrationBuilder.DropIndex(
                name: "IX_MovieRentals_RentalId",
                table: "MovieRentals");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MovieRentals");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieRentals",
                table: "MovieRentals",
                columns: new[] { "RentalId", "MovieId" });
        }
    }
}
