namespace Fifa.Core.Repos;

public interface IRepoUsuario
{
    List<Usuario> GetUsuarios();
    Usuario? GetUsuario(int idUsuario);
    Usuario? UsuarioPorEmailYPass(string email, string password);
    Usuario? GetUsuarioConPlantillas(int idUsuario);
    void InsertUsuario(Usuario usuario, string password);
    void UpdateUsuario(Usuario usuario, string password);
    void DeleteUsuario(int idUsuario);
}
