namespace Game.Presentation.Impl
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using DependencyLocation;
    using Game.Logic;
    using Game.Presentation.Impl.Properties;

    internal class RoomPresentation : IRoomPresentation
    {
        IRoomUser roomUser;
        public RoomPresentation(string sessionId)
        {
            Contract.Requires(!String.IsNullOrEmpty(sessionId), "sessionId is null or empty.");
            this.Init(sessionId);
        }

        private void Init(string sessionId)
        {
            Contract.Requires(!String.IsNullOrEmpty(sessionId), "sessionId is null or empty.");

            var accMgr = Dependency.Locator.GetSingleton<IAccountManager>();
            IUserSession userSession = SessionHelper.GetSession(sessionId);
            IRoomsManager roomMgr = Dependency.Locator.GetSingleton<IRoomsManager>();
            IOperationResult<IRoomUser> operation = roomMgr.GetRoomUser(userSession);
            if (operation.Result == ResultValue.Success)
            {
                this.roomUser = operation.ResultData;
                this.Usuarios = this.roomUser.Room.Users
                                .Select(u => Dependency.Locator.Create<IRoomUserDTO>(u));
                this.SelectedUserIndex = 0;
                this.LogoutButton = Dependency.Locator.Create<ILogoutButtonPresentation>(sessionId);
                this.UpdateBasedOnRoomUser();
            }
            else
            {
                throw new NavigationException(string.Format(Resources.PrimeroDebeEntrarAUnCuarto));
            }
        }

        private void UpdateBasedOnRoomUser()
        {
            Contract.Requires(this.roomUser != null);
            if (this.roomUser.State == InRoomState.Playing)
            {
                ShowUserDialog(AlreadyInBattleDialog);
            }
            else if (this.roomUser.State == InRoomState.RequestingBattle)
            {
                ShowUserDialog(BattleRequestedDialog);
            }
            else if (this.roomUser.State == InRoomState.RequestingChallenge)
            {
                ShowUserDialog(ChallengeSentDialog);
            }
            else
            {
                IOperationResult<IReceivedChallenge> operation = this.roomUser.Room
                                                                     .ChallengesManager
                                                                     .GetReceivedChallenge(this.roomUser);
                if (operation.Result == ResultValue.Success)
                {
                    ShowUserDialog(CreateReceivedChallengeDialog(operation.ResultData));
                }
            }
        }


        private UserDialogOptions CreateReceivedChallengeDialog(IReceivedChallenge challenge)
        {
            return new UserDialogOptions
            {
                Title = Resources.ChallengeRecvdDialogTitle,
                Subtitle = String.Format(Resources.ChallengeRecvdDialogSubtitle, challenge.Challenger.Username),
                DialogMessage = Resources.ChallengeRecvdDialogMessage,
                ButtonsText = new string[] { Resources.AcceptBtnTxt, Resources.RejectBtnTxt },
                ButtonHandler = (index) =>
                {
                    if (challenge.State != ChallengeState.Cancelled)
                    {
                        if (index == 0)
                        {
                            // Se aceptó el reto
                            challenge.Accept();
                            this.FindBattleButonEnabled = false;
                            this.ChallengeButtonEnabled = false;
                            // Nota: 
                            // ¿Estará conectado el objeto IRoomUser?
                            // De lo contrario, hay que volver a pedir el objeto (asumiendo que ya se actualizó)
                            // ¿Mostrar un diálogo de espera en lo que se prepara la batalla?
                        }
                        else
                        {
                            // Se rechazó el reto
                            challenge.Reject();
                            this.FindBattleButonEnabled = true;
                            this.ChallengeButtonEnabled = true;
                        }
                    }
                    else
                    {
                        this.FindBattleButonEnabled = true;
                        this.ChallengeButtonEnabled = true;
                    }

                    this.DialogVisible = false;
                }
            };
        }

        private UserDialogOptions BattleRequestedDialog
        {
            get
            {
                return new UserDialogOptions
                {
                    Title = Resources.FindingBattleDialogTitle,
                    Subtitle = "",
                    DialogMessage = Resources.FindingBattleDialogMessage,
                    ButtonsText = new string[] { Resources.FindingBattleDialogButtonTxt },
                    ButtonHandler = _ =>
                    {
                        IBattleRequest battleRequest = this.roomUser.Room.FindBattle(this.roomUser);
                        if (battleRequest.State != RequestState.Ready)
                        {
                            battleRequest.Cancel();
                            this.FindBattleButonEnabled = true;
                            this.ChallengeButtonEnabled = true;
                        }
                        else
                        {
                            this.FindBattleButonEnabled = false;
                            this.ChallengeButtonEnabled = false;
                        }

                        this.DialogVisible = false;
                    }
                };
            }
        }

        private void ShowUserDialog(UserDialogOptions options)
        {
            this.FindBattleButonEnabled = false;
            this.ChallengeButtonEnabled = false;
            this.DialogVisible = true;
            this.CurrentDialogPresentation = new UserDialogPresentation(options);
        }

        private UserDialogOptions CreateMessageDialog(string title, string message)
        {
            return new UserDialogOptions
            {
                Title = title,
                DialogMessage = message,
                ButtonsText = new string[] { Resources.AcceptBtnTxt },
                ButtonHandler = _ =>
                {
                    this.DialogVisible = false;
                    this.FindBattleButonEnabled = true;
                    this.ChallengeButtonEnabled = true;
                }
            };
        }

        private UserDialogOptions AlreadyInBattleDialog
        {
            get
            {
                return new UserDialogOptions
                {
                    Title = Resources.PendingBattleDialogTitle,
                    Subtitle = Resources.PendingBattleDialogSubtitle,
                    DialogMessage = Resources.PendingBattleDialogMessage,
                    ButtonsText = new string[] { Resources.PendingBattleDialogBtnText },
                    ButtonHandler = _ =>
                    {
                        this.DialogVisible = false;
                        this.FindBattleButonEnabled = true;
                        this.ChallengeButtonEnabled = true;
                    }
                };
            }
        }

        private UserDialogOptions BadChosenChallengeeDialog
        {
            get
            {
                return new UserDialogOptions
                {
                    Title = Resources.Error,
                    Subtitle = "",
                    DialogMessage = String.Format(Resources.BadChosenChallengeeMessage, Resources.RoomUserIdle),
                    ButtonsText = new string[] { Resources.AcceptBtnTxt },
                    ButtonHandler = _ =>
                    {
                        this.DialogVisible = false;
                        this.FindBattleButonEnabled = true;
                        this.ChallengeButtonEnabled = true;
                    }
                };
            }
        }

        private UserDialogOptions ChallengeSentDialog
        {
            get
            {
                return new UserDialogOptions
                {
                    Title = Resources.ChallengeDialogTitle,
                    Subtitle = "",
                    DialogMessage = Resources.ChallengeDialogMessage,
                    ButtonsText = new string[] { Resources.ChallengeDialogCancelBtn },
                    ButtonHandler = _ =>
                    {

                        IOperationResult<IIssuedChallenge> operation = this.roomUser.Room.ChallengesManager
                                                    .GetIssuedChallenge(this.roomUser);

                        if (operation.Result == ResultValue.Success &&
                            operation.ResultData.State != ChallengeState.Accepted)
                        {
                            operation.ResultData.Cancel();
                            this.FindBattleButonEnabled = true;
                            this.ChallengeButtonEnabled = true;
                        }
                        else
                        {
                            this.FindBattleButonEnabled = false;
                            this.ChallengeButtonEnabled = false;
                        }

                        this.DialogVisible = false;
                    }
                };
            }
        }

        #region IRoomPresentation Members
        public IEnumerable<IRoomUserDTO> Usuarios
        {
            get;
            private set;
        }

        public int SelectedUserIndex
        {
            private get;
            set;
        }

        private IRoomUserDTO SelectedUser
        {
            get
            {
                return this.Usuarios.ElementAt(SelectedUserIndex);
            }
        }

        private IRoomUser SelectedRoomUser
        {
            get
            {
                return this.roomUser.Room.Users.First(u => u.Username == SelectedUser.Username);
            }
        }

        public IStatisticsPresentation SelectedUserStatistics
        {
            get
            {
                return Dependency.Locator.Create<IStatisticsPresentation>(SelectedUser.Username);
            }
        }

        public bool ReadyForBattle
        {
            get
            {
                return this.roomUser.State == InRoomState.Playing;
            }
        }

        public bool DialogVisible
        {
            get;
            private set;
        }

        public bool ChallengeButtonEnabled
        {
            get;
            private set;
        }

        public bool FindBattleButonEnabled
        {
            get;
            private set;
        }

        public IUserDialogPresentation CurrentDialogPresentation
        {
            get;
            private set;
        }

        public ILogoutButtonPresentation LogoutButton
        {
            get;
            private set;
        }

        public void ChallengeButtonClicked()
        {
            IChallengeManager challengeManager = this.roomUser.Room.ChallengesManager;
            IRoomUserDTO selectedUser = this.SelectedUser;
            if (selectedUser.Username != this.roomUser.Username &&
               selectedUser.RoomUserState == Resources.RoomUserIdle)
            {
                IOperationResult<IIssuedChallenge> operation = challengeManager
                    .Send(this.roomUser, this.SelectedRoomUser);
                if (operation.Result != ResultValue.Success)
                {
                    ShowUserDialog(CreateMessageDialog(Resources.Error, operation.Message));
                }
                else
                {
                    ShowUserDialog(ChallengeSentDialog);
                }
            }
            else
            {
                ShowUserDialog(BadChosenChallengeeDialog);
            }
        }

        public void FindBattleButtonClicked()
        {
            IBattleRequest request = this.roomUser.Room.FindBattle(this.roomUser);
            ShowUserDialog(BattleRequestedDialog);
        }

        /// <summary>
        /// Cancela las peticiones de reto y búsqueda de batalla.
        /// </summary>
        public void HomeButtonClicked()
        {
            // Si se va a home, entonces se cancela todo
            if (this.roomUser.State == InRoomState.RequestingBattle)
            {
                this.roomUser.Room
                    .FindBattle(this.roomUser)
                    .Cancel();
            }

            if (this.roomUser.State == InRoomState.RequestingChallenge)
            {
                var opResult = 
                    this.roomUser.Room.ChallengesManager
                        .GetIssuedChallenge(this.roomUser);

                if(opResult.Result == ResultValue.Success)
                {
                    opResult.ResultData.Cancel();
                }
            }

            Dependency.Locator.GetSingleton<IRoomsManager>()
                .LeaveRoom(this.roomUser);
        }
        #endregion
    }
}
