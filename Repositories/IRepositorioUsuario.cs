using MVC.Models;

namespace MVC.Repositories;

public interface IRepositorioUsuario
{
    List<Usuario> ObtenerTodos();
    Usuario? ObtenerPorId(int id);
    bool ExisteEmail(string email, int idExcluir = 0);
    void Crear(Usuario usuario);
    void Modificar(Usuario usuario);
    void Eliminar(int id);
}