namespace Game.Presentation
{
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(IBattleCardPresentation))]
    internal abstract class IBattleCardPresentationCodeContract : Game.Presentation.IBattleCardPresentation
    {
        #region IBattleCardPresentation Members

        string IBattleCardPresentation.CardName
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        string IBattleCardPresentation.ImageUrl
        {
            get
            {
                return default(string);
            }
        }

        int IBattleCardPresentation.AttackPoints
        {
            get
            {
                return default(int);
            }
        }

        int IBattleCardPresentation.DefensePoints
        {
            get
            {
                return default(int);
            }
        }

        bool IBattleCardPresentation.Selected
        {
            set
            {
            }
        }

        bool IBattleCardPresentation.Selectable
        {
            get
            {
                return default(bool);
            }
        }

        #endregion
    }
}
