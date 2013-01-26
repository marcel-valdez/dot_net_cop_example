namespace SoftArch
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using System.Web.SessionState;
    using System.Web.Caching;
    using DependencyLocation.Setup;
    using Game.Core;
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            string currDir = AppDomain.CurrentDomain.BaseDirectory;
            Game.Core.RequestContext.ContextGetter = () => HttpContext.Current.Items;
            DependencyLoader.Loader.LoadDependencies(currDir + @"\dependencies.config");
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
