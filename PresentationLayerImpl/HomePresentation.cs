namespace Game.Presentation.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using DependencyLocation;
    using Game.Logic;
    using Game.Presentation.Impl.Properties;

    internal class HomePresentation : IHomePresentation
    {
        IUserSession session;
        public HomePresentation(string sessionId)
        {
            Contract.Requires(!String.IsNullOrEmpty(sessionId), "sessionId is null or empty.");
            this.Init(sessionId);
        }

        private void Init(string sessionId)
        {
            Contract.Requires(!String.IsNullOrEmpty(sessionId), "sessionId is null or empty.");
            var result = Dependency.Locator.GetSingleton<IAccountManager>()
                        .GetUserSession(sessionId);

            if (result.Result != ResultValue.Success)
            {
                throw new SecurityException(Resources.ErrorUserNotAuthentified);
            }
            else
            {
                this.session = result.ResultData;
                this.UserStats = Dependency.Locator.Create<IStatisticsPresentation>(session.Username);
                this.RoomNames = Dependency.Locator.GetSingleton<IRoomsManager>()
                                                .GetAvailableRooms(this.session)
                                                .Select(room => room.Name);
                this.LogoutButton = Dependency.Locator.Create<ILogoutButtonPresentation>(sessionId);
                this.SelectedRoomIndex = 0;
            }
        }

        #region IHomePresentation Members
        public string Title
        {
            get
            {
                Contract.Assume(!string.IsNullOrEmpty(Resources.HomePageTitle));
                return Resources.HomePageTitle;
            }
        }

        public string WelcomeMessage
        {
            get
            {
                Contract.Assume(!string.IsNullOrEmpty(Resources.WelcomeMessagePrefix));
                return String.Format(Resources.WelcomeMessagePrefix, session.Username);
            }
        }

        public string RoomsListTitle
        {
            get
            {
                Contract.Assume(!string.IsNullOrEmpty(Resources.RoomsListTitle));
                return Resources.RoomsListTitle;
            }
        }

        public IEnumerable<string> RoomNames
        {
            get;
            private set;
        }

        public int SelectedRoomIndex
        {
            private get;
            set;
        }

        public ILogoutButtonPresentation LogoutButton
        {
            get;
            private set;
        }

        public IStatisticsPresentation UserStats
        {
            get;
            private set;
        }

        public bool HasJoinedRoom
        {
            get;
            private set;
        }

        public string Message
        {
            get;
            private set;
        }

        public bool MessageVisible
        {
            get;
            private set;
        }

        public void IngresarClicked()
        {
            var roomsMgr = Dependency.Locator.GetSingleton<IRoomsManager>();
            var rooms = roomsMgr.GetAvailableRooms(this.session);
            string currentRoomName = this.RoomNames.ElementAt(this.SelectedRoomIndex);
            var room = rooms.First(r => r.Name == currentRoomName);

            var result = roomsMgr.JoinRoom(this.session, room);
            if (result.Result == ResultValue.Success)
            {
                this.Message = String.Format(Resources.HasJoinedRoomSuccess, this.RoomNames.ElementAt(this.SelectedRoomIndex));
                this.MessageVisible = true;
                this.HasJoinedRoom = true;
            }
            else
            {
                this.Message = String.Format(Resources.JoinRoomFail);
                this.MessageVisible = true;
                this.HasJoinedRoom = false;
            }
        }

        #endregion
    }
}
