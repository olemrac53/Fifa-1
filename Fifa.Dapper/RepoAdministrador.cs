using System.Data;
using Dapper;
using MySqlConnector; 
using Fifa.Core;
using Fifa.Core.Repos;

namespace Fifa.Dapper;
        
public class RepoAdministrador : Repo, IRepoAdministrador
{
    public RepoAdministrador(IDbConnection conexion) : base(conexion) { }

    #region Queries
    private static readonly string _queryAdministradores
        = @"SELECT  id_administrador AS IdAdministrador,
                    nombre AS Nombre,
                    apellido AS Apellido,
                    email AS Email,
                    fecha_nacimiento AS FechaNacimiento
            FROM Administrador
            ORDER BY apellido, nombre";

    private static readonly string _queryAdministrador
        = @"SELECT  id_administrador AS IdAdministrador,
                    nombre AS Nombre,
                    apellido AS Apellido,
                    email AS Email,
                    fecha_nacimiento AS FechaNacimiento
            FROM Administrador
            WHERE id_administrador = @id";

    private static readonly string _queryAdminPorEmail
        = @"SELECT  id_administrador AS IdAdministrador,
                    nombre AS Nombre,
                    apellido AS Apellido,
                    email AS Email,
                    fecha_nacimiento AS FechaNacimiento
            FROM Administrador
            WHERE email = @unEmail
            AND contrasenia = SHA2(@unaPass, 256)
            LIMIT 1";
    #endregion

    public List<Administrador> GetAdministradores()
    {
        return Conexion.Query<Administrador>(_queryAdministradores).ToList();
    }

    public Administrador? GetAdministrador(int idAdministrador)
    {
        return Conexion.QueryFirstOrDefault<Administrador>(_queryAdministrador, new { id = idAdministrador });
    }

    public Administrador? AdministradorPorEmailYPass(string email, string password)
    {
        return Conexion.QueryFirstOrDefault<Administrador>(
            _queryAdminPorEmail,
            new { unEmail = email, unaPass = password }
        );
    }

    public void InsertAdministrador(Administrador administrador, string password)
    {
        var parametros = new DynamicParameters();
        // CORREGIDO: Sin @ en los nombres de parámetros
        parametros.Add("p_nombre", administrador.Nombre);
        parametros.Add("p_apellido", administrador.Apellido);
        parametros.Add("p_email", administrador.Email);
        parametros.Add("p_fecha_nacimiento", administrador.FechaNacimiento);
        parametros.Add("p_contrasenia", password);
        parametros.Add("p_id_administrador", dbType: DbType.Int32, direction: ParameterDirection.Output);

        try
        {
            Conexion.Execute("AltaAdministrador", parametros, commandType: CommandType.StoredProcedure);
            administrador.IdAdministrador = parametros.Get<int>("p_id_administrador");
        }
        catch (MySqlException e)
        {
            if (e.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                throw new ConstraintException($"El email {administrador.Email} ya está registrado.");
            }
            throw;
        }
    }

    public void UpdateAdministrador(Administrador administrador, string password)
    {
        var parametros = new DynamicParameters();
        // CORREGIDO: Sin @ en los nombres de parámetros
        parametros.Add("p_id_administrador", administrador.IdAdministrador);
        parametros.Add("p_nombre", administrador.Nombre);
        parametros.Add("p_apellido", administrador.Apellido);
        parametros.Add("p_email", administrador.Email);
        parametros.Add("p_fecha_nacimiento", administrador.FechaNacimiento);
        parametros.Add("p_contrasenia", password);

        Conexion.Execute("ModificarAdministrador", parametros, commandType: CommandType.StoredProcedure);
    }

    public void DeleteAdministrador(int idAdministrador)
    {
        var parametros = new { p_id_administrador = idAdministrador };
        Conexion.Execute("EliminarAdministrador", parametros, commandType: CommandType.StoredProcedure);
    }
}