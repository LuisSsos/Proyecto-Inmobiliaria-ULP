using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;
using MySqlConnector;
namespace MVC.Controllers;

public class TipoInmuebleController : Controller
{
    private readonly IRepositorioTipoInmueble repo;

    public TipoInmuebleController(IRepositorioTipoInmueble repositorio)
    {
        repo = repositorio;
    }

    public IActionResult Index()
    {
        try
        {
            var lista = repo.GetAll();
            return View(lista);
        }
        catch (MySqlException ex)
        {
            TempData["Error"] = $"Error de conexión al consultar tipos de inmueble (Código {ex.Number}).";
            return View(new List<TipoInmueble>());
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Ocurrió un error al cargar el listado: {ex.Message}";
            return View(new List<TipoInmueble>());
        }
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(TipoInmueble ti)
    {
        if (!ModelState.IsValid)
        {
            return View(ti);
        }
        try
        {
            bool existeNombre = repo.GetAll().Any(t => t.Nombre.Equals(ti.Nombre, StringComparison.OrdinalIgnoreCase));
            if (existeNombre)
            {
                ModelState.AddModelError("Nombre", "Ya existe un tipo de inmueble registrado con este nombre.");
                return View(ti);
            }

            repo.Alta(ti);
            TempData["Success"] = "Nuevo tipo de inmueble guardado con éxito.";
            return RedirectToAction(nameof(Index));
        }
        catch (MySqlException ex)
        {
            ModelState.AddModelError(string.Empty, $"Error de base de datos ({ex.Number}): {ex.Message}");
            return View(ti);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Falla al guardar: {ex.Message}");
            return View(ti);
        }
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        try
        {
            var tipo = repo.GetAll().FirstOrDefault(t => t.IdTipoInmueble == id);
            if (tipo == null)
            {
                TempData["Error"] = "No se encontró el tipo de inmueble seleccionado.";
                return RedirectToAction(nameof(Index));
            }

            return View(tipo);
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al buscar la categoría: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(TipoInmueble ti)
    {
        if (!ModelState.IsValid)
        {
            return View(ti);
        }
        try
        {
            bool existeNombre = repo.GetAll().Any(t => t.Nombre.Equals(ti.Nombre, StringComparison.OrdinalIgnoreCase) && t.IdTipoInmueble != ti.IdTipoInmueble);
            if (existeNombre)
            {
                ModelState.AddModelError("Nombre", "Ya existe otro tipo de inmueble registrado con este mismo nombre.");
                return View(ti);
            }

            repo.Modificacion(ti);
            TempData["Success"] = "Tipo de inmueble actualizado con éxito.";
            return RedirectToAction(nameof(Index));
        }
        catch (MySqlException ex)
        {
            ModelState.AddModelError(string.Empty, $"Error de base de datos al actualizar ({ex.Number}): {ex.Message}");
            return View(ti);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al modificar: {ex.Message}");
            return View(ti);
        }
    }

    [HttpGet]
    public IActionResult Eliminar(int id)
    {
        try
        {
            var tipo = repo.GetAll().FirstOrDefault(t => t.IdTipoInmueble == id);
            if (tipo == null)
            {
                TempData["Error"] = "El registro no existe o ya fue eliminado.";
                return RedirectToAction(nameof(Index));
            }

            return View(tipo);
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al obtener datos: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public IActionResult Eliminar(TipoInmueble ti)
    {
        try
        {
            repo.Baja(ti.IdTipoInmueble);
            TempData["Success"] = "Tipo de inmueble eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (MySqlException ex) when (ex.Number == 1451)
        {
            TempData["Error"] = "No se puede eliminar el tipo de inmueble porque existen propiedades vinculadas a esta categoría.";
            return RedirectToAction(nameof(Index));
        }
        catch (MySqlException ex)
        {
            TempData["Error"] = $"Error MySQL ({ex.Number}): No se pudo eliminar la categoría.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error inesperado al borrar: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }

    }
}