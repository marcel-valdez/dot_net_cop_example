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

        // El �ndice del usuario seleccionado de la lista de usuarios
        int SelectedUserIndex
        {
            set;
        }

        // Las estad�sticas del usuario seleccionado: Nota, si ocupas hacer esto
        // sin tener que recargar la p�gina entera, ponle una interfaz ICallbackEventHandler a la p�gina
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

        // Determina si est� visible el di�logo (popup)
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

        // Es el estado del di�logo de buscar batalla
        IUserDialogPresentation CurrentDialogPresentation
        {
            get;
        }

        // Es el modelo de presentaci�n del bot�n de logout
        ILogoutButtonPresentation LogoutButton
        {
            get;
        }

        // Es el m�todo a llamar cuando se haga click en el bot�n de retar
        void ChallengeButtonClicked();

        // Es el m�todo a llamar cuando se haga click sobre el bot�n de buscar batalla
        void FindBattleButtonClicked();

        // Es el m�todo a llamar cuando se haga click en el bot�n Home
        // Lo que se har� es sacar al jugador de la sala
        void HomeButtonClicked();
    }
}