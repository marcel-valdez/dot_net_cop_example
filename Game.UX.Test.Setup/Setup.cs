namespace Game.UX.Test.Setup
{
  using DependencyLocation;
  using DependencyLocation.Setup;
  using Game.Presentation.TestImpl;
  using Game.Presentation;

  public class Setup : IDependencySetup
  {
    #region IDependencySetup Members
    public void SetupDependencies(IDependencyConfigurator injector, string prefix, string defaultKey)
    {
      injector.SetupDependency<CreateAccountPresentation, ICreateAccountPresentation>(prefix + defaultKey);
      injector.SetupDependency<LoginPresentation, ILoginPresentation>(prefix + defaultKey);
      injector.SetupDependency<HomePresentation, IHomePresentation>(prefix + defaultKey);
      injector.SetupDependency<StatisticsPresentation, IStatisticsPresentation>(prefix + defaultKey);
      //injector.SetupDependency<LogoutButtonPresentation, ILogoutButtonPresentation>(prefix + defaultKey);
    }
    #endregion
  }
}
