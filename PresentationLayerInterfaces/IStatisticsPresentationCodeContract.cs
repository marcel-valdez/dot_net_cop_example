namespace Game.Presentation
{
    using System.Diagnostics.Contracts;
    [ContractClassFor(typeof(IStatisticsPresentation))]
    internal abstract class IStatisticsPresentationCodeContract : IStatisticsPresentation
    {
        #region IStatisticsPresentation Members
        public string Username
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        public string PlayedGamesLabelText
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        public int PlayedGamesCount
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }

        public string WonGamesLabelText
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        public int WonGamesCount
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }

        public string LostGamesLabelText
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        public int LostGamesCount
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }

        public string RankingLabelText
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        public int Ranking
        {
            get
            {
                // 0 es error
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }
        #endregion
    }
}
