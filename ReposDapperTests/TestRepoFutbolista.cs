using Fifa.Core;
using Fifa.Core.Repos;
using Fifa.Dapper;
using MySqlConnector;
using Dapper;

namespace Fifa.Test;

public class TestRepoFutbolista : TestRepo, IDisposable
{
    readonly IRepoFutbolista repoFutbolista;
    private List<int> futbolistasCreados = new List<int>();
    private List<int> tiposCreados = new List<int>();
    private int equipoTestId;
    private int tipoTestId;

    public TestRepoFutbolista() : base()
    {
        repoFutbolista = new RepoFutbolista(_conexion);
        SetupTestData();
    }

    private void SetupTestData()
    {
        // Configuración de datos de prueba en la BD
        var sql = @"INSERT INTO Equipo (nombre) 
                   VALUES (@nombre);
                   SELECT LAST_INSERT_ID();";
        
        equipoTestId = _conexion.QuerySingle<int>(sql, new 
        { 
            nombre = $"Equipo Test {Guid.NewGuid().ToString().Substring(0, 8)}"
        });

        var tipoSql = @"INSERT INTO Tipo (nombre) 
                       VALUES (@nombre);
                       SELECT LAST_INSERT_ID();";
        
        tipoTestId = _conexion.QuerySingle<int>(tipoSql, new 
        { 
            nombre = $"Tipo Test {Guid.NewGuid().ToString().Substring(0, 8)}" 
        });
    }

    public void Dispose()
    {
        foreach (var id in futbolistasCreados)
        {
            try
            {
                repoFutbolista.DeleteFutbolista(id);
            }
            catch { }
        }

        foreach (var id in tiposCreados)
        {
            try
            {
                repoFutbolista.DeleteTipo(id);
            }
            catch { }
        }

        try
        {
            _conexion.Execute("DELETE FROM Equipo WHERE id_equipo = @id", new { id = equipoTestId });
            _conexion.Execute("DELETE FROM Tipo WHERE id_tipo = @id", new { id = tipoTestId });
        }
        catch { }
    }

    #region Tests de Futbolista CRUD

    [Fact]
    public void AltaFutbolista()
    {
        var nuevoFutbolista = new Futbolista()
        {
            Nombre = "Lionel",
            Apellido = "Messi",
            Apodo = "Leo",
            NumCamisa = "10",
            FechaNacimiento = new DateTime(1987, 6, 24),
            Cotizacion = 50000000,
            // CORREGIDO: Nombre con mayúscula
            Tipo = new Tipo() { IdTipo = tipoTestId, Nombre = "Delantero" },
            Equipo = new Equipo() { IdEquipo = equipoTestId, Nombre = "Equipo Test" }
        };

        repoFutbolista.InsertFutbolista(nuevoFutbolista);
        futbolistasCreados.Add(nuevoFutbolista.IdFutbolista);

        Assert.True(nuevoFutbolista.IdFutbolista > 0);
        
        var futbolistaGuardado = repoFutbolista.GetFutbolista(nuevoFutbolista.IdFutbolista);
        Assert.NotNull(futbolistaGuardado);
        Assert.Equal("Lionel", futbolistaGuardado.Nombre);
        Assert.Equal("Messi", futbolistaGuardado.Apellido);
        Assert.Equal("10", futbolistaGuardado.NumCamisa);
        Assert.Equal(50000000, futbolistaGuardado.Cotizacion);
    }

    [Fact]
    public void TraerTodosLosFutbolistas()
    {
        var futbolista = new Futbolista()
        {
            Nombre = "Test",
            Apellido = "Player",
            Apodo = "TP",
            NumCamisa = "99",
            FechaNacimiento = new DateTime(1990, 1, 1),
            Cotizacion = 1000000,
            // CORREGIDO: Nombre con mayúscula
            Tipo = new Tipo() { IdTipo = tipoTestId, Nombre = "Delantero" },
            Equipo = new Equipo() { IdEquipo = equipoTestId, Nombre = "Equipo Test" }
        };
        repoFutbolista.InsertFutbolista(futbolista);
        futbolistasCreados.Add(futbolista.IdFutbolista);

        var futbolistas = repoFutbolista.GetFutbolistas();

        Assert.NotNull(futbolistas);
        Assert.NotEmpty(futbolistas);
        Assert.All(futbolistas, f => 
        {
            Assert.NotNull(f.Equipo);
            Assert.NotNull(f.Tipo);
        });
    }

    [Fact]
    public void TraerFutbolistaPorId()
    {
        var futbolista = new Futbolista()
        {
            Nombre = "Cristiano",
            Apellido = "Ronaldo",
            Apodo = "CR7",
            NumCamisa = "7",
            FechaNacimiento = new DateTime(1985, 2, 5),
            Cotizacion = 45000000,
            // CORREGIDO: Nombre con mayúscula
            Tipo = new Tipo() { IdTipo = tipoTestId, Nombre = "Delantero" },
            Equipo = new Equipo() { IdEquipo = equipoTestId, Nombre = "Equipo Test" }
        };
        repoFutbolista.InsertFutbolista(futbolista);
        futbolistasCreados.Add(futbolista.IdFutbolista);

        var futbolistaObtenido = repoFutbolista.GetFutbolista(futbolista.IdFutbolista);

        Assert.NotNull(futbolistaObtenido);
        Assert.Equal(futbolista.IdFutbolista, futbolistaObtenido.IdFutbolista);
        Assert.Equal("Cristiano", futbolistaObtenido.Nombre);
        Assert.Equal("Ronaldo", futbolistaObtenido.Apellido);
        Assert.NotNull(futbolistaObtenido.Equipo);
        Assert.NotNull(futbolistaObtenido.Tipo);
    }

    [Fact]
    public void ModificarFutbolista()
    {
        var futbolista = new Futbolista()
        {
            Nombre = "Neymar",
            Apellido = "Junior",
            Apodo = "Ney",
            NumCamisa = "11",
            FechaNacimiento = new DateTime(1992, 2, 5),
            Cotizacion = 40000000,
            // CORREGIDO: Nombre con mayúscula
            Tipo = new Tipo() { IdTipo = tipoTestId, Nombre = "Delantero" },
            Equipo = new Equipo() { IdEquipo = equipoTestId, Nombre = "Equipo Test" }
        };
        repoFutbolista.InsertFutbolista(futbolista);
        futbolistasCreados.Add(futbolista.IdFutbolista);

        futbolista.Cotizacion = 55000000;
        futbolista.NumCamisa = "10";
        repoFutbolista.UpdateFutbolista(futbolista);

        var futbolistaModificado = repoFutbolista.GetFutbolista(futbolista.IdFutbolista);
        Assert.NotNull(futbolistaModificado);
        Assert.Equal(55000000, futbolistaModificado.Cotizacion);
        Assert.Equal("10", futbolistaModificado.NumCamisa);
    }

    [Fact]
    public void EliminarFutbolista()
    {
        var futbolista = new Futbolista()
        {
            Nombre = "Kylian",
            Apellido = "Mbappe",
            Apodo = "Kiki",
            NumCamisa = "7",
            FechaNacimiento = new DateTime(1998, 12, 20),
            Cotizacion = 60000000,
            // CORREGIDO: Nombre con mayúscula
            Tipo = new Tipo() { IdTipo = tipoTestId, Nombre = "Delantero" },
            Equipo = new Equipo() { IdEquipo = equipoTestId, Nombre = "Equipo Test" }
        };
        repoFutbolista.InsertFutbolista(futbolista);
        int idFutbolista = futbolista.IdFutbolista;

        repoFutbolista.DeleteFutbolista(idFutbolista);

        var futbolistaEliminado = repoFutbolista.GetFutbolista(idFutbolista);
        Assert.Null(futbolistaEliminado);
    }

    [Fact]
    public void FutbolistaConEquipoYTipo()
    {
        var futbolista = new Futbolista()
        {
            Nombre = "Sergio",
            Apellido = "Aguero",
            Apodo = "Kun",
            NumCamisa = "9",
            FechaNacimiento = new DateTime(1988, 6, 2),
            Cotizacion = 35000000,
            // CORREGIDO: Nombre con mayúscula
            Tipo = new Tipo() { IdTipo = tipoTestId, Nombre = "Delantero" },
            Equipo = new Equipo() { IdEquipo = equipoTestId, Nombre = "Equipo Test" }
        };
        repoFutbolista.InsertFutbolista(futbolista);
        futbolistasCreados.Add(futbolista.IdFutbolista);

        var futbolistaConRelaciones = repoFutbolista.GetFutbolista(futbolista.IdFutbolista);

        Assert.NotNull(futbolistaConRelaciones);
        Assert.NotNull(futbolistaConRelaciones.Equipo);
        Assert.NotNull(futbolistaConRelaciones.Tipo);
        Assert.True(futbolistaConRelaciones.Equipo.IdEquipo > 0);
        Assert.True(futbolistaConRelaciones.Tipo.IdTipo > 0);
        Assert.NotNull(futbolistaConRelaciones.Equipo.Nombre);
        // CORREGIDO: Nombre con mayúscula
        Assert.NotEmpty(futbolistaConRelaciones.Tipo.Nombre);
    }

    #endregion

    #region Tests de Tipo CRUD

    [Fact]
    public void AltaTipo()
    {
        var nombreUnico = $"Lateral Derecho {Guid.NewGuid().ToString().Substring(0, 8)}";
        var nuevoTipo = new Tipo()
        {
            IdTipo = 0,
            // CORREGIDO: Nombre con mayúscula
            Nombre = nombreUnico
        };

        repoFutbolista.InsertTipo(nuevoTipo);
        tiposCreados.Add(nuevoTipo.IdTipo);

        Assert.True(nuevoTipo.IdTipo > 0);
        
        var tipoGuardado = repoFutbolista.GetTipo(nuevoTipo.IdTipo);
        Assert.NotNull(tipoGuardado);
        // CORREGIDO: Nombre con mayúscula
        Assert.Equal(nombreUnico, tipoGuardado.Nombre);
    }

    [Fact]
    public void TraerTodosLosTipos()
    {
        var tipos = repoFutbolista.GetTipos();

        Assert.NotNull(tipos);
        Assert.NotEmpty(tipos);
    }

    [Fact]
    public void TraerTipoPorId()
    {
        var nombreUnico = $"Portero Test {Guid.NewGuid().ToString().Substring(0, 8)}";
        // CORREGIDO: Nombre con mayúscula
        var tipo = new Tipo() { IdTipo = 0, Nombre = nombreUnico };
        repoFutbolista.InsertTipo(tipo);
        tiposCreados.Add(tipo.IdTipo);

        var tipoObtenido = repoFutbolista.GetTipo(tipo.IdTipo);

        Assert.NotNull(tipoObtenido);
        Assert.Equal(tipo.IdTipo, tipoObtenido.IdTipo);
        // CORREGIDO: Nombre con mayúscula
        Assert.Equal(nombreUnico, tipoObtenido.Nombre);
    }

    [Fact]
    public void EliminarTipo()
    {
        var nombreUnico = $"Tipo Temporal {Guid.NewGuid().ToString().Substring(0, 8)}";
        // CORREGIDO: Nombre con mayúscula
        var tipo = new Tipo() { IdTipo = 0, Nombre = nombreUnico };
        repoFutbolista.InsertTipo(tipo);
        int idTipo = tipo.IdTipo;

        repoFutbolista.DeleteTipo(idTipo);

        var tipoEliminado = repoFutbolista.GetTipo(idTipo);
        Assert.Null(tipoEliminado);
    }

    [Fact]
    public void TipoDuplicadoLanzaExcepcion()
    {
        var nombreUnico = $"Mediocampista Test {Guid.NewGuid().ToString().Substring(0, 8)}";
        // CORREGIDO: Nombre con mayúscula
        var tipo = new Tipo() { IdTipo = 0, Nombre = nombreUnico };
        repoFutbolista.InsertTipo(tipo);
        tiposCreados.Add(tipo.IdTipo);

        // CORREGIDO: Nombre con mayúscula
        var tipoDuplicado = new Tipo() { IdTipo = 0, Nombre = nombreUnico };
        Assert.Throws<System.Data.ConstraintException>(() => 
            repoFutbolista.InsertTipo(tipoDuplicado)
        );
    }

    #endregion

    #region Tests de Validación

    [Fact]
    public void FutbolistaConCotizacionCero()
    {
        var futbolista = new Futbolista()
        {
            Nombre = "Jugador",
            Apellido = "Joven",
            Apodo = "Novato",
            NumCamisa = "99",
            FechaNacimiento = new DateTime(2005, 1, 1),
            Cotizacion = 0,
            // CORREGIDO: Nombre con mayúscula
            Tipo = new Tipo() { IdTipo = tipoTestId, Nombre = "Delantero" },
            Equipo = new Equipo() { IdEquipo = equipoTestId, Nombre = "Equipo Test" }
        };

        repoFutbolista.InsertFutbolista(futbolista);
        futbolistasCreados.Add(futbolista.IdFutbolista);

        var futbolistaGuardado = repoFutbolista.GetFutbolista(futbolista.IdFutbolista);
        Assert.NotNull(futbolistaGuardado);
        Assert.Equal(0, futbolistaGuardado.Cotizacion);
    }

    #endregion
}