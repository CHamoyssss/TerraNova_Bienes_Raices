USE TerraNovaDB;
GO

-- ============================================
-- 1. TRABAJADORES (primero, porque todo depende de ellos)
-- ============================================
INSERT INTO Trabajadores (Nombre, Apellido, Ci, Cargo, Telefono, Email, PorcentajeComision, FechaContratacion, Activo)
VALUES
('Ricardo', 'Mamani', '4521369', 'Agente', '70111222', 'ricardo.mamani@terranova.com', 3.5, '2022-03-10', 1),
('Daniela', 'Vargas', '5632147', 'Agente', '70222333', 'daniela.vargas@terranova.com', 4.0, '2023-06-01', 1),
('Fernando', 'Quispe', '6741852', 'Administrador', '70333444', 'fernando.quispe@terranova.com', 0, '2021-01-15', 1),
('Andrea', 'Choque', '7852963', 'Recepcionista', '70444555', 'andrea.choque@terranova.com', 0, '2023-09-20', 1),
('Jorge', 'Sanchez', '3698521', 'Agente', '70555666', 'jorge.sanchez@terranova.com', 3.0, '2024-02-05', 1);
GO

-- ============================================
-- 2. CLIENTES
-- ============================================
INSERT INTO Clientes (Nombre, Apellido, Ci, Telefono, Email, Direccion, FechaRegistro, TipoCliente)
VALUES
('Juan', 'Pérez', '5874123', '70012345', 'juan.perez@mail.com', 'Av. Arce #123', '2026-01-15', 'Comprador'),
('María', 'Gonzales', '6231458', '70023456', 'maria.gonzales@mail.com', 'Calle Sucre #456', '2026-02-10', 'Vendedor'),
('Carlos', 'Fernández', '7458963', '70034567', 'carlos.fernandez@mail.com', 'Av. Busch #789', '2026-02-20', 'Comprador'),
('Valentina', 'Rojas', '8123654', '70045678', 'valentina.rojas@mail.com', 'Calle Comercio #321', '2026-03-01', 'Ambos'),
('Luis', 'Mendoza', '9632587', '70056789', 'luis.mendoza@mail.com', 'Av. Cristo Redentor #55', '2026-03-15', 'Comprador'),
('Camila', 'Torres', '1478523', '70067890', 'camila.torres@mail.com', 'Barrio Equipetrol #200', '2026-04-02', 'Vendedor');
GO

-- ============================================
-- 3. PROPIEDADES (referencian a Trabajadores)
-- ============================================
INSERT INTO Propiedades (Titulo, Tipo, Direccion, Zona, Precio, AreaM2, Habitaciones, Banios, Garajes, Estado, TipoOperacion, Descripcion, ImagenUrl, IdTrabajador, FechaPublicacion)
VALUES
('Casa en Zona Sur', 'Casa', 'Calle 21 de Calacoto #100', 'Sur', 185000, 220, 4, 3, 2, 'Disponible', 'Venta', 'Casa amplia con jardín y terraza.', '', 1, '2026-01-10'),
('Departamento Equipetrol', 'Departamento', 'Av. San Martín #450', 'Equipetrol', 95000, 110, 2, 2, 1, 'Disponible', 'Venta', 'Departamento moderno, edificio con piscina.', '', 2, '2026-02-05'),
('Terreno Urubó', 'Terreno', 'Condominio Las Palmas', 'Urubó', 60000, 500, 0, 0, 0, 'Reservada', 'Venta', 'Terreno plano, listo para construir.', '', 1, '2025-11-20'),
('Oficina Centro', 'Oficina', 'Calle Bolívar #300', 'Centro', 1200, 60, 0, 1, 0, 'Disponible', 'Alquiler', 'Oficina en edificio corporativo.', '', 2, '2025-12-01'),
('Casa Las Palmas', 'Casa', 'Av. Roca y Coronado #870', 'Las Palmas', 210000, 280, 5, 4, 2, 'Disponible', 'Venta', 'Casa de dos plantas con piscina.', '', 5, '2026-03-05'),
('Local Comercial 2do Anillo', 'Local', 'Av. Cañoto #150', 'Centro', 1800, 90, 0, 1, 0, 'Disponible', 'Alquiler', 'Local a pie de calle, alto tráfico.', '', 5, '2026-04-10'),
('Departamento Norte', 'Departamento', 'Av. Banzer #3200', 'Norte', 78000, 85, 2, 1, 1, 'Vendida', 'Venta', 'Departamento a estrenar, primer piso.', '', 1, '2026-01-25');
GO

-- ============================================
-- 4. VENTAS (referencian a Clientes, Propiedades y Trabajadores)
-- ============================================
INSERT INTO Ventas (IdCliente, IdPropiedad, IdTrabajador, Fecha, Monto, ComisionGenerada, FormaPago, Estado, Observaciones)
VALUES
(1, 7, 1, '2026-02-01', 78000, 2730, 'Crédito', 'Completada', 'Venta financiada por banco.'),
(3, 4, 2, '2026-01-05', 1200, 48, 'Alquiler', 'Completada', 'Contrato de alquiler anual firmado.');
GO

-- ============================================
-- 5. VISITAS (referencian a Clientes, Propiedades y Trabajadores)
-- ============================================
INSERT INTO Visitas (IdCliente, IdPropiedad, IdTrabajador, FechaVisita, Estado, Comentarios)
VALUES
(2, 1, 1, '2026-07-25 10:00', 'Programada', 'Cliente interesado, primera visita.'),
(5, 2, 2, '2026-07-20 15:30', 'Realizada', 'Le gustó, evaluando oferta.'),
(4, 5, 5, '2026-07-28 09:00', 'Programada', 'Visita en pareja, buscan casa familiar.'),
(6, 3, 1, '2026-07-18 11:00', 'Realizada', 'Interesada en construir para negocio propio.'),
(1, 6, 5, '2026-07-30 16:00', 'Programada', 'Consulta por local para nuevo emprendimiento.');
GO

-- ============================================
-- 6. USUARIOS (Admin ya generado con hash BCrypt verificado)
-- ============================================
INSERT INTO Usuarios (NombreUsuario, ContraseñaHash, Rol, IdTrabajador, Activo)
VALUES
('admin', '$2b$11$o0KFq6fKe/3Mc/0AHJfYe.5zWXDbdKRNhap7iWF2iD.VBsfHu37Nq', 'Admin', 3, 1);
GO