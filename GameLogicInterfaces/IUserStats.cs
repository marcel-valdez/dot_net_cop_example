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
            Número de batallas ganadas
        */
        int BattlesWon
        {
            get;
        }

        /*
            Número de batallas perdidas
        */
        int BattlesLost
        {
            get;
        }

        /*
            Número de batallas empatadas
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