namespace Game.Presentation
{
    using System.Diagnostics.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    [ContractClassFor(typeof(IRoomPresentation))]
    internal abstract class IRoomPresentationCodeContract : IRoomPresentation
    {
        #region IRoomPresentation Members

        public void HomeButtonClicked()
        {
        }

        public void FindBattleButtonClicked()
        {
            Contract.Ensures(this.DialogVisible);
            Contract.Ensures(this.CurrentDialogPresentation != null);
        }

        public void ChallengeButtonClicked()
        {
            Contract.Ensures(this.DialogVisible);
            Contract.Ensures(this.CurrentDialogPresentation != null);
        }

        public IEnumerable<IRoomUserDTO> Usuarios
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<IRoomUserDTO>>() != null);
                return default(IEnumerable<IRoomUserDTO>);
            }
        }

        public int SelectedUserIndex
        {
            set
            {
                Contract.Requires(value >= 0);
            }
        }

        public IStatisticsPresentation SelectedUserStatistics
        {
            get
            {
                Contract.Ensures(Contract.Result<IStatisticsPresentation>() != null);
                return default(IStatisticsPresentation);
            }
        }

        public bool ReadyForBattle
        {
            get
            {
                Contract.Ensures(Contract.Result<bool>() ? !this.ChallengeButtonEnabled && !this.FindBattleButonEnabled : true);
                return default(bool);
            }
        }

        public bool DialogVisible
        {
            get
            {
                return default(bool);
            }
        }

        public IUserDialogPresentation CurrentDialogPresentation
        {
            get
            {
                Contract.Ensures(this.DialogVisible ? Contract.Result<IUserDialogPresentation>() != null : true);
                return default(IUserDialogPresentation);
            }
        }

        public ILogoutButtonPresentation LogoutButton
        {
            get
            {
                Contract.Ensures(Contract.Result<ILogoutButtonPresentation>() != null);
                return default(ILogoutButtonPresentation);
            }
        }

        public bool ChallengeButtonEnabled
        {
            get
            {
                Contract.Ensures(!this.ReadyForBattle);
                return default(bool);
            }
        }

        public bool FindBattleButonEnabled
        {
            get
            {
                Contract.Ensures(!this.ReadyForBattle);
                return default(bool);
            }
        }
        #endregion
    }
}
