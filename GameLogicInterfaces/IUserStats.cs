using System;
namespace Game.Logic
{
    public interface IUserStats
    {
        /*
            El nombre del usuario
        */
        string Username
        {
            get;
        }

        /*
            N�mero de batallas ganadas
        */
        int BattlesWon
        {
            get;
        }

        /*
            N�mero de batallas perdidas
        */
        int BattlesLost
        {
            get;
        }

        /*
            N�mero de batallas empatadas
        */
        int BattlesTied
        {
            get;
        }

        /*
            Ranking global del jugador
        */
        int Ranking
        {
            get;
        }
    }
}