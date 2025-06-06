using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace managerelchenchenvuelve.Migrations
{
    /// <inheritdoc />
    public partial class camposagrega : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsignacionClasss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NombreCompleto = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Letters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ids = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionClasss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessInstanceId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    StageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DatosRecas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompletaActividad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Etapa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gestor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaFormateada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaActualizacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TiempoTranscurrido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoDeSolicitud = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tiempo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosRecas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentReferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessInstanceId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DocumentTitle = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    StageName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentReferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Request_Info",
                columns: table => new
                {
                    Codigo_de_solicitud = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Fecha_de_Creacion = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Fecha_Actualizacion = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Gestor = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Etapa_del_Negocio = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: true),
                    Correo_Electronico = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Numero_identificacion = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Tipo_identificacion = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Nombre_Negocio = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Descripcion_negocio = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Actividad_economica = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    RUC = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Web_Site = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Provincia = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Distrito = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    corregimiento = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Proyeccion_ventas_mensuales = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Ventas_mensuales = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Fecha_Inicio_Operaciones = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Cuanto_Chenchen_necesitas = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    En_que_lo_invertiras = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Verificacion_Cliente = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Gestion_Realizada = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Tipo_atencion = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Porque_no_contacto = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Etapa = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Usuario_Asignado = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FechaFormateada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TiempoTranscurrido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletaActividad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tiempo = table.Column<int>(type: "int", nullable: true),
                    cod_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    id_chen = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "RequestDetails_copia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityToInvert = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReasonForMoney = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Suggestion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requests_copia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Suggestion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RolName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ActiveDirectoryGroup = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    IsActiveDirectorySync = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Names = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IndUpdate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdentificationNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IdentificationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enterprises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BusinessDescription = table.Column<string>(type: "varchar(600)", unicode: false, maxLength: 600, nullable: true),
                    EconomicActivity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Instagram = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ruc = table.Column<string>(type: "varchar(28)", unicode: false, maxLength: 28, nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessTime = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false, defaultValue: ""),
                    Corregimiento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    MonthlySales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperationsStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: ""),
                    ProyectedSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enterprises_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityToInvert = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReasonForMoney = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerifyClient = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    managementExecuted = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TipoAtencion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestDetails_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Include",
                table: "Contacts",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_RequestId",
                table: "Contacts",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_Include",
                table: "Enterprises",
                column: "BusinessDescription");

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_RequestId",
                table: "Enterprises",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestDetails_Include",
                table: "RequestDetails",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestDetails_RequestId",
                table: "RequestDetails",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_Id",
                table: "Requests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_Include",
                table: "Requests",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsignacionClasss");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "DatosRecas");

            migrationBuilder.DropTable(
                name: "DocumentReferences");

            migrationBuilder.DropTable(
                name: "Enterprises");

            migrationBuilder.DropTable(
                name: "Request_Info");

            migrationBuilder.DropTable(
                name: "RequestDetails");

            migrationBuilder.DropTable(
                name: "RequestDetails_copia");

            migrationBuilder.DropTable(
                name: "Requests_copia");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
