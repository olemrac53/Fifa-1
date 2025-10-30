using Fifa.Core;
using Fifa.Core.Repos;
using Fifa.Dapper;
using MySqlConnector;
using Dapper;


namespace Fifa.Test;

public class TestRepoAdministrador : TestRepo
{
    readonly IRepoAdministrador repoAdministrador;
    
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
        // Obtener el primer admin que exista
        var admins = repoAdministrador.GetAdministradores();
        Assert.NotEmpty(admins);
        
        var primerAdmin = admins.First();
        var administrador = repoAdministrador.GetAdministrador(primerAdmin.IdAdministrador);
        
        Assert.NotNull(administrador);
        Assert.NotEmpty(administrador.Nombre);
        Assert.NotEmpty(administrador.Email);
    }

    [Fact]
    public void AdministradorPorEmailYPass()
    {
        var admin = repoAdministrador.AdministradorPorEmailYPass("admin@fifa.com", "123456");
    
        Assert.NotNull(admin);
        Assert.Equal("admin@fifa.com", admin.Email);
        Assert.Equal("Admin", admin.Nombre);
        Assert.Equal("Test", admin.Apellido);
    }

    [Fact]
    public void AltaAdministrador()
    {
        var emailUnico = $"carlos{DateTime.Now.Ticks}@fifa.com";
        
        var nuevoAdmin = new Administrador
        {
            Nombre = "Carlos",
            Apellido = "Tevez",
            Email = emailUnico,
            FechaNacimiento = new DateTime(1984, 2, 5)
        };
        
        Assert.Equal(0, nuevoAdmin.IdAdministrador);
        
        repoAdministrador.InsertAdministrador(nuevoAdmin, "mipassword");
        
        // Como el SP no asigna el ID, lo buscamos manualmente
        var adminCreado = repoAdministrador.GetAdministradores()
            .FirstOrDefault(a => a.Email == emailUnico);
        
        Assert.NotNull(adminCreado);
        Assert.Equal(nuevoAdmin.Nombre, adminCreado.Nombre);
        Assert.Equal(nuevoAdmin.Apellido, adminCreado.Apellido);
        Assert.Equal(nuevoAdmin.Email, adminCreado.Email);
    }

    [Fact]
    public void ModificarAdministrador()
    {
        // Obtener el primer admin
        var admins = repoAdministrador.GetAdministradores();
        Assert.NotEmpty(admins);
        
        var admin = admins.First();
        Assert.NotNull(admin);
        
        var nombreOriginal = admin.Nombre;
        admin.Nombre = "NombreModificado";
        repoAdministrador.UpdateAdministrador(admin, "nuevapass");
        
        var adminModificado = repoAdministrador.GetAdministrador(admin.IdAdministrador);
        Assert.NotNull(adminModificado);
        Assert.Equal("NombreModificado", adminModificado.Nombre);
        
        // Restaurar nombre original
        admin.Nombre = nombreOriginal;
        repoAdministrador.UpdateAdministrador(admin, "123456");
    }

    [Fact]
    public void EliminarAdministrador()
    {
        var emailUnico = $"eliminar{DateTime.Now.Ticks}@fifa.com";
        
        var nuevoAdmin = new Administrador
        {
            Nombre = "Para",
            Apellido = "Eliminar",
            Email = emailUnico,
            FechaNacimiento = DateTime.Now.AddYears(-30)
        };
        
        repoAdministrador.InsertAdministrador(nuevoAdmin, "pass123");
        
        // Buscar el admin recién creado
        var adminCreado = repoAdministrador.GetAdministradores()
            .FirstOrDefault(a => a.Email == emailUnico);
        
        Assert.NotNull(adminCreado);
        
        repoAdministrador.DeleteAdministrador(adminCreado.IdAdministrador);

        var adminEliminado = repoAdministrador.GetAdministrador(adminCreado.IdAdministrador);
        
        Assert.Null(adminEliminado);
    }
}