using System.Data;
using Dapper;
using MySqlConnector; 
using Fifa.Core;
using Fifa.Core.Repos;

namespace Fifa.Dapper;

public class RepoFutbolista : Repo, IRepoFutbolista
{
    public RepoFutbolista(IDbConnection conexion) : base(conexion) { }

    #region Queries
    private static readonly string _queryFutbolistas
        = @"SELECT  f.id_futbolista AS IdFutbolista,
                    f.nombre AS Nombre,
                    f.apellido AS Apellido,
                    f.apodo AS Apodo,
                    f.num_camisa AS NumCamisa,
                    f.fecha_nacimiento AS FechaNacimiento,
                    f.cotizacion AS Cotizacion,
                    e.id_equipo AS IdEquipo,
                    e.nombre AS Nombre,
                    t.id_tipo AS IdTipo,
                    t.nombre AS Nombre
            FROM    Futbolista f
            JOIN    Equipo e ON f.id_equipo = e.id_equipo
            JOIN    Tipo t ON f.id_tipo = t.id_tipo
            ORDER BY f.id_futbolista";

    private static readonly string _queryFutbolista
        = @"SELECT  f.id_futbolista AS IdFutbolista,
                    f.nombre AS Nombre,
                    f.apellido AS Apellido,
                    f.apodo AS Apodo,
                    f.num_camisa AS NumCamisa,
                    f.fecha_nacimiento AS FechaNacimiento,
                    f.cotizacion AS Cotizacion,
                    e.id_equipo AS IdEquipo,
                    e.nombre AS Nombre,
                    t.id_tipo AS IdTipo,
                    t.nombre AS Nombre
            FROM    Futbolista f
            JOIN    Equipo e ON f.id_equipo = e.id_equipo
            JOIN    Tipo t ON f.id_tipo = t.id_tipo
            WHERE   f.id_futbolista = @id";

    private static readonly string _queryTipos
        = @"SELECT id_tipo AS IdTipo, nombre AS Nombre FROM Tipo ORDER BY id_tipo";

    private static readonly string _queryTipo
        = @"SELECT id_tipo AS IdTipo, nombre AS Nombre FROM Tipo WHERE id_tipo = @id";
    #endregion

    #region Futbolista - CRUD
    public List<Futbolista> GetFutbolistas()
    {
        var futbolistas = Conexion.Query<Futbolista, Equipo, Tipo, Futbolista>
            (_queryFutbolistas,
            (futbolista, equipo, tipo) =>
            {
                futbolista.Equipo = equipo;
                futbolista.Tipo = tipo;
                return futbolista;
            },
            splitOn: "IdEquipo, IdTipo")
            .ToList();

        // Optimización: eliminar duplicados de Equipo y Tipo
        var equipos = futbolistas.Select(f => f.Equipo).Distinct().ToList();
        var tipos = futbolistas.Select(f => f.Tipo).Distinct().ToList();

        futbolistas.ForEach(f =>
        {
            f.Equipo = equipos.First(e => e.idEquipo == f.Equipo.idEquipo);  
            f.Tipo = tipos.First(t => t.idTipo == f.Tipo.idTipo);  
        });

        return futbolistas;
    }

    public Futbolista? GetFutbolista(int idFutbolista)
    {
        var futbolista = Conexion.Query<Futbolista, Equipo, Tipo, Futbolista>
            (_queryFutbolista,
            (futbolista, equipo, tipo) =>
            {
                futbolista.Equipo = equipo;
                futbolista.Tipo = tipo;
                return futbolista;
            },
            new { id = idFutbolista },
            splitOn: "IdEquipo, IdTipo")
            .FirstOrDefault();

        return futbolista;
    }

    public void InsertFutbolista(Futbolista futbolista)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@p_nombre", futbolista.Nombre);
        parametros.Add("@p_apellido", futbolista.Apellido);
        parametros.Add("@p_apodo", futbolista.Apodo);
        parametros.Add("@p_num_camisa", futbolista.NumCamisa);
        parametros.Add("@p_fecha_nacimiento", futbolista.FechaNacimiento); 
        parametros.Add("@p_cotizacion", futbolista.Cotizacion);  
        parametros.Add("@p_id_tipo", futbolista.Tipo.idTipo);  
        parametros.Add("@p_id_equipo", futbolista.Equipo.idEquipo);  

        try
        {
            Conexion.Execute("AltaFutbolista", parametros, commandType: CommandType.StoredProcedure);
            
            futbolista.IdFutbolista = Conexion.QuerySingle<int>("SELECT LAST_INSERT_ID()");
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
        parametros.Add("@p_id_futbolista", futbolista.IdFutbolista);  
        parametros.Add("@p_nombre", futbolista.Nombre);
        parametros.Add("@p_apellido", futbolista.Apellido);
        parametros.Add("@p_apodo", futbolista.Apodo);
        parametros.Add("@p_num_camisa", futbolista.NumCamisa);
        parametros.Add("@p_fecha_nacimiento", futbolista.FechaNacimiento);  
        parametros.Add("@p_cotizacion", futbolista.Cotizacion);  
        parametros.Add("@p_id_tipo", futbolista.Tipo.idTipo); 
        parametros.Add("@p_id_equipo", futbolista.Equipo.idEquipo);  

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
        return Conexion.Query<Tipo>(_queryTipos).ToList();
    }

    public Tipo? GetTipo(int idTipo)
    {
        return Conexion.QueryFirstOrDefault<Tipo>(_queryTipo, new { id = idTipo });
    }

    public void InsertTipo(Tipo tipo)
    {
        var parametros = new { p_nombre = tipo.nombre }; 
        
        try
        {
            Conexion.Execute("AltaTipo", parametros, commandType: CommandType.StoredProcedure);
            tipo.idTipo = Conexion.QuerySingle<int>("SELECT LAST_INSERT_ID()");  
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