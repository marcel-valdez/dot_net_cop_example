using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.UX.Test
{
    public interface IHomePresentation
    {
        // Es el título que en el ejemplo viene siendo "Home"
        string Title
        {
            get;
        }

        // Es el mensaje entero que dice: "Hola!, [nombre de usuario]"
        string WelcomeMessage
        {
            get;
        }

        // Es el título de la lista de Salas: "Salas"
        string RoomsListTitle
        {
            get;
        }

        // Es la lista de salas disponibles
        IEnumerable<string> RoomNames
        {
            get;
        }

        // Es el índice de la sala que seleccionó el usuario: En el evento SelectedItemChanged puede actualizar este valor, aunque probablemente
        // sea mejor no mapear ese evento, y esperarte hasta que el usuario haga click en Ingresar para capturar el elemento seleccionado
        int SelectedRoomIndex
        {
            set;
        }

        // Este es el método a llamar cuando el usuario haga click en Ingresar
        void IngresarClicked();
    }
}
