using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.UX.Test
{
    public interface IUserDialogPresentation
    {

        // El texto del título del diálogo de usuario
        string Title
        {
            get;
        }

        // El texto del subtítulo del giálogo de usuario
        // En el ejemplo sería el mensaje de reto: "[Nombre de Retador] te ha retado!"
        string Subtitle
        {
            get;
        }

        // El mensaje principal del diálogo de usuario
        // En el ejmplo es el texto con el que pregunta ¿Aceptas el reto? o Esperando un oponente
        string DialogMessage
        {
            get;
        }

        // Tiene los textos de los botones a mostrar
        IEnumerable<string> ButtonsText
        {
            get;
        }

        // Se debe poner el índice del botón que presionó el usuario.
        int PressedButtonIndex
        {
            set;
        }

        void ButtonClicked();
  
    }
}
