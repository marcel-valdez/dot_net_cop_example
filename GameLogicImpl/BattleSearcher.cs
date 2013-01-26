namespace Game.Logic.Impl
{
    using System;
    using Game.Core;
    using DependencyLocation;
    using System.Linq;
    using System.Collections.Generic;
    using DynamicModel;
    /*
            Responsabilidad:
            Buscar batallas aleatorias entre usuarios, procurando que tengan el ranking más similar
            posible
        */
    internal class BattleSearcher : IBattleSearcher
    {
        IMessaging messaging = Dependency.Locator.GetSingleton<IMessaging>();
        private CachedList<IBattleRequest> battles = new CachedList<IBattleRequest>();
        public BattleSearcher()
        {
        }

        public IBattleRequest FindBattle(IRoomUser user)
        {
            BattleRequest request = new BattleRequest(user);
            request.State = RequestState.Waiting;
            IBattleRequest aRequest = this.battles.FirstOrDefault(battle => battle.State == RequestState.Waiting);
            if (aRequest != null)
            {
                ((BattleRequest)aRequest).State = RequestState.Ready;
                request.State = RequestState.Ready;
                ((BattleManager)user.Room.BattlesManager).AddBattle(user, aRequest.Requestor);
            }

            return request;
        }

        public IBattleRequest GetRequestedBattle(IRoomUser user)
        {
            return this.battles.Where(b => b.Requestor.Username == user.Username).FirstOrDefault();
        }
    }
}
