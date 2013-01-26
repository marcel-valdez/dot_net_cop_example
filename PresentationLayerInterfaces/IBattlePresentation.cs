namespace Game.Presentation
{
    using System.Collections.Generic;

    // Es la interfaz del modelo de presentaci�n de una
    // batalla
    [System.Diagnostics.Contracts.ContractClass(typeof(IBattlePresentationCodeContract))]
    public interface IBattlePresentation
    {

        /// <summary>
        /// Obtiene el t�tulo de la p�gina
        /// </summary>
        string Title
        {
            get;
        }

        // T�tulo del mensaje de resultados
        string MiddleMsgTitle
        {
            get;
        }

        // Texto del label de da�o causado
        string HarmInflictedTxt
        {
            get;
        }

        // Puntos de da�o causados
        int HarmInflictedPts
        {
            get;
        }

        // Texto del label de da�o recibido
        string HarmReceivedTxt
        {
            get;
        }

        // Puntos de da�o recibido
        int HarmReceivedPts
        {
            get;
        }

        // El texto en la parte de abajo del mensaje central
        string MiddleMsgFooter
        {
            get;
        }

        // Determina si el bot�n de confirmaci�n est� habilitado
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

        // Contiene el modelo de presentaci�n de la informaci�n del
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

        // Contiene el modelo de presentaci�n de la informaci�n del oponente
        IBattlePlayerPresentation OppInfo
        {
            get;
        }

        // Las cartas del oponente del usuario
        IEnumerable<IBattleCardPresentation> OppCards
        {
            get;
        }

        // Este m�todo se debe mandar llamar cuando se presiona el bot�n de confirmar
        void ConfirmButtonPressed();

        // Determina si la batalla ha terminado
        bool HasBattleEnded
        {
            get;
        }

        // Determina si se est� esperando a que el oponente haga su decisi�n
        bool IsWaitingForOpponent
        {
            get;
        }

        // Es el modelo de presentaci�n del bot�n de logout
        ILogoutButtonPresentation LogoutButton
        {
            get;
        }
    }

    // Notas de implementaci�n:
    // Como el evento de cuando ha hecho click el oponente para escoger su carta
    // es as�ncrono, entonces tendr�s que usar callbacks para actualizar el valor,
    // te recomiendo que mandes un callback cada 0.5 segundos, y entregues el valor
    // de WaitingForOpponent, y si WaitingForOpponent es true, entonces haz un
    // PostBack por medio de JavaScript:
    // WebForm_doPostBack(), y as� se actualizar� todo

    // Te recomiendo que hagas un UserControl, son los archivos .ascx, para las cartas
    // sino tendr�s que hacer 8 veces el mismo c�digo; un archivo .ascx es como 
    // otra paginita dentro de la p�gina. (n�tese: Composici�n de Componentes por agregaci�n)
}