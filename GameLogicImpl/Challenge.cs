namespace Game.Logic.Impl
{
    using System;
    using Game.Core;
    using DependencyLocation;

    /**
     * Es un reto que ha sido enviado, visto desde el punto de vista del retador
     **/
    internal class Challenge : IIssuedChallenge, IReceivedChallenge, IChallenge
    {
        private static IMessaging messaging = Dependency.Locator.GetSingleton<IMessaging>();

        public Challenge(IRoomUser challenger, IRoomUser challengee)
        {
            this.Challengee = challengee;
            this.Challenger = challenger;
            this.State = ChallengeState.Sent;
        }
        public IOperationResult Cancel()
        {
            if (this.State != ChallengeState.Accepted)
            {
                this.State = ChallengeState.Cancelled;
                // TODO: Debe checar de manera thread-safe que no haya sido aceptado el reto aún
                messaging.Publish(Challenger.Room, Tuple.Create(this, ChallengeState.Cancelled));
                return new OperationResult(ResultValue.Success, "");
            }
            
            return new OperationResult(ResultValue.Fail, "El reto ha sido aceptado.");
        }

        #region IChallenge Members

        public IRoomUser Challenger
        {
            get;
            private set;
        }

        public IRoomUser Challengee
        {
            get;
            private set;
        }

        public ChallengeState State
        {
            get;
            private set;
        }

        #endregion

        #region IReceivedChallenge Members

        public IOperationResult Accept()
        {
            if (this.State == ChallengeState.Sent)
            {
                this.State = ChallengeState.Accepted;
                // TODO: Debe checar de manera thread-safe que no haya sido aceptado el reto aún
                messaging.Publish(Challenger.Room, Tuple.Create(this, ChallengeState.Accepted));
                return new OperationResult(ResultValue.Success, "");
            }

            if (this.State == ChallengeState.Accepted)
            {
                return new OperationResult(ResultValue.Success, "");
            }

            return new OperationResult(ResultValue.Fail, "El reto ha sido aceptado.");
        }

        public void Reject()
        {
            if (this.State == ChallengeState.Sent)
            {
                this.State = ChallengeState.Rejected;
                // TODO: Debe checar de manera thread-safe que no haya sido aceptado el reto aún
                messaging.Publish(Challenger.Room, Tuple.Create(this, ChallengeState.Rejected));
            }
        }
        #endregion
    }
}
