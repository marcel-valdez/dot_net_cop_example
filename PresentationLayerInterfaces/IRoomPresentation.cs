namespace Game.Presentation
{
    using System.Diagnostics.Contracts;
    using System.Collections.Generic;

    [ContractClass(typeof(IRoomPresentationCodeContract))]
    public interface IRoomPresentation
    {
        // La lista de usuarios en la sala 
        IEnumerable<IRoomUserDTO> Usuarios
        {
            get;
        }

        // El índice del usuario seleccionado de la lista de usuarios
        int SelectedUserIndex
        {
            set;
        }

        // Las estadísticas del usuario seleccionado: Nota, si ocupas hacer esto
        // sin tener que recargar la página entera, ponle una interfaz ICallbackEventHandler a la página
        // http://msdn.microsoft.com/es-es/library/system.web.ui.icallbackeventhandler(v=VS.100).aspx
        IStatisticsPresentation SelectedUserStatistics
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether [ready for battle].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [ready for battle]; otherwise, <c>false</c>.
        /// </value>
        bool ReadyForBattle
        {
            get;
        }

        // Determina si está visible el diálogo (popup)
        bool DialogVisible
        {
            get;
        }

        bool ChallengeButtonEnabled
        {
            get;
        }


        /// <summary>
        /// Gets a value indicating whether [find battle buton enabled].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [find battle buton enabled]; otherwise, <c>false</c>.
        /// </value>
        bool FindBattleButonEnabled
        {
            get;
        }

        // Es el estado del diálogo de buscar batalla
        IUserDialogPresentation CurrentDialogPresentation
        {
            get;
        }

        // Es el modelo de presentación del botón de logout
        ILogoutButtonPresentation LogoutButton
        {
            get;
        }

        // Es el método a llamar cuando se haga click en el botón de retar
        void ChallengeButtonClicked();

        // Es el método a llamar cuando se haga click sobre el botón de buscar batalla
        void FindBattleButtonClicked();

        // Es el método a llamar cuando se haga click en el botón Home
        // Lo que se hará es sacar al jugador de la sala
        void HomeButtonClicked();
    }
}