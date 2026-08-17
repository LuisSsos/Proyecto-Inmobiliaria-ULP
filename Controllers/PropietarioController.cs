using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;

namespace MVC.Controllers;

public class PropietarioController : Controller
{
    private readonly RepositorioPropietario repositorio;

    public PropietarioController(RepositorioPropietario repositorio)
    {
        this.repositorio = repositorio;
    }

    public IActionResult Index()
    {
        var lista = repositorio.GetAll();
        return View(lista);
    }
}