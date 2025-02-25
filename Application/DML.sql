
INSERT INTO TblDatosPersona
VALUES
	('Jose', 'Miguel', 'Perez', 'Orjuela', 1);

INSERT INTO	TblDicRolesSistema
VALUES
	('Administrador', 1);

INSERT INTO	TblUsuarios
VALUES
	(1, 1, 'Admin', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 1);


insert into TblDicTipoContenido
values
    ('BT', 'Banner', 1),
    ('VT', 'Video', 1),
    ('VBL', 'Video/Banner Lateral', 1);

insert into TblContenido
values
    ('Titulo 1 BT',1, 1),
    ('Titulo 2 VT',2, 1),
    ('Titulo 3 VBL',3, 1),
    ('Titulo 4 BT',1, 1),
    ('Titulo 5 VT',2, 1),
    ('Titulo 6 VBL',3, 1),
    ('Titulo 7 BT',1, 1);

insert into TblVideo
values
    (2, 'ruta1', 1),
    (3, 'ruta2', 1),
    (5, 'ruta3', 1),
    (6, 'ruta4', 1);

insert into TblBanner
values
    (1, 'ruta1', 'Texto de prueba para banner', 1),
    (3, 'ruta2', null, 1),
    (4, null, 'Solo texto de prueba para banner', 1),
    (6, 'ruta4', 'Texto de prueba para banner', 1),
    (7, 'ruta5', null, 1);

insert into TblProgramacionContenido
values
    (1, '12:00:00', 1),
    (2, '13:00:00', 1),
    (3, '14:00:00', 1),
    (4, '15:00:00', 1),
    (5, '16:00:00', 1),
    (6, '17:00:00', 1),
    (7, '18:00:00', 1);
alter table TblBanner alter column bnr_duracion int;

select * from TblDicTipoContenido;

Select * from TblContenido inner join TblDicTipoContenido TDTC on TDTC.tcto_IdTipoContenidoPk = TblContenido.cto_tipoContenidoFk
WHERE TblContenido.cto_estado = 1;

select * from TblContenido
where cto_IdContenidoPk = 1007;

select * from TblVideo
where vdo_idContenidoFk = 1007;
select * from TblBanner 
where bnr_idContenidoFk = 1007;

select * from TblProgramacionContenido;
select * from TblProgramacionContenido inner join TblContenido TC on TC.cto_IdContenidoPk = TblProgramacionContenido.pco_IdProgramacionPk;
select * from TblProgramacionContenido 
	inner join TblContenido TC on TC.cto_IdContenidoPk = TblProgramacionContenido.pco_IdProgramacionPk
	inner join TblDicTipoContenido TDTC on TDTC.tcto_IdTipoContenidoPk = TC.cto_tipoContenidoFk;


SELECT
    TC.cto_titulo AS CtoTitulo,
    TC.cto_tipoContenidoFk AS CtoTipoContenidoFk,
    ISNULL(TB.bnr_rutaAcceso, NULL) AS CtoBanner,
    ISNULL(TV.vdo_rutaAcceso, NULL) AS CtoVideo,
    ISNULL(TB.bnr_texto, NULL) AS CtoTextoBanner,
    ISNULL(TB.bnr_duracion, NULL) AS CtoDurationBanner,
    TC.cto_estado AS CtoEstado
FROM TblContenido TC
LEFT JOIN TblBanner TB on TC.cto_IdContenidoPk = TB.bnr_idContenidoFk
LEFT JOIN TblVideo TV on TC.cto_IdContenidoPk = TV.vdo_idContenidoFk
WHERE TC.cto_estado = 1;

exec SP_GetAllFullContent;


--delete from TblContenido;
--delete from TblProgramacionContenido;
--delete from TblBanner;
--delete from TblVideo;

