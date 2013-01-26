namespace Game.Presentation.Tests
{
    using System;
    using DependencyLocation;
    using DependencyLocation.Setup;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    //[TestClass]
    public class Initializer
    {
        //[AssemblyInitialize]
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
