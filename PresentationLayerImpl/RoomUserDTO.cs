using Game.Presentation.Impl.Properties;
namespace Game.Presentation.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Game.Logic;

    internal class RoomUserDTO : IRoomUserDTO
    {
        public RoomUserDTO(IRoomUser user)
        {
            this.Username = user.Username;
            this.RoomUserState = ((Func<string>)(() =>
                {
                    /* JavaScript style: Booyah! */
                    switch (user.State)
                    {
                        case InRoomState.Idle:
                            return Resources.RoomUserIdle;
                        case InRoomState.Playing:
                            return Resources.RoomUserPlaying;
                        case InRoomState.RequestingBattle:
                            return Resources.RoomUserRequestedBattle;
                        case InRoomState.RequestingChallenge:
                            return Resources.RoomUserRequestedChallenge;
                        default:
                            throw new Exception("No puede ser. Imposible.");
                    }
                }))();
        }
        #region IRoomUserDTO Members

        public string Username
        {
            get;
            private set;
        }

        public string RoomUserState
        {
            get;
            private set;
        }
        #endregion
    }
}
