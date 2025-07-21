using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class dml_basic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert initial data into the necessary database tables and 1 user to start
            migrationBuilder.Sql(@"
                INSERT INTO TblDicRolesSistema VALUES ('Admin', 1), ('User',1);
                INSERT INTO TblDicTipoContenido VALUES ('BT', 'Banner', 1), ('VT', 'Video', 1), ('VTB', 'Video + Banner', 1);
                INSERT INTO TblDatosPersona VALUES ('Super', 'Admin', 'First', 'User', 1);
                INSERT INTO TblUsuarios values (1, 1, 'Admin23', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 1); -- Password: 123
                INSERT INTO TblUsuarios 
                VALUES 
	                (
		                (SELECT TOP 1 dp_IdDatosPersonaPk FROM TblDatosPersona), 
		                (SELECT rs_IdRolSistemaPk FROM TblDicRolesSistema WHERE rs_nombreRol IN ('Admin')), 
		                'Admin23', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 1
                    ); -- Password: 123
            ");

            // Create Store Procedure neccesary to get full content
            migrationBuilder.Sql(@"
                CREATE PROCEDURE SP_GetAllFullContent
                AS

                BEGIN
	                SELECT 
		                c.cto_IdContenidoPk as CtoIdContenidoPk,
		                c.cto_titulo as CtoTitulo,
		                c.cto_tipoContenidoFk as CtoTipoContenidoFk,
		                tc.tcto_tipoContenido as CtoTipoContenido,
		                ISNULL(bn.bnr_rutaAcceso, NULL) as CtoBanner,
		                ISNULL(vd.vdo_rutaAcceso, NULL) as CtoVideo,
		                ISNULL(bn.bnr_texto, NULL) as CtoTextoBanner,
		                ISNULL(bn.bnr_duracion,NULL) as CtoDurationBanner,
		                c.cto_estado as CtoEstado
	                FROM TblContenido c
	                INNER JOIN TblDicTipoContenido tc ON tc.tcto_IdTipoContenidoPk = c.cto_tipoContenidoFk
	                LEFT JOIN TblBanner bn ON bn.bnr_idContenidoFk = c.cto_IdContenidoPk
	                LEFT JOIN TblVideo vd ON vd.vdo_idContenidoFk = c.cto_IdContenidoPk
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar el SP si existe
            migrationBuilder.Sql(@"
                IF OBJECT_ID('SP_GetAllFullContent', 'P') IS NOT NULL
                DROP PROCEDURE SP_GetAllFullContent;
            ");

            // Eliminar datos semilla (si estás seguro de que no fueron modificados)
            migrationBuilder.Sql(@"
                DELETE FROM TblDatosPersona WHERE per_primerNombre = 'Super' AND per_primerApellido = 'Admin';
                DELETE FROM TblUsuarios WHERE usr_nombreUsuario = 'Admin23';
                DELETE FROM TblDicRolesSistema WHERE rol_nombre IN ('Admin', 'User');
                DELETE FROM TblDicTipoContenido WHERE tcto_tipoContenido IN ('Banner', 'Video', 'Video + Banner');
            ");
        }

    }
}
