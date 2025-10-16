namespace Fifa.Core.Repos;

public interface IRepoUsuario
{
    List<Usuario> GetUsuarios();
    Usuario GetUsuario(int idUsuario);
    void InsertUsuario(Usuario usuario);
    void UpdateUsuario(Usuario usuario);
    void DeleteUsuario(int idUsuario);

    List<Administrador> GetAdministradores();
    Administrador GetAdministrador(int idAdministrador);
    void InsertAdministrador(Administrador administrador);
    void UpdateAdministrador(Administrador administrador);
    void DeleteAdministrador(int idAdministrador);

}