using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaFormaPlusCoreMVC.Migrations
{
    public partial class ini0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Parcours_ParcoursId",
                table: "Modules");

            migrationBuilder.AlterColumn<int>(
                name: "ParcoursId",
                table: "Modules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Parcours_ParcoursId",
                table: "Modules",
                column: "ParcoursId",
                principalTable: "Parcours",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Parcours_ParcoursId",
                table: "Modules");

            migrationBuilder.AlterColumn<int>(
                name: "ParcoursId",
                table: "Modules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Parcours_ParcoursId",
                table: "Modules",
                column: "ParcoursId",
                principalTable: "Parcours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
