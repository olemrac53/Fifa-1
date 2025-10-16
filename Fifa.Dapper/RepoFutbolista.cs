using System.Data;
using Dapper;
using Fifa.Core;
using Fifa.Core.Repos;

namespace Fifa.Dapper;

public class RepoFutbolista : Repo, IRepoFutbolista
{

    // W.I.P FALTA
    public RepoFutbolista(IDbConnection conexion) : base(conexion)
    {
    }

    public void DeleteFutbolista(int idFutbolista)
    {
        throw new NotImplementedException();
    }

    public void DeleteTipo(int idTipo)
    {
        throw new NotImplementedException();
    }

    public Futbolista getfutbolista(int idFutbolista)
    {
        throw new NotImplementedException();
    }

    public List<Futbolista> GetFutbolistas()
    {
        string query = @"SELECT * FROM Futbolista ORDER BY idFutbolista";
        var futbolistas = Conexion.Query<Futbolista>(query);
        return futbolistas.ToList();
    }

    public Tipo GetTipo(int idTipo)
    {
        throw new NotImplementedException();
    }

    public List<Tipo> GetTipos()
    {
        string query = @"SELECT * FROM Tipo ORDER BY idTipo";
        var tipos = Conexion.Query<Tipo>(query);
        return tipos.ToList();
    }

    public void InsertFutbolista(Futbolista futbolista)
    {
        throw new NotImplementedException();
    }

    public void InsertTipo(Tipo tipo)
    {
        throw new NotImplementedException();
    }

    public void updateFutbolista(Futbolista futbolista)
    {
        throw new NotImplementedException();
    }

    public void UpdateTipo(Tipo tipo)
    {
        throw new NotImplementedException();
    }
}