using MVC.Models;

namespace MVC.Repositories;

public interface IRepositorioReserva
{
    void Crear(Reserva reserva);
    void Eliminar(int id);
    void Modificar(Reserva reserva);
    List<Reserva> ObtenerTodos();
    Reserva? ObtenerPorId(int id);

}