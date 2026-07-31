#!/bin/bash
# Seed script for TerraNova Bienes Raíces
# Generates realistic data: 10 trabajadores, 20 clientes, 20 propiedades,
# 35 ventas, 25 visitas, 6 usuarios

API="http://localhost:5150/api"
SQL="docker exec terranova-sql /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'T3rraN0v4!' -d TerraNovaDB -C -Q"
PASS_HASH='$2a$11$0y7LUt.b8DR5OEuuzmyeVePvDek7.4QodOVlE6WXJKcZD0O5TfE42'

echo "=== Adding Trabajadores ==="
eval $SQL "
SET IDENTITY_INSERT Trabajadores ON;
INSERT INTO Trabajadores (Id, Nombre, Apellido, CI, Cargo, Telefono, Email, PorcentajeComision, FechaContratacion, Activo)
VALUES
(6, 'Miguel', 'Ribera', '8881111', 'Agente', '71111111', 'miguel.ribera@terranova.bo', 4.0, '2025-06-01', 1),
(7, 'Patricia', 'Suárez', '8882222', 'Agente', '72222222', 'patricia.suarez@terranova.bo', 3.5, '2025-07-15', 1),
(8, 'Roberto', 'Vaca', '8883333', 'Agente', '73333333', 'roberto.vaca@terranova.bo', 3.0, '2025-08-01', 1),
(9, 'Carmen', 'Llanos', '8884444', 'Administrador', '74444444', 'carmen.llanos@terranova.bo', 0, '2025-04-01', 1),
(10, 'Diego', 'Padilla', '8885555', 'Agente', '75555555', 'diego.padilla@terranova.bo', 3.5, '2025-09-01', 1);
SET IDENTITY_INSERT Trabajadores OFF;
"

echo "=== Creating Usuarios via API ==="
declare -A USERS=(
  ["ricardo"]="1|Agente"
  ["daniela"]="2|Agente"
  ["fernando"]="3|Administrador"
  ["andrea"]="4|Recepcionista"
  ["jorge"]="5|Agente"
  ["miguel"]="6|Agente"
)

for user in "${!USERS[@]}"; do
  IFS='|' read -r worker id_role <<< "${USERS[$user]}"
  curl -s -X POST "$API/Usuario/Guardar" \
    -H "Content-Type: application/json" \
    -d "{\"nombreUsuario\":\"$user\",\"contraseñaHash\":\"123\",\"rol\":\"$id_role\",\"idTrabajador\":$worker,\"activo\":true}" > /dev/null
  echo "  User '$user' created (worker $worker, $id_role)"
done

# Update admin password to "123"
echo "  Updating admin password to '123'"
eval $SQL "UPDATE Usuarios SET ContraseñaHash='$PASS_HASH' WHERE NombreUsuario='admin'"

echo ""
echo "=== Adding Clientes ==="
eval $SQL "
SET IDENTITY_INSERT Clientes ON;
INSERT INTO Clientes (Id, Nombre, Apellido, CI, Telefono, Email, Direccion, FechaRegistro, TipoCliente) VALUES
(7,  'Pedro',   'Aguilar',  '1472589', '71234567', 'pedro.aguilar@mail.com',    'Av. Beni #450',                       '2025-05-10', 'Comprador'),
(8,  'Sofia',   'Cruz',     '2583691', '72345678', 'sofia.cruz@mail.com',      'Calle Los Olivos #200',               '2025-05-22', 'Comprador'),
(9,  'Miguel',  'Vargas',   '3691472', '73456789', 'miguel.vargas@mail.com',   'Av. Paraguá #800',                     '2025-06-05', 'Vendedor'),
(10, 'Lucía',   'Flores',   '4712583', '74567890', 'lucia.flores@mail.com',    'Calle Libertad #150',                  '2025-06-18', 'Ambos'),
(11, 'Andrés',  'Roca',     '5823694', '75678901', 'andres.roca@mail.com',     'Av. Cristo Redentor #320',             '2025-07-02', 'Comprador'),
(12, 'Gabriela','Pinto',    '6931475', '76789012', 'gabriela.pinto@mail.com',  'Calle Potosí #75',                     '2025-07-20', 'Comprador'),
(13, 'Fernando','Antelo',   '7142586', '77890123', 'fernando.antelo@mail.com', 'Av. San Aurelio #510',                 '2025-08-10', 'Vendedor'),
(14, 'Rosa',    'Medina',   '8253697', '78901234', 'rosa.medina@mail.com',     'Calle Bolívar #45',                    '2025-08-25', 'Ambos'),
(15, 'Javier',  'Ortiz',    '9361478', '79012345', 'javier.ortiz@mail.com',    'Av. Doble Vía La Guardia #1200',       '2025-09-12', 'Comprador'),
(16, 'Elena',   'Céspedes', '1472580', '70123456', 'elena.cespedes@mail.com',  'Calle Oriente #89',                    '2025-09-28', 'Comprador'),
(17, 'Marco',   'Zeballos', '2583690', '71234568', 'marco.zeballos@mail.com',  'Av. Cumavi #202',                      '2025-10-05', 'Vendedor'),
(18, 'Ana',     'Rivero',   '3691470', '72345679', 'ana.rivero@mail.com',      'Calle Junín #321',                     '2025-10-20', 'Ambos'),
(19, 'Pablo',   'Soria',    '4712580', '73456780', 'pablo.soria@mail.com',     'Av. Centenario #1500',                 '2025-11-02', 'Comprador'),
(20, 'Diana',   'Vásquez',  '5823690', '74567891', 'diana.vasquez@mail.com',   'Calle Suárez #55',                     '2025-11-18', 'Comprador');
SET IDENTITY_INSERT Clientes OFF;
"

echo ""
echo "=== Adding Propiedades ==="
eval $SQL "
SET IDENTITY_INSERT Propiedades ON;
INSERT INTO Propiedades (Id, Titulo, Tipo, Direccion, Zona, Precio, AreaM2, Habitaciones, Banios, Garajes, Estado, TipoOperacion, Descripcion, IdTrabajador, FechaPublicacion) VALUES
(8,  'Casa Villa Paraíso',       'Casa',        'Av. Paraíso #2500',             'Urubó',     350000, 350, 5, 4, 3, 'Disponible', 'Venta',   'Casa de lujo con piscina y amplio jardín en la zona más exclusiva de Urubó.', 1, '2025-05-01'),
(9,  'Departamento Torre Norte', 'Departamento','Av. San Martín #1010',          'Equipetrol', 120000, 90,  3, 2, 1, 'Disponible', 'Venta',   'Moderno departamento con vistas panorámicas en el corazón de Equipetrol.', 2, '2025-05-15'),
(10, 'Terreno Industrial Norte',  'Terreno',     'Av. Industrial #500',           'Norte',     180000, 800, 0, 0, 0, 'Disponible', 'Venta',   'Amplio terreno industrial con acceso directo a la avenida principal.', 6, '2025-06-01'),
(11, 'Oficina Ejecutiva Centro',  'Oficina',     'Calle La Paz #470',             'Centro',     35000,  50,  1, 1, 0, 'Disponible', 'Venta',   'Oficina ejecutiva completamente amoblada en el centro financiero.', 7, '2025-06-10'),
(12, 'Casa Los Tusequis',        'Casa',        'Av. Los Tusequis #1340',        'Equipetrol', 280000, 280, 4, 3, 2, 'Disponible', 'Venta',   'Casa familiar con áreas verdes y seguridad privada.', 8, '2025-06-20'),
(13, 'Local Comercial Mercado',  'Local',       'Av. Grigotá #99',               'Centro',     2500,    40,  0, 1, 0, 'Disponible', 'Alquiler','Local comercial en zona de alto tráfico peatonal.', 1, '2025-07-01'),
(14, 'Departamento Estudio',     'Departamento','Calle René Moreno #210',        'Centro',      45000,  45,  2, 1, 1, 'Disponible', 'Venta',   'Departamento tipo estudio ideal para inversión.', 10,'2025-07-15'),
(15, 'Casa Quinta Las Palmas',   'Casa',        'Camino Las Palmas #150',        'Urubó',     420000, 450, 5, 4, 3, 'Disponible', 'Venta',   'Quinta de lujo con piscina olímpica y cancha de tenis.', 2, '2025-08-01'),
(16, 'Terreno Residencial Sur',  'Terreno',     'Av. Cristo Redentor #2500',     'Sur',       120000, 500, 0, 0, 0, 'Disponible', 'Venta',   'Terreno plano listo para construir en zona residencial en crecimiento.', 6, '2025-08-15'),
(17, 'Oficina Compartida Cowork','Oficina',     'Av. San Martín #550',           'Equipetrol',  1800,   30,  0, 1, 0, 'Disponible', 'Alquiler','Espacio de coworking con todos los servicios incluidos.', 7, '2025-09-01'),
(18, 'Casa Don Bosco',           'Casa',        'Av. Don Bosco #750',            'Sur',       195000, 200, 3, 2, 2, 'Disponible', 'Venta',   'Casa en condominio cerrado con áreas comunes.', 8, '2025-09-10'),
(19, 'Duplex Norte',             'Departamento','Av. Alemana #400',              'Norte',      85000,  80,  3, 2, 1, 'Disponible', 'Venta',   'Dúplex con terraza en zona residencial del norte.', 10,'2025-09-20'),
(20, 'Local Galería Comercial',  'Local',       'Calle Sucre #200',               'Centro',     1200,    25,  0, 0, 0, 'Disponible', 'Alquiler','Pequeño local tipo galería ideal para emprendimiento.', 1, '2025-10-01');
SET IDENTITY_INSERT Propiedades OFF;
"

echo ""
echo "=== Adding Ventas (35 total) ==="
eval $SQL "
SET IDENTITY_INSERT Ventas ON;
-- 2023 Ventas históricas
INSERT INTO Ventas (Id, IdCliente, IdPropiedad, IdTrabajador, Fecha, Monto, ComisionGenerada, FormaPago, Estado, Observaciones) VALUES
(3,  3,  3,  3, '2023-03-15', 180000,  0, 'Contado',   'Completada', 'Venta directa por mudanza del propietario.'),
(4,  5,  4,  4, '2023-06-20', 120000,  0, 'Credito',   'Completada', 'Financiamiento bancario aprobado al 80%.'),
(5,  1,  5,  5, '2023-09-10', 28000,   980, 'Contado',  'Completada', 'Venta rápida, 15 días en el mercado.');
-- 2024
INSERT INTO Ventas (Id, IdCliente, IdPropiedad, IdTrabajador, Fecha, Monto, ComisionGenerada, FormaPago, Estado, Observaciones) VALUES
(6,  2,  2,  1, '2024-01-25', 95000,   3325, 'Credito',  'Completada', 'Primera venta del año, cliente recomendado.'),
(7,  4,  6,  2, '2024-02-14', 25000,   875,  'Contado',  'Completada', 'Local para nuevo restaurante.'),
(8,  6,  7,  6, '2024-03-30', 250000,  8750, 'Credito',  'Completada', 'Casa familiar, venta con hipoteca.'),
(9,  7,  1,  7, '2024-04-18', 185000,  6475, 'Contado',  'Completada', 'Cliente recomendó a 2 conocidos después.'),
(10, 8,  8,  8, '2024-05-05', 350000, 12250, 'Credito',  'Completada', 'Venta de lujo, proceso de 3 meses.'),
(11, 9,  3,  1, '2024-06-12', 22000,   770,  'Contado',  'Completada', 'Alquiler temporal de 2 años.'),
(12, 10, 9,  2, '2024-07-20', 120000,  4200, 'Credito',  'Completada', 'Departamento para inversión.'),
(13, 11, 10, 6, '2024-08-15', 180000,  6300, 'Contado',  'Completada', 'Terreno industrial para nueva planta.');
-- 2025
INSERT INTO Ventas (Id, IdCliente, IdPropiedad, IdTrabajador, Fecha, Monto, ComisionGenerada, FormaPago, Estado, Observaciones) VALUES
(14, 12, 11, 7, '2025-01-20', 35000,   1225, 'Contado',  'Completada', 'Oficina para startup tecnológica.'),
(15, 13, 4,  8, '2025-02-10', 36000,   1260, 'Credito',  'Completada', 'Alquiler con opción a compra.'),
(16, 14, 12, 1, '2025-03-05', 280000,  9800, 'Credito',  'Completada', 'Casa para familia grande.'),
(17, 3,  13, 2, '2025-03-22', 2500,    87,   'Contado',  'Completada', 'Alquiler de local comercial.'),
(18, 6,  14, 6, '2025-04-15', 45000,   1575, 'Contado',  'Completada', 'Departamento estudio para estudiante.'),
(19, 15, 15, 7, '2025-05-01', 420000, 14700, 'Credito',  'Completada', 'Quinta de lujo, cliente extranjero.'),
(20, 16, 1,  8, '2025-05-28', 185000,  6475, 'Credito',  'Completada', 'Casa en Zona Sur, tasación impecable.');
-- 2026 (Ene-Jun)
INSERT INTO Ventas (Id, IdCliente, IdPropiedad, IdTrabajador, Fecha, Monto, ComisionGenerada, FormaPago, Estado, Observaciones) VALUES
(21, 1,  2,  1, '2026-01-05', 95000,   3325, 'Credito',  'Completada', 'Venta de departamento Equipetrol.'),
(22, 2,  3,  2, '2026-01-18', 60000,   2100, 'Contado',  'Completada', 'Terreno Urubó para construcción.'),
(23, 17, 5,  6, '2026-02-01', 210000,  7350, 'Credito',  'Completada', 'Casa Las Palmas, precio negociado.'),
(24, 18, 16, 7, '2026-02-20', 120000,  4200, 'Contado',  'Completada', 'Terreno en Sur para proyecto inmobiliario.'),
(25, 4,  6,  8, '2026-03-08', 1800,    63,   'Mensual',  'Completada', 'Alquiler de local comercial 2do Anillo.'),
(26, 7,  17, 1, '2026-03-25', 1800,    63,   'Mensual',  'Completada', 'Coworking, contrato anual.'),
(27, 8,  7,  2, '2026-04-05', 78000,   2730, 'Credito',  'Completada', 'Departamento Norte, segunda vivienda.'),
(28, 19, 18, 6, '2026-04-20', 195000,  6825, 'Credito',  'Completada', 'Casa Don Bosco, hipoteca aprobada.'),
(29, 20, 12, 7, '2026-05-10', 280000,  9800, 'Contado',  'Completada', 'Casa Los Tusequis, venta directa.'),
(30, 2,  8,  8, '2026-05-25', 35000,   1225, 'Contado',  'Completada', 'Casa Villa Paraíso con piscina.'),
(31, 5,  9,  1, '2026-06-05', 120000,  4200, 'Credito',  'Completada', 'Torre Norte financiado al 70%.'),
(32, 3,  10, 2, '2026-06-20', 180000,  6300, 'Contado',  'Completada', 'Terreno industrial, cliente corporativo.');
-- 2026 (Jul - Recientes)
INSERT INTO Ventas (Id, IdCliente, IdPropiedad, IdTrabajador, Fecha, Monto, ComisionGenerada, FormaPago, Estado, Observaciones) VALUES
(33, 10, 14, 6, '2026-07-01', 45000,   1575, 'Contado',  'Completada', 'Departamento estudio para inversión.'),
(34, 12, 19, 7, '2026-07-10', 85000,   2975, 'Credito',  'Completada', 'Duplex Norte, familia joven.'),
(35, 14, 20, 8, '2026-07-18', 1200,    42,   'Mensual',  'Completada', 'Local galería para tienda de ropa.'),
(36, 16, 4,  1, '2026-07-22', 1800,    63,   'Mensual',  'Activa',    'Alquiler oficina centro renovado.'),
(37, 9,  5,  2, '2026-07-28', 12000,   420,  'Contado',  'Activa',    'Anticipo por alquiler con opción a compra.');
SET IDENTITY_INSERT Ventas OFF;
"

echo ""
echo "=== Adding Visitas (25 total) ==="
eval $SQL "
SET IDENTITY_INSERT Visitas ON;
INSERT INTO Visitas (Id, IdCliente, IdPropiedad, IdTrabajador, FechaVisita, Estado, Comentarios) VALUES
(6,  7,  8,  1,  '2025-04-01 10:00', 'Realizada',   'Cliente interesado en casa de lujo, agendó segunda visita con su esposa.'),
(7,  8,  9,  2,  '2025-04-10 15:00', 'Realizada',   'Vio el departamento, le gustó pero quiere comparar con otros.'),
(8,  9,  7,  6,  '2025-04-20 11:00', 'Realizada',   'Primera visita, cliente quiere vender primero.'),
(9,  10, 8,  7,  '2025-05-05 09:00', 'Realizada',   'Interesada en Casa Villa Paraíso, busca algo para su familia.'),
(10, 11, 12, 8, '2025-05-18 16:00', 'Realizada',   'Andrés vino con su cuñado, muy interesado en la zona.'),
(11, 12, 3,  1,  '2025-06-01 14:00', 'Realizada',   'Cliente vio el terreno Urubó, quiere construír allí.'),
(12, 13, 13, 2,  '2025-06-15 10:00', 'Realizada',   'Cliente corporativo buscando local para franquicia.'),
(13, 14, 5,  6,  '2025-07-01 11:30', 'Realizada',   'Rosa quiere vender su casa y comprar Las Palmas.'),
(14, 15, 15, 7,  '2025-07-20 15:00', 'Cancelada',   'Cliente canceló por viaje al exterior.'),
(15, 16, 4,  8,  '2025-08-05 10:00', 'Realizada',   'Elena busca oficina para su consultorio.'),
(16, 17, 16, 1, '2025-08-20 09:00', 'Realizada',   'Marco quiere vender su terreno, vino a tasarlo.'),
(17, 18, 18, 2, '2025-09-05 16:00', 'Realizada',   'Ana busca casa en condominio cerrado, Don Bosco le encantó.'),
(18, 19, 19, 6, '2025-09-20 11:00', 'Realizada',   'Pablo vio el duplex, interesado en compra inmediata.'),
(19, 20, 20, 7, '2025-10-05 14:00', 'Realizada',   'Diana busca local para su boutique.'),
(20, 1,  8,  8,  '2026-01-10 10:00', 'Realizada',   'Juan busca inversión, Casa Villa Paraíso le parece excelente.'),
(21, 3,  17, 1, '2026-02-15 11:00', 'Realizada',   'Carlos interesado en coworking para su equipo remoto.'),
(22, 5,  10, 2, '2026-03-10 09:00', 'Realizada',   'Luis busca terreno industrial para expansión.'),
(23, 8,  7,  6,  '2026-04-05 15:00', 'Cancelada',   'Cliente pidió reprogramar por emergencia familiar.'),
(24, 15, 6,  7,  '2026-05-20 10:00', 'Programada', 'Javier de vuelta, quiere ver locales comerciales.'),
(25, 11, 19, 8, '2026-06-15 16:00', 'Programada', 'Andrés interesado en duplex como inversión.'),
(26, 20, 11, 1, '2026-07-08 11:00', 'Programada', 'Diana busca oficina en el centro para su empresa.'),
(27, 13, 4,  2,  '2026-07-20 14:00', 'Programada', 'Nuevo cliente recomendado, busca casa en Urubó.'),
(28, 6,  12, 6,  '2026-07-25 09:00', 'Programada', 'Camila referida por venta anterior, busca casa Los Tusequis.'),
(29, 17, 7,  7,  '2026-07-30 16:00', 'Programada', 'Marco busca departamento para alquilar.'),
(30, 4,  1,  8,  '2026-08-02 10:00', 'Programada', 'Valentina interesada en casa Zona Sur para su hija.');
SET IDENTITY_INSERT Visitas OFF;
"

echo ""
echo "=== Data population complete ==="
