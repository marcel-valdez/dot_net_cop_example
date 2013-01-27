namespace Game.Presentation.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using DependencyLocation;
    using FizzWare.NBuilder;
    using Game.Logic;
    using NUnit.Framework;
    using Moq;
    using TestingTools.Core;
    using TestingTools.Extensions;


    [TestFixture]
    public class TestIRoomPresentation
    {
        static string[] usernames = { "marcel", "luis", "nathaly", "bebe" };
        readonly static string[] session_ids = { "0", "1", "2", "3" };
        static InRoomState[] userstates = 
            { 
                InRoomState.Idle, 
                InRoomState.Idle, 
                InRoomState.Idle,
                InRoomState.Idle 
            };

        static IUserSession Session
        {
            get;
            set;
        }

        static string roomName = "room";
        static IEnumerable<IRoomUser> Users
        {
            get;
            set;
        }

        static IRoom Room
        {
            get;
            set;
        }

        static Mock<IAccountManager> AccMgrMoq
        {
            get;
            set;
        }

        [SetUp]
        public static void Init()
        {
            Initializer.ReleaseDependencies();
            Initializer.LoadDependencies();
            // Setup room users
            SetupRoomUsers();

            // Setup the user session
            SetupUserSession();

            // Setup Room Challenge Manager
            Mock<IChallengeManager> challengeMgr = SetupChallengeManager();

            // Setup Room
            SetupRoom(challengeMgr);

            // Setup the room manager
            Mock<IRoomsManager> moqRoomsManager = SetupRoomManager();

            Mok.Helper.Inject(moqRoomsManager.Object);

            // Setup User's Stats
            var userstats = SetupUserStats();

            // Setup the stats manager
            Mock<IUserStatsManager> statsMgr = SetupStatsManager(userstats);

            // Inject the stats manager
            Mok.Helper.Inject(statsMgr.Object);

        }

        [TearDown]
        public static void Cleanup()
        {

        }

        private IRoomPresentation CreateInstance(IUserSession session)
        {
            return Dependency.Locator.Create<IRoomPresentation>(session.SessionId);
        }

        private IRoomPresentation CreateInstance()
        {
            return Dependency.Locator.Create<IRoomPresentation>(Session.SessionId);
        }

        [Test]
        public void IsCorrectlyInitialized()
        {
            // Arrange
            IRoomPresentation room;

            // Act
            room = CreateInstance();

            // Assert
            Verify.That(room.CurrentDialogPresentation).IsNull()
                .Now();
            Verify.That(room.DialogVisible).IsFalse()
                .Now();

            Verify.That(room.LogoutButton).IsNotNull()
                .Now();

            Verify.That(room.ReadyForBattle).IsFalse()
                .Now();

            Verify.That(room.SelectedUserStatistics).IsNotNull()
                .Now();

            Verify.That(room.SelectedUserStatistics.Username).IsEqualTo(usernames[0])
                .Now();

            Verify.That(room.Usuarios).IsTrueForAll(dto => usernames.Contains(dto.Username))
                .Now();
        }

        [Test]
        public void CanChallengeUser()
        {
            // Arrange
            Mock<IUserSession> challengee = new Mock<IUserSession>();
            challengee.SetupGet(uS => uS.SessionId)
                .Returns("id2");
            challengee.SetupGet(uS => uS.Username)
                .Returns("nathaly");
            Mok.Helper.AddSession(AccMgrMoq, challengee.Object);

            IRoomPresentation challengerRoom = this.CreateInstance();
            IRoomPresentation challengeeRoom = this.CreateInstance(challengee.Object);

            // Act
            challengerRoom.SelectedUserIndex = 1;
            challengerRoom.ChallengeButtonClicked();
            challengeeRoom = this.CreateInstance(challengee.Object);

            // Assert
            Verify.That(challengeeRoom.DialogVisible)
                .IsTrue()
                .Now();
            Verify.That(challengeeRoom.CurrentDialogPresentation)
                .IsNotNull()
                .Now();
            Verify.That(challengeeRoom.CurrentDialogPresentation.ButtonsText)
                .IsOfSize(2)
                .Now();
            Verify.That(challengerRoom.CurrentDialogPresentation)
                .IsNotNull()
                .Now();
            Verify.That(challengerRoom.DialogVisible)
                .IsTrue()
                .Now();

            /*
            // Act Again
            challengeeRoom.CurrentDialogPresentation.PressedButtonIndex = 0;
            challengeeRoom.CurrentDialogPresentation.ButtonClicked();
            challengeeRoom = this.CreateInstance(challengee.Object);
            challengerRoom = this.CreateInstance();

            // Assert Again
            Verify.That(challengeeRoom.ReadyForBattle)
                .IsTrue()
                .Now();
            Verify.That(challengerRoom.ReadyForBattle)
                .IsTrue()
                .Now();*/
        }

        [Test]
        public void CanFindRandomBattle()
        {
            // Arrange
            Mock<IUserSession> challengee = new Mock<IUserSession>();
            challengee.SetupGet(uS => uS.SessionId)
                .Returns("1");
            challengee.SetupGet(uS => uS.Username)
                .Returns("luis");

            Mok.Helper.AddSession(AccMgrMoq, challengee.Object);

            IRoomPresentation challengerRoom = this.CreateInstance();
            IRoomPresentation challengeeRoom = this.CreateInstance(challengee.Object);

            // Act
            challengeeRoom.FindBattleButtonClicked();
            challengerRoom.FindBattleButtonClicked();


            // Assert
            Verify.That(challengerRoom.ReadyForBattle).IsTrue();
            Verify.That(challengeeRoom.ReadyForBattle).IsTrue();
            Verify.That(challengerRoom.FindBattleButonEnabled).IsFalse();
            Verify.That(challengeeRoom.FindBattleButonEnabled).IsFalse();
            Verify.That(challengerRoom.ChallengeButtonEnabled).IsFalse();
            Verify.That(challengeeRoom.ChallengeButtonEnabled).IsFalse();
        }

        [Test]
        public void CanUpdateUserStats()
        {
            // Arrange
            IRoomPresentation room = this.CreateInstance();
            IStatisticsPresentation previous = room.SelectedUserStatistics;

            // Act
            room.SelectedUserIndex = 1;

            // Assert
            Verify.That(room.SelectedUserStatistics).IsNotNull();
            Verify.That(room.SelectedUserStatistics).IsNotEqualTo(previous);
            Verify.That(room.SelectedUserStatistics.Username).IsEqualTo(room.Usuarios.ElementAt(1).Username);
        }

        private static void SetupRoom(Mock<IChallengeManager> challengeMgr)
        {
            int requestCount = 0;
            Mock<IBattleRequest> moqBreq = null;
            var moqRoom = new Mock<IRoom>();
            moqRoom.SetupGet(r => r.Name)
                    .Returns(roomName);
            moqRoom.SetupGet(r => r.Users)
                    .Returns(() => Users);
            moqRoom.Setup(r => r.FindBattle(It.IsAny<IRoomUser>()))
                .Returns(
                    (IRoomUser user) =>
                    {
                        Mock.Get(user)
                            .SetupGet(u => u.State)
                            .Returns(InRoomState.RequestingBattle);
                        Mock<IBattleRequest> req = new Mock<IBattleRequest>();
                        req.SetupGet(r => r.Requestor)
                            .Returns(user);
                        req.SetupGet(r => r.State)
                            .Returns(RequestState.Waiting);
                        req.Setup(r => r.Cancel())
                            .Returns(() => Mok.Helper.CreateResultWith(ResultValue.Success, true))
                            .Callback(() =>
                            {
                                Mock.Get(user)
                                    .SetupGet(u => u.State)
                                    .Returns(InRoomState.Idle);
                                req.SetupGet(r => r.State)
                                   .Returns(RequestState.Cancelled);
                            });

                        if (++requestCount % 2 == 0)
                        {
                            req.SetupGet(r => r.State)
                                .Returns(RequestState.Ready);

                            moqBreq.SetupGet(r => r.State)
                                    .Returns(RequestState.Ready);
                        }
                        else
                        {
                            moqBreq = req;
                        }

                        return req.Object;
                    });

            moqRoom.SetupGet(r => r.ChallengesManager)
                   .Returns(challengeMgr.Object);

            Room = moqRoom.Object;
        }

        private static void SetupRoomUsers()
        {
            int i = 0;
            Users = Builder<Mock<IRoomUser>>
                            .CreateListOfSize(4)
                            .All()
                            .With(user => user.SetupGet(u => u.Username)
                                            .Returns(usernames[(i++) % 4]))
                            .With(user => user.SetupGet(u => u.Room)
                                            .Returns(() => Room))
                            .With(user => user.SetupGet(u => u.State)
                                            .Returns(userstates[(i++) % 4]))
                            .Build().Select(mock => mock.Object);
        }

        private static void SetupUserSession()
        {
            Mock<IUserSession> session = new Mock<IUserSession>();
            session.SetupGet(s => s.SessionId).Returns("0");
            session.SetupGet(s => s.Username).Returns("marcel");
            Session = session.Object;

            var accMgrMoq = Mok.Helper.SetupAccountManager(ResultValue.Fail, ResultValue.Success, Session);
            accMgrMoq.Setup(mgr => mgr.Logout(It.Is<IUserSession>(s => s == Session)))
                     .Returns(Mok.Helper.CreateResultWith(ResultValue.Success, true));

            AccMgrMoq = accMgrMoq;
        }

        private static Mock<IChallengeManager> SetupChallengeManager()
        {
            Dictionary<string, IIssuedChallenge> sent = new Dictionary<string, IIssuedChallenge>();
            Mock<IChallengeManager> challengeMgr = new Mock<IChallengeManager>();
            challengeMgr.Setup(mgr => mgr.Send(It.IsAny<IRoomUser>(), It.IsAny<IRoomUser>()))
                        .Returns((IRoomUser challenger, IRoomUser challengee) =>
                        {
                            // Create an issued challenge
                            Mock<IIssuedChallenge> issued = new Mock<IIssuedChallenge>();
                            issued.SetupGet(iss => iss.Challengee)
                                .Returns(challengee);

                            issued.SetupGet(iss => iss.Challenger)
                                .Returns(challenger);

                            issued.SetupGet(iss => iss.State)
                                .Returns(ChallengeState.Sent);

                            Mock.Get(challenger).SetupGet(ch => ch.State)
                                .Returns(InRoomState.RequestingChallenge);

                            issued.Setup(iss => iss.Cancel())
                                .Callback(() => issued.SetupGet(iss => iss.State).Returns(ChallengeState.Cancelled))
                                .Returns(() =>
                                    {
                                        Mock.Get(challenger).SetupGet(ch => ch.State)
                                            .Returns(InRoomState.Idle);
                                        return Mok.Helper.CreateResultWith(ResultValue.Success, true);
                                    });

                            if (!sent.ContainsKey(issued.Object.Challengee.Username))
                            {
                                sent.Add(issued.Object.Challengee.Username, issued.Object);
                            }
                            else
                            {
                                sent[issued.Object.Challengee.Username] = issued.Object;
                            }

                            return Mok.Helper.CreateResultWith(ResultValue.Success, issued.Object);
                        });

            challengeMgr.Setup(mgr => mgr.GetReceivedChallenge(It.IsAny<IRoomUser>()))
                        .Returns((IRoomUser user) =>
                        {
                            // Crate received challenges
                            if (sent.ContainsKey(user.Username))
                            {
                                string cUsername = user.Username;
                                Mock<IReceivedChallenge> received = new Mock<IReceivedChallenge>();
                                IIssuedChallenge issued = sent[cUsername];
                                received.SetupGet(ch => ch.Challengee)
                                    .Returns(issued.Challengee);
                                received.SetupGet(ch => ch.Challenger)
                                    .Returns(issued.Challenger);
                                received.SetupGet(ch => ch.State)
                                    .Returns(issued.State);
                                received.Setup(ch => ch.Accept())
                                    .Returns(() =>
                                        {
                                            Mock<IIssuedChallenge>.Get(issued)
                                                .SetupGet(ch => ch.State)
                                                .Returns(ChallengeState.Accepted);

                                            received.SetupGet(ch => ch.State)
                                                .Returns(ChallengeState.Accepted);

                                            Mock<IRoomUser>.Get(received.Object.Challengee)
                                                .SetupGet(u => u.State)
                                                .Returns(InRoomState.Playing);

                                            Mock<IRoomUser>.Get(received.Object.Challenger)
                                                .SetupGet(u => u.State)
                                                .Returns(InRoomState.Playing);

                                            Mock<IRoomUser>.Get(issued.Challenger)
                                                .SetupGet(u => u.State)
                                                .Returns(InRoomState.Playing);

                                            Mock<IRoomUser>.Get(issued.Challengee)
                                                .SetupGet(u => u.State)
                                                .Returns(InRoomState.Playing);

                                            Mock<IRoomUser>.Get(user)
                                                .SetupGet(u => u.State)
                                                .Returns(InRoomState.Playing);

                                            return Mok.Helper.CreateResultWith(ResultValue.Success, true);
                                        });

                                received.Setup(ch => ch.Reject())
                                    .Callback(() =>
                                    {
                                        Mock<IIssuedChallenge>.Get(issued)
                                            .SetupGet(ch => ch.State)
                                            .Returns(ChallengeState.Rejected);
                                        received.SetupGet(ch => ch.State)
                                            .Returns(ChallengeState.Rejected);
                                    });

                                return Mok.Helper.CreateResultWith(ResultValue.Success, received.Object);
                            }

                            return Mok.Helper.CreateResultWith(ResultValue.Fail, default(IReceivedChallenge));
                        });

            return challengeMgr;
        }

        private static Mock<IRoomsManager> SetupRoomManager()
        {
            var roomMgr = new Mock<IRoomsManager>();
            roomMgr.Setup(mgr => mgr.GetRoomUser(It.IsAny<IUserSession>()))
                   .Returns(
                   (IUserSession usession) =>
                   {
                       Mock<IRoomUser> ruser = new Mock<IRoomUser>();
                       ruser.SetupGet(r => r.Room)
                            .Returns(() => Room);
                       ruser.SetupGet(r => r.Username)
                           .Returns(usession.Username);
                       ruser.SetupGet(r => r.State)
                           .Returns(InRoomState.Idle);

                       return Mok.Helper.CreateResultWith(ResultValue.Success, ruser.Object);
                   });

            return roomMgr;
        }

        private static IEnumerable<IUserStats> SetupUserStats()
        {
            int i = 0;
            return Builder<Mock<IUserStats>>
                    .CreateListOfSize(4)
                    .All()
                    .With(stats =>
                        stats.SetupGet(s => s.BattlesLost).Returns(1))
                    .With(stats =>
                        stats.SetupGet(s => s.BattlesTied).Returns(2))
                    .With(stats =>
                        stats.SetupGet(s => s.BattlesWon).Returns(3))
                    .With(stats =>
                        stats.SetupGet(s => s.Ranking).Returns(4))
                    .With(stats =>
                        stats.SetupGet(s => s.Username).Returns(usernames[(i++) % 4]))
                        .Build()
                        .Select(mock => mock.Object);
        }

        private static Mock<IUserStatsManager> SetupStatsManager(IEnumerable<IUserStats> userstats)
        {
            Mock<IUserStatsManager> statsMgr = new Mock<IUserStatsManager>();
            statsMgr.Setup(mgr => mgr.GetUserStats(It.Is<string>(s => usernames.Contains(s))))
                    .Returns((string uname) =>
                        {

                            var stats = userstats.ElementAt(usernames.ToList().IndexOf(uname));
                            Mock<IOperationResult<IUserStats>> moqResult = new Mock<IOperationResult<IUserStats>>();
                            moqResult.SetupGet(r => r.Result)
                                    .Returns(ResultValue.Success);
                            moqResult.SetupGet(r => r.Message)
                                    .Returns("Success");
                            moqResult.SetupGet(r => r.ResultData)
                                .Returns(() => stats);

                            return moqResult.Object;
                        });

            return statsMgr;
        }
    }
}
