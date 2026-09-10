using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MVC.Controllers;

[Authorize]
public class UsuarioController : Controller
{
    private readonly IRepositorioUsuario repositorio;

    public UsuarioController(IRepositorioUsuario repositorio)
    {
        this.repositorio = repositorio;
    }

    [Authorize(Roles = "Administrador")]
    public IActionResult Index()
    {
        var lista = repositorio.ObtenerTodos();
        return View(lista);
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
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
    [Authorize(Roles = "Administrador")]
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
    [Authorize(Roles = "Administrador")]
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
    public async Task<IActionResult> MiPerfil(Usuario usuario)
    {
        var idActual = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        usuario.id_usuario = idActual;

        var usuarioActual = repositorio.ObtenerPorId(idActual);
        usuario.rol = usuarioActual!.rol;

        ModelState.Remove(nameof(usuario.rol));

        if (!ModelState.IsValid)
        {
            return View(usuario);
        }

        repositorio.Modificar(usuario);

        // recarga la cookie con los datos actualizados, para que el menu muestre el nombre nuevo
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, usuario.id_usuario.ToString()),
        new Claim(ClaimTypes.Name, usuario.nombre + " " + usuario.apellido),
        new Claim(ClaimTypes.Email, usuario.email),
        new Claim(ClaimTypes.Role, usuario.rol)
    };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("MiPerfil");
    }
}