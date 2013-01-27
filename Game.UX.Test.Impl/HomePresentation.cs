using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Presentation.TestImpl
{
    public class HomePresentation : IHomePresentation
    {

        private string _Title = "Home";
        private string _WelcomeMessage = "Hola ";
        private string _RoomsListTitle = "Sala";
        private IEnumerable<string> _RoomNames;
        private int _SelectedRoomIndex;

        public HomePresentation()
          : this(10)
        {
        }
        public HomePresentation(int numberOfRooms)
        {
            string[] roomName = new string[numberOfRooms];
            for (int i = 0; i < roomName.Length; i++)
            {
                roomName[i] = "Sala " + Convert.ToString(i);
            }
            _RoomNames = roomName;
        }

        public string Title
        {
            get
            {
                return _Title;
            }
        }

        public string WelcomeMessage
        {
            get
            {
                return _WelcomeMessage;
            }
        }

        public string RoomsListTitle
        {
            get
            {
                return _RoomsListTitle;
            }
        }

        public IEnumerable<string> RoomNames
        {
            get
            {
                return _RoomNames;
            }
        }

        public int SelectedRoomIndex
        {
            set
            {
                _SelectedRoomIndex = value;
            }
        }

        public void IngresarClicked()
        {
        }


        #region IHomePresentation Members


        public ILogoutButtonPresentation LogoutButton
        {
          get
          {
            throw new NotImplementedException();
          }
        }

        public IStatisticsPresentation UserStats
        {
          get
          {
            throw new NotImplementedException();
          }
        }

        public string Message
        {
          get
          {
            throw new NotImplementedException();
          }
        }

        public bool MessageVisible
        {
          get
          {
            throw new NotImplementedException();
          }
        }

        public bool HasJoinedRoom
        {
          get
          {
            throw new NotImplementedException();
          }
        }

        #endregion
    }
}