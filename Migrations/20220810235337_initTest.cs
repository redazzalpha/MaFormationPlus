using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaFormaPlusCoreMVC.Migrations
{
    public partial class initTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionStagiaire_Stagiaires_StagiairesId",
                table: "SessionStagiaire");

            migrationBuilder.DropTable(
                name: "Stagiaires");

            migrationBuilder.AlterColumn<string>(
                name: "StagiairesId",
                table: "SessionStagiaire",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cv",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DateDeNaissance",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionStagiaire_AspNetUsers_StagiairesId",
                table: "SessionStagiaire",
                column: "StagiairesId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionStagiaire_AspNetUsers_StagiairesId",
                table: "SessionStagiaire");

            migrationBuilder.DropColumn(
                name: "Adresse",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Cv",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateDeNaissance",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "StagiairesId",
                table: "SessionStagiaire",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Stagiaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDeNaissance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stagiaires", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SessionStagiaire_Stagiaires_StagiairesId",
                table: "SessionStagiaire",
                column: "StagiairesId",
                principalTable: "Stagiaires",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
