namespace Fifa.Core
{
    public class Puntaje
    {
        public required int idPuntaje {get; set;}
        public required Datetime fecha {get; set;}
        public required Decimal puntaje_total {get; set;}
        public required Futbolista Futbolista {get; set;}

    }
}
