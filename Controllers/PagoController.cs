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
    public IActionResult Index(int reservaId)
    {
        var pagos = repositorioPago.ObtenerTodos()
            .Where(p => p.ReservaId == reservaId)
            .ToList();

        ViewBag.ReservaId = reservaId;

        return View(pagos);
    }

    [HttpGet]
    public IActionResult Crear(int reservaId)
    {
        var pago = new Pago
        {
            ReservaId = reservaId,
            fecha_pago = DateTime.Today
        };

        return View(pago);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(Pago pago)
    {
        if (pago.importe < 20000 || pago.importe > 999999)
            ModelState.AddModelError(nameof(pago.importe), "El importe debe estar entre $20.000 y $999.999.");

        if (pago.fecha_pago < DateTime.Today)
            ModelState.AddModelError(nameof(pago.fecha_pago), "La fecha de pago no puede ser anterior a hoy.");

        if (pago.ReservaId <= 0)
            ModelState.AddModelError(nameof(pago.ReservaId), "Debe seleccionar una reserva.");

        if (string.IsNullOrWhiteSpace(pago.concepto))
            ModelState.AddModelError(nameof(pago.concepto), "Debe seleccionar un concepto.");

        if (!ModelState.IsValid)
            return View(pago);

        var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(usuarioId))
            return Unauthorized();

        pago.usuario_creador_id = int.Parse(usuarioId);
        pago.estado = "Pagado";
        pago.Activo = true;

        repositorioPago.Crear(pago);

        return RedirectToAction(nameof(Index), new { reservaId = pago.ReservaId });
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

        return RedirectToAction(nameof(Index), new { reservaId = pago.ReservaId });
    }
}