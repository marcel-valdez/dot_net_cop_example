namespace Game
{
  using System;
  using System.IO;
  using System.Web;
  using Core;
  using DependencyLocation;
  using DependencyLocation.Setup;

  public class Global : HttpApplication
  {

    void Application_Start(object sender, EventArgs e)
    {
      string currDir = AppDomain.CurrentDomain.BaseDirectory;
      RequestContext.ContextGetter = () => HttpContext.Current.Items;
      DependencyLoader.Loader.LoadDependencies(currDir + @"\dependencies.config");
      Dependency.Locator.GetSingleton<ILog>().Path = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
      Dependency.Locator.GetSingleton<ILog>().DebugOn = true;
    }

    void Application_End(object sender, EventArgs e)
    {
      //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
      // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
      Dependency.Locator.GetSingleton<ILog>().AddToLog("Starting new session.");
      //string sessionID = HttpContext.Current.Session.SessionID;
      //HttpContext.Current.Cache.Insert("session", sessionID, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20));
    }

    void Session_End(object sender, EventArgs e)
    {
      // Code that runs when a session ends. 
      // Note: The Session_End event is raised only when the sessionstate mode
      // is set to InProc in the Web.config file. If session mode is set to StateServer 
      // or SQLServer, the event is not raised.

    }

  }
}
