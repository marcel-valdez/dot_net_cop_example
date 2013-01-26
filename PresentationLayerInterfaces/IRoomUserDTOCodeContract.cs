namespace Game.Presentation
{
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(IRoomUserDTO))]
    internal abstract class IRoomUserDTOCodeContract : IRoomUserDTO
    {
        #region IRoomUserDTO Members

        string IRoomUserDTO.Username
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        string IRoomUserDTO.RoomUserState
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        #endregion
    }
}
