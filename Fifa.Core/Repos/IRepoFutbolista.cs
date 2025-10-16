namespace Fifa.Core.Repos;

public interface IRepoFutbolista
{
    List<Futbolista> GetFutbolistas();
    Futbolista getfutbolista(int idFutbolista);
    void InsertFutbolista(Futbolista futbolista);
    void updateFutbolista(Futbolista futbolista);
    void DeleteFutbolista(int idFutbolista);
    List<Tipo> GetTipos();
    Tipo GetTipo(int idTipo);
    void InsertTipo(Tipo tipo);
    void UpdateTipo(Tipo tipo);
    void DeleteTipo(int idTipo);
}