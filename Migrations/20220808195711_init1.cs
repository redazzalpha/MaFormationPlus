using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaFormaPlusCoreMVC.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleParcours_Modules_ModulesId",
                table: "ModuleParcours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleParcours",
                table: "ModuleParcours");

            migrationBuilder.RenameColumn(
                name: "ModulesId",
                table: "ModuleParcours",
                newName: "ModuleId");

            migrationBuilder.AddColumn<int>(
                name: "ModuleId",
                table: "Parcours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ModuleParcours",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleParcours",
                table: "ModuleParcours",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Parcours_ModuleId",
                table: "Parcours",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleParcours_ModuleId",
                table: "ModuleParcours",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleParcours_Modules_ModuleId",
                table: "ModuleParcours",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcours_Modules_ModuleId",
                table: "Parcours",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleParcours_Modules_ModuleId",
                table: "ModuleParcours");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcours_Modules_ModuleId",
                table: "Parcours");

            migrationBuilder.DropIndex(
                name: "IX_Parcours_ModuleId",
                table: "Parcours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleParcours",
                table: "ModuleParcours");

            migrationBuilder.DropIndex(
                name: "IX_ModuleParcours_ModuleId",
                table: "ModuleParcours");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "Parcours");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ModuleParcours");

            migrationBuilder.RenameColumn(
                name: "ModuleId",
                table: "ModuleParcours",
                newName: "ModulesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleParcours",
                table: "ModuleParcours",
                columns: new[] { "ModulesId", "ParcoursId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleParcours_Modules_ModulesId",
                table: "ModuleParcours",
                column: "ModulesId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
