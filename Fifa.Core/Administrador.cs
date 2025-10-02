namespace Fifa.Core
{
    public class Administrador
    {
        public required int idAdministrador { get; set; }
        public required string nombre { get; set; }
        public required string apellido { get; set; }
        public required string email { get; set; }
        public required DateTime fechaNacimiento { get; set; }
        public required string password { get; set; }
    }
}
