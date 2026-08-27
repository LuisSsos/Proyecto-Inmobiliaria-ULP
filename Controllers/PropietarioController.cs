using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;

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
        var lista = repositorio.GetAll();
        return View(lista);
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

        if (repositorio.ExisteDniCuit(propietario.DniCuit))
        {
            ModelState.AddModelError("DniCuit", "Ya existe un propietario registrado con ese DNI/CUIT.");
            return View(propietario);
        }

        repositorio.Alta(propietario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        Propietario? propietarioEncontrado = null;
        var lista = repositorio.GetAll();

        foreach (var p in lista)
        {
            if (p.IdPropietario == id)
            {
                propietarioEncontrado = p;
                break;
            }
        }

        if (propietarioEncontrado == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(propietarioEncontrado);
    }

    [HttpPost]
    public IActionResult Editar(Propietario propietario)
    {
        if (!ModelState.IsValid)
        {
            return View(propietario);
        }

        if (repositorio.ExisteDniCuit(propietario.DniCuit, propietario.IdPropietario))
        {
            ModelState.AddModelError("DniCuit", "Ya existe otro propietario registrado con ese DNI/CUIT.");
            return View(propietario);
        }

        repositorio.Modificacion(propietario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Eliminar(int id)
    {
        Propietario? propietarioEncontrado = null;
        var lista = repositorio.GetAll();

        foreach (var p in lista)
        {
            if (p.IdPropietario == id)
            {
                propietarioEncontrado = p;
                break;
            }
        }

        if (propietarioEncontrado == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(propietarioEncontrado);
    }

    [HttpPost, ActionName("Eliminar")]
    public IActionResult EliminarConfirmado(int id)
    {
        repositorio.Baja(id);
        return RedirectToAction("Index");
    }
}