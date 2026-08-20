using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;

namespace MVC.Controllers;

public class InquilinoController : Controller
{
    private readonly RepositorioInquilino repositorio;

    public InquilinoController(RepositorioInquilino repositorio)
    {
        this.repositorio = repositorio;
    }

    public IActionResult Index()
    {
        var lista = repositorio.ObtenerTodos();
        return View(lista);
    }

    // GET: muestra el formulario vacío
    public IActionResult Create()
    {
        return View();
    }

    // POST: recibe el formulario y guarda
    [HttpPost]
    public IActionResult Create(Inquilino inquilino)
    {
        repositorio.Crear(inquilino);
        return RedirectToAction("Index");
    }

    // GET: muestra el formulario con los datos actuales
    public IActionResult Edit(int id)
    {
        var inquilino = repositorio.ObtenerPorId(id);
        if (inquilino == null)
        {
            return NotFound();
        }
        return View(inquilino);
    }

    // POST: recibe el formulario editado y actualiza
    [HttpPost]
    public IActionResult Edit(Inquilino inquilino)
    {
        repositorio.Modificar(inquilino);
        return RedirectToAction("Index");
    }

    // GET: muestra pantalla de confirmación
    public IActionResult Eliminar(int id)
    {
        var inquilino = repositorio.ObtenerPorId(id);
        if (inquilino == null)
        {
            return NotFound();
        }
        return View(inquilino);
    }

    // POST: elimina de verdad
    [HttpPost, ActionName("Eliminar")]
    public IActionResult EliminarConfirmado(int id)
    {
        repositorio.Eliminar(id);
        return RedirectToAction("Index");
    }
}