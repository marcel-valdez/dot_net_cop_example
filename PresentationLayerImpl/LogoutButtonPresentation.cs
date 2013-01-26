namespace Game.Presentation.Impl
{
    using System;
    using System.Diagnostics.Contracts;
    using DependencyLocation;
    using Game.Logic;
    using Game.Presentation.Impl.Properties;

    internal class LogoutButtonPresentation : ILogoutButtonPresentation
    {
        string mSessionId;
        public LogoutButtonPresentation(string sessionId)
        {
            Contract.Requires(!String.IsNullOrEmpty(sessionId), "sessionId is null or empty.");
            this.mSessionId = sessionId;
        }

        #region ILogoutButtonPresentation Members
        public string ButtonText
        {
            get
            {
                return string.Format(Resources.LogoutBtnText);
            }
        }

        public void LogoutButtonClicked()
        {
            var result = Dependency.Locator.GetSingleton<IAccountManager>()
                                           .GetUserSession(mSessionId);
            if (result.Result.Equals(ResultValue.Success))
            {
                Dependency.Locator.GetSingleton<IAccountManager>()
                          .Logout(result.ResultData);
            }
        }
        #endregion
    }
}
