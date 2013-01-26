namespace Game.Presentation
{
    using System.Diagnostics.Contracts;
    [ContractClassFor(typeof(ICreateAccountPresentation))]
    internal abstract class ICreateAccountPresentationCodeContract : ICreateAccountPresentation
    {
        #region ICreateAccountPresentation Members

        string ICreateAccountPresentation.Username
        {
            set
            {
                Contract.Requires(!string.IsNullOrEmpty(value));
            }
        }

        bool ICreateAccountPresentation.UsernameVisible
        {
            get
            {
                return default(bool);
            }
        }

        string ICreateAccountPresentation.Password
        {
            set
            {
                Contract.Requires(!string.IsNullOrEmpty(value));
            }
        }

        bool ICreateAccountPresentation.PasswordVisible
        {
            get
            {
                return default(bool);
            }
        }

        string ICreateAccountPresentation.PasswordConfirmation
        {
            set
            {
                Contract.Requires(!string.IsNullOrEmpty(value));
            }
        }

        bool ICreateAccountPresentation.PasswordConfirmationVisible
        {
            get
            {
                return default(bool);
            }
        }

        string ICreateAccountPresentation.ResultMessage
        {
            get
            {
                return default(string);
            }
        }

        bool ICreateAccountPresentation.ResultMessageVisible
        {
            get
            {
                return default(bool);
            }
        }

        bool ICreateAccountPresentation.CreationSuccess
        {
            get
            {
                return default(bool);
            }
        }

        bool ICreateAccountPresentation.CreateButtonVisible
        {
            get
            {
                return default(bool);
            }
        }

        bool ICreateAccountPresentation.RedirectButtonVisible
        {
            get
            {
                return default(bool);
            }
        }

        void ICreateAccountPresentation.CreateAccount()
        {
            Contract.Requires((this as ICreateAccountPresentation).CreateButtonVisible == true);
            Contract.Ensures(!string.IsNullOrEmpty((this as ICreateAccountPresentation).ResultMessage));
            Contract.Ensures((this as ICreateAccountPresentation).ResultMessageVisible);
        }

        #endregion
    }
}
