using System;
namespace Game.Logic
{
    public interface IBattleRequest
    {
        /*
            El usuario de sala que hizo la petición de una batalla
        */
        IRoomUser Requestor
        {
            get;
        }

        /*
            Fase en el ciclo de vida de la petición de batalla
            { Waiting | Ready | Cancelled }
        */
        RequestState State
        {
            get;
        }

        /*
            Cancela la petición de batalla, puede fallar la operación
            si la petición de batalla ya está en estado Ready
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