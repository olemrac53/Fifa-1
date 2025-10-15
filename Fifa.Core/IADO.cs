namespace Fifa.Core;

public interface IADO
{
    void AltaEquipo(Equipo equipo);
    List<Equipo> GetEquipos();
    Equipo GetEquipo(int idEquipo);
    void UpdateEquipo(Equipo equipo);
    void DeleteEquipo(int idEquipo);


    List<Futbolista> GetFutbolistas();
    Futbolista getfutbolista(int idFutbolista);
    void InsertFutbolista(Futbolista futbolista);
    void updateFutbolista(Futbolista futbolista);
    void DeleteFutbolista(int idFutbolista);

    List<Plantilla> GetPlantillas();
    Plantilla GetPlantilla(int idPlantilla);
    void InsertPlantilla(Plantilla plantilla);
    void UpdatePlantilla(Plantilla plantilla);
    void DeletePlantilla(int idPlantilla);

    List<Usuario> GetUsuarios();
    Usuario GetUsuario(int idUsuario);
    void InsertUsuario(Usuario usuario);
    void UpdateUsuario(Usuario usuario);
    void DeleteUsuario(int idUsuario);

    List<Administrador> GetAdministradores();
    Administrador GetAdministrador(int idAdministrador);
    void InsertAdministrador(Administrador administrador);
    void UpdateAdministrador(Administrador administrador);
    void DeleteAdministrador(int idAdministrador);

    List<Tipo> GetTipos();
    Tipo GetTipo(int idTipo);
    void InsertTipo(Tipo tipo);
    void UpdateTipo(Tipo tipo);
    void DeleteTipo(int idTipo);

    
}
