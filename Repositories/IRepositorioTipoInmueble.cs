using MVC.Models;
namespace MVC.Repositories;

public interface IRepositorioTipoInmueble
{
    int Alta(TipoInmueble tipo);
    int Modificacion(TipoInmueble tipo);
    int Baja(int id);
    IList<TipoInmueble> GetAll();

}