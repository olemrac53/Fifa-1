using Fifa.Core.Repos;
using Fifa.Core;
using Fifa.Dapper;
using Dapper;


namespace Fifa.Test;

public class TestRepoUsuario : TestRepo, IDisposable
{
    // Repo que vamos a usar en este test
    readonly IRepoUsuario repoUsuario;
    
    // Guardar valores originales para restaurar
    private Dictionary<int, (string nombre, string apellido, string email)> usuariosOriginales;
    
    public TestRepoUsuario() : base()
    {
        repoUsuario = new RepoUsuario(_conexion);
        
        // Guardar estado original de usuarios de prueba
        usuariosOriginales = new Dictionary<int, (string, string, string)>
        {
            { 1, ("Juan", "Perez", "juan@mail.com") },
            { 2, ("Maria", "Lopez", "maria@mail.com") }
        };
    }

    public void Dispose()
    {
        // Restaurar usuarios a su estado original después de cada test
        foreach (var kvp in usuariosOriginales)
        {
            _conexion.Execute(@"
                UPDATE Usuario 
                SET nombre = @nombre, 
                    apellido = @apellido, 
                    email = @email,
                    contrasenia = SHA2(@pass, 256)
                WHERE id_usuario = @id
            ", new 
            { 
                id = kvp.Key,
                nombre = kvp.Value.nombre,
                apellido = kvp.Value.apellido,
                email = kvp.Value.email,
                pass = kvp.Key == 1 ? "pass123" : "pass456"
            });
        }
        
        _conexion.Execute("DELETE FROM Usuario WHERE email = 'nuevo@mail.com'");
    }

    [Theory]
    [InlineData("juan@mail.com", "pass123", "Juan")]
    [InlineData("maria@mail.com", "pass456", "Maria")]
    public void TraerUsuarioPorEmailYPass(string email, string pass, string nombreEsperado)
    {
        var usuario = repoUsuario.UsuarioPorEmailYPass(email, pass);

        Assert.NotNull(usuario);
        Assert.Equal(nombreEsperado, usuario.Nombre);
        Assert.Equal(email, usuario.Email);
    }

    [Theory]
    [InlineData("noexiste@mail.com", "pass123")]
    [InlineData("otro@mail.com", "wrongpass")]
    public void UsuariosNoExisten(string email, string pass)
    {
        var usuario = repoUsuario.UsuarioPorEmailYPass(email, pass);

        Assert.Null(usuario);
    }
    
    [Fact]
    public void AltaUsuario()
    {
        string email = "nuevo@mail.com";
        string pass = "nuevapass";
        string nombre = "Nuevo";
        string apellido = "Usuario";

        var usuario = repoUsuario.UsuarioPorEmailYPass(email, pass);

        Assert.Null(usuario);

        var nuevoUsuario = new Usuario()
        {
            Nombre = nombre,
            Apellido = apellido,
            Email = email,
            FechaNacimiento = new DateTime(1990, 1, 1)
        };

        repoUsuario.InsertUsuario(nuevoUsuario, pass);

        var mismoUsuario = repoUsuario.UsuarioPorEmailYPass(email, pass);
        
        Assert.NotNull(mismoUsuario);
        Assert.Equal(nombre, mismoUsuario.Nombre);
        Assert.Equal(apellido, mismoUsuario.Apellido);
        Assert.Equal(email, mismoUsuario.Email);
        
        // Cleanup inmediato (aunque Dispose también lo hace)
        _conexion.Execute("DELETE FROM Usuario WHERE email = @email", new { email });
    }

    [Fact]
    public void TraerTodosLosUsuarios()
    {
        var usuarios = repoUsuario.GetUsuarios();

        Assert.NotNull(usuarios);
        Assert.NotEmpty(usuarios);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void TraerUsuarioPorId(int idUsuario)
    {
        var usuario = repoUsuario.GetUsuario(idUsuario);

        Assert.NotNull(usuario);
        Assert.Equal(idUsuario, usuario.IdUsuario);
    }

    [Fact]
    public void ModificarUsuario()
    {
        int idUsuario = 1;
        string nuevoNombre = "NombreModificado";
        string pass = "pass123";

        var usuario = repoUsuario.GetUsuario(idUsuario);
        Assert.NotNull(usuario);
        
        // Guardar nombre original
        string nombreOriginal = usuario.Nombre;

        usuario.Nombre = nuevoNombre;
        repoUsuario.UpdateUsuario(usuario, pass);

        var usuarioModificado = repoUsuario.GetUsuario(idUsuario);
        Assert.NotNull(usuarioModificado);
        Assert.Equal(nuevoNombre, usuarioModificado.Nombre);
        
        // IMPORTANTE: El Dispose() restaurará automáticamente el nombre original
    }

    [Fact]
    public void TraerUsuarioConPlantillas()
    {
        int idUsuario = 1;

        var usuario = repoUsuario.GetUsuarioConPlantillas(idUsuario);

        Assert.NotNull(usuario);
        Assert.Equal(idUsuario, usuario.IdUsuario);
        Assert.NotNull(usuario.Plantillas);
    }
}