
namespace Fifa.Core.Repos;

public interface IRepoAdministrador
 {
    List<Administrador> GetAdministradores();
    Administrador? GetAdministrador(int idAdministrador);
    Administrador? AdministradorPorEmailYPass(string email, string password);
    void InsertAdministrador(Administrador administrador, string password);
    void UpdateAdministrador(Administrador administrador, string password);
    void DeleteAdministrador(int idAdministrador);
}