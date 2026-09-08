using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace MVC.Controllers;

[Authorize]
public class UsuarioController : Controller
{
    private readonly IRepositorioUsuario repositorio;

    public UsuarioController(IRepositorioUsuario repositorio)
    {
        this.repositorio = repositorio;
    }

    public IActionResult Index()
    {
        var lista = repositorio.ObtenerTodos();
        return View(lista);
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        if (repositorio.ExisteEmail(usuario.email))
        {
            ModelState.AddModelError("email", "Ya existe un usuario registrado con ese email.");
            return View(usuario);
        }

        repositorio.Crear(usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        var usuario = repositorio.ObtenerPorId(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return View(usuario);
    }

    [HttpPost]
    public IActionResult Editar(Usuario usuario)
    {
        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        if (repositorio.ExisteEmail(usuario.email, usuario.id_usuario))
        {
            ModelState.AddModelError("email", "Ya existe otro usuario registrado con ese email.");
            return View(usuario);
        }

        repositorio.Modificar(usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public IActionResult Eliminar(int id)
    {
        var usuario = repositorio.ObtenerPorId(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return View(usuario);
    }

    [HttpPost, ActionName("Eliminar")]
    [Authorize(Roles = "Administrador")]
    public IActionResult EliminarConfirmado(int id)
    {
        repositorio.Eliminar(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult MiPerfil()
    {
        var idActual = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var usuario = repositorio.ObtenerPorId(idActual);
        if (usuario == null)
        {
            return NotFound();
        }
        return View(usuario);
    }

    [HttpPost]
    public IActionResult MiPerfil(Usuario usuario)
    {
        var idActual = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        usuario.id_usuario = idActual;

        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        repositorio.Modificar(usuario);
        return RedirectToAction("MiPerfil");
    }
}