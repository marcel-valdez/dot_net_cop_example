using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.UX.Test
{
    public interface IBattlePlayerPresentation
    {

        string Username
        {
            get;
        }

        // Puntos de vida restantes
        int LifePoints
        {
            get;
        }

        // Es el texto del label de puntos de vida "HP: "
        string LifePointsTxt
        {
            get;
        }

    }
}
