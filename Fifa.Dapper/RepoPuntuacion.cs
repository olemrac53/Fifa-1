using System.Data;
using Dapper;
using MySqlConnector;
using Fifa.Core;
using Fifa.Core.Repos;

namespace Fifa.Dapper;

public class RepoPuntuacion : Repo, IRepoPuntuacion
{
    public RepoPuntuacion(IDbConnection conexion) : base(conexion) { }

    #region Queries
    private static readonly string _queryPuntuaciones
        = @"SELECT  pf.id_puntuacion AS IdPuntuacion,
                    pf.fecha AS Fecha,
                    pf.puntuacion AS Puntuacion,
                    f.id_futbolista AS IdFutbolista,
                    f.nombre AS Nombre,
                    f.apellido AS Apellido,
                    f.apodo AS Apodo
            FROM PuntuacionFutbolista pf
            JOIN Futbolista f ON pf.id_futbolista = f.id_futbolista
            ORDER BY pf.fecha DESC";

    private static readonly string _queryPuntuacionesPorFutbolista
        = @"SELECT  id_puntuacion AS IdPuntuacion,
                    fecha AS Fecha,
                    puntuacion AS Puntuacion,
                    id_futbolista AS IdFutbolista
            FROM PuntuacionFutbolista
            WHERE id_futbolista = @id
            ORDER BY fecha";

    private static readonly string _queryPuntuacionesPorFecha
        = @"SELECT  pf.id_puntuacion AS IdPuntuacion,
                    pf.fecha AS Fecha,
                    pf.puntuacion AS Puntuacion,
                    f.id_futbolista AS IdFutbolista,
                    f.nombre AS Nombre,
                    f.apellido AS Apellido,
                    f.apodo AS Apodo,
                    e.id_equipo AS IdEquipo,
                    e.nombre AS Nombre
            FROM PuntuacionFutbolista pf
            JOIN Futbolista f ON pf.id_futbolista = f.id_futbolista
            JOIN Equipo e ON f.id_equipo = e.id_equipo
            WHERE pf.fecha = @fecha
            ORDER BY pf.puntuacion DESC";
    #endregion

    public List<PuntuacionFutbolista> GetPuntuaciones()
    {
        var puntuaciones = Conexion.Query<PuntuacionFutbolista, Futbolista, PuntuacionFutbolista>
            (_queryPuntuaciones,
            (puntuacion, futbolista) =>
            {
                puntuacion.Futbolista = futbolista;
                return puntuacion;
            },
            splitOn: "IdFutbolista")
            .ToList();

        return puntuaciones;
    }

    public List<PuntuacionFutbolista> GetPuntuacionesPorFutbolista(int idFutbolista)
    {
        return Conexion.Query<PuntuacionFutbolista>(
            _queryPuntuacionesPorFutbolista,
            new { id = idFutbolista }
        ).ToList();
    }

    public List<PuntuacionFutbolista> GetPuntuacionesPorFecha(int fecha)
    {
        var puntuaciones = Conexion.Query<PuntuacionFutbolista, Futbolista, Equipo, PuntuacionFutbolista>
            (_queryPuntuacionesPorFecha,
            (puntuacion, futbolista, equipo) =>
            {
                futbolista.Equipo = equipo;
                puntuacion.Futbolista = futbolista;
                return puntuacion;
            },
            new { fecha },
            splitOn: "IdFutbolista, IdEquipo")
            .ToList();

        return puntuaciones;
    }

    public void AltaPuntuacion(int idFutbolista, int fecha, decimal puntuacion)
    {
        var parametros = new
        {
            p_id_futbolista = idFutbolista,
            p_fecha = fecha,
            p_puntuacion = puntuacion
        };

        try
        {
            Conexion.Execute("AltaPuntuacion", parametros, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException e)
        {
            if (e.Message.Contains("no es titular"))
            {
                throw new InvalidOperationException("El futbolista debe ser titular en alguna plantilla para asignarle puntaje.");
            }
            if (e.Message.Contains("ya tiene una puntuación"))
            {
                throw new InvalidOperationException("El futbolista ya tiene una puntuación asignada para esta fecha.");
            }
            throw;
        }
    }

    public void ModificarPuntuacion(int idPuntuacion, decimal puntuacion)
    {
        var parametros = new
        {
            p_id_puntuacion = idPuntuacion,
            p_puntuacion = puntuacion
        };

        Conexion.Execute("ModificarPuntuacion", parametros, commandType: CommandType.StoredProcedure);
    }

    public void EliminarPuntuacion(int idPuntuacion)
    {
        var parametros = new { p_id_puntuacion = idPuntuacion };
        Conexion.Execute("EliminarPuntuacion", parametros, commandType: CommandType.StoredProcedure);
    }
}