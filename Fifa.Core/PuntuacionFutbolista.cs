
using System;

namespace Fifa.Core;

public class PuntuacionFutbolista 
{
    public int IdPuntuacion { get; set; }
    public int Fecha { get; set; }
    public decimal Puntuacion { get; set; }

    public Futbolista Futbolista { get; set; } = null!;

    public override bool Equals(object? obj)
        => obj is PuntuacionFutbolista p && p.IdPuntuacion == IdPuntuacion;

    public override int GetHashCode()
        => IdPuntuacion.GetHashCode();

    public override string ToString()
        => $"Fecha {Fecha}: {Puntuacion} pts - {Futbolista.Nombre}";
}