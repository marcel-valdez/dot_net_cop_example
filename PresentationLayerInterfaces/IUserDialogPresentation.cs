namespace Game.Presentation
{
    using System.Collections.Generic;

    // Una interfaz gen�rica CurrentDialogPresentation para cualquier di�logo de usuario
    [System.Diagnostics.Contracts.ContractClass(typeof(IUserDialogPresentationCodeContract))]
    public interface IUserDialogPresentation
    {
        // El texto del t�tulo del di�logo de usuario
        string Title
        {
            get;
        }

        // El texto del subt�tulo del gi�logo de usuario
        // En el ejemplo ser�a el mensaje de reto: "[Nombre de Retador] te ha retado!"
        string Subtitle
        {
            get;
        }

        // El mensaje principal del di�logo de usuario
        // En el ejmplo es el texto con el que pregunta �Aceptas el reto? o Esperando un oponente
        string DialogMessage
        {
            get;
        }

        // Tiene los textos de los botones a mostrar
        IEnumerable<string> ButtonsText
        {
            get;
        }

        // Se debe poner el �ndice del bot�n que presion� el usuario.
        int PressedButtonIndex
        {
            set;
        }

        void ButtonClicked();
    }
}