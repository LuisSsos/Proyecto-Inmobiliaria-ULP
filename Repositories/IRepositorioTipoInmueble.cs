using MVC.Models;
namespace MVC.Repositories;

public interface IRepositorioTipoInmueble
{
    int Alta(TipoInmueble tipo);
    int Modificacion(TipoInmueble tipo);
    int Baja(int id);
    int BajaFisica(int id);
    TipoInmueble? GetById(int id);
    IList<TipoInmueble> GetAllIncludingInactive();
    IList<TipoInmueble> GetAll();

}