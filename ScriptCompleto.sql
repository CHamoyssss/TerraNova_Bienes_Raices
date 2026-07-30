-- =============================================================
-- SCRIPT COMPLETO - TerraNova Bienes Raíces
-- =============================================================
-- Uso: Ejecutar en SQL Server Management Studio (SSMS)
-- =============================================================

-- =============================================================
-- 1. CREAR BASE DE DATOS
-- =============================================================
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'TerraNovaDB')
BEGIN
    CREATE DATABASE TerraNovaDB;
END
GO

USE TerraNovaDB;
GO

-- =============================================================
-- 2. TABLA TRABAJADORES (debe crearse primero porque otras tablas dependen de ella)
-- =============================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Trabajadores') AND type = 'U')
BEGIN
    CREATE TABLE Trabajadores (
        Id INT IDENTITY(1,1) NOT NULL,
        Nombre NVARCHAR(100) NOT NULL,
        Apellido NVARCHAR(100) NOT NULL,
        CI NVARCHAR(20) NOT NULL,
        Cargo NVARCHAR(50) NOT NULL,
        Telefono NVARCHAR(20) NOT NULL,
        Email NVARCHAR(150) NOT NULL,
        PorcentajeComision DECIMAL(5,2) NOT NULL,
        FechaContratacion DATE NOT NULL DEFAULT GETDATE(),
        Activo BIT NOT NULL DEFAULT 1,
        CONSTRAINT PK_Trabajadores PRIMARY KEY (Id),
        CONSTRAINT UQ_Trabajadores_CI UNIQUE (CI),
        CONSTRAINT UQ_Trabajadores_Email UNIQUE (Email)
    );
END
GO

-- =============================================================
-- 3. TABLA CLIENTES
-- =============================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Clientes') AND type = 'U')
BEGIN
    CREATE TABLE Clientes (
        Id INT IDENTITY(1,1) NOT NULL,
        Nombre NVARCHAR(100) NOT NULL,
        Apellido NVARCHAR(100) NOT NULL,
        CI NVARCHAR(20) NOT NULL,
        Telefono NVARCHAR(20) NOT NULL,
        Email NVARCHAR(150) NOT NULL,
        Direccion NVARCHAR(250) NULL,
        FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
        TipoCliente NVARCHAR(20) NOT NULL DEFAULT 'Comprador',
        CONSTRAINT PK_Clientes PRIMARY KEY (Id),
        CONSTRAINT UQ_Clientes_CI UNIQUE (CI),
        CONSTRAINT UQ_Clientes_Telefono UNIQUE (Telefono),
        CONSTRAINT UQ_Clientes_Email UNIQUE (Email)
    );
END
GO

-- =============================================================
-- 4. TABLA PROPIEDADES
-- =============================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Propiedades') AND type = 'U')
BEGIN
    CREATE TABLE Propiedades (
        Id INT IDENTITY(1,1) NOT NULL,
        Titulo NVARCHAR(150) NOT NULL,
        Tipo NVARCHAR(50) NOT NULL,
        Direccion NVARCHAR(250) NOT NULL,
        Zona NVARCHAR(100) NOT NULL,
        Precio DECIMAL(12,2) NOT NULL,
        AreaM2 FLOAT NOT NULL,
        Habitaciones INT NOT NULL,
        Banios INT NOT NULL,
        Garajes INT NOT NULL,
        Estado NVARCHAR(20) NOT NULL DEFAULT 'Disponible',
        TipoOperacion NVARCHAR(20) NOT NULL DEFAULT 'Venta',
        Descripcion NVARCHAR(MAX) NULL,
        ImagenUrl NVARCHAR(300) NULL,
        IdTrabajador INT NOT NULL,
        FechaPublicacion DATETIME NOT NULL DEFAULT GETDATE(),
        Latitud FLOAT NULL,
        Longitud FLOAT NULL,
        CONSTRAINT PK_Propiedades PRIMARY KEY (Id),
        CONSTRAINT FK_Propiedades_Trabajadores FOREIGN KEY (IdTrabajador) REFERENCES Trabajadores(Id)
    );
END
GO

-- =============================================================
-- 5. TABLA USUARIOS (para autenticación del sistema)
-- =============================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Usuarios') AND type = 'U')
BEGIN
    CREATE TABLE Usuarios (
        Id INT IDENTITY(1,1) NOT NULL,
        NombreUsuario NVARCHAR(100) NOT NULL,
        ContraseñaHash NVARCHAR(300) NOT NULL,
        Rol NVARCHAR(20) NOT NULL DEFAULT 'Agente',
        IdTrabajador INT NULL,
        Activo BIT NOT NULL DEFAULT 1,
        CONSTRAINT PK_Usuarios PRIMARY KEY (Id),
        CONSTRAINT UQ_Usuarios_NombreUsuario UNIQUE (NombreUsuario),
        CONSTRAINT FK_Usuarios_Trabajadores FOREIGN KEY (IdTrabajador) REFERENCES Trabajadores(Id)
    );
END
GO

-- =============================================================
-- 6. TABLA VENTAS
-- =============================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Ventas') AND type = 'U')
BEGIN
    CREATE TABLE Ventas (
        Id INT IDENTITY(1,1) NOT NULL,
        IdCliente INT NOT NULL,
        IdPropiedad INT NOT NULL,
        IdTrabajador INT NOT NULL,
        Fecha DATETIME NOT NULL DEFAULT GETDATE(),
        Monto DECIMAL(12,2) NOT NULL,
        ComisionGenerada DECIMAL(12,2) NOT NULL,
        FormaPago NVARCHAR(20) NOT NULL DEFAULT 'Contado',
        Estado NVARCHAR(20) NOT NULL DEFAULT 'Completada',
        Observaciones NVARCHAR(500) NULL,
        CONSTRAINT PK_Ventas PRIMARY KEY (Id),
        CONSTRAINT FK_Ventas_Clientes FOREIGN KEY (IdCliente) REFERENCES Clientes(Id),
        CONSTRAINT FK_Ventas_Propiedades FOREIGN KEY (IdPropiedad) REFERENCES Propiedades(Id),
        CONSTRAINT FK_Ventas_Trabajadores FOREIGN KEY (IdTrabajador) REFERENCES Trabajadores(Id)
    );
END
GO

-- =============================================================
-- 7. TABLA VISITAS
-- =============================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Visitas') AND type = 'U')
BEGIN
    CREATE TABLE Visitas (
        Id INT IDENTITY(1,1) NOT NULL,
        IdCliente INT NOT NULL,
        IdPropiedad INT NOT NULL,
        IdTrabajador INT NOT NULL,
        FechaVisita DATETIME NOT NULL,
        Estado NVARCHAR(20) NOT NULL DEFAULT 'Programada',
        Comentarios NVARCHAR(500) NULL,
        CONSTRAINT PK_Visitas PRIMARY KEY (Id),
        CONSTRAINT FK_Visitas_Clientes FOREIGN KEY (IdCliente) REFERENCES Clientes(Id),
        CONSTRAINT FK_Visitas_Propiedades FOREIGN KEY (IdPropiedad) REFERENCES Propiedades(Id),
        CONSTRAINT FK_Visitas_Trabajadores FOREIGN KEY (IdTrabajador) REFERENCES Trabajadores(Id)
    );
END
GO

-- =============================================================
-- 8. TABLA __EFMigrationsHistory (para migraciones de Entity Framework)
-- =============================================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'__EFMigrationsHistory') AND type = 'U')
BEGIN
    CREATE TABLE __EFMigrationsHistory (
        MigrationId NVARCHAR(150) NOT NULL,
        ProductVersion NVARCHAR(32) NOT NULL,
        CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY (MigrationId)
    );
END
GO

-- =============================================================
-- 9. DATOS INICIALES (seed)
-- =============================================================

-- Solo insertar si la tabla está vacía
IF NOT EXISTS (SELECT 1 FROM Trabajadores)
BEGIN
    -- TRABAJADORES
    INSERT INTO Trabajadores (Nombre, Apellido, Ci, Cargo, Telefono, Email, PorcentajeComision, FechaContratacion, Activo)
    VALUES
    ('Ricardo', 'Mamani', '4521369',  'Agente',         '70111222', 'ricardo.mamani@terranova.com',   3.5, '2022-03-10', 1),
    ('Daniela', 'Vargas',  '5632147',  'Agente',         '70222333', 'daniela.vargas@terranova.com',   4.0, '2023-06-01', 1),
    ('Fernando','Quispe',  '6741852',  'Administrador',  '70333444', 'fernando.quispe@terranova.com',  0,   '2021-01-15', 1),
    ('Andrea',  'Choque',  '7852963',  'Recepcionista',  '70444555', 'andrea.choque@terranova.com',    0,   '2023-09-20', 1),
    ('Jorge',   'Sanchez', '3698521',  'Agente',         '70555666', 'jorge.sanchez@terranova.com',    3.0, '2024-02-05', 1);
END
GO

IF NOT EXISTS (SELECT 1 FROM Clientes)
BEGIN
    -- CLIENTES
    INSERT INTO Clientes (Nombre, Apellido, Ci, Telefono, Email, Direccion, FechaRegistro, TipoCliente)
    VALUES
    ('Juan',      'Pérez',     '5874123', '70012345', 'juan.perez@mail.com',       'Av. Arce #123',             '2026-01-15', 'Comprador'),
    ('María',     'Gonzales',  '6231458', '70023456', 'maria.gonzales@mail.com',   'Calle Sucre #456',          '2026-02-10', 'Vendedor'),
    ('Carlos',    'Fernández', '7458963', '70034567', 'carlos.fernandez@mail.com', 'Av. Busch #789',            '2026-02-20', 'Comprador'),
    ('Valentina', 'Rojas',     '8123654', '70045678', 'valentina.rojas@mail.com',  'Calle Comercio #321',       '2026-03-01', 'Ambos'),
    ('Luis',      'Mendoza',   '9632587', '70056789', 'luis.mendoza@mail.com',     'Av. Cristo Redentor #55',   '2026-03-15', 'Comprador'),
    ('Camila',    'Torres',    '1478523', '70067890', 'camila.torres@mail.com',    'Barrio Equipetrol #200',    '2026-04-02', 'Vendedor');
END
GO

IF NOT EXISTS (SELECT 1 FROM Propiedades)
BEGIN
    -- PROPIEDADES
    INSERT INTO Propiedades (Titulo, Tipo, Direccion, Zona, Precio, AreaM2, Habitaciones, Banios, Garajes, Estado, TipoOperacion, Descripcion, ImagenUrl, IdTrabajador, FechaPublicacion)
    VALUES
    ('Casa en Zona Sur',         'Casa',        'Calle 21 de Calacoto #100', 'Sur',        185000, 220, 4, 3, 2, 'Disponible', 'Venta',    'Casa amplia con jardín y terraza.',               '', 1, '2026-01-10'),
    ('Departamento Equipetrol',  'Departamento','Av. San Martín #450',       'Equipetrol', 95000,  110, 2, 2, 1, 'Disponible', 'Venta',    'Departamento moderno, edificio con piscina.',      '', 2, '2026-02-05'),
    ('Terreno Urubó',            'Terreno',     'Condominio Las Palmas',     'Urubó',      60000,  500, 0, 0, 0, 'Reservada',  'Venta',    'Terreno plano, listo para construir.',             '', 1, '2025-11-20'),
    ('Oficina Centro',           'Oficina',     'Calle Bolívar #300',        'Centro',     1200,   60,  0, 1, 0, 'Disponible', 'Alquiler', 'Oficina en edificio corporativo.',                 '', 2, '2025-12-01'),
    ('Casa Las Palmas',          'Casa',        'Av. Roca y Coronado #870',  'Las Palmas', 210000, 280, 5, 4, 2, 'Disponible', 'Venta',    'Casa de dos plantas con piscina.',                 '', 5, '2026-03-05'),
    ('Local Comercial 2do Anillo','Local',      'Av. Cañoto #150',           'Centro',     1800,   90,  0, 1, 0, 'Disponible', 'Alquiler', 'Local a pie de calle, alto tráfico.',              '', 5, '2026-04-10'),
    ('Departamento Norte',       'Departamento','Av. Banzer #3200',          'Norte',      78000,  85,  2, 1, 1, 'Vendida',    'Venta',    'Departamento a estrenar, primer piso.',            '', 1, '2026-01-25');
END
GO

IF NOT EXISTS (SELECT 1 FROM Ventas)
BEGIN
    -- VENTAS
    INSERT INTO Ventas (IdCliente, IdPropiedad, IdTrabajador, Fecha, Monto, ComisionGenerada, FormaPago, Estado, Observaciones)
    VALUES
    (1, 7, 1, '2026-02-01', 78000,  2730,     'Crédito', 'Completada', 'Venta financiada por banco.'),
    (3, 4, 2, '2026-01-05', 1200,   48,       'Alquiler','Completada', 'Contrato de alquiler anual firmado.');
END
GO

IF NOT EXISTS (SELECT 1 FROM Visitas)
BEGIN
    -- VISITAS
    INSERT INTO Visitas (IdCliente, IdPropiedad, IdTrabajador, FechaVisita, Estado, Comentarios)
    VALUES
    (2, 1, 1, '2026-07-25 10:00', 'Programada', 'Cliente interesado, primera visita.'),
    (5, 2, 2, '2026-07-20 15:30', 'Realizada',  'Le gustó, evaluando oferta.'),
    (4, 5, 5, '2026-07-28 09:00', 'Programada', 'Visita en pareja, buscan casa familiar.'),
    (6, 3, 1, '2026-07-18 11:00', 'Realizada',  'Interesada en construir para negocio propio.'),
    (1, 6, 5, '2026-07-30 16:00', 'Programada', 'Consulta por local para nuevo emprendimiento.');
END
GO

IF NOT EXISTS (SELECT 1 FROM Usuarios)
BEGIN
    -- USUARIO ADMIN (contraseña: Admin123! — hash BCrypt)
    INSERT INTO Usuarios (NombreUsuario, ContraseñaHash, Rol, IdTrabajador, Activo)
    VALUES ('admin', '$2b$11$o0KFq6fKe/3Mc/0AHJfYe.5zWXDbdKRNhap7iWF2iD.VBsfHu37Nq', 'Admin', 3, 1);
END
GO

-- =============================================================
-- 10. REGISTRO DE MIGRACIONES (para que EF Core reconozca la BD como actualizada)
-- =============================================================
IF NOT EXISTS (SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20260723122624_InitialCreate')
BEGIN
    INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20260723122624_InitialCreate', '8.0.13');
END
GO

IF NOT EXISTS (SELECT 1 FROM __EFMigrationsHistory WHERE MigrationId = '20260728121608_AddLatitudLongitud')
BEGIN
    INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20260728121608_AddLatitudLongitud', '8.0.13');
END
GO

PRINT '===========================================';
PRINT 'Base de datos TerraNovaDB creada/actualizada';
PRINT 'Usuario admin: admin / Admin123!';
PRINT '===========================================';
GO
