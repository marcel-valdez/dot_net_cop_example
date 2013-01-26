namespace Game.Logic
{
    using System;
    public interface IChallengeManager
    {
        /**
         *   Envía un reto nuevo
         *   Restricciones:
         *   Solamente se puede enviar un reto a la vez, si se intenta enviar otro reto,
         *   se cancelará el reto anterior y se enviará uno nuevo, a menos que el reto
         *   anterior ya haya sido aceptado, en cuyo caso, se regresará el reto
         *   enviado anteriormente que ha sido aceptado
         **/
        IOperationResult<IIssuedChallenge> Send(IRoomUser challenger, IRoomUser challengee);

        /**
         *   Obtiene un reto enviado por el jugador user, si no ha enviado
         *   ningún reto aún, entonces el resultado de la operación será
         *   Fail y ResultData será null, si sí hay retos enviados anteriormente
         *   entonces el resultado de la operación sera Success y ResultData
         *   tendrá el reto enviado.
         **/
        IOperationResult<IIssuedChallenge> GetIssuedChallenge(IRoomUser user);

        /**
         *   Obtiene los retos que ha recibido el jugador user
         *   Notas:
         *   Si hay más de 1 reto pendiente, cada llamada a este método
         *   obtendrá un reto de la lista (FIFO) de retos pendientes
         *   Si el resultado de la operación es Fail, entonces no se ha recibido
         * ningún reto y ResultData es null, si es Success, quiere decir que hay reto(s) pendiente(s)
         * y en ResultData se envía el reto pendiente
         **/
        IOperationResult<IReceivedChallenge> GetReceivedChallenge(IRoomUser user);
    }
}