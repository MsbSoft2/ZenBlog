using Microsoft.EntityFrameworkCore.Migrations;

namespace ZenBlog.DataLayer.Migrations
{
    public partial class postShowSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowInSlider",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowInSlider",
                table: "Posts");
        }
    }
}
