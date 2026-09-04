using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;
using MySqlConnector;


namespace MVC.Controllers;

public class InquilinoController : Controller
{
    private readonly IRepositorioInquilino repositorio;

    public InquilinoController(IRepositorioInquilino repositorio)
    {
        this.repositorio = repositorio;
    }

    public IActionResult Index()
    {
        try
        {
            var lista = repositorio.ObtenerTodos();
            return View(lista);
        }
        catch (Exception)
        {
            return StatusCode(500, "Ocurrió un error al obtener los inquilinos.");
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Inquilino inquilino)
    {
        if (!ModelState.IsValid)
        {
            return View(inquilino);
        }

        try
        {
            if (repositorio.ExisteDni(inquilino.dni))
            {
                ModelState.AddModelError(
                    "dni",
                    "Ya existe un inquilino registrado con ese DNI."
                );

                return View(inquilino);
            }

            repositorio.Crear(inquilino);

            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError(
                "",
                "Ocurrió un error al intentar crear el inquilino."
            );

            return View(inquilino);
        }
    }

    public IActionResult Edit(int id)
    {
        try
        {
            var inquilino = repositorio.ObtenerPorId(id);

            if (inquilino == null)
            {
                return NotFound();
            }

            return View(inquilino);
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                "Ocurrió un error al obtener el inquilino."
            );
        }
    }

    [HttpPost]
    public IActionResult Edit(Inquilino inquilino)
    {
        if (!ModelState.IsValid)
        {
            return View(inquilino);
        }

        try
        {
            if (repositorio.ExisteDni(
                inquilino.dni,
                inquilino.id_inquilino))
            {
                ModelState.AddModelError(
                    "dni",
                    "Ya existe otro inquilino registrado con ese DNI."
                );

                return View(inquilino);
            }

            repositorio.Modificar(inquilino);

            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            ModelState.AddModelError(
                "",
                "Ocurrió un error al intentar modificar el inquilino."
            );

            return View(inquilino);
        }
    }

    [HttpGet]
    public IActionResult Eliminar(int id)
    {
        try
        {
            var inquilino = repositorio.ObtenerPorId(id);

            if (inquilino == null)
            {
                return NotFound();
            }

            return View(inquilino);
        }
        catch (MySqlException ex)
        {
            TempData["Error"] = $"Error al obtener el inquilino (MySQL {ex.Number}).";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            TempData["Error"] = "Ocurrió un error al obtener el inquilino.";
            return RedirectToAction(nameof(Index));
        }
    }


    [HttpPost, ActionName("Eliminar")]
    public IActionResult EliminarConfirmado(int id)
    {
        try
        {
            repositorio.Eliminar(id);

            TempData["Success"] = "El inquilino fue eliminado correctamente.";

            return RedirectToAction(nameof(Index));
        }
        catch (MySqlException ex) when (ex.Number == 1451)
        {
            TempData["Error"] = "No puedes eliminar un inquilino que tenga reservas asociadas.";

            return RedirectToAction(nameof(Index));
        }
        catch (MySqlException ex)
        {
            TempData["Error"] = $"Error MySQL ({ex.Number}): No se pudo completar la eliminación.";

            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            TempData["Error"] = "Ocurrió un error inesperado al eliminar el inquilino.";

            return RedirectToAction(nameof(Index));
        }
    }
}