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

    // met para el alta del repo
    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Crear(Propietario propietario)
    {
        repositorio.Alta(propietario);
        return RedirectToAction(nameof(Index));
    }

    // formulario p/editar y guarda los cambios con el metodo de modificacion del repo y luego getall para mostrar los cambios

    [HttpGet]
    public IActionResult Editar(int id, Propietario propietario)
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
        repositorio.Modificacion(propietario);
        return RedirectToAction(nameof(Index));
    }
    // form para confirmación de eliminación y realiza la baja
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

    [HttpPost]
    public IActionResult Eliminar(Propietario propietario)
    {
        repositorio.Baja(propietario.IdPropietario);
        return RedirectToAction(nameof(Index));
    }
}