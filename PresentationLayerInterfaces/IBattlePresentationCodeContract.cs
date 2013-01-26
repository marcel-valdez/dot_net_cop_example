namespace Game.Presentation
{
    using System.Diagnostics.Contracts;
    using System.Collections.Generic;

    [ContractClassFor(typeof(IBattlePresentation))]
    internal abstract class IBattlePresentationCodeContract : IBattlePresentation
    {
        #region IBattlePresentation Members
        string IBattlePresentation.Title
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        string IBattlePresentation.MiddleMsgTitle
        {
            get
            {
                return default(string);
            }
        }

        string IBattlePresentation.HarmInflictedTxt
        {
            get
            {
                return default(string);
            }
        }

        int IBattlePresentation.HarmInflictedPts
        {
            get
            {
                return default(int);
            }
        }

        string IBattlePresentation.HarmReceivedTxt
        {
            get
            {
                return default(string);
            }
        }

        int IBattlePresentation.HarmReceivedPts
        {
            get
            {
                return default(int);
            }
        }

        string IBattlePresentation.MiddleMsgFooter
        {
            get
            {
                return default(string);
            }
        }

        bool IBattlePresentation.ConfirmBtnEnabled
        {
            get
            {
                return default(bool);
            }
        }

        IBattlePlayerPresentation IBattlePresentation.MyInfo
        {
            get
            {
                Contract.Ensures(Contract.Result<IBattlePlayerPresentation>() != null);
                return default(IBattlePlayerPresentation);
            }
        }

        IEnumerable<IBattleCardPresentation> IBattlePresentation.MyCards
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<IBattleCardPresentation>>() != null);
                return default(IEnumerable<IBattleCardPresentation>);
            }
        }

        IBattlePlayerPresentation IBattlePresentation.OppInfo
        {
            get
            {
                Contract.Ensures(Contract.Result<IBattlePlayerPresentation>() != null);
                return default(IBattlePlayerPresentation);
            }
        }

        IEnumerable<IBattleCardPresentation> IBattlePresentation.OppCards
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<IBattleCardPresentation>>() != null);
                return default(IEnumerable<IBattleCardPresentation>);
            }
        }

        void IBattlePresentation.ConfirmButtonPressed()
        {
        }

        bool IBattlePresentation.HasBattleEnded
        {
            get
            {
                return default(bool);
            }
        }

        bool IBattlePresentation.IsWaitingForOpponent
        {
            get
            {
                return default(bool);
            }
        }

        ILogoutButtonPresentation IBattlePresentation.LogoutButton
        {
            get
            {
                Contract.Ensures(Contract.Result<ILogoutButtonPresentation>() != null);
                return default(ILogoutButtonPresentation);
            }
        }
        
        bool IBattlePresentation.HarmResultsVisible
        {
            get
            {
                return default(bool);
            }
        }
        #endregion
    }
}
