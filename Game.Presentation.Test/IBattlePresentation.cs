using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.UX.Test
{
    public interface IBattlePresentation
    {

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
        string HarmReceivedPts
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

    }
}
