
namespace Fifa.Core.Repos;


public interface IRepoEquipo
{
    void AltaEquipo(Equipo equipo);
    List<Equipo> GetEquipos();
    Equipo GetEquipo(int idEquipo);
    void UpdateEquipo(Equipo equipo);
    void DeleteEquipo(int idEquipo);
}