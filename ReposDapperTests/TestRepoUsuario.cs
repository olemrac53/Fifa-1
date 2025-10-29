using Fifa.Core;
using Fifa.Core.Repos;
using Fifa.Dapper;

namespace Fifa.Test;

public class TestRepoUsuario : TestRepo
{
    // Repo que vamos a usar en este test
    readonly IRepoUsuario repoUsuario;
    
    // Este test va a usar la cadena de conexión que definí en la clase base TestRepo
    public TestRepoUsuario() : base()
        => repoUsuario = new RepoUsuario(_conexion);

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

        usuario.Nombre = nuevoNombre;
        repoUsuario.UpdateUsuario(usuario, pass);

        var usuarioModificado = repoUsuario.GetUsuario(idUsuario);
        Assert.NotNull(usuarioModificado);
        Assert.Equal(nuevoNombre, usuarioModificado.Nombre);
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