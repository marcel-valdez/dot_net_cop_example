namespace Game.Presentation.Tests
{
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
    public class TestIHomePresentation
    {
        private static int RoomCount = 3;
        private static string[] RoomNames = { "Room1", "Room2", "Room3" };
        private static IUserSession Session;
        private static string session_id = "session_id";
        private static bool UserJoined = false;
        private static int mRoomToJoinIndex = 0;

        private static int RoomToJoinIndex
        {
            get
            {
                return mRoomToJoinIndex;
            }

            set
            {
                mRoomToJoinIndex = value;
            }
        }

        public IHomePresentation CreateInstance()
        {
            // The presentation receives a session-id
            return Dependency.Locator.Create<IHomePresentation>(session_id);
        }

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            Initializer.ReleaseDependencies();
            Initializer.LoadDependencies();
            /* Initialize a new authenticated session,
             * so the presentation model can be succesfully created
             */
            Mock<IUserSession> session = new Mock<IUserSession>();
            session.SetupGet(s => s.SessionId).Returns("id");
            session.SetupGet(s => s.Username).Returns("marcel");
            Session = session.Object;

            Mock<IAccountManager> accMgr = new Mock<IAccountManager>(MockBehavior.Loose);

            accMgr.Setup(mgr => mgr.GetUserSession(It.IsAny<string>()))
                .Returns(() =>
                {
                    Mock<IOperationResult<IUserSession>> moqResult = new Mock<IOperationResult<IUserSession>>();
                    moqResult.SetupGet(r => r.Result)
                            .Returns(ResultValue.Success);
                    moqResult.SetupGet(r => r.Message)
                            .Returns("Success");
                    moqResult.SetupGet(r => r.ResultData)
                        .Returns(() => Session);
                    return moqResult.Object;
                });

            ((IDependencyConfigurator)Dependency.Locator).SetupSingleton(accMgr.Object);

            // Setup the user stats
            Mock<IUserStats> stats = new Mock<IUserStats>();
            stats.SetupGet(s => s.BattlesLost).Returns(1);
            stats.SetupGet(s => s.BattlesTied).Returns(2);
            stats.SetupGet(s => s.BattlesWon).Returns(3);
            stats.SetupGet(s => s.Ranking).Returns(4);
            stats.SetupGet(s => s.Username).Returns("marcel");

            // Setup the stats manager
            Mock<IUserStatsManager> statsMgr = new Mock<IUserStatsManager>();
            statsMgr.Setup(mgr => mgr.GetUserStats(It.Is<string>(s => s.Equals("marcel"))))
                    .Returns(() =>
                    {
                        Mock<IOperationResult<IUserStats>> moqResult = new Mock<IOperationResult<IUserStats>>();
                        moqResult.SetupGet(r => r.Result)
                                .Returns(ResultValue.Success);
                        moqResult.SetupGet(r => r.Message)
                                .Returns("Success");
                        moqResult.SetupGet(r => r.ResultData)
                            .Returns(() => stats.Object);

                        return moqResult.Object;
                    });

            // Inject the stats manager
            ((IDependencyConfigurator)Dependency.Locator).SetupSingleton(statsMgr.Object);

            /* Create rooms to be listed
             */
            int i = 0;
            IEnumerable<IRoom> rooms = Builder<Mock<IRoom>>
                .CreateListOfSize(RoomCount)
                .All()
                .With(mock =>
                        mock.SetupGet(r => r.Name)
                            .Returns(RoomNames[i++ % RoomCount]))
                .With(mock =>
                        mock.SetupGet(r => r.Users)
                            .Returns(default(IEnumerable<IRoomUser>)))
                .Build()
                .Select(mock => mock.Object).ToArray();

            // Setup the room manager
            Mock<IRoomsManager> roomMgr = new Mock<IRoomsManager>();
            roomMgr.Setup(mgr => mgr.GetAvailableRooms(It.IsAny<IUserSession>()))
                   .Returns(rooms);
            roomMgr.Setup(mgr => mgr.JoinRoom(
                It.Is<IUserSession>(s => s.Equals(Session)),
                It.Is<IRoom>(r => r.Name == RoomNames[RoomToJoinIndex])))
                .Returns(() =>
                    {
                        Mock<IRoomUser> rUser = new Mock<IRoomUser>();
                        rUser.SetupGet(u => u.Room)
                            .Returns(rooms.ElementAt(RoomToJoinIndex));
                        rUser.SetupGet(u => u.State)
                            .Returns(InRoomState.Idle);
                        rUser.SetupGet(u => u.Username)
                            .Returns("marcel");

                        Mock<IOperationResult<IRoomUser>> moqResult = new Mock<IOperationResult<IRoomUser>>();
                        moqResult.SetupGet(r => r.Result)
                                .Returns(ResultValue.Success);
                        moqResult.SetupGet(r => r.Message)
                                .Returns("Success");
                        moqResult.SetupGet(r => r.ResultData)
                            .Returns(() => rUser.Object);

                        return moqResult.Object;
                    })
                    .Callback(() => UserJoined = true);

            // Inject the room manager
            ((IDependencyConfigurator)Dependency.Locator).SetupSingleton(roomMgr.Object);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestCleanup]
        public void CleanUp()
        {
            UserJoined = false;
        }

        [TestMethod]
        public void IsCorrectlyInitialized()
        {
            // Arrange
            IHomePresentation target;

            // Act
            target = this.CreateInstance();

            // Assert
            Verify.That(target.Title).IsNotNull()
                .Now();
            Verify.That(target.WelcomeMessage).IsNotNull()
                .Now();
            Verify.That(target.RoomsListTitle).IsNotNull()
                .Now();
            Verify.That(target.RoomNames).IsNotNull()
                .Now();
            Verify.That(target.LogoutButton).IsNotNull()
                .Now();
            Verify.That(target.UserStats).IsNotNull()
                .Now();
            Verify.That(target.UserStats.Username).IsEqualTo("marcel")
                .Now();
            Verify.That(target.UserStats.LostGamesCount).IsEqualTo(1)
                .Now();
            Verify.That(target.UserStats.PlayedGamesCount).IsEqualTo(6)
                .Now();
            Verify.That(target.UserStats.Ranking).IsEqualTo(4)
                .Now();
            Verify.That(target.UserStats.WonGamesCount).IsEqualTo(3)
                .Now();

        }

        [TestMethod]
        public void CanListRoomsCorrectly()
        {
            // Arrange
            IHomePresentation target;
            IEnumerable<string> roomnames;

            // Act
            target = this.CreateInstance();
            roomnames = target.RoomNames;

            // Assert
            Verify.That(roomnames).IsOfSize(RoomCount)
                .Now();

            Verify.That(roomnames)
                .IsTrueForAll(name => RoomNames.Contains(name))
                .Now();

        }

        [TestMethod]
        public void CanGetIntoARoom()
        {
            // Arrange
            IHomePresentation target = this.CreateInstance();
            RoomToJoinIndex = 1;
            target.SelectedRoomIndex = 1;

            // Act
            target.IngresarClicked();

            // Assert
            Verify.That(UserJoined).IsTrue()
                .Now();

        }
    }
}
