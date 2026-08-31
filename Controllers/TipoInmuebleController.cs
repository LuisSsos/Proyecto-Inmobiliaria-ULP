using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;

namespace MVC.Controllers;

public class TipoInmuebleController : Controller
{
    private readonly IRepositorioTipoInmueble repo;

    public TipoInmuebleController(IRepositorioTipoInmueble repositorio)
    {
        repo=repositorio;
    }

    public IActionResult Index()
    {
        var lista = repo.GetAll();
        return View(lista);
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
        repo.Alta(ti);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        /*
        TipoInmueble? tipoEncontrado = null;
        var lista = repo.GetAll();

        foreach (var t in lista)
        {
            if (t.IdTipoInmueble == id)
            {
                tipoEncontrado = ti;
                break;
            }
        }

        if (tipoEncontrado == null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(tipoEncontrado);*/
        var tipoEncontrado = repo.GetAll().FirstOrDefault(t => t.IdTipoInmueble == id);

        if (tipoEncontrado == null)
        {
            return NotFound(); 
        }

        return View(tipoEncontrado);
    }    

    [HttpPost]
    public IActionResult Editar(TipoInmueble ti)
    {
        if(!ModelState.IsValid)
        {
            return View(ti);
        }
        repo.Modificacion(ti);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Eliminar(int id)
    {
        TipoInmueble? tipoEncontrado = (repo.GetAll()).FirstOrDefault(t=>t.IdTipoInmueble==id);
         /*
        var lista = repo.GetAll();
        foreach (var t in lista)
        {
            if (t.IdTipoInmueble == id)
            {
                tipoEncontrado = t;
                break;
            }
        }

        */
        if (tipoEncontrado == null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(tipoEncontrado);
    }

    [HttpPost]
    public IActionResult Eliminar(TipoInmueble ti)
    {
        repo.Baja(ti.IdTipoInmueble);
        return RedirectToAction(nameof(Index));
    }

}