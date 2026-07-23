using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrimeStore.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addtotalintoorderOtem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Total",
                table: "OrderItems",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "OrderItems");
        }
    }
}
