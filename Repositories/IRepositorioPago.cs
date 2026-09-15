using MVC.Models;

namespace MVC.Repositories;

public interface IRepositorioPago
{
    List<Pago> ObtenerTodos();
    Pago? ObtenerPorId(int id);
    void Crear(Pago pago);
    void Modificar(Pago pago);
}