namespace Game.Logic
{
    using System.Diagnostics.Contracts;
    [ContractClassFor(typeof(IRoomUser))]
    internal abstract class IRoomUserCodeContract : Game.Logic.IRoomUser
    {
        #region IRoomUser Members

        public string Username
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        public InRoomState State
        {
            get
            {
                return default(InRoomState);
            }
        }

        public IRoom Room
        {
            get
            {
                Contract.Ensures(Contract.Result<IRoom>() != null);
                return default(IRoom);
            }
        }

        #endregion
    }
}
