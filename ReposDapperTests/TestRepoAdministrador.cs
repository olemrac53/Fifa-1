using Fifa.Core;
using Fifa.Core.Repos;
using Fifa.Dapper;
using MySqlConnector;
using Dapper;

namespace Fifa.Test;

public class TestRepoAdministrador : TestRepo, IDisposable
{
    readonly IRepoAdministrador repoAdministrador;
    private List<int> administradoresCreados = new List<int>();
    
    public TestRepoAdministrador() : base()
    {
        repoAdministrador = new RepoAdministrador(_conexion);
    }

    public void Dispose()
    {
        // Limpiar administradores creados durante los tests
        foreach (var id in administradoresCreados)
        {
            try
            {
                repoAdministrador.DeleteAdministrador(id);
            }
            catch { }
        }
    }

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
        
        // Ahora el ID debe estar asignado por el SP
        Assert.True(nuevoAdmin.IdAdministrador > 0);
        administradoresCreados.Add(nuevoAdmin.IdAdministrador);
        
        var adminCreado = repoAdministrador.GetAdministrador(nuevoAdmin.IdAdministrador);
        Assert.NotNull(adminCreado);
        Assert.Equal("Carlos", adminCreado.Nombre);
        Assert.Equal("Tevez", adminCreado.Apellido);
        Assert.Equal(emailUnico, adminCreado.Email);
    }

    [Fact]
    public void ModificarAdministrador()
    {
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
        Assert.True(nuevoAdmin.IdAdministrador > 0);
        
        int idAEliminar = nuevoAdmin.IdAdministrador;
        repoAdministrador.DeleteAdministrador(idAEliminar);

        var adminEliminado = repoAdministrador.GetAdministrador(idAEliminar);
        Assert.Null(adminEliminado);
    }
}