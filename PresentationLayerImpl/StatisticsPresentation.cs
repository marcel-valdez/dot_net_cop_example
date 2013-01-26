namespace Game.Presentation.Impl
{
    using System.Diagnostics.Contracts;
    using DependencyLocation;
    using Game.Logic;
    using Game.Presentation.Impl.Properties;

    internal class StatisticsPresentation : IStatisticsPresentation
    {
        IUserStats stats;

        public StatisticsPresentation(string username)
        {
            Contract.Requires(!string.IsNullOrEmpty(username));
            this.Init(username);
        }

        private void Init(string username)
        {
            var result = Dependency.Locator.GetSingleton<IUserStatsManager>()
                                   .GetUserStats(username);

            if (result.Result == ResultValue.Success)
            {
                this.stats = result.ResultData;
            }
            else
            {
                this.stats = new ErrorUserStats(result.Message);
            }
        }


        #region IStatisticsPresentation Members

        public string Username
        {
            get
            {
                return stats.Username;
            }
        }

        public string PlayedGamesLabelText
        {
            get
            {
                return string.Format(Resources.TotalGamesPlayed);
            }
        }

        public int PlayedGamesCount
        {
            get
            {
                return stats.BattlesWon + stats.BattlesTied + stats.BattlesLost;
            }
        }

        public string WonGamesLabelText
        {
            get
            {
                return string.Format(Resources.TotalGamesWon);
            }
        }

        public int WonGamesCount
        {
            get
            {
                return stats.BattlesWon;
            }
        }

        public string LostGamesLabelText
        {
            get
            {
                return string.Format(Resources.TotalGamesLost);
            }
        }

        public int LostGamesCount
        {
            get
            {
                return stats.BattlesLost;
            }
        }

        public int Ranking
        {
            get
            {
                return stats.Ranking;
            }
        }


        public string RankingLabelText
        {
            get
            {
                return string.Format(Resources.PlayerRanking);
            }
        }
        #endregion
        
        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(this.stats != null);
        }
    }
}