using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;
using MySqlConnector;

namespace MVC.Controllers;

public class PropietarioController : Controller
{
    private readonly IRepositorioPropietario repositorio;

    public PropietarioController(IRepositorioPropietario repositorio)
    {
        this.repositorio = repositorio;
    }

    public IActionResult Index()
    {
        try
        {
            var lista = repositorio.GetAll();
            return View(lista);
        }
        catch (MySqlException ex)
        {
            TempData["Error"] = $"Error de conexión con la base de datos MySQL (Código: {ex.Number}).";
            return View(new List<Propietario>());
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al recuperar propietarios: {ex.Message}";
            return View(new List<Propietario>());
        }
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(Propietario propietario)
    {
        if (!ModelState.IsValid)
        {
            return View(propietario);
        }
        try
        {
            if (repositorio.ExisteDniCuit(propietario.DniCuit))
            {
                ModelState.AddModelError("DniCuit", "El DNI/CUIT ingresado ya está registrado a nombre de otro propietario.");
                return View(propietario);
            }

            repositorio.Alta(propietario);
            TempData["Success"] = "Propietario registrado con éxito.";
            return RedirectToAction(nameof(Index));
        }
        catch (MySqlException ex) when (ex.Number == 1062)
        {
            ModelState.AddModelError(string.Empty, "Error: Ya existe un registro duplicado en el sistema.");
            return View(propietario);
        }
        catch (MySqlException ex)
        {
            ModelState.AddModelError(string.Empty, $"Error de base de datos ({ex.Number}): {ex.Message}");
            return View(propietario);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Ocurrió una falla inesperada: {ex.Message}");
            return View(propietario);
        }

    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        try
        {
            var propietario = repositorio.GetAll().FirstOrDefault(p => p.IdPropietario == id);
            if (propietario == null)
            {
                TempData["Error"] = $"No se encontró el propietario con el ID {id}.";
                return RedirectToAction(nameof(Index));
            }

            return View(propietario);
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Error al obtener datos: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
        ;
    }

    [HttpPost]
    public IActionResult Editar(Propietario propietario)
    {
        if (!ModelState.IsValid)
        {
            return View(propietario);
        }

        try
        {
            if (repositorio.ExisteDniCuit(propietario.DniCuit, propietario.IdPropietario))
            {
                ModelState.AddModelError("DniCuit", "El DNI/CUIT pertenece a otro propietario registrado.");
                return View(propietario);
            }

            repositorio.Modificacion(propietario);
            TempData["Success"] = "Datos del propietario actualizados correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (MySqlException ex)
        {
            ModelState.AddModelError(string.Empty, $"Error de base de datos al actualizar ({ex.Number}): {ex.Message}");
            return View(propietario);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Ocurrió un error inesperado al editar: {ex.Message}");
            return View(propietario);
        }
    }

    [HttpGet]
    public IActionResult Eliminar(int id)
    {
        try
        {
            var propietario = repositorio.GetAll()
                .FirstOrDefault(p => p.IdPropietario == id);

            if (propietario == null)
            {
                TempData["Error"] = "No se encontró el propietario solicitado.";
                return RedirectToAction(nameof(Index));
            }

            return View(propietario);
        }
        catch (MySqlException ex)
        {
            TempData["Error"] = $"Error al obtener el propietario (MySQL {ex.Number}).";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            TempData["Error"] = "Ocurrió un error al obtener el propietario.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost, ActionName("Eliminar")]
    [ValidateAntiForgeryToken]
    public IActionResult EliminarConfirmado(int id)
    {
        try
        {
            repositorio.Baja(id);

            TempData["Success"] = "El propietario fue eliminado correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (MySqlException ex) when (ex.Number == 1451)
        {
            TempData["Error"] = "No puedes eliminar un propietario que tenga inmuebles asociados.";

            return RedirectToAction(nameof(Index));
        }
        catch (MySqlException ex)
        {
            TempData["Error"] = $"Error MySQL ({ex.Number}): No se pudo completar la eliminación.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            TempData["Error"] = "Ocurrió un error inesperado al eliminar el propietario.";

            return RedirectToAction(nameof(Index));
        }
    }
}
