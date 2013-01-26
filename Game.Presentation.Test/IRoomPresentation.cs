using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.UX.Test
{
    public interface IRoomPresentation
    {

        // La lista de usuarios en la sala 
        List<IRoomUserDTO> Usuarios
        {
            get;
        }

        // El índice del usuario seleccionado de la lista de usuarios
        int SelectedUserIndex
        {
            set;
        }

        // Las estadísticas del usuario seleccionado
        IStatisticsPresentation SelectedUserStatistics
        {
            get;
        }

        bool ReadyForBattle
        {
            get;
        }

        // Determina si está visible el diálogo (popup)
        bool DialogVisible
        {
            get;
        }

        // Es el estado del diálogo de buscar batalla
        IUserDialogPresentation CurrentDialog //IUserDialogPresentation CurrentDialogPresentation
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
