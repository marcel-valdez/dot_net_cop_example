namespace Game.Presentation
{
    using System.Diagnostics.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    [ContractClassFor(typeof(IHomePresentation))]
    internal abstract class IHomePresentationCodeContract : IHomePresentation
    {
        #region IHomePresentation Members

        public string Title
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        public string WelcomeMessage
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        public string RoomsListTitle
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        public IEnumerable<string> RoomNames
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
                return default(IEnumerable<string>);
            }
        }

        public int SelectedRoomIndex
        {
            set
            {
                Contract.Requires(value >= 0 && value < RoomNames.Count());
            }
        }

        public IStatisticsPresentation UserStats
        {
            get
            {
                Contract.Ensures(Contract.Result<IStatisticsPresentation>() != null);
                return default(IStatisticsPresentation);
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

        public void IngresarClicked()
        {
        }
        #endregion

        #region IHomePresentation Members
        public string Message
        {
            get
            {
                Contract.Ensures(this.MessageVisible ? !string.IsNullOrEmpty(Contract.Result<string>()) : true);
                return default(string);
            }
        }

        public bool MessageVisible
        {
            get
            {
                Contract.Ensures(Contract.Result<bool>() ? !string.IsNullOrEmpty(this.Message) : true);
                return default(bool);
            }
        }

        public bool HasJoinedRoom
        {
            get
            {
                return default(bool);
            }
        }

        #endregion
    }
}
