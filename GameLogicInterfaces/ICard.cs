using System;
namespace Game.Logic
{
    public interface ICard
    {
        /*
            Es el nombre de la carta
        */
        string Name
        {
            get;
        }

        /*
            Puntos de ataque de la carta
        */
        int AttackPoints
        {
            get;
        }

        /*
            Puntos de defensa de la carta
        */
        int DefensePoints
        {
            get;
        }

        /*
            Efecto especial de la carta
        */
        IEffect Effect
        {
            get;
        }

        /*
            URL de la imagen de la carta
        */
        string ImageUrl
        {
            get;
        }
    }
}