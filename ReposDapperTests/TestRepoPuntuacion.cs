using Fifa.Core;
using Fifa.Core.Repos;
using Fifa.Dapper;
using MySqlConnector;
using Dapper;

namespace Fifa.Test;

public class TestRepoPuntuacion : TestRepo
{
    readonly IRepoPuntuacion repoPuntuacion;
    readonly IRepoPlantilla repoPlantilla;

    public TestRepoPuntuacion() : base()
    {
        repoPuntuacion = new RepoPuntuacion(_conexion);
        repoPlantilla = new RepoPlantilla(_conexion);
    }

    [Fact]
    public void TraerTodasLasPuntuaciones()
    {
        // Given - Limpiar datos residuales primero
        var plantilla = CrearPlantillaConTitular();
        int idFutbolista = ObtenerPrimerTitular(plantilla.IdPlantilla);
        
        // Limpiar puntuaciones residuales de este futbolista
        _conexion.Execute("DELETE FROM PuntuacionFutbolista WHERE id_futbolista = @id", new { id = idFutbolista });
        
        repoPuntuacion.AltaPuntuacion(idFutbolista, 1, 8.5m);

        // When
        var puntuaciones = repoPuntuacion.GetPuntuaciones();

        // Then
        Assert.NotNull(puntuaciones);
        Assert.NotEmpty(puntuaciones);
        Assert.Contains(puntuaciones, p => p.Futbolista.IdFutbolista == idFutbolista && p.Fecha == 1);
        // Cleanup
        LimpiarDatosPrueba(plantilla.IdPlantilla, idFutbolista, 1);
    }

    [Fact]
    public void TraerPuntuacionesPorFutbolista()
    {
        // Given - Crear futbolista titular con múltiples puntuaciones
        var plantilla = CrearPlantillaConTitular();
        int idFutbolista = ObtenerPrimerTitular(plantilla.IdPlantilla);
        
        // Limpiar puntuaciones residuales
        _conexion.Execute("DELETE FROM PuntuacionFutbolista WHERE id_futbolista = @id", new { id = idFutbolista });
        
        repoPuntuacion.AltaPuntuacion(idFutbolista, 1, 7.5m);
        repoPuntuacion.AltaPuntuacion(idFutbolista, 2, 8.0m);
        repoPuntuacion.AltaPuntuacion(idFutbolista, 3, 6.5m);

        // When
        var puntuaciones = repoPuntuacion.GetPuntuacionesPorFutbolista(idFutbolista);

        // Then
        Assert.NotNull(puntuaciones);
        Assert.Equal(3, puntuaciones.Count);
        
        // Verificar que todas las puntuaciones pertenecen al futbolista
        Assert.All(puntuaciones, p => Assert.Equal(idFutbolista, p.IdFutbolista));
        
        Assert.Equal(1, puntuaciones[0].Fecha); // Ordenado por fecha
        Assert.Equal(2, puntuaciones[1].Fecha);
        Assert.Equal(3, puntuaciones[2].Fecha);

        // Cleanup
        LimpiarDatosPrueba(plantilla.IdPlantilla, idFutbolista, null);
    }

    [Fact]
    public void TraerPuntuacionesPorFecha()
    {
        // Given - Crear múltiples futbolistas con puntuaciones en la misma fecha
        var plantilla = CrearPlantillaConTitular();
        int idFutbolista1 = ObtenerPrimerTitular(plantilla.IdPlantilla);
        
        // Agregar segundo titular
        int idFutbolista2 = AgregarSegundoTitular(plantilla.IdPlantilla);
        
        int fechaPrueba = 5;
        
        // Limpiar datos residuales
        _conexion.Execute("DELETE FROM PuntuacionFutbolista WHERE fecha = @fecha", new { fecha = fechaPrueba });
        
        repoPuntuacion.AltaPuntuacion(idFutbolista1, fechaPrueba, 9.0m);
        repoPuntuacion.AltaPuntuacion(idFutbolista2, fechaPrueba, 7.5m);

        // When
        var puntuaciones = repoPuntuacion.GetPuntuacionesPorFecha(fechaPrueba);

        // Then
        Assert.NotNull(puntuaciones);
        Assert.True(puntuaciones.Count >= 2);
        Assert.True(puntuaciones.All(p => p.Fecha == fechaPrueba));
        Assert.NotNull(puntuaciones[0].Futbolista);
        Assert.NotNull(puntuaciones[0].Futbolista.Equipo);
        
        // Verificar orden descendente por puntuación
        Assert.True(puntuaciones[0].Puntuacion >= puntuaciones[1].Puntuacion);

        // Cleanup
        LimpiarDatosPrueba(plantilla.IdPlantilla, null, fechaPrueba);
    }

    [Fact]
    public void AltaPuntuacionExitosa()
    {
        // Given - Futbolista titular
        var plantilla = CrearPlantillaConTitular();
        int idFutbolista = ObtenerPrimerTitular(plantilla.IdPlantilla);
        int fecha = 10;
        decimal puntuacion = 8.5m;
        
        // Limpiar datos residuales
        _conexion.Execute("DELETE FROM PuntuacionFutbolista WHERE id_futbolista = @id AND fecha = @fecha", 
            new { id = idFutbolista, fecha });

        // When
        repoPuntuacion.AltaPuntuacion(idFutbolista, fecha, puntuacion);

        // Then
        var puntuaciones = repoPuntuacion.GetPuntuacionesPorFutbolista(idFutbolista);
        var puntuacionCreada = puntuaciones.FirstOrDefault(p => p.Fecha == fecha);
        
        Assert.NotNull(puntuacionCreada);
        Assert.Equal(puntuacion, puntuacionCreada.Puntuacion);
        Assert.Equal(fecha, puntuacionCreada.Fecha);

        // Cleanup
        LimpiarDatosPrueba(plantilla.IdPlantilla, idFutbolista, fecha);
    }

    [Fact]
    public void AltaPuntuacionFutbolistaNoCumpleValidacionTitular()
    {
        // Given - Crear un futbolista que NO es titular en ninguna plantilla
        // Verificamos que existe el futbolista 3 pero NO lo agregamos como titular
        VerificarFutbolistaExiste(3);

        // When & Then - Debe lanzar excepción
        var exception = Assert.Throws<InvalidOperationException>(() =>
            repoPuntuacion.AltaPuntuacion(3, 1, 7.0m)
        );

        Assert.Contains("titular", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void AltaPuntuacionDuplicadaMismaFecha()
    {
        // Futbolista con puntuación ya asignada
        var plantilla = CrearPlantillaConTitular();
        int idFutbolista = ObtenerPrimerTitular(plantilla.IdPlantilla);
        int fecha = 15;
        
        // Limpiar datos residuales
        _conexion.Execute("DELETE FROM PuntuacionFutbolista WHERE id_futbolista = @id AND fecha = @fecha", 
            new { id = idFutbolista, fecha });
        
        repoPuntuacion.AltaPuntuacion(idFutbolista, fecha, 7.5m);

        // When & Then - Debe lanzar excepción al intentar duplicar
        var exception = Assert.Throws<InvalidOperationException>(() =>
            repoPuntuacion.AltaPuntuacion(idFutbolista, fecha, 8.0m)
        );

        Assert.Contains("ya tiene una puntuación", exception.Message, StringComparison.OrdinalIgnoreCase);

        // Cleanup
        LimpiarDatosPrueba(plantilla.IdPlantilla, idFutbolista, fecha);
    }

    [Fact]
    public void ModificarPuntuacion()
    {
        // Given - Puntuación existente
        var plantilla = CrearPlantillaConTitular();
        int idFutbolista = ObtenerPrimerTitular(plantilla.IdPlantilla);
        int fecha = 20;
        decimal puntuacionOriginal = 6.5m;
        decimal puntuacionNueva = 9.0m;
        
        // Limpiar datos residuales
        _conexion.Execute("DELETE FROM PuntuacionFutbolista WHERE id_futbolista = @id AND fecha = @fecha", 
            new { id = idFutbolista, fecha });
        
        repoPuntuacion.AltaPuntuacion(idFutbolista, fecha, puntuacionOriginal);
        var puntuaciones = repoPuntuacion.GetPuntuacionesPorFutbolista(idFutbolista);
        int idPuntuacion = puntuaciones.First(p => p.Fecha == fecha).IdPuntuacion;

        // When
        repoPuntuacion.ModificarPuntuacion(idPuntuacion, puntuacionNueva);

        // Then
        var puntuacionesActualizadas = repoPuntuacion.GetPuntuacionesPorFutbolista(idFutbolista);
        var puntuacionModificada = puntuacionesActualizadas.First(p => p.Fecha == fecha);
        
        Assert.Equal(puntuacionNueva, puntuacionModificada.Puntuacion);
        Assert.NotEqual(puntuacionOriginal, puntuacionModificada.Puntuacion);

        // Cleanup
        LimpiarDatosPrueba(plantilla.IdPlantilla, idFutbolista, fecha);
    }

    [Fact]
    public void EliminarPuntuacion()
    {
        // Given - Puntuación existente
        var plantilla = CrearPlantillaConTitular();
        int idFutbolista = ObtenerPrimerTitular(plantilla.IdPlantilla);
        int fecha = 25;
        
        // Limpiar datos residuales
        _conexion.Execute("DELETE FROM PuntuacionFutbolista WHERE id_futbolista = @id AND fecha = @fecha", 
            new { id = idFutbolista, fecha });
        
        repoPuntuacion.AltaPuntuacion(idFutbolista, fecha, 7.0m);
        var puntuaciones = repoPuntuacion.GetPuntuacionesPorFutbolista(idFutbolista);
        int idPuntuacion = puntuaciones.First(p => p.Fecha == fecha).IdPuntuacion;

        // When
        repoPuntuacion.EliminarPuntuacion(idPuntuacion);

        // Then
        var puntuacionesDespues = repoPuntuacion.GetPuntuacionesPorFutbolista(idFutbolista);
        Assert.DoesNotContain(puntuacionesDespues, p => p.Fecha == fecha);

        // Cleanup
        repoPlantilla.DeletePlantilla(plantilla.IdPlantilla);
    }

    [Fact]
    public void ObtenerPuntuacionesFutbolistaSinPuntuaciones()
    {
        // Given - Futbolista titular sin puntuaciones
        var plantilla = CrearPlantillaConTitular();
        int idFutbolista = ObtenerPrimerTitular(plantilla.IdPlantilla);
        
        // Limpiar todas las puntuaciones de este futbolista
        _conexion.Execute("DELETE FROM PuntuacionFutbolista WHERE id_futbolista = @id", 
            new { id = idFutbolista });

        // When
        var puntuaciones = repoPuntuacion.GetPuntuacionesPorFutbolista(idFutbolista);

        // Then
        Assert.NotNull(puntuaciones);
        Assert.Empty(puntuaciones);

        // Cleanup
        repoPlantilla.DeletePlantilla(plantilla.IdPlantilla);
    }

    // ==================== MÉTODOS AUXILIARES ====================

    private Plantilla CrearPlantillaConTitular()
    {
        // Verificar que exista el futbolista antes de usarlo
        VerificarFutbolistaExiste(1);

        // Crear plantilla básica
        var plantilla = new Plantilla()
        {
            Usuario = new Usuario() { IdUsuario = 1 },
            PresupuestoMax = 5000000,
            CantMaxFutbolistas = 25
        };
        repoPlantilla.InsertPlantilla(plantilla);

        // Agregar un futbolista titular usando SQL directo
        _conexion.Execute(
            "INSERT INTO PlantillaTitular (id_plantilla, id_futbolista) VALUES (@idPlantilla, 1)",
            new { idPlantilla = plantilla.IdPlantilla }
        );

        return plantilla;
    }

    private void VerificarFutbolistaExiste(int idFutbolista)
    {
        var existe = _conexion.QueryFirstOrDefault<int?>(
            "SELECT id_futbolista FROM Futbolista WHERE id_futbolista = @id",
            new { id = idFutbolista }
        );

        if (!existe.HasValue)
        {
            throw new InvalidOperationException(
                $"El futbolista con ID {idFutbolista} no existe. Ejecuta el script SQL de inicialización primero.");
        }
    }

    private int ObtenerPrimerTitular(int idPlantilla)
    {
        // Obtener el id del primer titular de la plantilla
        return _conexion.QueryFirst<int>(
            "SELECT id_futbolista FROM PlantillaTitular WHERE id_plantilla = @id LIMIT 1",
            new { id = idPlantilla }
        );
    }

    private int AgregarSegundoTitular(int idPlantilla)
    {
        // Verificar que exista el futbolista 2
        VerificarFutbolistaExiste(2);
        
        // Agregar un segundo futbolista titular
        _conexion.Execute(
            "INSERT INTO PlantillaTitular (id_plantilla, id_futbolista) VALUES (@idPlantilla, 2)",
            new { idPlantilla }
        );
        return 2;
    }

    private void LimpiarDatosPrueba(int idPlantilla, int? idFutbolista = null, int? fecha = null)
    {
        // Eliminar puntuaciones
        if (idFutbolista.HasValue && fecha.HasValue)
        {
            _conexion.Execute(
                "DELETE FROM PuntuacionFutbolista WHERE id_futbolista = @idFutbolista AND fecha = @fecha",
                new { idFutbolista, fecha }
            );
        }
        else if (idFutbolista.HasValue)
        {
            _conexion.Execute(
                "DELETE FROM PuntuacionFutbolista WHERE id_futbolista = @idFutbolista",
                new { idFutbolista }
            );
        }
        else if (fecha.HasValue)
        {
            _conexion.Execute(
                "DELETE FROM PuntuacionFutbolista WHERE fecha = @fecha",
                new { fecha }
            );
        }

        // Eliminar plantilla (esto eliminará los titulares por CASCADE)
        repoPlantilla.DeletePlantilla(idPlantilla);
    }
}