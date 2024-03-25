using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorPacientes.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Usuarios_UserId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_Usuarios_UserId",
                table: "Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Usuarios_UserId",
                table: "Pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pruebas_Usuarios_UserId",
                table: "Pruebas");

            migrationBuilder.DropForeignKey(
                name: "FK_Resultados_Usuarios_UserId",
                table: "Resultados");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Usuarios_UserId",
                table: "Citas",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_Usuarios_UserId",
                table: "Medicos",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Usuarios_UserId",
                table: "Pacientes",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pruebas_Usuarios_UserId",
                table: "Pruebas",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resultados_Usuarios_UserId",
                table: "Resultados",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Usuarios_UserId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_Usuarios_UserId",
                table: "Medicos");

            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Usuarios_UserId",
                table: "Pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pruebas_Usuarios_UserId",
                table: "Pruebas");

            migrationBuilder.DropForeignKey(
                name: "FK_Resultados_Usuarios_UserId",
                table: "Resultados");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Usuarios_UserId",
                table: "Citas",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_Usuarios_UserId",
                table: "Medicos",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Usuarios_UserId",
                table: "Pacientes",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pruebas_Usuarios_UserId",
                table: "Pruebas",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Resultados_Usuarios_UserId",
                table: "Resultados",
                column: "UserId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
