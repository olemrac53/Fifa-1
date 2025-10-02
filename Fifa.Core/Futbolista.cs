namespace Fifa.Core
{
    internal class Futbolista
    {
        public required int idFutbolista { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string num_camiseta { get; set; }
        public string apodo { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public decimal cotizacion { get; set; }
        public required Equipo Equipo { get; set; }
        public required Tipo Tipo { get; set; }

    }
}
