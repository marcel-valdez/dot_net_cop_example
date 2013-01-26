using System;

namespace Game.Logic
{
    /*
            Representa un usuario durante una batalla (dentro de un BattleScenario)
        */
    internal class Player : IPlayer
    {
        public Player()
        {

        }
        public string Username
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public int LifePoints
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public IBattleDeck Deck
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public PlayerTurn Turn
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
