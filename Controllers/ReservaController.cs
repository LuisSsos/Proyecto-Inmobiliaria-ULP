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
        var inquilinos = repositorioInquilino.ObtenerTodos();
        var inmuebles = repositorioInmueble.GetAll();

        var modelo = reservas.Select(r => new ReservaIndexViewModel
        {
            Reserva = r,
            Inquilino = inquilinos.FirstOrDefault(i => i.id_inquilino == r.inquilino_id)!,
            Inmueble = inmuebles.FirstOrDefault(i => i.IdInmueble == r.inmueble_id)!
        }).ToList();

        return View(modelo);

    }



    // Alta get
    [HttpGet]
    public IActionResult Crear(int inmuebleId)
    {
        var inmueble = repositorioInmueble.GetAll()
            .FirstOrDefault(i => i.IdInmueble == inmuebleId);

        if (inmueble == null)
        {
            return NotFound();
        }

        var reserva = new Reserva
        {
            inmueble_id = inmuebleId,
            monto_por_dia = inmueble.PrecioPorDia,
            multa = 0,
            estado = "Pendiente",
            fecha_fin_real = null
        };

        ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
        ViewBag.Inmueble = inmueble;

        return View(reserva);
    }

    // Alta post
    [HttpPost]
    public IActionResult Crear(Reserva reserva)
    {
        var inmueble = repositorioInmueble.GetAll()
            .FirstOrDefault(i => i.IdInmueble == reserva.inmueble_id);

        if (inmueble == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();
            ViewBag.Inmueble = inmueble;
            return View(reserva);
        }

        reserva.monto_por_dia = inmueble.PrecioPorDia;
        reserva.estado = "Pendiente";
        reserva.fecha_fin_real = null;
        reserva.multa = 0;
        repositorioReserva.Crear(reserva);

        return RedirectToAction("Index", "Inmueble");
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

    [HttpGet]
    public IActionResult Cancelar(int id)
    {
        var reserva = repositorioReserva.ObtenerPorId(id);

        if (reserva == null)
            return NotFound();

        return View(reserva);
    }

    [HttpPost]
    public IActionResult CancelarConfirmado(int id)
    {
        var reserva = repositorioReserva.ObtenerPorId(id);

        if (reserva == null)
            return NotFound();

        DateTime fechaCancelacion = DateTime.Today;

        int diasRestantes = (reserva.fecha_hasta - fechaCancelacion).Days;

        if (diasRestantes < 0)
            diasRestantes = 0;

        decimal totalRestante = diasRestantes * reserva.monto_por_dia;

        reserva.multa = totalRestante * 0.50m;
        reserva.estado = "Cancelada";
        reserva.fecha_fin_real = fechaCancelacion;

        repositorioReserva.Modificar(reserva);

        return RedirectToAction(nameof(Index));
    }
}