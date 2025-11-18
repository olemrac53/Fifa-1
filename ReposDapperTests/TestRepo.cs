using System.Data;
using MySqlConnector;

namespace Fifa.Test;
/// El objetivo de esta clase es brindar una instancia de Ado para los test
public class TestRepo
{
    protected readonly IDbConnection _conexion;
    private const string _cadena = "Server=localhost;Database=5to_GranET12;Uid=root;pwd=root;Allow User Variables=True";
    public TestRepo() => _conexion = new MySqlConnection(_cadena);
    public TestRepo(string cadena) => _conexion = new MySqlConnection(cadena);
}