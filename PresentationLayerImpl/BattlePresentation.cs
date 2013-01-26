namespace Game.Presentation.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Diagnostics.Contracts;
    using Game.Logic;
    using DependencyLocation;

    internal class BattlePresentation : IBattlePresentation
    {
        IBattleDirector director;
        IRoomUser roomUser;
        IPlayer player;
        IBattleScenario scenario;
        IPlayer opponent;
        IUserSession session;
        Action buttonHandler;
        int myHpLastTurn;
        int oppHpLastTurn;

        public BattlePresentation(string sessionId)
        {
            Contract.Requires(!string.IsNullOrEmpty(sessionId));
            this.Init(sessionId);
        }

        private void Init(string sessionId)
        {
            Contract.Requires(!string.IsNullOrEmpty(sessionId));
            session = SessionHelper.GetSession(sessionId);
            var result = Dependency.Locator.GetSingleton<IRoomsManager>()
                                   .GetRoomUser(session);
            if (result.Result == ResultValue.Success)
            {
                roomUser = result.ResultData;
                director = roomUser.Room.BattlesManager
                                        .GetOngoingBattle(roomUser);
                scenario = director.Scenario;
                this.InitCommonState();
                switch (scenario.State)
                {
                    case BattleState.WaitingForCardElection:
                        this.SetCardElectionState();
                        break;
                    case BattleState.WaitingForCardRemoval:
                        this.SetCardRemovalState();
                        break;
                    case BattleState.CalculatingResult:
                        this.SetCalculatingState();
                        break;
                    case BattleState.Concluded:
                        this.SetConcludedState();
                        break;
                    case BattleState.Aborted:
                        this.SetAbortedState();
                        break;
                }
            }
            else
            {
                throw new NavigationException("Primero debe iniciar una batalla");
            }
        }

        private void InitCommonState()
        {
            if (scenario.PlayerA.Username.Equals(this.session.Username))
            {
                this.player = scenario.PlayerA;
                this.opponent = scenario.PlayerB;
            }
            else
            {
                this.player = scenario.PlayerB;
                this.opponent = scenario.PlayerA;
            }

            this.MyInfo = Dependency.Locator.Create<IBattlePlayerPresentation>(this.player);
            this.OppInfo = Dependency.Locator.Create<IBattlePlayerPresentation>(this.opponent);
            this.mMyCards = this.player.Deck.Cards.Select(c => new BattleCardPresentation(c));
            this.mOppCards = this.opponent.Deck.Cards.Select(c => new BattleCardPresentation(c));
            this.LogoutButton = Dependency.Locator.Create<ILogoutButtonPresentation>(this.session.SessionId);

            var operation = this.session.GetValue<IDictionary<IPlayer, int>>(BattleHistory.PreviousHp);
            if (operation.Result == ResultValue.Success)
            {
                myHpLastTurn = operation.ResultData[this.player];
                oppHpLastTurn = operation.ResultData[this.opponent];
            }
        }

        private void SetCardElectionState()
        {
            this.HarmResultsVisible = false;
            this.MiddleMsgFooter = "Elija la carta a lanzar";
            this.ConfirmBtnEnabled = true;
            this.buttonHandler = () =>
                {
                    var operation = this.player.Deck.Choose(this.player.Deck.Cards
                                                    .Where(c => ChosenCards.Any(cc => cc.CardName == c.Name)));
                    if (operation.Result == ResultValue.Fail)
                    {
                        this.MiddleMsgTitle = operation.Message;
                        this.HarmResultsVisible = false;
                        this.ConfirmBtnEnabled = true;
                        foreach (var card in this.mMyCards)
                        {
                            card.Selected = false;
                        }
                    }
                    else
                    {
                        if (this.opponent.Deck.HasChosen)
                        {
                            this.SetCalculatingState();
                        }
                        else
                        {
                            this.SetWaitingForOpponent();
                        }
                    }
                };

        }

        private void SetWaitingForOpponent()
        {
            this.MiddleMsgTitle = "Esperando al oponente";
            this.MiddleMsgFooter = "";
            this.HarmResultsVisible = false;
            this.ConfirmBtnEnabled = false;
            foreach (var card in this.mMyCards)
            {
                card.Selectable = false;
            }

            this.buttonHandler = null;
        }

        private void SetAbortedState()
        {
            this.MiddleMsgTitle = "La batalla fue abortada!";
            this.MiddleMsgFooter = "";
            this.HarmResultsVisible = false;
            this.ConfirmBtnEnabled = true;
            foreach (var card in this.mMyCards)
            {
                card.Selectable = false;
            }

            this.buttonHandler = null;
        }

        private void SetConcludedState()
        {
            this.MiddleMsgTitle = "La batalla ha finalizado!";
            bool playerA = this.player == this.scenario.PlayerA;
            IPlayer winner = this.scenario.Result == BattleResult.PlayerAWon ? this.scenario.PlayerA : this.scenario.PlayerB;
            this.MiddleMsgFooter = String.Format("{0} ha vencido!", winner.Username);
            this.HarmResultsVisible = false;
            this.ConfirmBtnEnabled = true;
            foreach (var card in this.mMyCards)
            {
                card.Selectable = false;
            }

            this.buttonHandler = null;
        }

        private void SetCalculatingState()
        {
            this.MiddleMsgTitle = "Atacando al enemigo!";
            this.MiddleMsgFooter = "Sin piedad!";
            this.HarmResultsVisible = false;
            this.ConfirmBtnEnabled = false;
            foreach (var card in this.mMyCards)
            {
                card.Selectable = false;
            }

            this.buttonHandler = null;
        }

        private void SetShufflingCards()
        {
            this.MiddleMsgTitle = "Canjeando cartas";
            this.MiddleMsgFooter = "Buena suerte!";
            this.HarmResultsVisible = false;
            this.ConfirmBtnEnabled = false;
            foreach (var card in this.mMyCards)
            {
                card.Selectable = false;
            }

            this.buttonHandler = null;
        }

        private void SetCardRemovalState()
        {
            if (myHpLastTurn != 0 || oppHpLastTurn != 0)
            {
                this.HarmReceivedPts = myHpLastTurn - this.player.LifePoints;
                this.HarmInflictedPts = oppHpLastTurn - this.opponent.LifePoints;
            }

            var cardOperation = this.session.GetValue<IEnumerable<ICard>>(BattleHistory.PreviousThrownCards);
            if (cardOperation.Result == ResultValue.Success)
            {
                foreach (var card in cardOperation.ResultData)
                {
                    // Son las cartas que se tienen que ir
                    var pCard = this.mMyCards.First(c => c.CardName == card.Name);
                    pCard.Selectable = false;
                    pCard.Selected = true;
                }
            }

            this.MiddleMsgTitle = "Resultados";
            this.MiddleMsgFooter = "Elija cartas a cambiar";
            this.HarmResultsVisible = true;
            this.ConfirmBtnEnabled = true;
            this.buttonHandler = () =>
            {
                IEnumerable<ICard> playerCards = this.player.Deck.Cards;
                IBattleDeck playerDeck = this.player.Deck;
                IOperationResult operation =
                    playerDeck.Choose(
                        playerCards.Where(c => ChosenCards.Any(cc => cc.CardName == c.Name)));

                if (operation.Result == ResultValue.Fail)
                {
                    this.MiddleMsgTitle = operation.Message;
                    this.HarmResultsVisible = false;
                    this.ConfirmBtnEnabled = true;
                    foreach (var card in this.mMyCards)
                    {
                        if (card.Selectable)
                        {
                            card.Selected = false;
                        }
                    }
                }
                else
                {
                    if (this.opponent.Deck.HasChosen)
                    {
                        this.SetShufflingCards();
                    }
                    else
                    {
                        this.SetWaitingForOpponent();
                    }
                }
            };

        }
        #region IBattlePresentation Members

        public string Title
        {
            get
            {
                return "Battle Scenario";
            }
        }

        public string MiddleMsgTitle
        {
            get;
            private set;
        }

        public string HarmInflictedTxt
        {
            get
            {
                return "Daño hecho: ";
            }
        }

        public int HarmInflictedPts
        {
            get;
            private set;
        }

        public string HarmReceivedTxt
        {
            get
            {
                return "Daño recibido: ";
            }
        }

        public int HarmReceivedPts
        {
            get;
            private set;
        }

        public string MiddleMsgFooter
        {
            get;
            private set;
        }

        public bool ConfirmBtnEnabled
        {
            get;
            private set;
        }

        public bool HarmResultsVisible
        {
            get;
            private set;
        }

        public IBattlePlayerPresentation MyInfo
        {
            get;
            private set;
        }

        private IEnumerable<BattleCardPresentation> mMyCards;
        public IEnumerable<IBattleCardPresentation> MyCards
        {
            get
            {
                return mMyCards;
            }
        }

        private IEnumerable<IBattleCardPresentation> ChosenCards
        {
            get
            {
                return this.mMyCards.Where(c => c.Selected);
            }
        }

        public IBattlePlayerPresentation OppInfo
        {
            get;
            private set;
        }

        private IEnumerable<BattleCardPresentation> mOppCards;
        public IEnumerable<IBattleCardPresentation> OppCards
        {
            get
            {
                return mOppCards;
            }
        }

        public void ConfirmButtonPressed()
        {
            if (buttonHandler != null)
            {
                buttonHandler();
            }
        }

        public bool HasBattleEnded
        {
            get
            {
                return this.scenario.State == BattleState.Concluded || this.scenario.State == BattleState.Aborted;
            }
        }

        public bool IsWaitingForOpponent
        {
            get
            {
                return this.player.Deck.HasChosen && !this.opponent.Deck.HasChosen
                    && (this.scenario.State == BattleState.WaitingForCardElection ||
                    this.scenario.State == BattleState.WaitingForCardRemoval);
            }
        }

        public ILogoutButtonPresentation LogoutButton
        {
            get;
            private set;
        }
        #endregion
    }
}
