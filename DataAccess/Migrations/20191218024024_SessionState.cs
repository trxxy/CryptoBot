using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class SessionState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sushi",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "SushiId",
                table: "Baskets");

            migrationBuilder.AddColumn<int>(
                name: "SushiInBasketId",
                table: "Baskets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SushiInBasket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SushiId = table.Column<int>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    BasketId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SushiInBasket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SushiInBasket_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SushiInBasket_Sushies_SushiId",
                        column: x => x.SushiId,
                        principalTable: "Sushies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SushiInBasket_BasketId",
                table: "SushiInBasket",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_SushiInBasket_SushiId",
                table: "SushiInBasket",
                column: "SushiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SushiInBasket");

            migrationBuilder.DropColumn(
                name: "SushiInBasketId",
                table: "Baskets");

            migrationBuilder.AddColumn<string>(
                name: "Sushi",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SushiId",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
