using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class applied_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "SpContentDto",
            //    columns: table => new
            //    {
            //        CtoIdContenidoPk = table.Column<int>(type: "int", nullable: true),
            //        CtoTitulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CtoTipoContenidoFk = table.Column<int>(type: "int", nullable: false),
            //        CtoTipoContenido = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CtoBanner = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CtoVideo = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CtoTextoBanner = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CtoDurationBanner = table.Column<int>(type: "int", nullable: true),
            //        CtoEstado = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //    });

            migrationBuilder.CreateTable(
                name: "TblDatosPersona",
                columns: table => new
                {
                    dp_IdDatosPersonaPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dp_nombre1 = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    dp_nombre2 = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    dp_apellido1 = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    dp_apellido2 = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    dp_estado = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TblDatos__0B8745621CD910D3", x => x.dp_IdDatosPersonaPk);
                });

            migrationBuilder.CreateTable(
                name: "TblDicRolesSistema",
                columns: table => new
                {
                    rs_IdRolSistemaPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rs_nombreRol = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    rs_estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TblDicRo__8FBF7D6F97F33995", x => x.rs_IdRolSistemaPk);
                });

            migrationBuilder.CreateTable(
                name: "TblDicTipoContenido",
                columns: table => new
                {
                    tcto_IdTipoContenidoPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tcto_tipoContenido = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    tcto_descripcion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    tcto_estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TblDicTi__C738477F6652813B", x => x.tcto_IdTipoContenidoPk);
                });

            migrationBuilder.CreateTable(
                name: "TblUsuarios",
                columns: table => new
                {
                    us_IdUsuarioPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    us_datosPersonaFk = table.Column<int>(type: "int", nullable: false),
                    us_rolSistemaFk = table.Column<int>(type: "int", nullable: false),
                    us_alias = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    us_contrasena = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    us_estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TblUsuar__EA793E97B5F5D1FD", x => x.us_IdUsuarioPk);
                    table.ForeignKey(
                        name: "FK__TblUsuari__us_da__3F466844",
                        column: x => x.us_datosPersonaFk,
                        principalTable: "TblDatosPersona",
                        principalColumn: "dp_IdDatosPersonaPk");
                    table.ForeignKey(
                        name: "FK__TblUsuari__us_ro__403A8C7D",
                        column: x => x.us_rolSistemaFk,
                        principalTable: "TblDicRolesSistema",
                        principalColumn: "rs_IdRolSistemaPk");
                });

            migrationBuilder.CreateTable(
                name: "TblContenido",
                columns: table => new
                {
                    cto_IdContenidoPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cto_titulo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    cto_tipoContenidoFk = table.Column<int>(type: "int", nullable: false),
                    cto_estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TblConte__11ED543B015314DD", x => x.cto_IdContenidoPk);
                    table.ForeignKey(
                        name: "FK__TblConten__cto_t__44FF419A",
                        column: x => x.cto_tipoContenidoFk,
                        principalTable: "TblDicTipoContenido",
                        principalColumn: "tcto_IdTipoContenidoPk");
                });

            migrationBuilder.CreateTable(
                name: "TblBanner",
                columns: table => new
                {
                    bnr_IdBannerPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bnr_idContenidoFk = table.Column<int>(type: "int", nullable: false),
                    bnr_rutaAcceso = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    bnr_texto = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    bnr_estado = table.Column<bool>(type: "bit", nullable: false),
                    bnr_duracion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TblBanne__426D162CE02E3A3D", x => x.bnr_IdBannerPk);
                    table.ForeignKey(
                        name: "FK__TblBanner__bnr_e__5070F446",
                        column: x => x.bnr_idContenidoFk,
                        principalTable: "TblContenido",
                        principalColumn: "cto_IdContenidoPk");
                });

            migrationBuilder.CreateTable(
                name: "TblProgramacionContenido",
                columns: table => new
                {
                    pco_IdProgramacionPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pco_idContenidoFk = table.Column<int>(type: "int", nullable: false),
                    pco_horaProgramada = table.Column<TimeOnly>(type: "time", nullable: false),
                    pco_estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TblProgr__5AFE44BACCAC05ED", x => x.pco_IdProgramacionPk);
                    table.ForeignKey(
                        name: "FK__TblProgra__pco_e__4D94879B",
                        column: x => x.pco_idContenidoFk,
                        principalTable: "TblContenido",
                        principalColumn: "cto_IdContenidoPk");
                });

            migrationBuilder.CreateTable(
                name: "TblVideo",
                columns: table => new
                {
                    vdo_IdVideoPk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vdo_idContenidoFk = table.Column<int>(type: "int", nullable: false),
                    vdo_rutaAcceso = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    vdo_estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TblVideo__150E869A49468549", x => x.vdo_IdVideoPk);
                    table.ForeignKey(
                        name: "FK__TblVideo__vdo_id__47DBAE45",
                        column: x => x.vdo_idContenidoFk,
                        principalTable: "TblContenido",
                        principalColumn: "cto_IdContenidoPk");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblBanner_bnr_idContenidoFk",
                table: "TblBanner",
                column: "bnr_idContenidoFk");

            migrationBuilder.CreateIndex(
                name: "IX_TblContenido_cto_tipoContenidoFk",
                table: "TblContenido",
                column: "cto_tipoContenidoFk");

            migrationBuilder.CreateIndex(
                name: "IX_TblProgramacionContenido_pco_idContenidoFk",
                table: "TblProgramacionContenido",
                column: "pco_idContenidoFk");

            migrationBuilder.CreateIndex(
                name: "IX_TblUsuarios_us_datosPersonaFk",
                table: "TblUsuarios",
                column: "us_datosPersonaFk");

            migrationBuilder.CreateIndex(
                name: "IX_TblUsuarios_us_rolSistemaFk",
                table: "TblUsuarios",
                column: "us_rolSistemaFk");

            migrationBuilder.CreateIndex(
                name: "IX_TblVideo_vdo_idContenidoFk",
                table: "TblVideo",
                column: "vdo_idContenidoFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "SpContentDto");

            migrationBuilder.DropTable(
                name: "TblBanner");

            migrationBuilder.DropTable(
                name: "TblProgramacionContenido");

            migrationBuilder.DropTable(
                name: "TblUsuarios");

            migrationBuilder.DropTable(
                name: "TblVideo");

            migrationBuilder.DropTable(
                name: "TblDatosPersona");

            migrationBuilder.DropTable(
                name: "TblDicRolesSistema");

            migrationBuilder.DropTable(
                name: "TblContenido");

            migrationBuilder.DropTable(
                name: "TblDicTipoContenido");
        }
    }
}
