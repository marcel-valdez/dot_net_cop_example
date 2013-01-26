namespace Game.Presentation.Setup
{
    using DependencyLocation;
    using DependencyLocation.Setup;
    using Game.Presentation.Impl;

    public class Setup : IDependencySetup
    {
        #region IDependencySetup Members
        public void SetupDependencies(IDependencyConfigurator injector, string prefix, string defaultKey)
        {
            injector.SetupDependency<CreateAccountPresentation, ICreateAccountPresentation>(prefix + defaultKey);
            injector.SetupDependency<LoginPresentation, ILoginPresentation>(prefix + defaultKey);
            injector.SetupDependency<HomePresentation, IHomePresentation>(prefix + defaultKey);
            injector.SetupDependency<StatisticsPresentation, IStatisticsPresentation>(prefix + defaultKey);
            injector.SetupDependency<LogoutButtonPresentation, ILogoutButtonPresentation>(prefix + defaultKey);
            injector.SetupDependency<RoomPresentation, IRoomPresentation>(prefix + defaultKey);
            injector.SetupDependency<RoomUserDTO, IRoomUserDTO>(prefix + defaultKey);
            injector.SetupDependency<BattlePresentation, IBattlePresentation>(prefix + defaultKey);
            injector.SetupDependency<BattleCardPresentation, IBattleCardPresentation>(prefix + defaultKey);
            injector.SetupDependency<BattlePlayerInfo, IBattlePlayerPresentation>(prefix + defaultKey);
        }
        #endregion
    }
}
