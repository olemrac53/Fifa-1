namespace Fifa.Core;

public class Usuario
{
    public int IdUsuario { get; set; } 
    public string Nombre { get; set; } = string.Empty;  
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }  
    
    public List<Plantilla> Plantillas { get; set; } = new();

    public string NombreCompleto => $"{Nombre} {Apellido}";

    public override bool Equals(object? obj)
        => obj is Usuario u && u.IdUsuario == IdUsuario;

    public override int GetHashCode()
        => IdUsuario.GetHashCode();

    public override string ToString()
        => $"{NombreCompleto} ({Email})";
}