using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace roomshare_room_service.Migrations
{
    public partial class AddCEPColumnOnRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "cep",
                table: "room",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cep",
                table: "room");
        }
    }
}
