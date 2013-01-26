namespace Game.Logic.Setup
{
    using System;
    using System.Collections.Generic;
    using DependencyLocation;
    using System.Linq;
    using System.Text;
    using DependencyLocation.Setup;
    using Game.Logic;
    using Game.Logic.Impl;
    using Game.Logic.Model;
    using Game.Logic.DynamicModel;

    public class Setup : IDependencySetup
    {
        #region IDependencySetup Members

        public void SetupDependencies(IDependencyConfigurator injector, string prefix, string defaultKey)
        {
            injector.SetupSingleton<ICardContainer>(() => new CardContainer(), prefix + defaultKey);
            injector.SetupSingleton<IAccountManager>(() => new AccountManager(), prefix + defaultKey);
            injector.SetupSingleton<IRoomsManager>(() => new RoomsManager(), prefix + defaultKey);
            injector.SetupSingleton<IUserStatsManager>(() => new UserStatsManager(), prefix + defaultKey);
            injector.SetupSingleton<IRankingsManager>(() => new RankingsManager(), prefix + defaultKey);

            injector.SetupDependency<OperationResult, IOperationResult>(prefix + defaultKey);
            injector.SetupDependency(typeof(OperationResult<>), typeof(IOperationResult<>), prefix + defaultKey);
            injector.SetupDependency<Challenge, IChallenge>(prefix + defaultKey);
            injector.SetupDependency<Challenge, IIssuedChallenge>(prefix + defaultKey);
            injector.SetupDependency<Challenge, IReceivedChallenge>(prefix + defaultKey);
            injector.SetupDependency<RoomUser, IRoomUser>(prefix + defaultKey);
            injector.SetupDependency<UserSession, IUserSession>(prefix + defaultKey);
            injector.SetupDependency<Session, ISession>(prefix + defaultKey);
            injector.SetupDependency<BattleRequest, IBattleRequest>(prefix + defaultKey);
            injector.SetupDependency<UserStats, IUserStats>(prefix + defaultKey);

        }

        #endregion        
    }
}
