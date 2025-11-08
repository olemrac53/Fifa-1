using System.Data;
using Dapper;
using MySqlConnector;
using Fifa.Core;
using Fifa.Core.Repos;

namespace Fifa.Dapper;

public class RepoUsuario : Repo, IRepoUsuario
{
    public RepoUsuario(IDbConnection conexion) : base(conexion) { }

    #region Queries
      private static readonly string _queryUsuarios
        = @"SELECT  id_usuario AS IdUsuario,
                nombre AS Nombre,
                apellido AS Apellido,
                email AS Email,
                fecha_nacimiento AS FechaNacimiento
        FROM Usuario
        ORDER BY apellido, nombre";

       private static readonly string _queryUsuario
       = @"SELECT  id_usuario AS IdUsuario,
                nombre AS Nombre,
                apellido AS Apellido,
                email AS Email,
                fecha_nacimiento AS FechaNacimiento
        FROM Usuario
        WHERE id_usuario = @id";

       private static readonly string _queryUsuarioPorEmail
        = @"SELECT  id_usuario AS IdUsuario,
                nombre AS Nombre,
                apellido AS Apellido,
                email AS Email,
                fecha_nacimiento AS FechaNacimiento
        FROM Usuario
        WHERE email = @unEmail
        AND contrasenia = SHA2(@unaPass, 256)
        LIMIT 1";

        private static readonly string _queryUsuarioConPlantillas
        = @"SELECT  u.id_usuario AS IdUsuario,
                u.nombre AS Nombre,
                u.apellido AS Apellido,
                u.email AS Email,
                u.fecha_nacimiento AS FechaNacimiento
        FROM Usuario u
        WHERE u.id_usuario = @id;

        SELECT  id_plantilla AS IdPlantilla,
                id_usuario AS IdUsuario,
                presupuesto_max AS PresupuestoMax,
                cant_max_futbolistas AS CantMaxFutbolistas
        FROM Plantilla
        WHERE id_usuario = @id";
    #endregion

    public List<Usuario> GetUsuarios()
    {
        return Conexion.Query<Usuario>(_queryUsuarios).ToList();
    }

    public Usuario? GetUsuario (int idUsuario)
    {
        return Conexion.QueryFirstOrDefault<Usuario>(_queryUsuario, new { id = idUsuario });
    }

    public Usuario? UsuarioPorEmailYPass(string email, string password)
    {
        return Conexion.QueryFirstOrDefault<Usuario>(
            _queryUsuarioPorEmail,
            new { unEmail = email, unaPass = password }
        );
    }

    public Usuario? GetUsuarioConPlantillas(int idUsuario)
    {
        using (var multi = Conexion.QueryMultiple(_queryUsuarioConPlantillas, new { id = idUsuario }))
        {
            var usuario = multi.ReadSingleOrDefault<Usuario>();
            if (usuario is not null)
            {
                usuario.Plantillas = multi.Read<Plantilla>().ToList();
                usuario.Plantillas.ForEach(p => p.Usuario = usuario);
            }
            return usuario;
        }
    }

    public void InsertUsuario(Usuario usuario, string password)
    {
        var parametros = new DynamicParameters();
        // Nombres sin @ para DynamicParameters
        parametros.Add("p_nombre", usuario.Nombre);
        parametros.Add("p_apellido", usuario.Apellido);
        parametros.Add("p_email", usuario.Email);
        parametros.Add("p_fecha_nacimiento", usuario.FechaNacimiento);
        parametros.Add("p_contrasenia", password);
        parametros.Add("p_id_usuario", dbType: DbType.Int32, direction: ParameterDirection.Output);

        try
        {
            Conexion.Execute("AltaUsuario", parametros, commandType: CommandType.StoredProcedure);
            usuario.IdUsuario = parametros.Get<int>("p_id_usuario");
        }
        catch (MySqlException e)
        {
            if (e.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                throw new ConstraintException($"El email {usuario.Email} ya está registrado.");
            }
            throw;
        }
    }

    public void UpdateUsuario(Usuario usuario, string password)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@p_id_usuario", usuario.IdUsuario);
        parametros.Add("@p_nombre", usuario.Nombre);
        parametros.Add("@p_apellido", usuario.Apellido);
        parametros.Add("@p_email", usuario.Email);
        parametros.Add("@p_fecha_nacimiento", usuario.FechaNacimiento);
        parametros.Add("@p_contrasenia", password);
        parametros.Add("@p_rol", "usuario");  // ← AGREGAR ESTA LÍNEA con un valor por defecto


        Conexion.Execute("ModificarUsuario", parametros, commandType: CommandType.StoredProcedure);
    }

    public void DeleteUsuario(int idUsuario)
    {
        var parametros = new { p_id_usuario = idUsuario };
        Conexion.Execute("EliminarUsuario", parametros, commandType: CommandType.StoredProcedure);
    }

}