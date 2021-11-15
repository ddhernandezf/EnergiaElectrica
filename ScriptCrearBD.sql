use EnergiaElectrica;

create table [TipoCliente] (
	[Id]		tinyint not null identity(1,1),
	[Nombre]	varchar(50) not null,
	[Precio]	decimal(18,2) not null,
	constraint pkTipoCliente primary key([Id]),
	constraint uqTipoCliente unique([Nombre])
);

create table [TipoMedidor] (
	[Id]		tinyint not null identity(1,1),
	[Nombre]	varchar(40) not null,
	constraint pkTipoMedidor primary key([Id]),
	constraint uqTipoMedidor unique([Nombre])
);

create table [Cliente] (
	[Id]				bigint not null identity(1,1),
	[Tipo]				tinyint not null,
	[Medidor]			tinyint not null,
	[NombreCompleto]	varchar(120) not null,
	[Correo]			varchar(200) not null,
	[Direccion]			varchar(200) not null,
	[Telefono]			varchar(8) not null,
	[EnviarFactura]		bit not null,
	[NumeroContador]	varchar(10) not null,
	constraint pkCliente primary key ([Id]),
	constraint uqCliente unique ([NumeroContador]),
	constraint fkClienteTipocliente foreign key([Tipo]) references [TipoCliente]([Id]),
	constraint fkClienteTipomedidor foreign key([Medidor]) references [TipoMedidor]([Id]),
);

create table [Usuario] (
	[Id]		tinyint not null identity(1,1),
	[Usuario]	varchar(45) not null,
	[Password]	varchar(45) not null,
	constraint pkUsuario primary key([Id])
);

create table [Medicion] (
	[Id]				bigint not null identity(1,1),
	[Anio]				smallint not null,
	[Mes]				tinyint not null,
	[Cliente]			bigint not null,
	[Lectura]			bigint not null,
	[Fecha]				datetime not null,
	[Usuario]			tinyint not null,
	[MontoCobrar]		decimal(18,2) not null,
	constraint pkMedicion primary key ([Id]),
	constraint fkMedicionCliente foreign key([Cliente]) references [Cliente]([Id]),
	constraint fkMedicionUsuario foreign key([Usuario]) references [Usuario]([Id]),
);

create table [Factura] (
	[Id]		bigint not null,
	[Codigo]	uniqueidentifier not null,
	[Fecha]		datetime not null,
	[Cliente]	bigint not null,
	[Medicion]	bigint not null,
	constraint pkFactura primary key ([Id]),
	constraint fkFacturaCliente foreign key([Cliente]) references [Cliente]([Id]),
	constraint fkFacturaUsuario foreign key([Medicion]) references [Medicion]([Id]),
);

insert into [TipoCliente] values
('Residencial', 2.5),
('Industria', 5.87),
('Residencia tipo a', 3.84);

insert into [TipoMedidor] values
('Análogo'),
('Digital');