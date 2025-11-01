using System.Data;
using Dapper;
using MySqlConnector; 
using Fifa.Core;
using Fifa.Core.Repos;

namespace Fifa.Dapper
{
    public class RepoEquipo : Repo, IRepoEquipo
    {
        // === QUERIES ===

        private static readonly string _queryEquipos = 
            @"SELECT id_equipo AS IdEquipo, 
                     nombre AS Nombre, 
                     presupuesto AS Presupuesto 
              FROM Equipo
              ORDER BY nombre";

        private static readonly string _queryEquipo = 
            @"SELECT id_equipo AS IdEquipo, 
                     nombre AS Nombre, 
                     presupuesto AS Presupuesto 
              FROM Equipo
              WHERE id_equipo = @id";

        private static readonly string _queryEquipoConFutbolistas = 
            @"SELECT id_equipo AS IdEquipo, 
                     nombre AS Nombre, 
                     presupuesto AS Presupuesto 
              FROM Equipo
              WHERE id_equipo = @id;

              SELECT  f.id_futbolista AS IdFutbolista,
                      f.nombre AS Nombre,
                      f.apellido AS Apellido,
                      f.apodo AS Apodo,
                      f.num_camisa AS NumCamisa,
                      f.cotizacion AS Cotizacion,
                      f.fecha_nacimiento AS FechaNacimiento,
                      t.id_tipo AS IdTipo,
                      t.nombre AS Nombre
              FROM Futbolista f
              JOIN Tipo t ON f.id_tipo = t.id_tipo
              WHERE f.id_equipo = @id
              ORDER BY f.apellido, f.nombre";

        
        // === CONSTRUCTOR ===

        public RepoEquipo(IDbConnection conexion) : base(conexion) { }

        // === IMPLEMENTACIÓN DE MÉTODOS ===

        public List<Equipo> GetEquipos()
        {
            return Conexion.Query<Equipo>(_queryEquipos).ToList();
        }

        public Equipo? GetEquipo(int idEquipo)
        {
            return Conexion.QueryFirstOrDefault<Equipo>(_queryEquipo, new { id = idEquipo });
        }

        public Equipo? GetEquipoConFutbolistas(int idEquipo)
        {
            using (var multi = Conexion.QueryMultiple(_queryEquipoConFutbolistas, new { id = idEquipo }))
            {
                // Leer el equipo
                var equipo = multi.ReadSingleOrDefault<Equipo>();
                
                if (equipo is not null)
                {
                    // Leer los futbolistas (con su Tipo)
                    equipo.Futbolistas = multi.Read<Futbolista, Tipo, Futbolista>
                        ((f, t) => 
                        {
                            f.Tipo = t;
                            f.Equipo = equipo; // Asignamos el equipo que ya leímos
                            return f;
                        }, 
                        splitOn: "IdTipo")
                        .ToList();
                }
                return equipo;
            }
        }

        public void InsertEquipo(Equipo equipo)
        {
            // Usamos el SP 'AltaEquipo' de tu SP.sql
            // Nota: Tu SP 'AltaEquipo' solo inserta 'nombre'.
            var parametros = new DynamicParameters();
            parametros.Add("p_nombre", equipo.Nombre);
            parametros.Add("p_id_equipo", dbType: DbType.Int32, direction: ParameterDirection.Output);

            Conexion.Execute("AltaEquipo", parametros, commandType: CommandType.StoredProcedure);
            
            // Asignamos el ID devuelto por el SP al objeto
            equipo.IdEquipo = parametros.Get<int>("p_id_equipo");
        }

        public void UpdateEquipo(Equipo equipo)
        {
            // Usamos el SP 'ModificarEquipo' de tu SP.sql
            // Nota: Tu SP 'ModificarEquipo' solo actualiza 'nombre'.
            var parametros = new 
            {
                p_id_equipo = equipo.IdEquipo,
                p_nombre = equipo.Nombre
            };

            Conexion.Execute("ModificarEquipo", parametros, commandType: CommandType.StoredProcedure);
        }

        public void DeleteEquipo(int idEquipo)
        {
            // Usamos el SP 'EliminarEquipo' de tu SP.sql
            var parametros = new { p_id_equipo = idEquipo };
            Conexion.Execute("EliminarEquipo", parametros, commandType: CommandType.StoredProcedure);
        }
    }
}