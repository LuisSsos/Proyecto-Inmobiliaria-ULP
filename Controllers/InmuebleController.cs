using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;

namespace mvc.Controllers;

public class InmuebleController : Controller
{
    private readonly IRepositorioInmueble repoInmueble;
    private readonly IRepositorioPropietario repoProp;
    private readonly IRepositorioTipoInmueble repoTipo;

    public InmuebleController(
        IRepositorioInmueble repoInmueble,
        IRepositorioPropietario repoProp,
        IRepositorioTipoInmueble repoTipo)
    {
        this.repoInmueble = repoInmueble;
        this.repoProp = repoProp;
        this.repoTipo = repoTipo;
    }

    // LISTAR
    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            var lista = repoInmueble.GetAll();
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
            repoInmueble.Baja(id);

            return RedirectToAction("Index");
        }
        catch (MySqlConnector.MySqlException)
        {
            TempData["Error"] = "No puedes eliminar un inmueble que posea una reserva.";

            return RedirectToAction("Index");
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