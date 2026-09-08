using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;

namespace mvc.Controllers;

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
    public IActionResult Index()
    {
        try
        {
            var lista = repoInmueble.GetAll();

            // busca si tiene una foto para mostrar en el listado
            var imagenesPorInmueble = new Dictionary<int, ImagenInmueble>();
            foreach (var inmueble in lista)
            {
                var imagenes = repoImagen.ObtenerPorInmueble(inmueble.IdInmueble);
                var portada = imagenes.FirstOrDefault(img => img.esPortada) ?? imagenes.FirstOrDefault();
                if (portada != null)
                {
                    imagenesPorInmueble[inmueble.IdInmueble] = portada;
                }
            }
            ViewBag.ImagenesPorInmueble = imagenesPorInmueble;

            return View(lista);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocurrió un error al obtener los inmuebles.");
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
}