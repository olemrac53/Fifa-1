using System.Data;
using Dapper;
using MySqlConnector; 
using Fifa.Core;
using Fifa.Core.Repos;

namespace Fifa.Dapper;

public class RepoFutbolista : Repo, IRepoFutbolista
{
    
    public RepoFutbolista(IDbConnection conexion) : base(conexion)
    {
    }

    #region Queries
    private static readonly string _queryFutbolistas =
        @"SELECT  f.id_futbolista AS IdFutbolista,
                f.nombre AS Nombre,
                f.apellido AS Apellido,
                f.apodo AS Apodo,
                f.num_camisa AS NumCamisa,
                f.fecha_nacimiento AS FechaNacimiento,
                f.cotizacion AS Cotizacion,
                e.id_equipo AS idEquipo,
                e.nombre AS Nombre,
                t.id_tipo AS idTipo,
                t.nombre AS nombre
        FROM Futbolista f
        JOIN Equipo e ON f.id_equipo = e.id_equipo
        JOIN Tipo t ON f.id_tipo = t.id_tipo
        ORDER BY f.id_futbolista;";

    private static readonly string _queryFutbolista =
    @"SELECT  f.id_futbolista AS IdFutbolista,
              f.nombre AS Nombre,
              f.apellido AS Apellido,
              f.apodo AS Apodo,
              f.num_camisa AS NumCamisa,
              f.fecha_nacimiento AS FechaNacimiento,
              f.cotizacion AS Cotizacion,
              e.id_equipo AS idEquipo,
              e.nombre AS Nombre,
              t.id_tipo AS idTipo,
              t.nombre AS nombre
      FROM Futbolista f
      JOIN Equipo e ON f.id_equipo = e.id_equipo
      JOIN Tipo t ON f.id_tipo = t.id_tipo
      WHERE f.id_futbolista = @id;";
    #endregion

    #region Futbolista - CRUD
    public List<Futbolista> GetFutbolistas()
    {
        return Conexion.Query<Futbolista, Equipo, Tipo, Futbolista>(
            _queryFutbolistas,
            (f, e, t) =>
            {
                e.Nombre = e.Nombre ?? string.Empty;
                t.nombre = t.nombre ?? string.Empty;
                f.Equipo = e;
                f.Tipo = t;
                return f;
            },
            splitOn: "IdEquipo,IdTipo"
        ).ToList();
    }

    public Futbolista? GetFutbolista(int idFutbolista)
    {
        return Conexion.Query<Futbolista, Equipo, Tipo, Futbolista>(
            _queryFutbolista,
            (f, e, t) =>
            {
                f.Equipo = e;
                f.Tipo = t;
                return f;
            },
            new { id = idFutbolista },
            splitOn: "IdEquipo,IdTipo"
        ).FirstOrDefault();
    }

    public void InsertFutbolista(Futbolista futbolista)
    {
        var parametros = new DynamicParameters();
        
        // CORREGIDO: Sin @ en los nombres de parámetros
        parametros.Add("p_nombre", futbolista.Nombre);
        parametros.Add("p_apellido", futbolista.Apellido);
        parametros.Add("p_apodo", futbolista.Apodo);
        parametros.Add("p_num_camisa", futbolista.NumCamisa);
        parametros.Add("p_fecha_nacimiento", futbolista.FechaNacimiento);
        parametros.Add("p_cotizacion", futbolista.Cotizacion);
        parametros.Add("p_id_tipo", futbolista.Tipo?.idTipo ?? 0);
        parametros.Add("p_id_equipo", futbolista.Equipo?.IdEquipo ?? 0);

        // Parámetro OUT
        parametros.Add("p_id_futbolista", dbType: DbType.Int32, direction: ParameterDirection.Output);

        try
        {
            Conexion.Execute("AltaFutbolista", parametros, commandType: CommandType.StoredProcedure);
            futbolista.IdFutbolista = parametros.Get<int>("p_id_futbolista");
        }
        catch (MySqlException e)
        {
            if (e.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                throw new ConstraintException($"El futbolista {futbolista.Nombre} {futbolista.Apellido} ya existe.");
            }
            throw;
        }
    }

    public void UpdateFutbolista(Futbolista futbolista)
    {
        var parametros = new DynamicParameters();
        
        // CORREGIDO: Sin @ en los nombres de parámetros (consistente con Insert)
        parametros.Add("p_id_futbolista", futbolista.IdFutbolista);
        parametros.Add("p_nombre", futbolista.Nombre);
        parametros.Add("p_apellido", futbolista.Apellido);
        parametros.Add("p_apodo", futbolista.Apodo);
        parametros.Add("p_num_camisa", futbolista.NumCamisa);
        parametros.Add("p_fecha_nacimiento", futbolista.FechaNacimiento);
        parametros.Add("p_cotizacion", futbolista.Cotizacion);
        parametros.Add("p_id_tipo", futbolista.Tipo?.idTipo ?? 0);
        parametros.Add("p_id_equipo", futbolista.Equipo?.IdEquipo ?? 0);

        Conexion.Execute("ModificarFutbolista", parametros, commandType: CommandType.StoredProcedure);
    }

    public void DeleteFutbolista(int idFutbolista)
    {
        var parametros = new { p_id_futbolista = idFutbolista };
        Conexion.Execute("EliminarFutbolista", parametros, commandType: CommandType.StoredProcedure);
    }
    #endregion

    #region Tipo - CRUD
    public List<Tipo> GetTipos()
    {
        return Conexion.Query<Tipo>("SELECT id_tipo AS idTipo, nombre AS Nombre FROM Tipo ORDER BY id_tipo;").ToList();
    }

    public Tipo? GetTipo(int idTipo)
    {
        return Conexion.QueryFirstOrDefault<Tipo>(
            "SELECT id_tipo AS idTipo, nombre AS Nombre FROM Tipo WHERE id_tipo = @id;",
            new { id = idTipo });
    }

    public void InsertTipo(Tipo tipo)
    {
        var parametros = new DynamicParameters();
        parametros.Add("p_nombre", tipo.nombre);
        parametros.Add("p_id_tipo", dbType: DbType.Int32, direction: ParameterDirection.Output);

        try
        {
            Conexion.Execute("AltaTipo", parametros, commandType: CommandType.StoredProcedure);
            tipo.idTipo = parametros.Get<int>("p_id_tipo");
        }
        catch (MySqlException e)
        {
            if (e.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                throw new ConstraintException($"El tipo {tipo.nombre} ya existe.");
            }
            throw;
        }
    }

    public void DeleteTipo(int idTipo)
    {
        var parametros = new { p_id_tipo = idTipo };
        Conexion.Execute("EliminarTipo", parametros, commandType: CommandType.StoredProcedure);
    }
    #endregion
}