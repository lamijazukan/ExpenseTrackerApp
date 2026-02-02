using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTrackerApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateBudgetEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Category_UserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "PeriodType",
                table: "Budget");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId_ParentCategoryId_Name",
                table: "Category",
                columns: new[] { "UserId", "ParentCategoryId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Category_UserId_ParentCategoryId_Name",
                table: "Category");

            migrationBuilder.AddColumn<string>(
                name: "PeriodType",
                table: "Budget",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId",
                table: "Category",
                column: "UserId");
        }
    }
}
