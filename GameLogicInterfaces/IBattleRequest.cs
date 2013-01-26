using System;
namespace Game.Logic
{
    public interface IBattleRequest
    {
        /*
            El usuario de sala que hizo la petici�n de una batalla
        */
        IRoomUser Requestor
        {
            get;
        }

        /*
            Fase en el ciclo de vida de la petici�n de batalla
            { Waiting | Ready | Cancelled }
        */
        RequestState State
        {
            get;
        }

        /*
            Cancela la petici�n de batalla, puede fallar la operaci�n
            si la petici�n de batalla ya est� en estado Ready
        */
        IOperationResult Cancel();
    }

    public enum RequestState
    {
        Waiting = 0,
        Ready = 1,
        Cancelled = 2
    }
}