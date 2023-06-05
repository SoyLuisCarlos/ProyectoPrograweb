using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPrograweb.Migrations
{
    public partial class Init70 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenUsuario",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenUsuario",
                table: "AspNetUsers");
        }
    }
}
