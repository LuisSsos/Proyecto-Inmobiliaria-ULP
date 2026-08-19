using MVC.Models;

namespace MVC.Repositories;

public interface IRepositorioPropietario
{
    int Alta(Propietario p);
    int Baja(int id);
    int Modificacion(Propietario p);
    IList<Propietario> GetAll();

}