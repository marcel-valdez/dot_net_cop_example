using System;

namespace Game.Core.Tests
{
    using DependencyLocation;
    using DependencyLocation.Setup;

  public class Initializer
    {        
        public static void LoadDependencies()
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            DependencyLoader.Loader.LoadDependencies(dir + @"\dependencies.config");
        }

        public static void ReleaseDependencies()
        {
            ((IDependencyConfigurator)Dependency.Locator).ReleaseInjections();
        }
    }
}
