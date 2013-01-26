using System;
namespace Game.Logic
{
    public interface IChallenge
    {
        /* Es el jugador en una sala que ha enviado un reto*/
        IRoomUser Challenger
        {
            get;
        }

        /* Es el jugador en una sala que ha recibido un reto*/
        IRoomUser Challengee
        {
            get;
        }

        /* Es el estado del reto (Sent | Accepted | Rejected | Cancelled )*/
        ChallengeState State
        {
            get;
        }
    }

    public enum ChallengeState
    {
        Sent = 0,
        Accepted = 1,
        Rejected = 2,
        Cancelled = 3
    }
}