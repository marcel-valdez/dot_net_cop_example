namespace Game.Presentation.Impl
{
    using System.Diagnostics.Contracts;
    using DependencyLocation;
    using Game.Logic;

    internal class LoginPresentation : ILoginPresentation
    {
        IUserSession session;
        string sessionId;

        public LoginPresentation(string sessionId)
        {
            Contract.Requires(!string.IsNullOrEmpty(sessionId));
            this.sessionId = sessionId;
            session = Dependency.Locator.GetSingleton<IAccountManager>()
                                        .GetUserSession(sessionId).ResultData;
            this.Init();
        }

        private void Init()
        {
            if (!this.IsAuthenticated)
            {
                /* Init normal */
                this.MessageVisible = false;
            }
            else
            {
                /* Init distinto: Ya hizo login */
                this.MessageVisible = true;
                this.Message = Game.Presentation.Impl.Properties.Resources.LoginAlreadySignedIn;
            }
        }

        #region ILoginPresentation Members

        public string Username
        {
            private get;
            set;
        }

        public string Password
        {
            private get;
            set;
        }

        public bool IsAuthenticated
        {
            get
            {
                return this.session != null && !string.IsNullOrEmpty(this.session.Username);
            }
        }

        public string Message
        {
            get;
            private set;
        }

        public bool MessageVisible
        {
            get;
            private set;
        }

        public void LoginClicked()
        {
            var result = Dependency.Locator.GetSingleton<IAccountManager>()
                                   .Autentificar(this.Username, this.Password, this.sessionId);
            if (result.Result == ResultValue.Success)
            {
                this.session = result.ResultData;
                this.Message = Game.Presentation.Impl.Properties.Resources.LoginSuccessful;
                this.MessageVisible = true;
            }
            else
            {
                this.Message = result.Message;
                this.MessageVisible = true;
            }
        }
        #endregion
    }
}
