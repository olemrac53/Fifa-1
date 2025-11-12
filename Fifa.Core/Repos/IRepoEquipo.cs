namespace Fifa.Core.Repos;


public interface IRepoEquipo
{
    List<Equipo> GetEquipos();
    Equipo? GetEquipo(int idEquipo);
    Equipo? GetEquipoConFutbolistas(int idEquipo);
    void InsertEquipo(Equipo equipo);
    void UpdateEquipo(Equipo equipo);
    void DeleteEquipo(int idEquipo);
}