namespace Fifa.Core;

public class Administrador
{
    public int IdAdministrador { get; set; }  
    public string Nombre { get; set; } = string.Empty;  
    public string Apellido { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }  

    public string NombreCompleto => $"{Nombre} {Apellido}";

    public override bool Equals(object? obj)
        => obj is Administrador a && a.IdAdministrador == IdAdministrador;

    public override int GetHashCode()
        => IdAdministrador.GetHashCode();

    public override string ToString()
        => $"{NombreCompleto} (Admin)";
}