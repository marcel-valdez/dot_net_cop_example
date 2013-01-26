namespace Game.Presentation
{
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(IBattlePlayerPresentation))]
    internal abstract class IBattlePlayerPresentationCodeContract : IBattlePlayerPresentation
    {
        #region IBattlePlayerPresentation Members

        string IBattlePlayerPresentation.Username
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        int IBattlePlayerPresentation.LifePoints
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }

        string IBattlePlayerPresentation.LifePointsTxt
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        #endregion
    }
}
