using System;
namespace Game.Logic
{
    public interface IReceivedChallenge : IChallenge
    {
        // Objetivo: Aceptar un reto recibido
        // Se utiliza IOperationResult, porque podría suceder que el retador
        // cancele el reto antes de que se procese la aceptación del reto
        IOperationResult Accept();

        // Objetivo: Rechazar un reto recibido
        // No se utiliza IOperationResult, porque si el reto fue 
        // exitosamente rechazado o no, no importa; simplemente no se
        // desea aceptar el reto.
        void Reject();
    }
}