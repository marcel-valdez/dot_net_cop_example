using System;
namespace Game.Logic
{
    public interface IIssuedChallenge : IChallenge
    {
        // Objetivo: Cancela el reto enviado.
        // Se utiliza IOperationResult, porque podría suceder que el
        // jugador que ha recibido el reto, lo acepte antes de que el
        // retador pueda cancelarlo.
        IOperationResult Cancel();

    }
}