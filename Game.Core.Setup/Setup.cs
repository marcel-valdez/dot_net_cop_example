namespace Game.Core.Setup
{
    using System.Data.SqlClient;
    using DependencyLocation;
    using DependencyLocation.Setup;
    using Game.Core;
    using Game.Core.Impl;

    public class Setup : IDependencySetup
    {
        #region IDependencySetup Members
        public void SetupDependencies(IDependencyConfigurator injector, string prefix, string defaultKey)
        {
            SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder();
            connBuilder.InitialCatalog = "CardGame";
            connBuilder.IntegratedSecurity = true;
            connBuilder.MultipleActiveResultSets = true;
            connBuilder.ApplicationName = "EntityFramework";
            //connBuilder.DataSource = "MSSQL";
            
            injector.SetupSingleton<ILog>(new Log(), prefix + defaultKey);
            injector.SetupSingleton<IMessaging>(new Messaging(), prefix + defaultKey);
            injector.SetupDependency<MessageQueue, IMessageQueue>(prefix + defaultKey);
            //injector.PutConfiguration("model-args", new object[] { connBuilder.ToString() });
            injector.PutConfiguration("model-args", new object[] { "metadata=res://*/Model.DomainDataModel.csdl|res://*/Model.DomainDataModel.ssdl|res://*/Model.DomainDataModel.msl;provider=System.Data.SqlClient;provider connection string=\"data source=(local);initial catalog=CardGame;integrated security=True;multipleactiveresultsets=True;App=EntityFramework\"" });
        }
        #endregion
    }
}
