using System;
using System.Collections.Generic;

namespace Game.Logic.Impl
{    

    internal class BattleDeck : IBattleDeck
    {
        public BattleDeck()
        {

        }

        public IEnumerable<ICard> Cards
        {
            get;
            private set;
        }

        public int MaxToChoose
        {
            get;
            private set;
        }

        public IOperationResult Choose(IEnumerable<ICard> chosen)
        {
            throw new NotImplementedException();
        }

        public bool HasChosen
        {
            get;
            private set;
        }
    }
}
