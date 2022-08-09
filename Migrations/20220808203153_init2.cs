using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaFormaPlusCoreMVC.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleParcours_Modules_ModuleId",
                table: "ModuleParcours");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleParcours_Parcours_ParcoursId",
                table: "ModuleParcours");

            migrationBuilder.AlterColumn<int>(
                name: "ParcoursId",
                table: "ModuleParcours",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "ModuleParcours",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleParcours_Modules_ModuleId",
                table: "ModuleParcours",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleParcours_Parcours_ParcoursId",
                table: "ModuleParcours",
                column: "ParcoursId",
                principalTable: "Parcours",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleParcours_Modules_ModuleId",
                table: "ModuleParcours");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleParcours_Parcours_ParcoursId",
                table: "ModuleParcours");

            migrationBuilder.AlterColumn<int>(
                name: "ParcoursId",
                table: "ModuleParcours",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "ModuleParcours",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleParcours_Modules_ModuleId",
                table: "ModuleParcours",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleParcours_Parcours_ParcoursId",
                table: "ModuleParcours",
                column: "ParcoursId",
                principalTable: "Parcours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
