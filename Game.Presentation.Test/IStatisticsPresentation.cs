using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.UX.Test
{
    public interface IStatisticsPresentation
    {
        string Username
        {
            get;
        }

        // El texto label de "Partidas Jugadas"
        string PlayedGamesLabelText
        {
            get;
        }

        // El número de partidas jugadas
        int PlayedGamesCount
        {
            get;
        }

        // El texto del label de "Partidas Ganadas"
        string WonGamesLabelText
        {
            get;
        }

        // El número de partidas ganadas
        int WonGamesCount
        {
            get;
        }

        // El texto del label de "Partidas Perdidas"
        string LostGamesLabelText
        {
            get;
        }

        // El número de partidas perdidas
        int LostGamesCount
        {
            get;
        }
    }
}
