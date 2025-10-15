namespace Fifa.Core
{
    public class PlantillaSuplente
    {
        public required int idPlantillaSuplente { get; set; }
        public required Plantilla Plantilla { get; set; }
        public required Futbolista Futbolista { get; set; }
    }
}
