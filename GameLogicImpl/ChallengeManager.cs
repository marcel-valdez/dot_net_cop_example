namespace Game.Logic.Impl
{
    using System;
    using Game.Core;
    using DependencyLocation;
    using DynamicModel;
    using Model;

    internal class ChallengeManager : IChallengeManager
    {
        private static IMessaging messaging = Dependency.Locator.GetSingleton<IMessaging>();

        public ChallengeManager()
        {

        }

        /**
        *   Envía un reto nuevo
        *   Restricciones:
        *   Solamente se puede enviar un reto a la vez, si se intenta enviar otro reto,
        *   se cancelará el reto anterior y se enviará uno nuevo, a menos que el reto
        *   anterior ya haya sido aceptado, en cuyo caso, se regresará el reto
        *   enviado anteriormente que ha sido aceptado
        **/
        public IOperationResult<IIssuedChallenge> Send(IRoomUser challenger, IRoomUser challengee)
        {
            var chall = new Challenge(challenger, challengee);
            Model.Cache.Add(this, chall);
            messaging.Publish(challengee.Room, Tuple.Create(challengee, chall));

            return new OperationResult<IIssuedChallenge>(ResultValue.Success, "", chall);
        }

        /**
        *   Obtiene un reto enviado por el jugador user, si no ha enviado
        *   ningún reto aún, entonces el resultado de la operación será
        *   Fail y ResultData será null, si sí hay retos enviados anteriormente
        *   entonces el resultado de la operación sera Success y ResultData
        *   tendrá el reto enviado.
        **/
        public IOperationResult<IIssuedChallenge> GetIssuedChallenge(IRoomUser user)
        {
            OperationResult<IIssuedChallenge> result;
            var value = Model.Cache.Retrieve<IIssuedChallenge>(chal => chal.Challenger.Username == user.Username);

            if (value != null)
            {
                result = new OperationResult<IIssuedChallenge>(ResultValue.Success, "", (IIssuedChallenge)value);
            }
            else
            {
                result = new OperationResult<IIssuedChallenge>(ResultValue.Fail, "", value);
            }

            return result;
        }
        /**
        *   Obtiene los retos que ha recibido el jugador user
        *   Notas:
        *   Si hay más de 1 reto pendiente, cada llamada a este método
        *   obtendrá un reto de la lista (FIFO) de retos pendientes
        *   Si el resultado de la operación es Fail, entonces no se ha recibido
        * ningún reto y ResultData es null, si es Success, quiere decir que hay reto(s) pendiente(s)
        * y en ResultData se envía el reto pendiente
        **/
        public IOperationResult<IReceivedChallenge> GetReceivedChallenge(IRoomUser user)
        {
            OperationResult<IReceivedChallenge> result;
            var value = Model.Cache.Retrieve<IReceivedChallenge>(chal => chal.Challengee.Username == user.Username);
            if (value != null)
            {
                result = new OperationResult<IReceivedChallenge>(ResultValue.Success, "", (IReceivedChallenge)value);
            }
            else
            {
                result = new OperationResult<IReceivedChallenge>(ResultValue.Fail, "", value);
            }

            return result;
        }
    }
}
