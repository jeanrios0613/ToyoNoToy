using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElChenVuelve.Migrations
{
    /// <inheritdoc />
    public partial class ToyNoToyContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actividad",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreActividad = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Activida__3213E83F51989242", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Formularios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cedula = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RedesSociales = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NombreEmpresa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ActividadEconomica = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DescripcionNegocio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MontoInversion = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DescripcionInversion = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    RazonCambio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndicaSolicitud = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocumentacionAdjunta = table.Column<bool>(type: "bit", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UsuarioAnalista = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaAtencion = table.Column<DateTime>(type: "datetime", nullable: true),
                    UsuarioSupervisor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaAprobacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    Localidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Formular__3213E83F4D1883E6", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "userprofile",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    perfil = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_pro__3213E83FADAEA96E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    correo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    ultimo_acceso = table.Column<DateTime>(type: "datetime", nullable: true),
                    rol = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true, defaultValue: "usuario"),
                    estado = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true, defaultValue: "activo"),
                    fecha_ultima_actualizacion = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuarios__B9BE370F2C1E936A", x => x.user_id);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__usuarios__2A586E0B9F3E4B81",
                table: "usuarios",
                column: "correo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actividad");

            migrationBuilder.DropTable(
                name: "Formularios");

            migrationBuilder.DropTable(
                name: "userprofile");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
