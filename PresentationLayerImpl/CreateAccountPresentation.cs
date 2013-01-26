using System.Diagnostics.Contracts;
namespace Game.Presentation.Impl
{
    using System;
    using DependencyLocation;
    using Game.Logic;

    internal class CreateAccountPresentation : ICreateAccountPresentation
    {
        string mSessionId;

        public CreateAccountPresentation(string sessionId)
        {
            Contract.Requires(!String.IsNullOrEmpty(sessionId), "sessionId is null or empty.");
            this.mSessionId = sessionId;
            this.Init();
        }

        private void Init()
        {
            Contract.Requires(!String.IsNullOrEmpty(this.mSessionId), "sessionId is null or empty.");
            var accmgr = Dependency.Locator.GetSingleton<IAccountManager>();
            if (accmgr.GetUserSession(mSessionId).Result != ResultValue.Success)
            {
                this.PasswordConfirmationVisible = true;
                this.PasswordVisible = true;
                this.RedirectButtonVisible = false;
                this.ResultMessageVisible = false;
                this.UsernameVisible = true;
                this.CreateButtonVisible = true;
            }
            else
            {
                this.ShowConcludedForm(Properties.Resources.CantCreateAccAlreadyLoggedIn);
            }
        }

        #region ICreateAccountPresentation Members
        private string _Username;
        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
            }
        }

        private bool _UsernameVisible;
        public bool UsernameVisible
        {
            get
            {
                return _UsernameVisible;
            }
            private set
            {
                _UsernameVisible = value;
            }
        }

        private string _Password;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }

        private bool _PasswordVisible;
        public bool PasswordVisible
        {
            get
            {
                return _PasswordVisible;
            }
            private set
            {
                _PasswordVisible = value;
            }
        }

        private string _PasswordConfirmation;
        public string PasswordConfirmation
        {
            get
            {
                return _PasswordConfirmation;
            }
            set
            {
                _PasswordConfirmation = value;
            }
        }

        private bool _PasswordConfirmationVisible;
        public bool PasswordConfirmationVisible
        {
            get
            {
                return _PasswordConfirmationVisible;
            }
            private set
            {
                _PasswordConfirmationVisible = value;
            }
        }

        private string _ResultMessage;
        public string ResultMessage
        {
            get
            {
                return _ResultMessage;
            }
            private set
            {
                _ResultMessage = value;
            }
        }

        private bool _ResultMessageVisible;
        public bool ResultMessageVisible
        {
            get
            {
                return _ResultMessageVisible;
            }
            private set
            {
                _ResultMessageVisible = value;
            }
        }

        private bool _CreationSuccess;
        public bool CreationSuccess
        {
            get
            {
                return _CreationSuccess;
            }
            private set
            {
                _CreationSuccess = value;
            }
        }

        private bool _CreateButtonVisible;
        public bool CreateButtonVisible
        {
            get
            {
                return _CreateButtonVisible;
            }
            private set
            {
                _CreateButtonVisible = value;
            }
        }

        private bool _RedirectButtonVisible;
        public bool RedirectButtonVisible
        {
            get
            {
                return _RedirectButtonVisible;
            }
            private set
            {
                _RedirectButtonVisible = value;
            }
        }

        public void CreateAccount()
        {
            if (IsFormValid())
            {
                IOperationResult result = Dependency.Locator.GetSingleton<IAccountManager>()
                                            .CreateAccount(this.Username, this.Password);

                if (result.Result == ResultValue.Success)
                {
                    this.ShowConcludedForm(Properties.Resources.AccountCreationSuccessful);
                    this.CreationSuccess = true;
                }
                else
                {
                    this.ShowFormError(result.Message);
                }
            }

            //Contract.Assume(!string.IsNullOrEmpty(ResultMessage));
        }

        private bool IsFormValid()
        {
            Contract.Ensures(!Contract.Result<bool>() ? !string.IsNullOrEmpty(this.ResultMessage) : true);
            Contract.Ensures(Contract.Result<bool>() ? !string.IsNullOrEmpty(this.Username) : true);
            Contract.Ensures(Contract.Result<bool>() ? !string.IsNullOrEmpty(this.Password) : true);
            Contract.Ensures(Contract.Result<bool>() ? this.Password.Equals(this.PasswordConfirmation) : true);

            bool isValid = true;
            if (string.IsNullOrEmpty(this.Username))
            {
                this.ShowFormError(Properties.Resources.BadEmptyUsername);
                isValid = false;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                this.ShowFormError(Properties.Resources.BadEmptyPassword);
                isValid = false;
            }

            if (this.PasswordConfirmation != this.Password)
            {
                this.ShowFormError(Properties.Resources.BadPasswordConfirmation);
                isValid = false;
            }

            return isValid;
        }

        private void ShowFormError(string message)
        {
            Contract.Requires(!string.IsNullOrEmpty(message));
            Contract.Ensures(this.ResultMessage == message);
            Contract.Ensures(!string.IsNullOrEmpty(this.ResultMessage));

            this.ResultMessage = message;
            this.ResultMessageVisible = true;
            this.CreationSuccess = false;
        }

        private void ShowConcludedForm(string message)
        {
            Contract.Requires(!string.IsNullOrEmpty(message));
            Contract.Ensures(this.ResultMessage == message);
            Contract.Ensures(!string.IsNullOrEmpty(this.ResultMessage));

            this.PasswordConfirmationVisible = false;
            this.PasswordVisible = false;
            this.RedirectButtonVisible = true;
            this.ResultMessageVisible = true;
            this.UsernameVisible = false;
            this.CreateButtonVisible = false;
            this.ResultMessage = message;
        }
        #endregion
    }
}
