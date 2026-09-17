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
    private readonly IWebHostEnvironment environment;

    public UsuarioController(IRepositorioUsuario repositorio,IWebHostEnvironment environment)
    {
        this.repositorio = repositorio;
        this.environment = environment;
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
    [ValidateAntiForgeryToken]
    public IActionResult CambiarContrasena(CambiarContrasenaViewModel modelo)
    {
        var idActual = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (!ModelState.IsValid)
        {
            var usuario = repositorio.ObtenerPorId(idActual);

            if(usuario == null)
            {
                return NotFound();    
            }
            return View("miPerfil", usuario);        
        }    
        var contrasenaCorrecta = repositorio.VerificarContrasena(idActual, modelo.ContrasenaActual);

        if (!contrasenaCorrecta)
        {
            ModelState.AddModelError(
                nameof(modelo.ContrasenaActual),
                "La contraseña actual es incorrecta");
            
            var usuario = repositorio.ObtenerPorId(idActual);

            if (usuario == null)
        {
            return NotFound();
        }

        return View("MiPerfil", usuario);
        }
        repositorio.CambiarContrasena(idActual,modelo.NuevaContrasena);

        TempData["Success"] = "La contraseña se cambio correctamente";

        return RedirectToAction("MiPerfil");
    }
    [HttpPost]
    public async Task<IActionResult> MiPerfil(Usuario usuario)
    {
        var idActual = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        usuario.id_usuario = idActual;

        var usuarioActual = repositorio.ObtenerPorId(idActual);
        if(usuarioActual == null)
        {
            return NotFound();
        }
        
        usuario.rol = usuarioActual!.rol;
        usuario.avatar = usuarioActual.avatar;

        ModelState.Remove(nameof(usuario.rol));
        ModelState.Remove(nameof(usuario.contrasena));

        if (!ModelState.IsValid)
        {
            return View(usuario);
        }
        Console.WriteLine("===== PRUEBA AVATAR =====");
    Console.WriteLine("Cantidad de archivos recibidos: " + Request.Form.Files.Count);

    if (usuario.avatarFile == null)
    {
        Console.WriteLine("avatarFile ES NULL");
    }
    else
    {
        Console.WriteLine("Archivo recibido: " + usuario.avatarFile.FileName);
        Console.WriteLine("Tamaño: " + usuario.avatarFile.Length);
    }
        if(usuario.avatarFile != null)
        {
            string wwwPath= environment.WebRootPath;

            string carpeta = Path.Combine(
                wwwPath,"img","usuarios"
            );
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }
            string extension = Path.GetExtension(usuario.avatarFile.FileName);

            String nombreArchivo = "avatar_" + idActual + extension ;

            string rutaCompleta= Path.Combine(carpeta, nombreArchivo);

            using(FileStream stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                usuario.avatarFile.CopyTo(stream);
            }
            usuario.avatar = "/img/usuarios/" + nombreArchivo;
        }
        repositorio.ModificarPerfil(usuario);

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

        TempData["Success"] = "Los datos del perfil se guardaron correctamente";
        return RedirectToAction("MiPerfil");
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EliminarAvatar()
    {
        Console.WriteLine("===== ENTRO A ELIMINAR AVATAR =====");

        var idActual = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var usuario = repositorio.ObtenerPorId(idActual);

        if(usuario == null)
        {
            return NotFound();
        }
        if (!string.IsNullOrEmpty(usuario.avatar))
        {
           string nombreArchivo = Path.GetFileName(usuario.avatar);

        string rutaCompleta = Path.Combine(
            environment.WebRootPath,"img","usuarios",nombreArchivo);
        if(System.IO.File.Exists(rutaCompleta))
            {
                System.IO.File.Delete(rutaCompleta);
            }

        }
        usuario.avatar = null ;
        repositorio.ModificarPerfil(usuario);
        var usuarioPrueba = repositorio.ObtenerPorId(idActual);

    Console.WriteLine("AVATAR QUE SE INTENTA GUARDAR: " + usuario.avatar);
    Console.WriteLine(
    "AVATAR RECUPERADO DE BD: " +
    (usuarioPrueba?.avatar ?? "NULL")
    );
        TempData["Success"] = "La foto de perfil fue eliminada correctamente";
        return RedirectToAction("MiPerfil") ;
    }
}