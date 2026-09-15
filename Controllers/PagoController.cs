using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;
using System.Security.Claims;

namespace MVC.Controllers;

[Authorize]
public class PagoController : Controller
{
    private readonly IRepositorioPago repositorioPago;
    private readonly IRepositorioReserva repositorioReserva;
    private readonly IRepositorioInquilino repositorioInquilino;

    public PagoController(
        IRepositorioPago repositorioPago,
        IRepositorioReserva repositorioReserva,
        IRepositorioInquilino repositorioInquilino)
    {
        this.repositorioPago = repositorioPago;
        this.repositorioReserva = repositorioReserva;
        this.repositorioInquilino = repositorioInquilino;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var pagos = repositorioPago.ObtenerTodos();

        return View(pagos);
    }


    [HttpGet]
    public IActionResult Crear()
    {
        ViewBag.Reservas = repositorioReserva.ObtenerTodos();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(Pago pago)
    {
        if (pago.importe < 20000 || pago.importe > 999999)
            ModelState.AddModelError(nameof(pago.importe), "El importe debe estar entre $20.000 y $999.999.");

        if (pago.ReservaId <= 0)
            ModelState.AddModelError(nameof(pago.ReservaId), "Debe seleccionar una reserva.");

        if (string.IsNullOrWhiteSpace(pago.concepto))
            ModelState.AddModelError(nameof(pago.concepto), "Debe seleccionar un concepto.");

        if (!ModelState.IsValid)
        {
            ViewBag.Reservas = repositorioReserva.ObtenerTodos();
            return View(pago);
        }

        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(usuarioId))
            return Unauthorized();

        pago.usuario_creador_id = int.Parse(usuarioId);
        pago.estado = "Pagado";
        pago.Activo = true;

        repositorioPago.Crear(pago);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Anular(int id)
    {
        var pago = repositorioPago.ObtenerPorId(id);

        if (pago == null)
            return NotFound();

        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(usuarioId))
            return Unauthorized();

        pago.usuario_anulador_id = int.Parse(usuarioId);
        pago.estado = "Anulado";
        pago.Activo = false;

        repositorioPago.Modificar(pago);

        return RedirectToAction(nameof(Index));
    }
}