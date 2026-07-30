# Búsqueda en ListarVisitas — Plan de Implementación

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Agregar barra de búsqueda y filtros (texto, estado, rango de fechas) a `ListarVisitas.razor`.

**Architecture:** Todo client-side sobre la lista completa. Sin cambios en backend. Mismo patrón que `ListarPropiedades.razor`.

**Tech Stack:** Blazor WebAssembly, Bootstrap 5, Bootstrap Icons

**File to modify:**
- `Blazor.Client/Pages/Visitas/ListarVisitas.razor`

---
### Task 1: Agregar barra de filtros y lógica de filtrado en ListarVisitas.razor

**Files:**
- Modify: `Blazor.Client/Pages/Visitas/ListarVisitas.razor`

- [ ] **Step 1: Agregar el panel de filtros en el markup**

Insertar después del `</div>` del header (línea 17) y antes del `@if (cargando)` (línea 19):

```razor
<div class="panel-card shadow-sm mb-4 p-3">
    <div class="row g-2">
        <div class="col-md-4">
            <input type="text" class="form-control" placeholder="Buscar por cliente, propiedad o comentarios..."
                   @bind="textoBusqueda" @bind:event="oninput" @onkeyup="Filtrar" />
        </div>
        <div class="col-md-2">
            <select class="form-select" @bind="estadoFiltro" @bind:after="Filtrar">
                <option value="">Todos los estados</option>
                <option value="Programada">Programada</option>
                <option value="Realizada">Realizada</option>
                <option value="Cancelada">Cancelada</option>
            </select>
        </div>
        <div class="col-md-2">
            <input type="date" class="form-control" @bind="fechaDesde" @bind:after="Filtrar" placeholder="Desde" />
        </div>
        <div class="col-md-2">
            <input type="date" class="form-control" @bind="fechaHasta" @bind:after="Filtrar" placeholder="Hasta" />
        </div>
        <div class="col-md-2">
            <button class="btn btn-outline-secondary w-100" @onclick="LimpiarFiltros">
                <i class="bi bi-x-circle"></i> Limpiar
            </button>
        </div>
    </div>
</div>
```

- [ ] **Step 2: Reemplazar el empty state condicional**

Cambiar la línea 33 (`else if (!visitas.Any())`) por:

```razor
else if (!visitas.Any())
{
    <div class="alert alert-info">No hay visitas programadas.</div>
}
else if (!visitasFiltradas.Any())
{
    <div class="alert alert-info">No se encontraron visitas con esos filtros.</div>
}
```

- [ ] **Step 3: Cambiar el `foreach` para usar `visitasFiltradas`**

En línea 52, cambiar:
```razor
@foreach (var visita in visitas)
```
a:
```razor
@foreach (var visita in visitasFiltradas)
```

- [ ] **Step 4: Agregar nuevas variables y métodos en `@code`**

Reemplazar el bloque `@code` completo (líneas 108-183) con:

```razor
@code {
    private List<VisitaDTO> visitas = new();
    private List<VisitaDTO> visitasFiltradas = new();
    private string textoBusqueda = string.Empty;
    private string estadoFiltro = string.Empty;
    private DateTime? fechaDesde;
    private DateTime? fechaHasta;
    private bool cargando = true;
    private string? mensajeError;
    private bool mostrarModalConfirmacion = false;
    private int visitaIdAEliminar;

    protected override async Task OnInitializedAsync()
    {
        await CargarVisitas();
    }

    private async Task CargarVisitas()
    {
        cargando = true;
        mensajeError = null;
        try
        {
            visitas = await ServicioVisitas.Lista();
            AplicarFiltros();
        }
        catch (Exception ex)
        {
            mensajeError = "No se pudo cargar la lista de visitas: " + ex.Message;
        }
        finally
        {
            cargando = false;
        }
    }

    private void Filtrar()
    {
        AplicarFiltros();
    }

    private void AplicarFiltros()
    {
        var resultado = visitas.AsEnumerable();

        if (!string.IsNullOrEmpty(estadoFiltro))
            resultado = resultado.Where(v => v.Estado == estadoFiltro);

        if (fechaDesde.HasValue)
            resultado = resultado.Where(v => v.FechaVisita >= fechaDesde.Value);

        if (fechaHasta.HasValue)
            resultado = resultado.Where(v => v.FechaVisita <= fechaHasta.Value.AddDays(1));

        if (!string.IsNullOrWhiteSpace(textoBusqueda))
        {
            var texto = textoBusqueda.ToLower();
            resultado = resultado.Where(v =>
                (v.IdClienteNavigation?.Nombre?.ToLower().Contains(texto) ?? false) ||
                (v.IdClienteNavigation?.Apellido?.ToLower().Contains(texto) ?? false) ||
                (v.IdPropiedadNavigation?.Titulo?.ToLower().Contains(texto) ?? false) ||
                (v.Comentarios?.ToLower().Contains(texto) ?? false));
        }

        visitasFiltradas = resultado.ToList();
    }

    private void LimpiarFiltros()
    {
        textoBusqueda = string.Empty;
        estadoFiltro = string.Empty;
        fechaDesde = null;
        fechaHasta = null;
        visitasFiltradas = visitas;
    }

    private void IrARegistrar()
    {
        NavigationManager.NavigateTo("/visitas/registrar");
    }

    private void IrAEditar(int id)
    {
        NavigationManager.NavigateTo($"/visitas/registrar/{id}");
    }

    private void MostrarConfirmacion(int id)
    {
        visitaIdAEliminar = id;
        mostrarModalConfirmacion = true;
    }

    private void CancelarEliminacion()
    {
        mostrarModalConfirmacion = false;
    }

    private async Task EliminarVisita()
    {
        mostrarModalConfirmacion = false;
        try
        {
            await ServicioVisitas.Eliminar(visitaIdAEliminar);
            await CargarVisitas();
        }
        catch (Exception ex)
        {
            mensajeError = "No se pudo eliminar: " + ex.Message;
        }
    }

    private string ObtenerClaseEstado(string estado)
    {
        return estado switch
        {
            "Programada" => "bg-warning text-dark",
            "Realizada" => "bg-success",
            "Cancelada" => "bg-danger",
            _ => "bg-secondary"
        };
    }
}
```

- [ ] **Step 5: Compilar y verificar**

```bash
dotnet build Blazor.Client/Blazor.Client.csproj
```

Expected: Build succeeds with no errors.

- [ ] **Step 6: Commit**

```bash
git add Blazor.Client/Pages/Visitas/ListarVisitas.razor docs/superpowers/plans/2026-07-30-busqueda-visitas-frontend.md docs/superpowers/specs/2026-07-30-busqueda-visitas-frontend-design.md
git commit -m "feat: add search and filters to visit list page"
```
