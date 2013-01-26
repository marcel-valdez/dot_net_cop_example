namespace Game.Presentation
{
    [System.Diagnostics.Contracts.ContractClass(typeof(ICreateAccountPresentationCodeContract))]
    public interface ICreateAccountPresentation
    {
        // El nombre de usuario que ingrese el cliente
        string Username
        {
            set;
        }

        // Determina si es visible el label y textbox de nombre de usuario
        bool UsernameVisible
        {
            get;
        }

        // El password que introduzca el cliente
        string Password
        {
            set;
        }

        // Determina si es visible el label y textbox de password
        bool PasswordVisible
        {
            get;
        }

        // La confirmación del password de cliente
        string PasswordConfirmation
        {
            set;
        }

        // Determina si es visible el label y textbox de confirmación de password
        bool PasswordConfirmationVisible
        {
            get;
        }

        // Es el mensaje a mostrar en un label
        string ResultMessage
        {
            get;
        }

        // Determina si el mensaje es visible
        bool ResultMessageVisible
        {
            get;
        }

        // Determina si la operación fue exitosa
        bool CreationSuccess
        {
            get;
        }

        // Determina si el botón de crear cuenta está visible
        bool CreateButtonVisible
        {
            get;
        }

        // Determina si el botón de redireccionar está visible
        bool RedirectButtonVisible
        {
            get;
        }

        // Este método se debe mandar llamar cuando el usuario haga click en crear botón
        void CreateAccount();
    }
}