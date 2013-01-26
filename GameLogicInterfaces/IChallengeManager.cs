namespace Game.Logic
{
    using System;
    public interface IChallengeManager
    {
        /**
         *   Env�a un reto nuevo
         *   Restricciones:
         *   Solamente se puede enviar un reto a la vez, si se intenta enviar otro reto,
         *   se cancelar� el reto anterior y se enviar� uno nuevo, a menos que el reto
         *   anterior ya haya sido aceptado, en cuyo caso, se regresar� el reto
         *   enviado anteriormente que ha sido aceptado
         **/
        IOperationResult<IIssuedChallenge> Send(IRoomUser challenger, IRoomUser challengee);

        /**
         *   Obtiene un reto enviado por el jugador user, si no ha enviado
         *   ning�n reto a�n, entonces el resultado de la operaci�n ser�
         *   Fail y ResultData ser� null, si s� hay retos enviados anteriormente
         *   entonces el resultado de la operaci�n sera Success y ResultData
         *   tendr� el reto enviado.
         **/
        IOperationResult<IIssuedChallenge> GetIssuedChallenge(IRoomUser user);

        /**
         *   Obtiene los retos que ha recibido el jugador user
         *   Notas:
         *   Si hay m�s de 1 reto pendiente, cada llamada a este m�todo
         *   obtendr� un reto de la lista (FIFO) de retos pendientes
         *   Si el resultado de la operaci�n es Fail, entonces no se ha recibido
         * ning�n reto y ResultData es null, si es Success, quiere decir que hay reto(s) pendiente(s)
         * y en ResultData se env�a el reto pendiente
         **/
        IOperationResult<IReceivedChallenge> GetReceivedChallenge(IRoomUser user);
    }
}