using System;
namespace Game.Logic
{
    [System.Diagnostics.Contracts.ContractClass(typeof(IRoomUserCodeContract))]
    public interface IRoomUser
    {
        /*
            Nombre de usuario
        */
        string Username
        {
            get;
        }

        /*
            Estado dentro de la sala (Playing, RequestingBattle, RequestingChallenge, Idle)
        */
        InRoomState State
        {
            get;
        }

        /*
            Sala en la que se encuentra el usuario de sala
        */
        IRoom Room
        {
            get;
        }
    }

    public enum InRoomState
    {
        Playing = 0,
        RequestingBattle = 1,
        RequestingChallenge = 2,
        Idle = 3 
    }
}