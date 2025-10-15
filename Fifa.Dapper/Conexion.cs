using System.Windows.Forms;
using System.Data;
using MySqlConnector;
using System;
using MySql.Data.MySqlClient;
using Dapper;
using Fifa.Core;


namespace fifa.Dapper
{
    class Cconexion
    {
        IDbConnection conex = new MySql.Data.MySqlClient.MySqlConnection();

        static string servidor = "Localhost";
        static string bd = "prueba2";
        static string usuario = "root";
        static string password = "Elias5314";
        static string puerto = "3306";

        string cadenaConexion = "server=" + servidor + ";" + "port=" + puerto + ";" + "user id=" + usuario + ";" + "password=" + password + ";" + "database=" + bd + ";";

        public IDbConnection EstablecerConexion()
        {

            {
                try
                {
                    conex.ConnectionString = cadenaConexion;
                    conex.Open();
                    MessageBox.Show("SE LOGRO CONECTAR A LA BASE DE DATOS");

                }

                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    MessageBox.Show("No se logro conectar, error: " + e.ToString());
                }
            }
            return conex;
        }
        public void cerrarConexion()
        {
            conex.Close();
        }
    }
}
