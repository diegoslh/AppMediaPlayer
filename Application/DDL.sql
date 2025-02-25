create table TblDatosPersona(
	dp_IdDatosPersonaPk int identity(1,1) primary key,
	dp_nombre1 varchar(20) not null,
	dp_nombre2 varchar(20),
	dp_apellido1 varchar(20) not null,
	dp_apellido2 varchar(20),
	dp_estado bit not null default 1
);

create table TblDicRolesSistema(
	rs_IdRolSistemaPk int identity(1,1) primary key,
	rs_nombreRol varchar(15) not null,
	rs_estado bit not null
);

create table TblUsuarios(
	us_IdUsuarioPk int identity(1,1) primary key,
	us_datosPersonaFk int not null,
	us_rolSistemaFk int not null,
	us_alias varchar(10) not null,
	us_contrasena varchar(80) not null,
	us_estado bit not null,
	foreign key (us_datosPersonaFk) references TblDatosPersona(dp_IdDatosPersonaPk),
	foreign key (us_rolSistemaFk) references TblDicRolesSistema(rs_IdRolSistemaPk)
);



create table TblDicTipoContenido(
	tcto_IdTipoContenidoPk int identity(1,1) primary key,
	tcto_tipoContenido varchar(3) not null,
	tcto_descripción varchar(50) not null,
	tcto_estado bit not null
);

create table TblContenido (
	cto_IdContenidoPk int identity(1,1) primary key,
	cto_titulo varchar(100) not null,
	cto_tipoContenidoFk int not null,
	cto_estado bit not null,
	foreign key (cto_tipoContenidoFk) references TblDicTipoContenido(tcto_IdTipoContenidoPk)
);

create table TblVideo(
	vdo_IdVideoPk int identity(1,1) primary key,
	vdo_idContenidoFk int not null,
	vdo_rutaAcceso varchar(50) not null,
	vdo_estado bit not null,
	foreign key (vdo_idContenidoFk) references TblContenido(cto_IdContenidoPk)
);

create table TblBanner(
	bnr_IdBannerPk int identity(1,1) primary key,
	bnr_idContenidoFk int not null,
	bnr_rutaAcceso varchar(50),
	bnr_texto varchar(255),
	bnr_estado bit not null
	foreign key (bnr_idContenidoFk) references TblContenido(cto_IdContenidoPk)
);

create table TblProgramacionContenido(
	pco_IdProgramacionPk int identity(1,1) primary key,
	pco_idContenidoFk int not null,
	pco_horaProgramada time not null,
	pco_estado bit not null
	foreign key (pco_idContenidoFk) references TblContenido(cto_IdContenidoPk)
);
