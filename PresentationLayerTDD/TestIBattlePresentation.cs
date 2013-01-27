namespace Game.Presentation.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DependencyLocation;
    using FizzWare.NBuilder;
    using Game.Logic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TestingTools.Core;
    using TestingTools.Extensions;

    [TestClass]
    public class TestIBattlePresentation
    {
        static IRoomUser mRoomUser;
        static IPlayer mUser;
        static IPlayer mOpp;
        static IRoom mRoom;
        static IEnumerable<ICard> mUserCards;
        static IEnumerable<ICard> mOppCards;
        static IBattleDirector mBattleDirector;

        static IEnumerable<ICard> UserCards
        {
            get
            {
                return mUserCards;
            }
        }

        static IEnumerable<ICard> OppCards
        {
            get
            {
                return mOppCards;
            }
        }

        static IPlayer User
        {
            get
            {
                return mUser;
            }
        }

        static IPlayer Opp
        {
            get
            {
                return mOpp;
            }
        }

        static IRoom Room
        {
            get
            {
                return mRoom;
            }
        }

        static IBattleDirector BattleDirector
        {
            get
            {
                return mBattleDirector;
            }
        }

        static IRoomUser RoomUser
        {
            get
            {
                return mRoomUser;
            }
        }

        static IUserSession UserSession
        {
            get;
            set;
        }

        static IUserSession OppSession
        {
            get;
            set;
        }

        static string SessionId
        {
            get
            {
                return "0";
            }
        }

        static string OppSessionId
        {
            get
            {
                return "1";
            }
        }

        public static IBattlePresentation CreateInstance()
        {
            // El constructor debería recibir el usuario que está en la batalla,
            // y el director de la batalla en que se encuentra
            return Dependency.Locator.Create<IBattlePresentation>(SessionId);
        }


        [ClassInitialize]
        public static void Init(TestContext tc)
        {
            Initializer.ReleaseDependencies();
            Initializer.LoadDependencies();
            // Mock Room User
            Mock<IRoomUser> roomUser = MakeRoomUser();
            mRoomUser = roomUser.Object;

            // Make user session
            Func<IOperationResult<IDictionary<IPlayer, int>>> valuesGetter = () =>
            {
                IDictionary<IPlayer, int> values = new Dictionary<IPlayer, int>();
                values.Add(Opp, 10);
                values.Add(User, 20);
                return Mok.Helper.CreateResultWith(ResultValue.Fail, values);
            };

            Mock<IUserSession> uSession = MakeUserSession(roomUser, valuesGetter);
            UserSession = uSession.Object;

            Mock<IRoomUser> roomOpponent = MakeRoomOpponent();
            Mock<IUserSession> oSession = MakeOpponentSession(roomOpponent, valuesGetter);
            OppSession = oSession.Object;

            Mok.Helper.SetupAccountManager(ResultValue.Fail, ResultValue.Success, uSession.Object, oSession.Object);

            // Setup the room
            Mock<IRoom> moqRoom = MakeRoom(roomUser, roomOpponent);
            mRoom = moqRoom.Object;


            // Setup the users' cards (8)
            IOperable<Mock<ICard>> buildOperation = CreateCardsMaker();
            int i = 0;
            mUserCards = buildOperation
                .With(moqCard => moqCard.SetupGet(c => c.Name).Returns("user-card#" + ((++i % 8) + 1)))
                .Build().Select(moq => moq.Object);
            mOppCards = buildOperation
                .With(moqCard => moqCard.SetupGet(c => c.Name).Returns("opp-card#" + ((++i % 8) + 1)))
                .Build().Select(moq => moq.Object);

            // Setup a successful operation result
            Mock<IOperationResult> opResult = MakeOperationResult();

            // Setup the users
            Mock<IPlayer> moqUser = SetupUsers(opResult);
            mUser = moqUser.Object;

            // Setup the battle opponent
            Mock<IPlayer> moqOpp = MakeOpponentPlayer(opResult);
            mOpp = moqOpp.Object;

            // Setup the battle scenario
            Mock<IBattleDirector> director = MakeBattleDirector(opResult);
            mBattleDirector = director.Object;

            // Setup the Rooms Manager
            Mock<IRoomsManager> moqRoomsManager = MakeRoomsManager(roomOpponent);

            Mok.Helper.Inject(moqRoomsManager.Object);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            // TODO: Eliminate all globally static elements
        }

        /// <summary>
        /// Verifies that the instance [is correctly initialized].
        /// </summary>
        [TestMethod]
        public void IsCorrectlyInitialized()
        {
            // Arrange
            IBattlePresentation target;

            // Act
            target = TestIBattlePresentation.CreateInstance();

            // Assert
            Verify.That(target.ConfirmBtnEnabled).IsTrue()
                .Now();
            Verify.That(target.HarmResultsVisible).IsFalse()
                .Now();
            Verify.That(target.Title).ItsTrueThat(msg => !string.IsNullOrEmpty(msg))
                .Now();
            Verify.That(target.MiddleMsgTitle).ItsTrueThat(msg => string.IsNullOrEmpty(msg))
                .Now();
            Verify.That(target.MyCards).IsOfSize(8)
                .Now();
            Verify.That(target.OppCards).IsOfSize(8)
                .Now();
            Verify.That(target.LogoutButton).IsNotNull()
                .Now();
            Verify.That(target.HasBattleEnded).IsFalse()
                .Now();
            Verify.That(target.OppInfo).IsNotNull()
                .Now();
            Verify.That(target.MyInfo).IsNotNull()
                .Now();
            Verify.That(target.MyCards).IsTrueForAll(card => card.Selectable)
                .Now();
            Verify.That(target.OppCards).IsTrueForAll(card => !card.Selectable)
                .Now();
        }

        /// <summary>
        /// Verifies that its possible to pick a card from the available cards
        /// </summary>
        [TestMethod]
        public void CanPickCardsOrCardToUse()
        {
            // Arrange
            IBattlePresentation target = TestIBattlePresentation.CreateInstance();

            // Act
            target.MyCards.ElementAt(0).Selected = true;
            target.ConfirmButtonPressed();
            target = TestIBattlePresentation.CreateInstance();

            // Assert
            Verify.That(target.MyCards)
                .IsTrueForAll(card => !card.Selectable)
                .Now();
            /*Verify.That(target.IsWaitingForOpponent)
                .IsTrue()
                .Now();*/
        }

        /// <summary>
        /// Verifies that its possible for the battle to finish, when the state of the battle is set to concluded
        /// </summary>
        [TestMethod]
        public void CanFinishBattle()
        {
            // Arrange
            IBattlePresentation target = TestIBattlePresentation.CreateInstance();

            // Act
            target.MyCards.ElementAt(0).Selected = true;
            target.ConfirmButtonPressed();
            Mock.Get(BattleDirector.Scenario)
                .SetupGet(sce => sce.State)
                .Returns(BattleState.Concluded);


            // Assert
            Verify.That(target.HasBattleEnded).IsTrue()
                .Now();
        }

        /// <summary>
        /// TODO: Verifies that the board changes when the turn changes: 
        /// Show Options -> Choose -> Show Results -> Choose Rejected -> Show Options
        /// </summary>
        [TestMethod]
        public void CanPlayAWholeTurn()
        {
            // Arrange
            Mock.Get(BattleDirector.Scenario)
                .SetupGet(sce => sce.State)
                .Returns(BattleState.WaitingForCardElection);
            IBattlePresentation target = TestIBattlePresentation.CreateInstance();

            // Act
            target.MyCards.ElementAt(0).Selected = true;
            target.ConfirmButtonPressed();

            // Assert
            Verify.That(target.HarmResultsVisible).IsFalse()
                .Now();
            /*Verify.That(target.IsWaitingForOpponent).IsTrue()
                .Now();*/
            Verify.That(target.ConfirmBtnEnabled).IsFalse()
                .Now();

            // Act II
            Mock.Get(BattleDirector.Scenario)
                .SetupGet(sce => sce.State)
                .Returns(BattleState.CalculatingResult);

            // Assert II
            Verify.That(target.HarmResultsVisible).IsFalse()
                .Now();
            /*Verify.That(target.IsWaitingForOpponent).IsFalse()
                .Now();*/
            Verify.That(target.ConfirmBtnEnabled).IsFalse()
                .Now();

            // Act III
            Func<IOperationResult<IDictionary<IPlayer, int>>> valuesGetter = () =>
                               {
                                   IDictionary<IPlayer, int> values = new Dictionary<IPlayer, int>();
                                   values.Add(Opp, 10);
                                   values.Add(User, 20);
                                   return Mok.Helper.CreateResultWith(ResultValue.Success, values);
                               };
            Mock.Get(BattleDirector.Scenario)
                .SetupGet(sce => sce.State)
                .Returns(BattleState.WaitingForCardRemoval);
            Mock.Get(UserSession)
                .Setup(u => u.GetValue<IDictionary<IPlayer, int>>(It.IsAny<object>()))
                .Returns(valuesGetter);
            Mock.Get(OppSession)
                .Setup(u => u.GetValue<IDictionary<IPlayer, int>>(It.IsAny<object>()))
                .Returns(valuesGetter);
            target = TestIBattlePresentation.CreateInstance();

            // Assert III
            Verify.That(target.HarmResultsVisible).IsTrue()
                .Now();
            /*Verify.That(target.IsWaitingForOpponent).IsFalse()
                .Now();*/
            Verify.That(target.ConfirmBtnEnabled).IsTrue()
                .Now();
            Verify.That(target.MyCards.ElementAt(0).Selectable)
                .IsFalse()
                .Now();
            Verify.That(target.MyCards.Skip(1))
                .IsTrueForAll(card => card.Selectable)
                .Now();

            // Act IV
            target.MyCards.ElementAt(1).Selected = true;
            target.ConfirmButtonPressed();
            target = TestIBattlePresentation.CreateInstance();

            // Assert IV
            Verify.That(target.HarmResultsVisible).IsTrue()
                .Now();
            /*Verify.That(target.ConfirmBtnEnabled).IsFalse()
                .Now();*/
            /*Verify.That(target.IsWaitingForOpponent).IsTrue()
                .Now();*/
            Verify.That(target.MyCards).IsTrueForAll(card => !card.Selectable)
                .Now();

            // Act V
            Mock.Get(BattleDirector.Scenario)
                .SetupGet(sce => sce.State)
                .Returns(BattleState.WaitingForCardElection);
            target = TestIBattlePresentation.CreateInstance();

            // Assert V
            Verify.That(target.MyCards).IsTrueForAll(card => card.Selectable)
                .Now();
            Verify.That(target.OppCards).IsTrueForAll(card => card.Selectable)
                .Now();
            Verify.That(target.HarmResultsVisible).IsFalse()
                .Now();
            Verify.That(target.ConfirmBtnEnabled).IsTrue()
                .Now();
        }

        private static Mock<IOperationResult> MakeOperationResult()
        {
            Mock<IOperationResult> opResult = new Mock<IOperationResult>();
            opResult.SetupGet(result => result.Result)
                .Returns(ResultValue.Success);
            opResult.SetupGet(result => result.Message)
                .Returns("Success");
            return opResult;
        }

        private static Mock<IRoomUser> MakeRoomUser()
        {
            Mock<IRoomUser> rUser = new Mock<IRoomUser>();
            rUser.SetupGet(u => u.Room)
                .Returns(() => Room);
            rUser.SetupGet(u => u.Username)
                .Returns("user");
            rUser.SetupGet(u => u.State)
                .Returns(InRoomState.Playing);
            return rUser;
        }

        private static Mock<IUserSession> MakeUserSession(Mock<IRoomUser> roomUser, Func<IOperationResult<IDictionary<IPlayer, int>>> valuesGetter)
        {

            Mock<IUserSession> uSession = Mok.Helper.CreateSession(roomUser.Object, "0");
            uSession.Setup(s => s.GetValue<IDictionary<IPlayer, int>>(It.IsAny<object>()))
                    .Returns(valuesGetter);

            uSession.Setup(s => s.GetValue<IEnumerable<ICard>>(It.IsAny<object>()))
                    .Returns(() =>
                    {
                        return Mok.Helper.CreateResultWith<IEnumerable<ICard>>(ResultValue.Fail, default(IEnumerable<ICard>));
                    });

            return uSession;
        }

        private static Mock<IRoomUser> MakeRoomOpponent()
        {
            Mock<IRoomUser> roomOpponent = new Mock<IRoomUser>();
            roomOpponent.SetupGet(u => u.Room)
                .Returns(() => Room);
            roomOpponent.SetupGet(u => u.Username)
                .Returns("opp");
            roomOpponent.SetupGet(u => u.State)
                .Returns(InRoomState.Playing);
            return roomOpponent;
        }

        private static Mock<IUserSession> MakeOpponentSession(Mock<IRoomUser> roomOpponent, Func<IOperationResult<IDictionary<IPlayer, int>>> valuesGetter)
        {
            Mock<IUserSession> oSession = Mok.Helper.CreateSession(roomOpponent.Object, "1");
            oSession.Setup(s => s.GetValue<IDictionary<IPlayer, int>>(It.IsAny<object>()))
                    .Returns(valuesGetter);
            oSession.Setup(s => s.GetValue<IEnumerable<ICard>>(It.IsAny<object>()))
                    .Returns(() =>
                    {
                        return Mok.Helper.CreateResultWith<IEnumerable<ICard>>(ResultValue.Fail, default(IEnumerable<ICard>));
                    });
            return oSession;
        }

        private static Mock<IRoom> MakeRoom(Mock<IRoomUser> user, Mock<IRoomUser> opponent)
        {
            Mock<IRoom> moqRoom = new Mock<IRoom>();
            moqRoom.SetupGet(r => r.Name)
                .Returns("room");
            moqRoom.SetupGet(r => r.Users)
                .Returns(() => new IRoomUser[] { user.Object, opponent.Object });
            moqRoom.SetupGet(r => r.BattlesManager)
                .Returns(
                () =>
                {
                    Mock<IBattleManager> bMgr = new Mock<IBattleManager>();
                    bMgr.Setup(mgr => mgr.GetOngoingBattle(
                            It.Is<IRoomUser>(u => u.Equals(user.Object) || u.Equals(opponent.Object))))
                        .Returns(() => BattleDirector);

                    return bMgr.Object;
                });
            return moqRoom;
        }
        private static IOperable<Mock<ICard>> CreateCardsMaker()
        {
            IOperable<Mock<ICard>> buildOperation;
            Random rand = new Random();
            int minAtk = 1;
            int maxAtk = 100;
            int minDef = 1;
            int maxDef = 100;
            buildOperation = Builder<Mock<ICard>>
                            .CreateListOfSize(8)
                            .All()
                            .With(moqCard => moqCard.SetupGet(c => c.AttackPoints).Returns(rand.Next(minAtk, maxAtk)))
                            .With(moqCard => moqCard.SetupGet(c => c.DefensePoints).Returns(rand.Next(minDef, maxDef)))
                            .With(moqCard => moqCard.SetupGet(c => c.Effect).Returns(default(IEffect)))
                            .With(moqCard => moqCard.SetupGet(c => c.ImageUrl).Returns("card-url"));

            return buildOperation;
        }

        private static Mock<IPlayer> SetupUsers(Mock<IOperationResult> operationResult)
        {
            Mock<IPlayer> moqUser = new Mock<IPlayer>();
            moqUser.SetupGet(u => u.Username)
                .Returns("user");
            moqUser.SetupGet(u => u.LifePoints)
                .Returns(100);
            moqUser.SetupGet(u => u.Turn)
                .Returns(PlayerTurn.FirstToAttack);
            moqUser.SetupGet(u => u.Deck)
                .Returns(
                () =>
                {
                    // Setup the user's battle deck
                    Mock<IBattleDeck> deck = new Mock<IBattleDeck>();
                    deck.SetupGet(d => d.Cards)
                        .Returns(() => UserCards);

                    deck.SetupGet(d => d.MaxToChoose)
                        .Returns(1);

                    deck.Setup(d => d.Choose(It.IsAny<IEnumerable<ICard>>()))
                        .Returns(() => operationResult.Object);

                    return deck.Object;
                });
            return moqUser;
        }

        private static Mock<IPlayer> MakeOpponentPlayer(Mock<IOperationResult> opResult)
        {
            Mock<IPlayer> moqOpp = new Mock<IPlayer>();
            moqOpp.SetupGet(u => u.Username)
                .Returns("opp");
            moqOpp.SetupGet(u => u.LifePoints)
                .Returns(100);
            moqOpp.SetupGet(u => u.Turn)
                .Returns(PlayerTurn.SecondToAttack);
            moqOpp.SetupGet(u => u.Deck)
                .Returns(
                () =>
                {
                    // Setup the user's battle deck
                    Mock<IBattleDeck> deck = new Mock<IBattleDeck>();
                    deck.SetupGet(d => d.Cards)
                        .Returns(() => UserCards);

                    deck.SetupGet(d => d.MaxToChoose)
                        .Returns(1);

                    deck.Setup(d => d.Choose(It.IsAny<IEnumerable<ICard>>()))
                        .Returns(() => opResult.Object);

                    return deck.Object;
                });
            return moqOpp;
        }

        private static Mock<IBattleDirector> MakeBattleDirector(Mock<IOperationResult> opResult)
        {
            Mock<IBattleScenario> scenario = new Mock<IBattleScenario>();
            scenario.SetupGet(scen => scen.PlayerA)
                .Returns(() => User);
            scenario.SetupGet(scen => scen.PlayerB)
                .Returns(() => Opp);
            scenario.SetupGet(scen => scen.State)
                .Returns(BattleState.WaitingForCardElection);

            // Setup the battle director
            Mock<IBattleDirector> director = new Mock<IBattleDirector>();
            director.SetupGet(dir => dir.Scenario)
                .Returns(scenario.Object);
            director.Setup(dir => dir.ChooseCards(It.IsAny<IPlayer>(), It.IsAny<IBattleDeck>()))
                .Returns(opResult.Object);
            return director;
        }

        private static Mock<IRoomsManager> MakeRoomsManager(Mock<IRoomUser> roomOpponent)
        {
            Mock<IRoomsManager> moqRoomsMgr = new Mock<IRoomsManager>();
            moqRoomsMgr.Setup(mgr => mgr.GetRoomUser(It.IsAny<IUserSession>()))
                       .Returns((IUserSession u) =>
                       {
                           if (u.SessionId.Equals(TestIBattlePresentation.SessionId))
                           {
                               return Mok.Helper.CreateResultWith(ResultValue.Success, RoomUser);
                           }
                           else
                           {
                               return Mok.Helper.CreateResultWith(ResultValue.Success, roomOpponent.Object);
                           }
                       });
            return moqRoomsMgr;
        }
    }
}
