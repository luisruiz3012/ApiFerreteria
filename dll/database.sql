CREATE TABLE [productos] (
  [id] INTEGER IDENTITY PRIMARY KEY,
  [nombre] VARCHAR(MAX),
  [precio] NUMERIC(10,5),
  [unidades] INT
);

CREATE TABLE [empleados] (
  [id] INTEGER IDENTITY PRIMARY KEY,
  [nombre] VARCHAR(MAX),
  [apellido] VARCHAR(MAX)
);

CREATE TABLE [ventas] (
  [id] INTEGER IDENTITY PRIMARY KEY,
  [producto_id] INT,
  [empleado_id] INT,
  [total] NUMERIC(10,5),
  [created_at] DATETIME DEFAULT CURRENT_TIMESTAMP
);

ALTER TABLE [ventas] ADD FOREIGN KEY ([empleado_id]) REFERENCES [empleados] ([id]) ON DELETE CASCADE;

ALTER TABLE [ventas] ADD FOREIGN KEY ([producto_id]) REFERENCES [productos] ([id]) ON DELETE CASCADE;


CREATE PROCEDURE create_venta @Producto_Id INT, @Empleado_Id INT, @Unidades INT, @Total NUMERIC(10,5)
AS
BEGIN
	DECLARE @VentaId INT
	DECLARE @UnidadesProducto INT
	
	SET @UnidadesProducto = (SELECT unidades FROM productos WHERE id = @Producto_Id)
	
	IF (@UnidadesProducto >= @Unidades)
	BEGIN
		INSERT INTO ventas (producto_id, empleado_id, unidades, total) VALUES (@Producto_Id, @Empleado_Id, @Unidades, @Total)
	
		SET @VentaId = SCOPE_IDENTITY()
		UPDATE productos SET unidades = @UnidadesProducto - @Unidades WHERE id = @Producto_Id
		
		SELECT @VentaId AS VentaId
	END
	ELSE
	BEGIN
        SELECT @VentaId AS VentaId
	END
	
END

CREATE PROCEDURE get_ventas
AS
SELECT v.id, p.nombre AS producto, v.unidades, v.total, (e.nombre + ' ' + e.apellido) AS empleado,
v.created_at as fecha_facturacion
FROM
ventas v
JOIN productos p ON p.nombre = p.nombre
JOIN empleados e ON e.nombre = e.nombre AND e.apellido = e.apellido
WHERE p.id = v.producto_id AND e.id = v.empleado_id

CREATE PROCEDURE get_venta @id INT
AS
SELECT v.id, p.nombre AS producto, v.unidades, v.total, (e.nombre + ' ' + e.apellido) AS empleado,
v.created_at as fecha_facturacion
FROM
ventas v
JOIN productos p ON p.nombre = p.nombre
JOIN empleados e ON e.nombre = e.nombre AND e.apellido = e.apellido
WHERE p.id = v.producto_id AND e.id = v.empleado_id AND v.id = @id