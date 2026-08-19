using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;

namespace MVC.Controllers;

public class PropietarioController : Controller
{
    private readonly IRepositorioPropietario repositorio;

    public PropietarioController(RepositorioPropietario repositorio)
    {
        this.repositorio = repositorio;
    }

    public IActionResult Index()
    {
        var lista = repositorio.GetAll();
        return View(lista);
    }

    // met para el alta del repo
    public IActionResult Crear(Propietario propietario)
    {
        //el Request lo hereda de la clase Controller
        //es una propiedad de ASP.net core que representa la petición http que hace el navegador del user
        if (Request.Method == "POST")
        {
            repositorio.Alta(propietario);
            //el redirecttoaction tambien es un metodo heredado de controller, envia una instruiacción al navegador del user para que
            //haga una nueva petición a la acción indicada (index en este caso)
            return RedirectToAction("Index");
        }

        return View();
    }

    // formulario p/editar y guarda los cambios con el metodo de modificacion del repo y luego getall para mostrar los cambios
    public IActionResult Editar(int id, Propietario propietario)
    {
        if (Request.Method == "POST")
        {
            repositorio.Modificacion(propietario);
            return RedirectToAction("Index");
        }

        Propietario propietarioEncontrado = null;
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
            return RedirectToAction("Index");
        }

        return View(propietarioEncontrado);
    }

    // form para confirmación de eliminación y realiza la baja
    public IActionResult Eliminar(int id)
    {
        if (Request.Method == "POST")
        {
            repositorio.Baja(id);
            return RedirectToAction("Index");
        }

        Propietario propietarioEncontrado = null;
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
            return RedirectToAction("Index");
        }

        return View(propietarioEncontrado);
    }
}