using MVC.Models;

namespace MVC.Repositories;

public interface IRepositorioImagenInmueble
{
    List<ImagenInmueble> ObtenerPorInmueble(int inmuebleId);
    ImagenInmueble? ObtenerPorId(int id);
    void Crear(ImagenInmueble imagen);
    void Eliminar(int id);
}