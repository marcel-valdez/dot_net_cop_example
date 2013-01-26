namespace Game.Logic.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal partial class Account : IPlayerAccount
    {
        public IEnumerable<Battle> Battles
        {
            get
            {
                return this.BattlesA.Union(this.BattlesB);
            }
        }

        #region IPlayerAccount Members

        string IPlayerAccount.Username
        {
            get
            {
                return this.Username;
            }
        }

        IEnumerable<ICard> IPlayerAccount.PlayerDeck
        {
            get
            {
                return this.Deck;
            }
        }

        #endregion
    }
}
