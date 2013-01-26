using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.UX.Test
{
    public interface IBattleCardPresentation
    {

        string CardName
        {
            get;
        }

        // Url de la imagen de la carta
        string ImageUrl
        {
            get;
        }

        // Puntos de ataque de la carta
        int AttackPoints
        {
            get;
        }

        // Puntos de defensa de la carta
        int DefensePoints
        {
            get;
        }

        // Determina si la carta ha sido seleccionada
        bool Selected
        {
            set;
        }

        // Determina si la carta puede ser seleccionada o no
        bool Selectable
        {
            get;
        }

    }
}
