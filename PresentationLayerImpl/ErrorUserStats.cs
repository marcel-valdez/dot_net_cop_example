namespace Game.Presentation.Impl
{
    using Game.Logic;

    internal class ErrorUserStats : IUserStats
    {
        string mMessage;
        public ErrorUserStats(string msg)
        {
            mMessage = msg;
        }
        #region IUserStats Members

        public string Username
        {
            get
            {
                return "Error: " + this.mMessage;
            }
        }

        public int BattlesWon
        {
            get
            {
                return 0;
            }
        }

        public int BattlesLost
        {
            get
            {
                return 0;
            }
        }

        public int BattlesTied
        {
            get
            {
                return 0;
            }
        }

        public int Ranking
        {
            get
            {
                return 0;
            }
        }

        #endregion
    }
}
