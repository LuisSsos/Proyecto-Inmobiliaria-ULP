using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;
using System.Security.Claims;

namespace MVC.Controllers;

public class AuthController : Controller
{
    private readonly IRepositorioUsuario repositorio;

    public AuthController(IRepositorioUsuario repositorio)
    {
        this.repositorio = repositorio;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel modelo)
    {
        if (!ModelState.IsValid)
        {
            return View(modelo);
        }

        var usuario = repositorio.ValidarCredenciales(modelo.email, modelo.contrasena);

        if (usuario == null)
        {
            ModelState.AddModelError("", "Email o contraseña incorrectos.");
            return View(modelo);
        }

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

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    public IActionResult AccesoDenegado()
{
    return View();
}
}