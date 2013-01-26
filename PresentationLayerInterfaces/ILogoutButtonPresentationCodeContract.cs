namespace Game.Presentation
{
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(ILogoutButtonPresentation))]
    internal abstract class ILogoutButtonPresentationCodeContract : ILogoutButtonPresentation
    {
        public void LogoutButtonClicked()
        {
            // No se puede verificar, pero debe asegurar que no exista sesión alguna.
        }

        #region ILogoutButtonPresentation Members

        public string ButtonText
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }
        }

        #endregion
    }
}