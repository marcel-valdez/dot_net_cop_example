using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.UX.Test.Impl
{
    public class RoomPresentation : IRoomPresentation
    {

        private List<IRoomUserDTO> _Usuarios;
        private int _SelectedUserIndex;
        private IStatisticsPresentation _SelectedUserStatistics;
        private bool _ReadyForBattle;
        private bool _DialogVisible;
        private IUserDialogPresentation _CurrentDialog;

        public RoomPresentation()
        {
            _Usuarios = new List<IRoomUserDTO>();
            for (int i = 0; i < 20; i++)
            {
                string msg = "";
                switch (i % 3)
                {
                    case 0:
                        msg = "En sala";
                        break;
                    case 1:
                        msg = "Jugando";
                        break;
                    case 2:
                        msg = "Buscando";
                        break;
                }
                RoomUserDTO user = new RoomUserDTO("Player " + Convert.ToString(i), msg);
                IRoomUserDTO iUser = (IRoomUserDTO) user;
                _Usuarios.Add(iUser);
            }
        }

        public List<IRoomUserDTO> Usuarios
        {
            get
            {
                return _Usuarios;
            }
        }

        public int SelectedUserIndex
        {
            set
            {
                _SelectedUserIndex = value;
            }
        }

        public IStatisticsPresentation SelectedUserStatistics
        {
            get
            {
                StatisticsPresentation sp = new StatisticsPresentation(_Usuarios[_SelectedUserIndex].Username,100,50);
                _SelectedUserStatistics = (IStatisticsPresentation) sp;
                return _SelectedUserStatistics;
            }
        }

        public bool ReadyForBattle
        {
            get
            {
                return _ReadyForBattle;
            }
        }

        public bool DialogVisible
        {
            get
            {
                return _DialogVisible;
            }
        }

        public IUserDialogPresentation CurrentDialog
        {
            get
            {
                return _CurrentDialog;
            }
        }

        public void ChallengeButtonClicked()
        {
        }

        public void FindBattleButtonClicked()
        {
            UserDialogPresentation ud = new UserDialogPresentation("Buscando reto", new string[1] { "Cancelar" }, "Buscando nuevo reto");
            _CurrentDialog = (IUserDialogPresentation)ud;
        }

        public void HomeButtonClicked()
        {
        }

    }
}