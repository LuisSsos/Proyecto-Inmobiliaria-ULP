using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;

namespace MVC.Controllers;

public class ReservaController : Controller
{
    private readonly IRepositorioReserva repositorioReserva;
    private readonly IRepositorioInquilino repositorioInquilino;
    private readonly IRepositorioInmueble repositorioInmueble;

    public ReservaController(
        IRepositorioReserva repositorioReserva,
        IRepositorioInquilino repositorioInquilino,
        IRepositorioInmueble repositorioInmueble)
    {
        this.repositorioReserva = repositorioReserva;
        this.repositorioInquilino = repositorioInquilino;
        this.repositorioInmueble = repositorioInmueble;
    }


    // Listado
    public IActionResult Index()
    {
        var reservas = repositorioReserva.ObtenerTodos();

        return View(reservas);
    }


    // Alta get
    [HttpGet]
    public IActionResult Crear()
    {
        ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
        ViewBag.Inmuebles = repositorioInmueble.GetAll();

        return View();
    }


    // Alta post
    [HttpPost]
    public IActionResult Crear(Reserva reserva)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
            ViewBag.Inmuebles = repositorioInmueble.GetAll();

            return View(reserva);
        }

        repositorioReserva.Crear(reserva);

        return RedirectToAction(nameof(Index));
    }

     // Modificar get
    [HttpGet]
    public IActionResult Editar(int id)
    {
        var reserva = repositorioReserva.ObtenerPorId(id);

        if (reserva == null)
        {
            return NotFound();
        }

        ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
        ViewBag.Inmuebles = repositorioInmueble.GetAll();

        return View(reserva);
    }

    // Modificar post
    [HttpPost]
    public IActionResult Editar(Reserva reserva)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
            ViewBag.Inmuebles = repositorioInmueble.GetAll();

            return View(reserva);
        }

        repositorioReserva.Modificar(reserva);

        return RedirectToAction(nameof(Index));
    }

    // Eliminar get
    [HttpGet]
    public IActionResult Eliminar(int id)
    {
        var reserva = repositorioReserva.ObtenerPorId(id);

        if (reserva == null)
        {
            return NotFound();
        }

        return View(reserva);
    }

    // Eliminar post
    [HttpPost]
    public IActionResult EliminarConfirmado(int id)
    {
        repositorioReserva.Eliminar(id);

        return RedirectToAction(nameof(Index));
    }
}