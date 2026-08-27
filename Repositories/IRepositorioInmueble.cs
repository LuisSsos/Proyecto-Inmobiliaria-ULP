using MVC.Models;
namespace MVC.Repositories;
public interface IRepositorioInmueble
{
    int Alta(Inmueble p);
    int Baja(int id);
    int Modificacion(Inmueble p);
    IList<Inmueble> GetAll();

}