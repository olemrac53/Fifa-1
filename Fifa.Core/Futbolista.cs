

namespace Fifa.Core
{
    public class Futbolista
    {
        public required int idFutbolista { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Num_camiseta { get; set; }
        public required string Apodo { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public decimal cotizacion { get; set; }
        public required Equipo Equipo { get; set; }
        public required Tipo Tipo { get; set; }


    }
}
