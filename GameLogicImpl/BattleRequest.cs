namespace Game.Logic.Impl
{
   /**
    *    Responsabilidades:
    *    Conocer 
    **/
    using System;
    using Game.Core;
    using DependencyLocation;

    internal class BattleRequest : IBattleRequest
    {
        private static IMessaging messaging = Dependency.Locator.GetSingleton<IMessaging>();

        public BattleRequest(IRoomUser user)
        {
            this.Requestor = user;
        }

        public IRoomUser Requestor
        {
            get;
            private set;
        }
        public RequestState State
        {
            get;
            internal set;
        }

        public IOperationResult Cancel()
        {
            if (this.State == RequestState.Waiting)
            {
                messaging.Publish(this.Requestor.Room, Tuple.Create(this, RequestState.Cancelled));
                this.State = RequestState.Cancelled;
            }

            if (this.State == RequestState.Cancelled)
            {
                return new OperationResult(ResultValue.Success, "");
            }

            return new OperationResult(ResultValue.Fail, "No puede cancelar, porque la batalla ya ha empezado!");
        }
    }
}
