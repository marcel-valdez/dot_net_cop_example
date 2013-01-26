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
            Se pide la sesi�n de usuario para poder verificar que el usuario est� autentificado
        **/
        IEnumerable<IRoom> GetAvailableRooms(IUserSession session);

        /**
            Permite a un usuario en sesi�n ingresar a una sala,
            si el usuario en sesi�n no est� autentificado, se lanzar� una 
            excepci�n de seguridad
        **/
        IOperationResult<IRoomUser> JoinRoom(IUserSession session, IRoom room);

        /// <summary>
        /// Lets a room user leave a room
        /// </summary>
        /// <param name="roomUser">The room user.</param>
        IOperationResult LeaveRoom(IRoomUser roomUser);
        /**
            En caso de que un usuario en una sesi�n de usuario espec�fica
            ya se haya unido previamente a una sala, con esta operaci�n
            se puede obtener el objeto IRoomUser. En caso de que no se
            haya unido previamente a una sala, el resultado es NULL
        **/
        IOperationResult<IRoomUser> GetRoomUser(IUserSession session);
    }
}