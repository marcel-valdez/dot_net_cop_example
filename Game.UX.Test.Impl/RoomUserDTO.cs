using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.UX.Test.Impl
{
    public class RoomUserDTO : IRoomUserDTO
    {

        private string _Username;
        private string _RoomUserState;

        public RoomUserDTO(string unam, string rommus)
        {
            _Username = unam;
            _RoomUserState = rommus;
        }

        public string Username
        {
            get
            {
                return _Username;
            }
        }

        public string RoomUserState
        {
            get
            {
                return _RoomUserState;
            }
        }

    }
}