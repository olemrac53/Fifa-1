using System.Data;
using Dapper;
using MySqlConnector;
using Fifa.Core;
using Fifa.Core.Repos;

namespace Fifa.Dapper;

public class RepoPlantilla : Repo, IRepoPlantilla
{
    public RepoPlantilla(IDbConnection conexion) : base(conexion) { }

    #region Queries
    private static readonly string _queryPlantillas
        = @"SELECT  p.id_plantilla AS IdPlantilla,
                    p.presupuesto_max AS PresupuestoMax,
                    p.cant_max_futbolistas AS CantMaxFutbolistas,
                    u.id_usuario AS IdUsuario,
                    u.nombre AS Nombre,
                    u.apellido AS Apellido,
                    u.email AS Email
            FROM Plantilla p
            JOIN Usuario u ON p.id_usuario = u.id_usuario
            ORDER BY p.id_plantilla";

    private static readonly string _queryPlantilla
        = @"SELECT  p.id_plantilla AS IdPlantilla,
                    p.presupuesto_max AS PresupuestoMax,
                    p.cant_max_futbolistas AS CantMaxFutbolistas,
                    u.id_usuario AS IdUsuario,
                    u.nombre AS Nombre,
                    u.apellido AS Apellido,
                    u.email AS Email
            FROM Plantilla p
            JOIN Usuario u ON p.id_usuario = u.id_usuario
            WHERE p.id_plantilla = @id";

    private static readonly string _queryPlantillaCompleta
        = @"SELECT  p.id_plantilla AS IdPlantilla,
                    p.presupuesto_max AS PresupuestoMax,
                    p.cant_max_futbolistas AS CantMaxFutbolistas,
                    u.id_usuario AS IdUsuario,
                    u.nombre AS Nombre,
                    u.apellido AS Apellido,
                    u.email AS Email
            FROM Plantilla p
            JOIN Usuario u ON p.id_usuario = u.id_usuario
            WHERE p.id_plantilla = @id;

            SELECT  f.id_futbolista AS IdFutbolista,
                    f.nombre AS Nombre,
                    f.apellido AS Apellido,
                    f.apodo AS Apodo,
                    f.num_camisa AS NumCamisa,
                    f.cotizacion AS Cotizacion,
                    t.id_tipo AS IdTipo,
                    t.nombre AS Nombre,
                    e.id_equipo AS IdEquipo,
                    e.nombre AS Nombre
            FROM PlantillaTitular pt
            JOIN Futbolista f ON pt.id_futbolista = f.id_futbolista
            JOIN Tipo t ON f.id_tipo = t.id_tipo
            JOIN Equipo e ON f.id_equipo = e.id_equipo
            WHERE pt.id_plantilla = @id;

            SELECT  f.id_futbolista AS IdFutbolista,
                    f.nombre AS Nombre,
                    f.apellido AS Apellido,
                    f.apodo AS Apodo,
                    f.num_camisa AS NumCamisa,
                    f.cotizacion AS Cotizacion,
                    t.id_tipo AS IdTipo,
                    t.nombre AS Nombre,
                    e.id_equipo AS IdEquipo,
                    e.nombre AS Nombre
            FROM PlantillaSuplente ps
            JOIN Futbolista f ON ps.id_futbolista = f.id_futbolista
            JOIN Tipo t ON f.id_tipo = t.id_tipo
            JOIN Equipo e ON f.id_equipo = e.id_equipo
            WHERE ps.id_plantilla = @id";
    #endregion

    #region Plantilla - CRUD
    public List<Plantilla> GetPlantillas()
    {
        var plantillas = Conexion.Query<Plantilla, Usuario, Plantilla>
            (_queryPlantillas,
            (p, u) =>
            {
                p.Usuario = u;
                return p;
            },
            splitOn: "IdUsuario")
            .ToList();

        return plantillas;
    }

    public Plantilla GetPlantilla(int idPlantilla)
    {
        var plantilla = Conexion.Query<Plantilla, Usuario, Plantilla>
            (_queryPlantilla,
            (p, u) =>
            {
                p.Usuario = u;
                return p;
            },
            new { id = idPlantilla },
            splitOn: "IdUsuario")
            .FirstOrDefault();

        if (plantilla == null)
            throw new InvalidOperationException($"No se encontró la plantilla con id {idPlantilla}.");

        return plantilla;
    }

    public Plantilla? GetPlantillaCompleta(int idPlantilla)
    {
        using (var multi = Conexion.QueryMultiple(_queryPlantillaCompleta, new { id = idPlantilla }))
        {
            var plantilla = multi.Read<Plantilla, Usuario, Plantilla>
                ((p, u) =>
                {
                    p.Usuario = u;
                    return p;
                },
                splitOn: "IdUsuario")
                .FirstOrDefault();

            if (plantilla is null)
                return null;

            // Leer titulares
            plantilla.Titulares = multi.Read<Futbolista, Tipo, Equipo, Futbolista>
                ((f, t, e) =>
                {
                    f.Tipo = t;
                    f.Equipo = e;
                    return f;
                },
                splitOn: "IdTipo, IdEquipo")
                .ToList();

            // Leer suplentes
            plantilla.Suplentes = multi.Read<Futbolista, Tipo, Equipo, Futbolista>
                ((f, t, e) =>
                {
                    f.Tipo = t;
                    f.Equipo = e;
                    return f;
                },
                splitOn: "IdTipo, IdEquipo")
                .ToList();

            return plantilla;
        }
    }

    public void InsertPlantilla(Plantilla plantilla)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@p_id_usuario", plantilla.Usuario.IdUsuario);
        parametros.Add("@p_presupuesto_max", plantilla.PresupuestoMax);
        parametros.Add("@p_cant_max_futbolistas", plantilla.CantMaxFutbolistas);
        parametros.Add("@p_id_plantilla", dbType: DbType.Int32, direction: ParameterDirection.Output);


        Conexion.Execute("CrearPlantilla", parametros, commandType: CommandType.StoredProcedure);
        plantilla.IdPlantilla = Conexion.QuerySingle<int>("SELECT LAST_INSERT_ID()");
        plantilla.IdPlantilla = parametros.Get<int>("@p_id_plantilla");

        
        ;
    }

    public void UpdatePlantilla(Plantilla plantilla)
    {
        var parametros = new
        {
            p_id_plantilla = plantilla.IdPlantilla,
            p_presupuesto_max = plantilla.PresupuestoMax,
            p_cant_max_futbolistas = plantilla.CantMaxFutbolistas
        };

        Conexion.Execute("ModificarPlantilla", parametros, commandType: CommandType.StoredProcedure);
    }

    public void DeletePlantilla(int idPlantilla)
    {
        var parametros = new { p_id_plantilla = idPlantilla };
        Conexion.Execute("EliminarPlantilla", parametros, commandType: CommandType.StoredProcedure);
    }
    #endregion

    #region Titulares y Suplentes
    public void AgregarTitular(int idPlantilla, int idFutbolista)
    {
        var parametros = new
        {
            p_id_plantilla = idPlantilla,
            p_id_futbolista = idFutbolista
        };

        try
        {
            Conexion.Execute("AltaTitular", parametros, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException e)
        {
            if (e.Message.Contains("ya está en suplentes"))
            {
                throw new InvalidOperationException("El futbolista ya está registrado como suplente en esta plantilla.");
            }
            if (e.Message.Contains("excede presupuesto"))
            {
                throw new InvalidOperationException("Agregar este titular excede el presupuesto de la plantilla.");
            }
            if (e.Message.Contains("máximo"))
            {
                throw new InvalidOperationException("La plantilla ya tiene la cantidad máxima de futbolistas.");
            }
            throw;
        }
    }

    public void EliminarTitular(int idPlantilla, int idFutbolista)
    {
        var parametros = new
        {
            p_id_plantilla = idPlantilla,
            p_id_futbolista = idFutbolista
        };

        Conexion.Execute("EliminarTitular", parametros, commandType: CommandType.StoredProcedure);
    }

    public void AgregarSuplente(int idPlantilla, int idFutbolista)
    {
        var parametros = new
        {
            p_id_plantilla = idPlantilla,
            p_id_futbolista = idFutbolista
        };

        try
        {
            Conexion.Execute("AltaSuplente", parametros, commandType: CommandType.StoredProcedure);
        }
        catch (MySqlException e)
        {
            if (e.Message.Contains("ya está en titulares"))
            {
                throw new InvalidOperationException("El futbolista ya está registrado como titular en esta plantilla.");
            }
            if (e.Message.Contains("excede presupuesto"))
            {
                throw new InvalidOperationException("Agregar este suplente excede el presupuesto de la plantilla.");
            }
            if (e.Message.Contains("máximo"))
            {
                throw new InvalidOperationException("La plantilla ya tiene la cantidad máxima de futbolistas.");
            }
            throw;
        }
    }

    public void EliminarSuplente(int idPlantilla, int idFutbolista)
    {
        var parametros = new
        {
            p_id_plantilla = idPlantilla,
            p_id_futbolista = idFutbolista
        };

        Conexion.Execute("EliminarSuplente", parametros, commandType: CommandType.StoredProcedure);
    }
    #endregion

    #region Funciones de Validación y Puntaje
    public decimal CalcularPresupuestoPlantilla(int idPlantilla)
    {
        return Conexion.QuerySingle<decimal>(
            "SELECT PresupuestoPlantilla(@id)",
            new { id = idPlantilla }
        );
    }

    public int ContarFutbolistasPlantilla(int idPlantilla)
    {
        return Conexion.QuerySingle<int>(
            "SELECT CantidadFutbolistasPlantilla(@id)",
            new { id = idPlantilla }
        );
    }

    public bool PlantillaEsValida(int idPlantilla)
    {
        return Conexion.QuerySingle<bool>(
            "SELECT PlantillaEsValida(@id)",
            new { id = idPlantilla }
        );
    }

    public decimal CalcularPuntajeFutbolistaFecha(int idFutbolista, int fecha)
    {
        return Conexion.QuerySingle<decimal>(
            "SELECT PuntajeFutbolistaFecha(@idFut, @fecha)",
            new { idFut = idFutbolista, fecha }
        );
    }

    public decimal CalcularPuntajePlantillaFecha(int idPlantilla, int fecha)
    {
        return Conexion.QuerySingle<decimal>(
            "SELECT PuntajePlantillaFecha(@idPlant, @fecha)",
            new { idPlant = idPlantilla, fecha }
        );
    }
    #endregion
}