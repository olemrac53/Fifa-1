using Fifa.Core;
using Fifa.Core.Repos;
using Fifa.Dapper;
using MySqlConnector;

namespace Fifa.Test;

public class TestRepoPlantilla : TestRepo
{
    // Repo que vamos a usar en este test
    readonly IRepoPlantilla repoPlantilla;

    // Este test va a usar la cadena de conexión que está en la clase base TestRepo
    public TestRepoPlantilla() : base()
        => repoPlantilla = new RepoPlantilla(_conexion);

    [Fact]
    public void AltaPlantilla()
    {
        // Given - Usuario Juan existe con id 1
        var nuevaPlantilla = new Plantilla()
        {
            Usuario = new Usuario() { IdUsuario = 1 },
            PresupuestoMax = 5000000, // Valor más realista
            CantMaxFutbolistas = 25
        };

        // When
        repoPlantilla.InsertPlantilla(nuevaPlantilla);

        // Then
        Assert.True(nuevaPlantilla.IdPlantilla > 0);
        
        var plantillaGuardada = repoPlantilla.GetPlantilla(nuevaPlantilla.IdPlantilla);
        Assert.NotNull(plantillaGuardada);
        Assert.Equal(5000000, plantillaGuardada.PresupuestoMax);
        Assert.Equal(25, plantillaGuardada.CantMaxFutbolistas);

        // Cleanup
        repoPlantilla.DeletePlantilla(nuevaPlantilla.IdPlantilla);
    }

    [Fact]
    public void TraerTodasLasPlantillas()
    {
        // Given - Crear una plantilla temporal
        var plantilla = new Plantilla()
        {
            Usuario = new Usuario() { IdUsuario = 1 },
            PresupuestoMax = 3000000,
            CantMaxFutbolistas = 20
        };
        repoPlantilla.InsertPlantilla(plantilla);

        // When
        var plantillas = repoPlantilla.GetPlantillas();

        // Then
        Assert.NotNull(plantillas);
        Assert.NotEmpty(plantillas);
        Assert.Contains(plantillas, p => p.IdPlantilla == plantilla.IdPlantilla);

        // Cleanup
        repoPlantilla.DeletePlantilla(plantilla.IdPlantilla);
    }

    [Fact]
    public void TraerPlantilla()
    {
        // Given - Crear plantilla temporal
        var plantilla = new Plantilla()
        {
            Usuario = new Usuario() { IdUsuario = 1 },
            PresupuestoMax = 4000000,
            CantMaxFutbolistas = 22
        };
        repoPlantilla.InsertPlantilla(plantilla);

        // When
        var plantillaObtenida = repoPlantilla.GetPlantilla(plantilla.IdPlantilla);

        // Then
        Assert.NotNull(plantillaObtenida);
        Assert.Equal(plantilla.IdPlantilla, plantillaObtenida.IdPlantilla);
        Assert.NotNull(plantillaObtenida.Usuario);
        Assert.Equal("Juan", plantillaObtenida.Usuario.Nombre);

        // Cleanup
        repoPlantilla.DeletePlantilla(plantilla.IdPlantilla);
    }

    [Fact]
    public void TraerPlantillaCompleta()
    {
        // Given - Crear plantilla temporal
        var plantilla = new Plantilla()
        {
            Usuario = new Usuario() { IdUsuario = 1 },
            PresupuestoMax = 5000000,
            CantMaxFutbolistas = 25
        };
        repoPlantilla.InsertPlantilla(plantilla);

        // When
        var plantillaCompleta = repoPlantilla.GetPlantillaCompleta(plantilla.IdPlantilla);

        // Then
        Assert.NotNull(plantillaCompleta);
        Assert.Equal(plantilla.IdPlantilla, plantillaCompleta.IdPlantilla);
        Assert.NotNull(plantillaCompleta.Usuario);
        Assert.NotNull(plantillaCompleta.Titulares);
        Assert.NotNull(plantillaCompleta.Suplentes);

        // Cleanup
        repoPlantilla.DeletePlantilla(plantilla.IdPlantilla);
    }

    [Fact]
    public void ModificarPlantilla()
    {
        // Given - Crear plantilla temporal
        var plantilla = new Plantilla()
        {
            Usuario = new Usuario() { IdUsuario = 1 },
            PresupuestoMax = 3000000,
            CantMaxFutbolistas = 20
        };
        repoPlantilla.InsertPlantilla(plantilla);

        // When - Modificar presupuesto
        plantilla.PresupuestoMax = 4500000;
        plantilla.CantMaxFutbolistas = 22;
        repoPlantilla.UpdatePlantilla(plantilla);

        // Then
        var plantillaModificada = repoPlantilla.GetPlantilla(plantilla.IdPlantilla);
        Assert.Equal(4500000, plantillaModificada.PresupuestoMax);
        Assert.Equal(22, plantillaModificada.CantMaxFutbolistas);

        // Cleanup
        repoPlantilla.DeletePlantilla(plantilla.IdPlantilla);
    }

    [Fact]
    public void CalcularPresupuestoPlantillaVacia()
    {
        // Given - Crear plantilla vacía
        var plantilla = new Plantilla()
        {
            Usuario = new Usuario() { IdUsuario = 1 },
            PresupuestoMax = 5000000,
            CantMaxFutbolistas = 25
        };
        repoPlantilla.InsertPlantilla(plantilla);

        // When
        var presupuesto = repoPlantilla.CalcularPresupuestoPlantilla(plantilla.IdPlantilla);

        // Then
        Assert.Equal(0, presupuesto); // Plantilla vacía = 0

        // Cleanup
        repoPlantilla.DeletePlantilla(plantilla.IdPlantilla);
    }

    [Fact]
    public void ContarFutbolistasPlantillaVacia()
    {
        // Given - Crear plantilla vacía
        var plantilla = new Plantilla()
        {
            Usuario = new Usuario() { IdUsuario = 1 },
            PresupuestoMax = 5000000,
            CantMaxFutbolistas = 25
        };
        repoPlantilla.InsertPlantilla(plantilla);

        // When
        var cantidad = repoPlantilla.ContarFutbolistasPlantilla(plantilla.IdPlantilla);

        // Then
        Assert.Equal(0, cantidad); // Plantilla vacía = 0 futbolistas

        // Cleanup
        repoPlantilla.DeletePlantilla(plantilla.IdPlantilla);
    }

    [Fact]
    public void ValidarPlantillaVacia()
    {
        // Given - Crear plantilla vacía
        var plantilla = new Plantilla()
        {
            Usuario = new Usuario() { IdUsuario = 1 },
            PresupuestoMax = 5000000,
            CantMaxFutbolistas = 25
        };
        repoPlantilla.InsertPlantilla(plantilla);

        // When
        var esValida = repoPlantilla.PlantillaEsValida(plantilla.IdPlantilla);

        // Then
        Assert.IsType<bool>(esValida);
        // Una plantilla vacía probablemente NO sea válida según tus reglas de negocio
        Assert.False(esValida);

        // Cleanup
        repoPlantilla.DeletePlantilla(plantilla.IdPlantilla);
    }

    [Theory]
    [InlineData(1, 2)]
    public void CrearDosPlantillasParaDiferentesUsuarios(int idUsuario1, int idUsuario2)
    {
        // Given - Dos usuarios diferentes
        var plantilla1 = new Plantilla()
        {
            Usuario = new Usuario() { IdUsuario = idUsuario1 },
            PresupuestoMax = 3000000,
            CantMaxFutbolistas = 20
        };
        var plantilla2 = new Plantilla()
        {
            Usuario = new Usuario() { IdUsuario = idUsuario2 },
            PresupuestoMax = 4000000,
            CantMaxFutbolistas = 25
        };

        // When
        repoPlantilla.InsertPlantilla(plantilla1);
        repoPlantilla.InsertPlantilla(plantilla2);

        // Then
        Assert.True(plantilla1.IdPlantilla > 0);
        Assert.True(plantilla2.IdPlantilla > 0);
        Assert.NotEqual(plantilla1.IdPlantilla, plantilla2.IdPlantilla);

        var p1 = repoPlantilla.GetPlantilla(plantilla1.IdPlantilla);
        var p2 = repoPlantilla.GetPlantilla(plantilla2.IdPlantilla);
        
        Assert.Equal("Juan", p1.Usuario.Nombre);
        Assert.Equal("Maria", p2.Usuario.Nombre);

        // Cleanup
        repoPlantilla.DeletePlantilla(plantilla1.IdPlantilla);
        repoPlantilla.DeletePlantilla(plantilla2.IdPlantilla);
    }

    [Fact]
    public void EliminarPlantilla()
    {
        // Given - Crear plantilla temporal
        var plantilla = new Plantilla()
        {
            Usuario = new Usuario() { IdUsuario = 1 },
            PresupuestoMax = 3000000,
            CantMaxFutbolistas = 20
        };
        repoPlantilla.InsertPlantilla(plantilla);
        int idPlantilla = plantilla.IdPlantilla;

        // When
        repoPlantilla.DeletePlantilla(idPlantilla);

        // Then - Debe lanzar excepción al buscar plantilla eliminada
        Assert.Throws<InvalidOperationException>(() => 
            repoPlantilla.GetPlantilla(idPlantilla)
        );
    }
}