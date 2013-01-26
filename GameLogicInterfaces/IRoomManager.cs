namespace Game.Logic
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Collections.Generic;

    [ContractClass(typeof(IRoomsManagerCodeContract))]
    public interface IRoomsManager
    {
        /**
            Obtiene la lista de salas disponibles
            Nota:
            Se pide la sesión de usuario para poder verificar que el usuario está autentificado
        **/
        IEnumerable<IRoom> GetAvailableRooms(IUserSession session);

        /**
            Permite a un usuario en sesión ingresar a una sala,
            si el usuario en sesión no está autentificado, se lanzará una 
            excepción de seguridad
        **/
        IOperationResult<IRoomUser> JoinRoom(IUserSession session, IRoom room);

        /// <summary>
        /// Lets a room user leave a room
        /// </summary>
        /// <param name="roomUser">The room user.</param>
        IOperationResult LeaveRoom(IRoomUser roomUser);
        /**
            En caso de que un usuario en una sesión de usuario específica
            ya se haya unido previamente a una sala, con esta operación
            se puede obtener el objeto IRoomUser. En caso de que no se
            haya unido previamente a una sala, el resultado es NULL
        **/
        IOperationResult<IRoomUser> GetRoomUser(IUserSession session);
    }
}