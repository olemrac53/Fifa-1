namespace Fifa.Core.Repos
{

    public interface IRepoPuntuacion
    {
        List<PuntuacionFutbolista> GetPuntuaciones();
        List<PuntuacionFutbolista> GetPuntuacionesPorFutbolista(int idFutbolista);
        List<PuntuacionFutbolista> GetPuntuacionesPorFecha(int fecha);
        void AltaPuntuacion(int idFutbolista, int fecha, decimal puntuacion);
        void ModificarPuntuacion(int idPuntuacion, decimal puntuacion);
        void EliminarPuntuacion(int idPuntuacion);
    }

}