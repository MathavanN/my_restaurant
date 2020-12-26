using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRestaurant.Core.Migrations
{
    public partial class RestaurantInfoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestaurantInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(256)", nullable: false),
                    Address = table.Column<string>(type: "varchar(256)", nullable: false),
                    City = table.Column<string>(type: "varchar(100)", nullable: false),
                    Country = table.Column<string>(type: "varchar(100)", nullable: false),
                    LandLine = table.Column<string>(type: "varchar(20)", nullable: true),
                    Mobile = table.Column<string>(type: "varchar(20)", nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurantInfos");
        }
    }
}
