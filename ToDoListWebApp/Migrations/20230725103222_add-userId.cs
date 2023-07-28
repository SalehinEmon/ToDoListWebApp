using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoListWebApp.Migrations
{
    public partial class adduserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ToDoNames",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ToDoNames");
        }
    }
}
