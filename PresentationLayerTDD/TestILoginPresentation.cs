namespace Game.Presentation.Tests
{
    using DependencyLocation;
    using Game.Logic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TestingTools.Core;
    using TestingTools.Extensions;

    [TestClass]
    public class TestILoginPresentation
    {
        public ILoginPresentation CreateInstance()
        {
            return Dependency.Locator.Create<ILoginPresentation>("sessionId");
        }

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            Initializer.ReleaseDependencies();
            Initializer.LoadDependencies();
            // Setup account manager with username marcel already taken, and
            // the rest free
            Mock<IAccountManager> accMgr = new Mock<IAccountManager>(MockBehavior.Loose);

            accMgr.Setup(mgr => mgr.GetUserSession(It.IsAny<string>()))
                .Returns(() =>
                {
                    Mock<IOperationResult<IUserSession>> moqResult = new Mock<IOperationResult<IUserSession>>();
                    moqResult.SetupGet(r => r.Result)
                            .Returns(ResultValue.Fail);
                    moqResult.SetupGet(r => r.Message)
                            .Returns("Fail");
                    moqResult.SetupGet(r => r.ResultData)
                        .Returns(default(IUserSession));
                    return moqResult.Object;
                });

            accMgr.Setup(
                mgr => mgr.Autentificar(
                    It.Is<string>(uname => uname == "marcel"),
                    It.Is<string>(pass => pass == "password"),
                    It.IsAny<string>()))
               .Returns(() =>
               {
                   Mock<IUserSession> sess = new Mock<IUserSession>();
                   sess.SetupGet(s => s.Username)
                       .Returns("marcel");
                   sess.SetupGet(s => s.SessionId)
                       .Returns("session_id");

                   Mock<IOperationResult<IUserSession>> res = new Mock<IOperationResult<IUserSession>>();
                   res.SetupGet(r => r.ResultData)
                       .Returns(sess.Object);
                   res.SetupGet(r => r.Result)
                       .Returns(ResultValue.Success);
                   res.SetupGet(r => r.Message)
                       .Returns("Success");

                   return res.Object;
               });

            accMgr.Setup(
                mgr => mgr.Autentificar(
                    It.Is<string>(uname => uname != "marcel"),
                    It.Is<string>(pass => pass != "password"),
                    It.IsAny<string>()))
               .Returns(() =>
               {
                   Mock<IUserSession> sess = new Mock<IUserSession>();
                   sess.SetupGet(s => s.Username)
                       .Returns("");
                   sess.SetupGet(s => s.SessionId)
                       .Returns("session_id");

                   Mock<IOperationResult<IUserSession>> res = new Mock<IOperationResult<IUserSession>>();
                   res.SetupGet(r => r.ResultData)
                       .Returns(sess.Object);
                   res.SetupGet(r => r.Result)
                       .Returns(ResultValue.Fail);
                   res.SetupGet(r => r.Message)
                       .Returns("Fail");

                   return res.Object;
               });

            ((IDependencyConfigurator)(Dependency.Locator)).SetupSingleton(accMgr.Object);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestCleanup]
        public void CleanupTest()
        {
            // Close Session
        }

        #region Unit Tests
        /// <summary>
        /// Determines whether this instance [can handle valid login].
        /// Se asume que existe el usuario: marcel:password
        /// </summary>
        [TestMethod]
        public void CanAuthenticateValidLogin()
        {
            // Arrange
            ILoginPresentation login = this.CreateInstance();
            login.Username = "marcel";
            login.Password = "password";

            // Act
            login.LoginClicked();

            // Assert
            Verify.That(login.IsAuthenticated).IsTrue("El usuario debería autentificar correctamente")
                .Now();

            Verify.That(login.MessageVisible).IsTrue()
                .Now();

            Verify.That(login.Message).IsNotNull()
                .Now();
        }

        /// <summary>
        /// Tests the invalid login.
        /// Se asume que el usuario dont-exist-ha no existe
        /// </summary>
        [TestMethod]
        public void CanRejectInvalidLogin()
        {
            // Arrange
            ILoginPresentation login = this.CreateInstance();
            login.Username = "dont-exist-ha";
            login.Password = "passwordz";

            // Act
            login.LoginClicked();

            // Assert
            Verify.That(login.IsAuthenticated).IsFalse("El usuario no debería autentificarse.")
                .Now();

            Verify.That(login.MessageVisible).IsTrue()
                .Now();

            Verify.That(login.Message).IsNotNull()
                .Now();
        }

        [TestMethod]
        public void IsInitializedToUnauthenticated()
        {
            // Arrange
            ILoginPresentation login;

            // Act
            login = this.CreateInstance();

            // Assert
            // Lógicamente debe inicializarse a false
            Verify.That(login.IsAuthenticated).IsFalse()
                .Now();
        }
        #endregion

        #region Integration Tests
        [TestMethod]
        public void CanRecognizeAuthenticatedSession()
        {
            // Arrange
            ILoginPresentation login1;
            login1 = this.CreateInstance();
            ILoginPresentation login2;

            // Act
            login1.Username = "marcel";
            login1.Password = "password";
            login1.LoginClicked();
            login2 = this.CreateInstance();

            // Assert
            // Debería ser true, porque ya se creó una sesión para marcel,
            // y cuando se cree un nuevo control de login, se debe reconocer
            // que la sesión ya ha sido inicializada
            Verify.That(login2.IsAuthenticated).IsTrue();
        }
    }
        #endregion
}
