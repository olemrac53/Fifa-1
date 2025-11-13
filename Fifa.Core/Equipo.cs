using System.Collections.Generic; // <-- Es para la lista, no lo borren porfa

namespace Fifa.Core
{
    public class Equipo
    {
        public int IdEquipo { get; set; } 

        public string Nombre { get; set; }

        public decimal Presupuesto { get; set; }

        public List<Futbolista> Futbolistas { get; set; } = new List<Futbolista>();
    }
}