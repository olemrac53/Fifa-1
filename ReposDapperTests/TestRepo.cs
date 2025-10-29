using System.Data;
using MySqlConnector;

namespace Fifa.Test;
/// <summary>
/// El objetivo de esta clase es brindar una instancia de Ado para los test
/// </summary>
public class TestRepo
{
    protected readonly IDbConnection _conexion;
    private const string _cadena = "Server=localhost;Database=GranET12;Uid=5to_agbd;pwd=Trigg3rs!;Allow User Variables=True";
    public TestRepo() => _conexion = new MySqlConnection(_cadena);
    public TestRepo(string cadena) => _conexion = new MySqlConnection(cadena);
}