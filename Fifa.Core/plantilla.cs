using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifa.Core
{
    public class Plantilla
    {
        public required int idPlantilla { get; set; }
        public required Usuario Usuario { get; set; }
        public required Administrador Administrador { get; set; }
        public decimal  presupuesto_max {get; set;}

    }
}
