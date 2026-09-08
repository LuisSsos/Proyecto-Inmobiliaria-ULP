using MVC.Models;
namespace MVC.Repositories;

public interface IRepositorioInmueble
{
    int Alta(Inmueble p);
    int Baja(int id);
    int BajaFisica(int id);
    int Modificacion(Inmueble p);

    Inmueble? GetById(int id);
    IList<Inmueble> GetAll();
    IList<Inmueble> GetAllIncludingInactive();

}