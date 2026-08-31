using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;

namespace mvc.Controllers;

public class InmuebleController : Controller
{
    private readonly IRepositorioInmueble repoInmueble;
    private readonly IRepositorioPropietario repoProp;
    private readonly IRepositorioTipoInmueble repoTipo;

    public InmuebleController(IRepositorioInmueble repoInmueble, IRepositorioPropietario repoProp,
        IRepositorioTipoInmueble repoTipo)
    {
        this.repoInmueble = repoInmueble;
        this.repoProp = repoProp;
        this.repoTipo = repoTipo;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var lista = repoInmueble.GetAll();
        return View(lista);
    }


    //crear
    [HttpGet]
    public IActionResult Crear()
    {
        CargarForeignInmueble();
        return View();
    }

    [HttpPost]
    public IActionResult Crear(Inmueble inmueble)
    {
        if (!ModelState.IsValid)
        {
            CargarForeignInmueble();
            return View(inmueble);
        }
        repoInmueble.Alta(inmueble);
        return RedirectToAction("Index");

    }

    //eliminar

    [HttpGet]
    public IActionResult Eliminar(int id)
    {   
        var inmueble = (repoInmueble.GetAll()).FirstOrDefault(i=>i.PropietarioId==id);
        if (inmueble == null)
        {
            return NotFound();
        }
        return View(inmueble);
    }

    [HttpPost, ActionName("Eliminar")]
    public IActionResult EliminarConfirmado (int id)
    {
        repoInmueble.Baja(id);
        return RedirectToAction("Index");
    }
    //modificar
    [HttpGet]
    public IActionResult Editar(int id)
    {
        var inmueble = (repoInmueble.GetAll()).FirstOrDefault(i=>i.PropietarioId==id);
        if (inmueble == null)
        {
            return NotFound();
        }
        return View(inmueble);
    }
    [HttpPost]
    public IActionResult Editar(Inmueble inmueble)
    {
        if(!ModelState.IsValid)
        {
            CargarForeignInmueble();
            return View(inmueble);
        }

        repoInmueble.Modificacion(inmueble);
        return RedirectToAction("Index");
    }

    //viewbag

    public void CargarForeignInmueble()
    {
        ViewBag.Props = repoProp.GetAll();
        ViewBag.Tipos = repoTipo.GetAll();
    }

}