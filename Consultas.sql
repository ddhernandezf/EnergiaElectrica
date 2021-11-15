/*Devuelve cliente en base a contador*/
select	tc.Nombre[TipoCliente], tm.Nombre[TipoMedidor], NombreCompleto, Direccion, Telefono, NumeroContador
  from	Cliente c
		inner join TipoCliente tc
			on c.Tipo = tc.Id
		inner join TipoMedidor tm
			on c.Medidor = tm.Id
 where	c.NumeroContador = 'WX-0001'

/*Historial cobros cliente*/
select	m.Fecha, m.Mes, m.Anio, m.Lectura, m.MontoCobrar
  from	Cliente c
		inner join Medicion m
			on	c.Id = m.Cliente
 where	c.NumeroContador = 'WX-0001'
   and	m.Anio = 2021
 order	by	 m.Mes desc

/*Cálculo*/
declare @Lectura as bigint = 1200;
declare @AnioActual as smallint = year(getdate());
declare @MesActual as tinyint = month(getdate());

select	tc.Precio, @Lectura[LecturaActual], m.Lectura[LecturaAnterior], @Lectura - m.Lectura[Lectura],
		tc.Precio * (@Lectura - m.Lectura)[Monto], @MesActual, @AnioActual
  from	Cliente c
		inner join TipoCliente tc
			on c.Tipo = tc.Id
		inner join Medicion m
			on	c.Id = m.Cliente
 where	c.NumeroContador = 'WX-0001'
   and	m.Anio = @AnioActual
   and	m.Mes = (@MesActual - 1)