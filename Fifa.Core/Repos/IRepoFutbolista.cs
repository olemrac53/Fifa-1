namespace Fifa.Core.Repos;

public interface IRepoFutbolista
{
    // Futbolistas - CRUD
    List<Futbolista> GetFutbolistas();
    Futbolista? GetFutbolista(int idFutbolista); 
    void InsertFutbolista(Futbolista futbolista);
    void UpdateFutbolista(Futbolista futbolista);  
    void DeleteFutbolista(int idFutbolista);

    // Tipos - CRUD
    List<Tipo> GetTipos();
    Tipo? GetTipo(int idTipo);
    void InsertTipo(Tipo tipo);
    void DeleteTipo(int idTipo);
}