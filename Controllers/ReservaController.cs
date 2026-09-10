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


    public IActionResult ProbarError()
    {
        throw new Exception("Esto es una prueba del middleware de excepciones.");
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

        if (repositorioReserva.ExisteSolapamiento(reserva.inmueble_id, reserva.fecha_desde, reserva.fecha_hasta))
        {
            ModelState.AddModelError("", "Ya existe otra reserva para este inmueble en esas fechas.");
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

        if (repositorioReserva.ExisteSolapamiento(reserva.inmueble_id, reserva.fecha_desde, reserva.fecha_hasta, reserva.id_reserva))
        {
            ModelState.AddModelError("", "Ya existe otra reserva para este inmueble en esas fechas.");
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
    [HttpPost, ActionName("Eliminar")]
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

    [HttpPost, ActionName("Cancelar")]
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

    [HttpGet]
    public IActionResult TerminarAnticipado(int id)
    {
        var reserva = repositorioReserva.ObtenerPorId(id);
        if (reserva == null) return NotFound();

        reserva.fecha_fin_real = DateTime.Today;
        return View(reserva);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult TerminarAnticipado(int id_reserva, DateTime fecha_fin_real, decimal multa)
    {
        var reserva = repositorioReserva.ObtenerPorId(id_reserva);
        if (reserva == null) return NotFound();

        if (fecha_fin_real < reserva.fecha_desde || fecha_fin_real > reserva.fecha_hasta)
        {
            ModelState.AddModelError("fecha_fin_real", "La fecha de terminación debe estar dentro del rango de la reserva.");
            return View(reserva);
        }

        reserva.fecha_fin_real = fecha_fin_real;
        reserva.multa = multa;
        reserva.estado = "Finalizada Anticipadamente";

        repositorioReserva.Modificar(reserva);
        TempData["Success"] = "La reserva fue terminada anticipadamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Renovar(int id)
    {
        var reserva = repositorioReserva.ObtenerPorId(id);
        if (reserva == null) return NotFound();

        var nuevaReserva = new Reserva
        {
            inmueble_id = reserva.inmueble_id,
            inquilino_id = reserva.inquilino_id,
            fecha_desde = reserva.fecha_hasta.AddDays(1),
            fecha_hasta = reserva.fecha_hasta.AddMonths(1),
            monto_por_dia = reserva.monto_por_dia,
            estado = "Pendiente"
        };

        ViewBag.Inmueble = repositorioInmueble.GetById(reserva.inmueble_id);
        ViewBag.Inquilinos = repositorioInquilino.ObtenerTodos();

        return View("Crear", nuevaReserva);
    }
}