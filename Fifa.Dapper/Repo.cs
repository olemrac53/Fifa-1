using System.Data;

namespace Fifa.Dapper;
abstract public class Repo
{
    protected readonly IDbConnection Conexion;
    protected Repo(IDbConnection conexion) => Conexion = conexion;
}