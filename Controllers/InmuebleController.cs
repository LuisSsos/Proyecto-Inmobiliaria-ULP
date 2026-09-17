using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;
using Microsoft.AspNetCore.Authorization;
namespace mvc.Controllers;

[Authorize]
public class InmuebleController : Controller
{
    private readonly IRepositorioInmueble repoInmueble;
    private readonly IRepositorioPropietario repoProp;
    private readonly IRepositorioTipoInmueble repoTipo;
    private readonly IRepositorioImagenInmueble repoImagen;
    private readonly IRepositorioReserva repoReserva;
    public InmuebleController(
        IRepositorioInmueble repoInmueble,
        IRepositorioPropietario repoProp,
        IRepositorioTipoInmueble repoTipo,
        IRepositorioImagenInmueble repoImagen,
         IRepositorioReserva repoReserva)
    {
        this.repoInmueble = repoInmueble;
        this.repoProp = repoProp;
        this.repoTipo = repoTipo;
        this.repoImagen = repoImagen;
        this.repoReserva = repoReserva;
    }

    // LISTAR
    [HttpGet]
[HttpGet]
public IActionResult Index(
    int pagina = 1,
    string? estado = null,
    int? propietarioId = null)
{
    try
    {
        int cantidadPorPagina = 10;

        if (pagina < 1)
        {
            pagina = 1;
        }

        var lista = repoInmueble.ObtenerPaginado(
            pagina,
            cantidadPorPagina,
            estado,
            propietarioId);

        int totalInmuebles =
            repoInmueble.ContarInmuebles(
                estado,
                propietarioId);

        int totalPaginas = (int)Math.Ceiling(
            totalInmuebles / (double)cantidadPorPagina);

        var imagenesPorInmueble =
            new Dictionary<int, ImagenInmueble>();

        foreach (var inmueble in lista)
        {
            var imagenes =
                repoImagen.ObtenerPorInmueble(
                    inmueble.IdInmueble);

            var portada =
                imagenes.FirstOrDefault(
                    img => img.esPortada)
                ?? imagenes.FirstOrDefault();

            if (portada != null)
            {
                imagenesPorInmueble[
                    inmueble.IdInmueble] = portada;
            }
        }

        ViewBag.ImagenesPorInmueble =
            imagenesPorInmueble;

        ViewBag.PaginaActual =
            pagina;

        ViewBag.TotalPaginas =
            totalPaginas;

        ViewBag.CantidadPorPagina =
            cantidadPorPagina;

        ViewBag.EstadoActual =
            estado;

        ViewBag.PropietarioActual =
            propietarioId;

        return View(lista);
    }
    catch (Exception)
    {
        return StatusCode(
            500,
            "Ocurrió un error al obtener los inmuebles.");
    }
}

    // CREAR - GET
    [HttpGet]
    public IActionResult Crear()
    {
        try
        {
            CargarForeignInmueble();
            return View();
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocurrió un error al cargar el formulario de creación.");
        }
    }

    // CREAR - POST
    [HttpPost]
    public IActionResult Crear(Inmueble inmueble)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                CargarForeignInmueble();
                return View(inmueble);
            }

            repoInmueble.Alta(inmueble);

            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            CargarForeignInmueble();
            ModelState.AddModelError("", "Ocurrió un error al crear el inmueble.");
            return View(inmueble);
        }
    }

    // ELIMINAR - GET
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public IActionResult Eliminar(int id)
    {
        try
        {
            var inmueble = repoInmueble
                .GetAll()
                .FirstOrDefault(i => i.IdInmueble == id);

            if (inmueble == null)
            {
                return NotFound();
            }

            if (repoReserva.TieneReservasAsociadas(id))
            {
                TempData["Error"] = "No se puede dar de baja el inmueble porque tiene reservas registradas o activas.";
                return RedirectToAction(nameof(Index));
            }

            return View(inmueble);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocurrió un error al obtener el inmueble.");
        }
    }

    // ELIMINAR - POST
    [HttpPost, ActionName("Eliminar")]
    [Authorize(Roles = "Administrador")]
    public IActionResult EliminarConfirmado(int id)
    {
        try
        {
            if (repoReserva.TieneReservasAsociadas(id))
            {
                TempData["Error"] = "No se puede dar de baja el inmueble porque tiene reservas asociadas.";
                return RedirectToAction(nameof(Index));
            }

            repoInmueble.Baja(id); // O tu método de baja lógica
            TempData["Exito"] = "El inmueble ha sido dado de baja correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            TempData["Error"] = "Ocurrió un error al eliminar el inmueble.";

            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult CambiarEstado(int id)
    {
        try
        {
            var inmueble = repoInmueble.GetById(id);

            if (inmueble == null)
            {
                return NotFound();
            }

            // alterna entre suspendido y disponible
            var nuevoEstado = inmueble.Estado == "Suspendido" ? "Disponible" : "Suspendido";
            repoInmueble.CambiarEstado(id, nuevoEstado);

            TempData["Exito"] = nuevoEstado == "Suspendido"
                ? "El inmueble fue suspendido y ya no aparece en los listados para alquilar."
                : "El inmueble volvió a estar disponible.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            TempData["Error"] = "Ocurrió un error al cambiar el estado del inmueble.";
            return RedirectToAction(nameof(Index));
        }
    }

    // MODIFICAR - GET
    [HttpGet]
    public IActionResult Editar(int id)
    {
        try
        {
            var inmueble = repoInmueble
                .GetAll()
                .FirstOrDefault(i => i.IdInmueble == id);

            if (inmueble == null)
            {
                return NotFound();
            }

            CargarForeignInmueble();

            return View(inmueble);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocurrió un error al obtener el inmueble para editar.");
        }
    }

    // MODIFICAR - POST
    [HttpPost]
    public IActionResult Editar(Inmueble inmueble)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                CargarForeignInmueble();
                return View(inmueble);
            }

            repoInmueble.Modificacion(inmueble);

            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            CargarForeignInmueble();
            ModelState.AddModelError("", "Ocurrió un error al modificar el inmueble.");
            return View(inmueble);
        }
    }

    // VIEWBAG
    public void CargarForeignInmueble()
    {
        try
        {
            ViewBag.Props = repoProp.GetAll();
            ViewBag.Tipos = repoTipo.GetAll();
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet]
    public IActionResult BuscarPropietarios(string texto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(texto) || texto.Length < 2)
            {
                return Json(new List<object>());
            }

            var propietarios = repoProp.Buscar(texto);

            var resultado = propietarios.Select(p => new
            {
                id = p.IdPropietario,
                nombre = p.Nombre,
                dniCuit = p.DniCuit
            });

            return Json(resultado);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocurrió un error al buscar propietarios.");
        }
    }
}