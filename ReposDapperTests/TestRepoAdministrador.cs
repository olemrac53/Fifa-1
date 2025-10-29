using Fifa.Core;
using Fifa.Core.Repos;
using Fifa.Dapper;
using MySqlConnector;

namespace Fifa.Test;

public class TestRepoAdministrador : TestRepo
{
    // Repo que vamos a usar en este test
    readonly IRepoAdministrador repoAdministrador;
    
    // Este test va a usar la cadena de conexión que esta en test repo en la clase base TestRepo
    public TestRepoAdministrador() : base()
        => repoAdministrador = new RepoAdministrador(_conexion);

    [Fact]
    public void TraerAdministradores()
    {
        var administradores = repoAdministrador.GetAdministradores();
        
        Assert.NotEmpty(administradores);
        Assert.Contains(administradores, a => !string.IsNullOrEmpty(a.Nombre) && !string.IsNullOrEmpty(a.Apellido));
    }

    [Fact]
    public void AdministradorPorId()
    {
        var administrador = repoAdministrador.GetAdministrador(1);
        
        Assert.NotNull(administrador);
        Assert.NotEmpty(administrador.Nombre);
        Assert.NotEmpty(administrador.Email);
    }

    [Fact]
    public void AdministradorPorEmailYPass()
    {
        var admin = repoAdministrador.AdministradorPorEmailYPass("admin@fifa.com", "123456");
    
        Assert.NotNull(admin);
        Assert.Equal("adminn@fifa.com", admin.Email);
        Assert.Equal("Adminn", admin.Nombre);
        Assert.Equal("Test", admin.Apellido);
    }

    [Fact]
    public void AltaAdministrador()
    {
        var nuevoAdmin = new Administrador
        {
            Nombre = "Carlos",
            Apellido = "Tevez",
            Email = "carlitos@fifa.com",
            FechaNacimiento = new DateTime(1984, 2, 5)
        };
        
        Assert.Equal(0, nuevoAdmin.IdAdministrador);
        
        repoAdministrador.InsertAdministrador(nuevoAdmin, "mipassword");
        Assert.NotEqual(0, nuevoAdmin.IdAdministrador);
        
        var mismoAdmin = repoAdministrador.GetAdministrador(nuevoAdmin.IdAdministrador);
        Assert.NotNull(mismoAdmin);
        Assert.Equal(nuevoAdmin.Nombre, mismoAdmin.Nombre);
        Assert.Equal(nuevoAdmin.Apellido, mismoAdmin.Apellido);
        Assert.Equal(nuevoAdmin.Email, mismoAdmin.Email);
    }

    [Fact]
    public void ModificarAdministrador()
    {
        var admin = repoAdministrador.GetAdministrador(1);
        Assert.NotNull(admin);
        
        admin.Nombre = "NombreModificado";
        repoAdministrador.UpdateAdministrador(admin, "nuevapass");
        
        var adminModificado = repoAdministrador.GetAdministrador(admin.IdAdministrador);
        Assert.NotNull(adminModificado);
        Assert.Equal("NombreModificado", adminModificado.Nombre);
    }

    [Fact]
    public void EliminarAdministrador()
    {
        var nuevoAdmin = new Administrador
        {
            Nombre = "Para",
            Apellido = "Eliminar",
            Email = "eliminar@fifa.com",
            FechaNacimiento = DateTime.Now.AddYears(-30)
        };
        
        repoAdministrador.InsertAdministrador(nuevoAdmin, "pass123");
        Assert.NotEqual(0, nuevoAdmin.IdAdministrador);
        
        repoAdministrador.DeleteAdministrador(nuevoAdmin.IdAdministrador);
        
        var adminEliminado = repoAdministrador.GetAdministrador(nuevoAdmin.IdAdministrador);
        Assert.Null(adminEliminado);
    }
}