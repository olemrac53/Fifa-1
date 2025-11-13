using Fifa.Core.Repos;
using Fifa.Core;
using Fifa.Dapper;
using Dapper;

namespace Fifa.Test;

public class TestRepoEquipo : TestRepo, IDisposable
{
    private readonly IRepoEquipo _repoEquipo;
    
    private readonly string _nombreOriginalEquipo1;
    private const int IdEquipoPrueba = 1;
    private const string NombreEquipoNuevo = "Equipo de Prueba Nuevo";

    public TestRepoEquipo() : base()
    {
        _repoEquipo = new RepoEquipo(_conexion);
        
        var equipo1 = _repoEquipo.GetEquipo(IdEquipoPrueba);
        Assert.NotNull(equipo1); 
        _nombreOriginalEquipo1 = equipo1.Nombre;
    }

    public void Dispose()
    {
        _conexion.Execute(@"
            UPDATE Equipo 
            SET nombre = @nombre
            WHERE id_equipo = @id
        ", new { id = IdEquipoPrueba, nombre = _nombreOriginalEquipo1 });

        _conexion.Execute("DELETE FROM Equipo WHERE nombre = @nombre", new { nombre = NombreEquipoNuevo });
    }

    [Fact]
    public void TraerTodosLosEquipos()
    {
        var equipos = _repoEquipo.GetEquipos();

        Assert.NotNull(equipos);
        Assert.NotEmpty(equipos);
    }

    [Theory]
    [InlineData(1)] 
    public void TraerEquipoPorId(int idEquipo)
    {
        var equipo = _repoEquipo.GetEquipo(idEquipo);

        Assert.NotNull(equipo);
        Assert.Equal(idEquipo, equipo.IdEquipo);
        Assert.Equal(_nombreOriginalEquipo1, equipo.Nombre);
    }

    [Fact]
    public void TraerEquipoInexistente()
    {
        var equipo = _repoEquipo.GetEquipo(9999);
        Assert.Null(equipo);
    }

    [Fact]
    public void TraerEquipoConFutbolistas()
    {
        var equipo = _repoEquipo.GetEquipoConFutbolistas(IdEquipoPrueba);

        Assert.NotNull(equipo);
        Assert.Equal(IdEquipoPrueba, equipo.IdEquipo);
        
        Assert.NotNull(equipo.Futbolistas);
        Assert.NotEmpty(equipo.Futbolistas);
    }

    [Fact]
    public void AltaYBajaEquipo()
    {
        var nuevoEquipo = new Equipo
        {
            Nombre = NombreEquipoNuevo,
            Presupuesto = 100000 
        };

        _repoEquipo.InsertEquipo(nuevoEquipo);

        Assert.True(nuevoEquipo.IdEquipo > 0);

        var equipoInsertado = _repoEquipo.GetEquipo(nuevoEquipo.IdEquipo);
        Assert.NotNull(equipoInsertado);
        Assert.Equal(NombreEquipoNuevo, equipoInsertado.Nombre);

        _repoEquipo.DeleteEquipo(nuevoEquipo.IdEquipo);

        var equipoEliminado = _repoEquipo.GetEquipo(nuevoEquipo.IdEquipo);
        Assert.Null(equipoEliminado);
    }

    [Fact]
    public void ModificarEquipo()
    {
        string nombreModificado = "Nombre Modificado";

        var equipo = _repoEquipo.GetEquipo(IdEquipoPrueba);
        Assert.NotNull(equipo);
        Assert.Equal(_nombreOriginalEquipo1, equipo.Nombre);
        
        equipo.Nombre = nombreModificado;
        _repoEquipo.UpdateEquipo(equipo);

        var equipoModificado = _repoEquipo.GetEquipo(IdEquipoPrueba);
        Assert.NotNull(equipoModificado);
        Assert.Equal(nombreModificado, equipoModificado.Nombre);
        
    }
}