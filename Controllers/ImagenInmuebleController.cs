using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;
using Microsoft.AspNetCore.Authorization;
namespace MVC.Controllers;

[Authorize]
public class ImagenInmuebleController : Controller
{
    private readonly IRepositorioImagenInmueble repositorio;
    private readonly Cloudinary cloudinary;

    public ImagenInmuebleController(IRepositorioImagenInmueble repositorio, IConfiguration configuration)
    {
        this.repositorio = repositorio;

        // config de cloudinary
        var cuenta = new Account(
            configuration["Cloudinary:CloudName"],
            configuration["Cloudinary:ApiKey"],
            configuration["Cloudinary:ApiSecret"]
        );
        cloudinary = new Cloudinary(cuenta);
    }

    // Listado de imagenes de un inmueble
    public IActionResult Index(int inmuebleId)
    {
        var lista = repositorio.ObtenerPorInmueble(inmuebleId);
        ViewBag.InmuebleId = inmuebleId;
        return View(lista);
    }

    [HttpGet]
    public IActionResult Crear(int inmuebleId)
    {
        ViewBag.InmuebleId = inmuebleId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Crear(int inmuebleId, IFormFile archivo, bool esPortada)
    {
        if (archivo == null || archivo.Length == 0)
        {
            ModelState.AddModelError("", "Tenés que seleccionar una imagen.");
            ViewBag.InmuebleId = inmuebleId;
            return View();
        }

        // subida a cloudinary
        using var stream = archivo.OpenReadStream();
        var parametros = new ImageUploadParams
        {
            File = new FileDescription(archivo.FileName, stream),
            Folder = "inmobiliaria"
        };

        var resultado = await cloudinary.UploadAsync(parametros);

        // si Cloudinary no devuelve la URL, fallo en la subida
        if (resultado.Error != null || resultado.SecureUrl == null)
        {
            ModelState.AddModelError("", "Error al subir la imagen a Cloudinary: " + (resultado.Error?.Message ?? "motivo desconocido"));
            ViewBag.InmuebleId = inmuebleId;
            return View();
        }

        var imagen = new ImagenInmueble
        {
            inmueble_id = inmuebleId,
            url = resultado.SecureUrl.ToString(),
            esPortada = esPortada
        };

        repositorio.Crear(imagen);

        return RedirectToAction("Index", new { inmuebleId });
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public IActionResult Eliminar(int id)
    {
        var imagen = repositorio.ObtenerPorId(id);
        if (imagen == null)
        {
            return NotFound();
        }
        return View(imagen);
    }

    [HttpPost, ActionName("Eliminar")]
    [Authorize(Roles = "Administrador")]
    public IActionResult EliminarConfirmado(int id)
    {
        var imagen = repositorio.ObtenerPorId(id);
        if (imagen == null)
        {
            return NotFound();
        }

        var inmuebleId = imagen.inmueble_id;
        repositorio.Eliminar(id);

        return RedirectToAction("Index", new { inmuebleId });
    }
}