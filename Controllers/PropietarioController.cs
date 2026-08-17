using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;

namespace MVC.Controllers;

public class PropietarioController : Controller
{
    public IActionResult Index()
    {
        var repo = new RepositorioPropietario();
        var lista = repo.GetAll();

        return View(lista);
    }
}