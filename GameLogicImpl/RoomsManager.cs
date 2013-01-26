namespace Game.Logic.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Game.Logic.Model;
    using Game.Logic.DynamicModel;
    using App = Game.Logic.DynamicModel.Model;
    using Game.Core;
    using DependencyLocation;
    /*()
            Gestiona las salas de usuarios
        */
    internal class RoomsManager : IRoomsManager
    {
        private static IMessaging messaging = Dependency.Locator.GetSingleton<IMessaging>();

        Dictionary<IRoom, CachedList<IRoomUser>> roomsUserLists;

        public RoomsManager()
        {
            this.roomsUserLists = new Dictionary<IRoom, CachedList<IRoomUser>>();
            foreach (var room in RequestContext.Model<Entities>().Rooms)
            {
                var users = new CachedList<IRoomUser>();
                room.Users = users;
                this.roomsUserLists.Add(room, users);
            }
        }

        public IEnumerable<IRoom> GetAvailableRooms(IUserSession session)
        {
            return RequestContext.Model<Entities>().Rooms;
        }

        public IOperationResult<IRoomUser> JoinRoom(IUserSession session, IRoom room)
        {
            RoomUser rUser = new RoomUser(session, room);
            this.roomsUserLists[room].Add(rUser);
            messaging.Publish(room, Tuple.Create(session, RoomAction.Leave));
            return new OperationResult<IRoomUser>(ResultValue.Success, "", rUser);
        }

        /// <summary>
        /// Permite a un usuario de cuarto salirse del cuarto en que se encuentra
        /// </summary>
        /// <param name="roomUser">El usuario de cuarto.</param>
        public IOperationResult LeaveRoom(IRoomUser roomUser)
        {
            OperationResult result;
            if (this.roomsUserLists[roomUser.Room].Remove(roomUser))
            {
                messaging.Publish(roomUser.Room, Tuple.Create(roomUser, RoomAction.Leave));
                result = new OperationResult(ResultValue.Success, "");
            }
            else
            {
                result = new OperationResult(ResultValue.Fail, Properties.Resources.UserWasNotInRoom);
            }

            return result;
        }

        public IOperationResult<IRoomUser> GetRoomUser(IUserSession session)
        {
            OperationResult<IRoomUser> result;
            IRoomUser roomUser = this.roomsUserLists.SelectMany(pair => pair.Value)
                                     .FirstOrDefault(user => user.Username == session.Username);
            if (roomUser != null)
            {
                result = new OperationResult<IRoomUser>(
                            ResultValue.Fail,
                            "",
                            roomUser);
            }
            else
            {
                result = new OperationResult<IRoomUser>(
                            ResultValue.Fail,
                            Properties.Resources.UserSessionNotInRoom,
                            default(IRoomUser));
            }

            return result;
        }
    }
}
