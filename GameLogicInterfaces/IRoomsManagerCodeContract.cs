namespace Game.Logic
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Collections.Generic;

    [ContractClassFor(typeof(IRoomsManager))]
    internal abstract class IRoomsManagerCodeContract : IRoomsManager
    {
        #region IRoomsManager Members

        public IOperationResult<IRoomUser> GetRoomUser(IUserSession session)
        {
            Contract.Requires(session != null, "session is null.");
            Contract.Ensures(Contract.Result<IOperationResult<IRoomUser>>() != null);
            return default(IOperationResult<IRoomUser>);
        }

        public IOperationResult LeaveRoom(IRoomUser roomUser)
        {
            Contract.Requires(roomUser != null, "roomUser is null.");

            return default(IOperationResult);
        }

        public IEnumerable<IRoom> GetAvailableRooms(IUserSession session)
        {
            Contract.Requires(session != null, "session is null.");
            Contract.Ensures(Contract.Result<IEnumerable<IRoom>>() != null);

            return default(IEnumerable<IRoom>);
        }

        public IOperationResult<IRoomUser> JoinRoom(IUserSession session, IRoom room)
        {
            Contract.Requires(room != null, "room is null.");
            Contract.Requires(session != null, "session is null.");
            Contract.Ensures(Contract.Result<IOperationResult<IRoomUser>>() != null);

            return default(IOperationResult<IRoomUser>);
        }

        #endregion
    }
}
