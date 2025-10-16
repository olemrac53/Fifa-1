namespace Fifa.Core;

public interface IADO
{
    void AltaEquipo(Equipo equipo);
    List<Equipo> GetEquipos();
    Equipo GetEquipo(int idEquipo);
    void UpdateEquipo(Equipo equipo);
    void DeleteEquipo(int idEquipo);

    List<Plantilla> GetPlantillas();
    Plantilla GetPlantilla(int idPlantilla);
    void InsertPlantilla(Plantilla plantilla);
    void UpdatePlantilla(Plantilla plantilla);
    void DeletePlantilla(int idPlantilla); 

    
}
