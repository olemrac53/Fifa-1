using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifa.Core
{
    public interface IADO
    {
        List<Equipo> GetEquipos();
        Equipo GetEquipo(int idEquipo);
        void InsertEquipo(Equipo equipo);
        void UpdateEquipo(Equipo equipo);
        void DeleteEquipo(int idEquipo);


        List<Futbolista> GetFutbolistas();
        Futbolista getfutbolista(int idFutbolista);
        void InsertFutbolista(Futbolista futbolista);
        void updateFutbolista(Futbolista futbolista);
        void DeleteFutbolista(int idFutbolista);

        List<Plantilla> GetPlantillas();
        Plantilla GetPlantilla(int idPlantilla);
        void InsertPlantilla(Plantilla plantilla);
        void UpdatePlantilla()
    }
}
