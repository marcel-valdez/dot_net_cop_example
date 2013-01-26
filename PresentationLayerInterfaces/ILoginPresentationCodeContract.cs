namespace Game.Presentation
{
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(ILoginPresentation))]
    internal abstract class ILoginPresentationCodeContract : ILoginPresentation
    {
        #region ILoginPresentation Members

        public string Username
        {
            set
            {
                Contract.Requires(!string.IsNullOrEmpty(value));
            }
        }

        public string Password
        {
            set
            {
                Contract.Requires(!string.IsNullOrEmpty(value));
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return default(bool);
            }
        }

        public string Message
        {
            get
            {
                Contract.Ensures(this.MessageVisible ? !string.IsNullOrEmpty(this.Message) : true);
                return default(string);
            }
        }

        public bool MessageVisible
        {
            get
            {
                Contract.Ensures(this.MessageVisible ? !string.IsNullOrEmpty(this.Message) : true);
                return default(bool);
            }
        }

        public void LoginClicked()
        {
            // Asegura que si el usuario ya estaba autentifiado, y vuelve a hacer click en login,
            // seguirá estando autentificado: se ignoran los valores user y password
            Contract.Ensures(Contract.OldValue<bool>(this.IsAuthenticated) ? this.IsAuthenticated : true);
        }
        #endregion
    }
}
