using System.Data;
using MySqlConnector;

namespace Fifa.Dapper;

abstract public class Repo
{
    protected readonly IDbConnection Conexion;
    protected Repo(IDbConnection conexion) => Conexion = conexion;

}


public static class ConexionDB
{
    private const string CadenaConexion = 
        "Server=localhost;Port=3306;Database=GranET12;Uid=root;Pwd=tu_password;";

    public static IDbConnection CrearConexion()
    {
        return new MySqlConnection(CadenaConexion);
    }
}
