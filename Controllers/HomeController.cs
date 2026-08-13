using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers;

public class HomeController : Controller
{
    // hac  er la db
    //hacer un controlador por cada entidad
    //hacer el index de propietario: devolver la lista de props
    //despues del model de propietario hacer el repositorio de propietario
    //instalar el nag  de sql ---> hacer build para los cambios 

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
