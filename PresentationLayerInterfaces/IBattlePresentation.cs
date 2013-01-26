namespace Game.Presentation
{
    using System.Collections.Generic;

    // Es la interfaz del modelo de presentación de una
    // batalla
    [System.Diagnostics.Contracts.ContractClass(typeof(IBattlePresentationCodeContract))]
    public interface IBattlePresentation
    {

        /// <summary>
        /// Obtiene el título de la página
        /// </summary>
        string Title
        {
            get;
        }

        // Título del mensaje de resultados
        string MiddleMsgTitle
        {
            get;
        }

        // Texto del label de daño causado
        string HarmInflictedTxt
        {
            get;
        }

        // Puntos de daño causados
        int HarmInflictedPts
        {
            get;
        }

        // Texto del label de daño recibido
        string HarmReceivedTxt
        {
            get;
        }

        // Puntos de daño recibido
        int HarmReceivedPts
        {
            get;
        }

        // El texto en la parte de abajo del mensaje central
        string MiddleMsgFooter
        {
            get;
        }

        // Determina si el botón de confirmación está habilitado
        bool ConfirmBtnEnabled
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether [harm results texts are visible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [harm results visible]; otherwise, <c>false</c>.
        /// </value>
        bool HarmResultsVisible
        {
            get;
        }

        // Contiene el modelo de presentación de la información del
        // usuario en juego
        IBattlePlayerPresentation MyInfo
        {
            get;
        }

        // Las cartas del usuario en juego
        IEnumerable<IBattleCardPresentation> MyCards
        {
            get;
        }

        // Contiene el modelo de presentación de la información del oponente
        IBattlePlayerPresentation OppInfo
        {
            get;
        }

        // Las cartas del oponente del usuario
        IEnumerable<IBattleCardPresentation> OppCards
        {
            get;
        }

        // Este método se debe mandar llamar cuando se presiona el botón de confirmar
        void ConfirmButtonPressed();

        // Determina si la batalla ha terminado
        bool HasBattleEnded
        {
            get;
        }

        // Determina si se está esperando a que el oponente haga su decisión
        bool IsWaitingForOpponent
        {
            get;
        }

        // Es el modelo de presentación del botón de logout
        ILogoutButtonPresentation LogoutButton
        {
            get;
        }
    }

    // Notas de implementación:
    // Como el evento de cuando ha hecho click el oponente para escoger su carta
    // es asíncrono, entonces tendrás que usar callbacks para actualizar el valor,
    // te recomiendo que mandes un callback cada 0.5 segundos, y entregues el valor
    // de WaitingForOpponent, y si WaitingForOpponent es true, entonces haz un
    // PostBack por medio de JavaScript:
    // WebForm_doPostBack(), y así se actualizará todo

    // Te recomiendo que hagas un UserControl, son los archivos .ascx, para las cartas
    // sino tendrás que hacer 8 veces el mismo código; un archivo .ascx es como 
    // otra paginita dentro de la página. (nótese: Composición de Componentes por agregación)
}