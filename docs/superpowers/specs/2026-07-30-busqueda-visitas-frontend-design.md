# Búsqueda y Filtros en ListarVisitas

## Resumen
Agregar una barra de búsqueda y filtros a la página `ListarVisitas.razor` (ruta `/visitas`) siguiendo el mismo patrón client-side que ya usan `ListarPropiedades.razor` y `ListarClientes.razor`.

## Comportamiento
- Todo el filtrado es **client-side** sobre la lista completa obtenida de `ServicioVisitas.Lista()`.
- No se requieren cambios en el backend (API, servicios, DTOs).
- Los filtros se aplican automáticamente al escribir o cambiar un valor.
- Botón "Limpiar filtros" que resetea todos los campos y muestra la lista completa.

## Campos de filtro

| Tipo | Campos que busca |
|------|-----------------|
| **Texto libre** | `Cliente.Nombre`, `Cliente.Apellido`, `Propiedad.Titulo`, `Comentarios` |
| **Estado** | dropdown: Todos, Programada, Realizada, Cancelada |
| **Fecha desde** | `FechaVisita >= fecha seleccionada` |
| **Fecha hasta** | `FechaVisita <= fecha seleccionada` |

## Diseño visual
Panel gris (`panel-card shadow-sm`) con 4 columnas en fila (`row g-2`):
```
[ Texto (col-md-4) ] [ Estado (col-md-2) ] [ Fecha Desde (col-md-2) ] [ Fecha Hasta (col-md-2) ] [ Limpiar (col-md-2) ]
```

## Cambios en `ListarVisitas.razor`

### Nuevas variables en `@code`
- `List<VisitaDTO> visitasFiltradas` — lista a renderizar en la tabla
- `string textoBusqueda`
- `string estadoFiltro`
- `DateTime? fechaDesde`
- `DateTime? fechaHasta`

### Métodos nuevos
- `AplicarFiltros()` — ejecuta LINQ combinando todos los filtros activos
- `Filtrar()` — llamado desde los controles, delega a `AplicarFiltros()`
- `LimpiarFiltros()` — resetea todas las variables y restaura `visitasFiltradas`

### Modificaciones
- El `foreach` itera sobre `visitasFiltradas` en lugar de `visitas`
- `CargarVisitas()` llama a `AplicarFiltros()` después de asignar `visitas`
- Mensaje empty state condicional: "No hay visitas programadas." si no hay datos, o "No se encontraron visitas con esos filtros." si hay datos pero no coinciden.

## No se modifica
- Backend (API, controladores, servicios, DTOs, modelos)
- ServicioVisitas.cs
- RegistrarVisita.razor
- Router, navegación, layout
