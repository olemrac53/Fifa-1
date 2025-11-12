using System.Data;
using Dapper;
using Fifa.Core;
using Fifa.Core.Repos;
using System.Collections.Generic;
using System.Linq;

namespace Fifa.Dapper
{
    public class RepoTipo : Repo, IRepoTipo
    {
        private static readonly string _queryTipos = "SELECT id_tipo AS IdTipo, nombre AS Nombre FROM Tipo";

        public RepoTipo(IDbConnection conexion) : base(conexion) { }

        public List<Tipo> GetTipos()
        {
            return Conexion.Query<Tipo>(_queryTipos).ToList();
        }
    }
}