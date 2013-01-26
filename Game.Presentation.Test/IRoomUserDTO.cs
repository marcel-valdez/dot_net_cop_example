using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.UX.Test
{
    public interface IRoomUserDTO
    {
        // El nombre de usuario
        string Username
        {
            get;
        }

        // El estado de usuario en la sala: 
        // En Sala, Jugando, Esperando
        string RoomUserState
        {
            get;
        }
    }
}
