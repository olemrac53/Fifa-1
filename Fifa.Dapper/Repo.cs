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
        "Server=localhost;Port=3306;Database=5to_GranET12;Uid=5to_agbd;Pwd=Trigg3rs!;";

    public static IDbConnection CrearConexion()
    {
        return new MySqlConnection(CadenaConexion);
    }
}
