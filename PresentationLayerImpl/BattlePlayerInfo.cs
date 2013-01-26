namespace Game.Presentation.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Game.Logic;
    using DependencyLocation;

    internal class BattlePlayerInfo : IBattlePlayerPresentation
    {
        IPlayer mPlayer;
        public BattlePlayerInfo(string sessionId)
        {
            this.mPlayer = SessionHelper.GetBattlePlayer(sessionId);
        }

        public BattlePlayerInfo(IPlayer plyr)
        {
            this.mPlayer = plyr;
        }

        #region IBattlePlayerPresentation Members

        public string Username
        {
            get
            {
                return mPlayer.Username;
            }
        }

        public int LifePoints
        {
            get
            {
                return mPlayer.LifePoints;
            }
        }

        public string LifePointsTxt
        {
            get
            {
                return "HP: ";
            }
        }
        #endregion
    }
}
