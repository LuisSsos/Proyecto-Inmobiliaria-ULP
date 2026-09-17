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

    int CambiarEstado(int id, string estado);
    IList<Inmueble> ObtenerPaginado( int pagina, int cantidadPorPagina, string? estado, int? propietarioId);
    int ContarInmuebles( string? estado, int? propietarioId);
    IList<Inmueble> ObtenerMasReservados(int dias, int pagina, int cantidadPorPagina);
    int ContarMasReservados(int dias);
}