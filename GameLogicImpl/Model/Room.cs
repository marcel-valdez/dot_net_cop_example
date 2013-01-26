namespace Game.Logic.Model
{
    using System.Collections.Generic;
    using System;
    /*
            Responsabilidades:
            Conocer el nombre de una sala y los usuarios dentor de la misma
        */
    internal partial class Room : IRoom
    {

        #region IRoom Members
        string IRoom.Name
        {
            get
            {
                return this.Name;
            }
        }

        public IEnumerable<IRoomUser> Users
        {
            get;
            set;
        }

        public IBattleManager BattlesManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IBattleRequest FindBattle(IRoomUser requestor)
        {
            throw new NotImplementedException();
        }

        public IChallengeManager ChallengesManager
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }


}
