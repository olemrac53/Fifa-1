namespace Fifa.Core;

public class Futbolista
{
    public int IdFutbolista { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string? Apodo { get; set; }
    public string? NumCamisa { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public decimal Cotizacion { get; set; }

    public Tipo Tipo { get; set; } = null!;
    public Equipo Equipo { get; set; } = null!;

    // CORRECCIÓN INTEGRAL DE DISPLAY: Propiedad computada
    public string NombreCompleto
    {
        get { return $"{Apellido}, {Nombre}"; }
    }
}