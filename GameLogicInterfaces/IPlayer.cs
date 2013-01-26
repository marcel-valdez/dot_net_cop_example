using System;
namespace Game.Logic
{
    public interface IPlayer
    {
        /*
            El nombre de usuario
        */
        string Username
        {
            get;
        }

        /*
            Puntos de vida restantes
        */
        int LifePoints
        {
            get;
        }

        /*
            Cartas disponibles en la batalla
        */
        IBattleDeck Deck
        {
            get;
        }

        /*
            Determina si en los c�lculos de ataques, este usuario ser� el primero en atacar o segundo.
            { FirstToAttack | SecondToAttack }
        */
        PlayerTurn Turn
        {
            get;
        }
    }

    public enum PlayerTurn
    {
        FirstToAttack = 0,
        SecondToAttack = 1
    }
}