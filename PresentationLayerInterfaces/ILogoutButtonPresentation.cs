namespace Game.Presentation
{
    using System.Diagnostics.Contracts;

    // Es el modelo de presentación del botón de logout
    [ContractClass(typeof(ILogoutButtonPresentationCodeContract))]
    public interface ILogoutButtonPresentation
    {
        // El texto a mostrar para el botón, en el diseño de pantalla
        // sería el texto "Logout"
        string ButtonText
        {
            get;
        }

        // Operación a ejecutar cuando el usuarios haga click sobre el botón
        void LogoutButtonClicked();
    }
}