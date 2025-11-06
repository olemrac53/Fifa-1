namespace Fifa.Core.Repos;

public interface IRepoPlantilla
{
    // CRUD 
    List<Plantilla> GetPlantillas();
    Plantilla GetPlantilla(int idPlantilla);
    void InsertPlantilla(Plantilla plantilla);
    void UpdatePlantilla(Plantilla plantilla);
    void DeletePlantilla(int idPlantilla);

    // Métodos extendidos
    Plantilla? GetPlantillaCompleta(int idPlantilla);

    // Gestión de titulares y suplentes
    void AgregarTitular(int idPlantilla, int idFutbolista);
    void EliminarTitular(int idPlantilla, int idFutbolista);
    void AgregarSuplente(int idPlantilla, int idFutbolista);
    void EliminarSuplente(int idPlantilla, int idFutbolista);

    // Funciones de validación y cálculo
    decimal CalcularPresupuestoPlantilla(int idPlantilla);
    int ContarFutbolistasPlantilla(int idPlantilla);
    bool PlantillaEsValida(int idPlantilla);
    decimal CalcularPuntajeFutbolistaFecha(int idFutbolista, int fecha);
    decimal CalcularPuntajePlantillaFecha(int idPlantilla, int fecha);
}
