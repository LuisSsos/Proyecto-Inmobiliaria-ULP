using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;

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
        var lista = repositorio.ObtenerTodos();
        return View(lista);
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

        if (repositorio.ExisteDni(inquilino.dni))
        {
            ModelState.AddModelError("dni", "Ya existe un inquilino registrado con ese DNI.");
            return View(inquilino);
        }

        repositorio.Crear(inquilino);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var inquilino = repositorio.ObtenerPorId(id);
        if (inquilino == null)
        {
            return NotFound();
        }
        return View(inquilino);
    }

    [HttpPost]
    public IActionResult Edit(Inquilino inquilino)
    {
        if (!ModelState.IsValid)
        {
            return View(inquilino);
        }

        if (repositorio.ExisteDni(inquilino.dni, inquilino.id_inquilino))
        {
            ModelState.AddModelError("dni", "Ya existe otro inquilino registrado con ese DNI.");
            return View(inquilino);
        }

        repositorio.Modificar(inquilino);
        return RedirectToAction("Index");
    }

    public IActionResult Eliminar(int id)
    {
        var inquilino = repositorio.ObtenerPorId(id);
        if (inquilino == null)
        {
            return NotFound();
        }
        return View(inquilino);
    }

    [HttpPost, ActionName("Eliminar")]
    public IActionResult EliminarConfirmado(int id)
    {
        repositorio.Eliminar(id);
        return RedirectToAction("Index");
    }
}