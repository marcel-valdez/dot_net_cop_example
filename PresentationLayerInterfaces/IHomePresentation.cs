namespace Game.Presentation
{
    using System.Diagnostics.Contracts;
    using System.Collections.Generic;

    [ContractClass(typeof(IHomePresentationCodeContract))]
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

        // Es el modelo de presentación del botón de logout
        ILogoutButtonPresentation LogoutButton
        {
            get;
        }

        /// <summary>
        /// Es el modelo de presentación de las estadísticas de jugador
        /// </summary>
        IStatisticsPresentation UserStats
        {
            get;
        }

        /// <summary>
        /// Gets the message to show to the user.
        /// </summary>
        string Message
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether the [message is visible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if the [message is visible]; otherwise, <c>false</c>.
        /// </value>
        bool MessageVisible
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether this user has joined a room (after clicking join rooom).
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has joined room; otherwise, <c>false</c>.
        /// </value>
        bool HasJoinedRoom
        {
            get;
        }

        // Este es el método a llamar cuando el usuario haga click en Ingresar
        void IngresarClicked();
    }
}