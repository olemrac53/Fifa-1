namespace Fifa.Core
{
    public class PlantillaJugador
    {
        public required int idPlantillaJugador { get; set; }
        public required Plantilla Plantilla { get; set; }
        public required Futbolista Futbolista { get; set; }
    }
}
