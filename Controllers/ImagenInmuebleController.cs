using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Repositories;
using Microsoft.AspNetCore.Authorization;
namespace MVC.Controllers;

[Authorize]
public class ImagenInmuebleController : Controller
{
    private readonly IRepositorioImagenInmueble repositorio;
    private readonly IWebHostEnvironment entorno;

    public ImagenInmuebleController(IRepositorioImagenInmueble repositorio, IWebHostEnvironment entorno)
    {
        this.repositorio = repositorio;
        this.entorno = entorno;
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

        // carpeta destino: wwwroot/img/inmuebles
        var imagenes = Path.Combine(entorno.WebRootPath, "img", "inmuebles");
        if (!Directory.Exists(imagenes))
        {
            Directory.CreateDirectory(imagenes);
        }

        // nombre unico 
        var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(archivo.FileName);
        var rutaFisica = Path.Combine(imagenes, nombreArchivo);

        using (var stream = new FileStream(rutaFisica, FileMode.Create))
        {
            await archivo.CopyToAsync(stream);
        }

        var imagen = new ImagenInmueble
        {
            inmueble_id = inmuebleId,
            url = "/img/inmuebles/" + nombreArchivo,
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

        // borra el archivo físico del disco
        var rutaFisica = Path.Combine(entorno.WebRootPath, imagen.url!.TrimStart('/'));
        if (System.IO.File.Exists(rutaFisica))
        {
            System.IO.File.Delete(rutaFisica);
        }

        repositorio.Eliminar(id);

        return RedirectToAction("Index", new { inmuebleId });
    }
}