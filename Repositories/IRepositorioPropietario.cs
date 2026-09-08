using MVC.Models;

namespace MVC.Repositories;

public interface IRepositorioPropietario
{
    int Alta(Propietario p);
    int Baja(int id);
    int BajaFisica(int id);
    int Modificacion(Propietario p);
    Propietario? GetById(int id);
    IList<Propietario> GetAll();
    IList<Propietario> GetAllIncludingInactive();
    bool ExisteDniCuit(string dniCuit, int idExcluir = 0);
}