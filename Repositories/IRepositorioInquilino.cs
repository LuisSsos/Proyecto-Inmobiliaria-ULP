using MVC.Models;

namespace MVC.Repositories;

public interface IRepositorioInquilino
{
    void Crear(Inquilino i);
    void Eliminar(int id);
    void Modificar(Inquilino i);
    List<Inquilino> ObtenerTodos();
    Inquilino? ObtenerPorId(int id);
    bool ExisteDni(string dni, int idExcluir = 0);
}