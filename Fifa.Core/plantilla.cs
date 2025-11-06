using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifa.Core;

public class Plantilla
{
    public int IdPlantilla { get; set; }
    public decimal PresupuestoMax { get; set; }
    public int CantMaxFutbolistas { get; set; }

    public Usuario Usuario { get; set; } = null!;

    public List<Futbolista> Titulares { get; set; } = new();
    public List<Futbolista> Suplentes { get; set; } = new();

    public int TotalFutbolistas => Titulares.Count + Suplentes.Count;

    public decimal PresupuestoUtilizado
        => Titulares.Sum(f => f.Cotizacion) + Suplentes.Sum(f => f.Cotizacion);

    public decimal PresupuestoDisponible
        => PresupuestoMax - PresupuestoUtilizado;

    public bool EstaCompleta => TotalFutbolistas >= CantMaxFutbolistas;

    public override bool Equals(object? obj)
        => obj is Plantilla p && p.IdPlantilla == IdPlantilla;

    public override int GetHashCode()
        => IdPlantilla.GetHashCode();

    public override string ToString()
        => $"Plantilla #{IdPlantilla} - {Usuario.NombreCompleto}";
}