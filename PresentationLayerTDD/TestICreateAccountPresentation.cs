namespace Game.Presentation.Tests
{
    using System.Diagnostics.Contracts;
    using System;
    using DependencyLocation;
    using Game.Logic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using TestingTools.Core;
    using TestingTools.Extensions;

    [TestClass]
    public class TestICreateAccountPresentation
    {
        static ISession Session
        {
            get;
            set;
        }

        public ICreateAccountPresentation CreateInstance()
        {
            return Dependency.Locator.Create<ICreateAccountPresentation>(Session.SessionId);
        }

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            Initializer.ReleaseDependencies();
            Initializer.LoadDependencies();
            // Initialize pre-created account (marcel:password)
            // Do this by creating a mocked  version of the Account Manager
            // and then storing it in the Dependency Locator

            Mock<ISession> session = new Mock<ISession>();
            session.SetupGet(s => s.SessionId)
                   .Returns("session_id");
            Session = session.Object;
            
            Mock<IAccountManager> accountManager = new Mock<IAccountManager>();           
            accountManager.Setup(manager => manager.GetUserSession(It.IsAny<string>()))
                .Returns(() =>
                    {
                        Mock<IOperationResult<IUserSession>> moqOperationResult = new Mock<IOperationResult<IUserSession>>();
                        moqOperationResult.SetupGet(r => r.Result)
                                .Returns(ResultValue.Fail);
                        moqOperationResult.SetupGet(r => r.Message)
                                .Returns("Fail");
                        moqOperationResult.SetupGet(r => r.ResultData)
                            .Returns(default(IUserSession));
                        return moqOperationResult.Object;
                    });

            Mock<IOperationResult> operationResult = new Mock<IOperationResult>();
            operationResult.SetupGet(result => result.Message)
                    .Returns("Success");
            operationResult.SetupGet(result => result.Result)
                    .Returns(ResultValue.Success);

            accountManager.Setup(
                    manager => manager.CreateAccount(It.IsAny<string>(), It.IsAny<string>())
                ).Returns(operationResult.Object);

            ((IDependencyConfigurator)(Dependency.Locator)).SetupSingleton(accountManager.Object);
        }

        [ClassCleanup]
        public static void CleanupClass()
        {
        }

        [TestMethod]
        public void IsInitializedCorrectly()
        {
            // Arrange
            ICreateAccountPresentation pmod;

            // Act
            pmod = this.CreateInstance();

            // Assert
            Verify.That(pmod.CreationSuccess).IsFalse()
                .Now();

            Verify.That(pmod.CreateButtonVisible).IsTrue()
                .Now();

            Verify.That(pmod.ResultMessageVisible).IsFalse()
                .Now();
            
            Verify.That(pmod.UsernameVisible).IsTrue()
                .Now();
            
            Verify.That(pmod.PasswordVisible).IsTrue()
                .Now();
            
            Verify.That(pmod.PasswordConfirmationVisible).IsTrue()
                .Now();
            
        }

        [TestMethod]
        public void CanCreateNewAccount()
        {
            // Arrange
            string username = "test";
            string password = "password";
            string passwordConfirmation = "password";
            ICreateAccountPresentation pmod = this.CreateInstance();

            // Act
            pmod.Username = username;
            pmod.Password = password;
            pmod.PasswordConfirmation = passwordConfirmation;
            pmod.CreateAccount();

            // Assert
            Verify.That(pmod.CreationSuccess).IsTrue()
                .Now();
            
            Verify.That(pmod.ResultMessage).IsNotNull()
                .Now();
            
            Verify.That(pmod.ResultMessageVisible).IsTrue()
                .Now();
            
            Verify.That(pmod.CreateButtonVisible).IsFalse()
                .Now();
            
            Verify.That(pmod.PasswordConfirmationVisible).IsFalse()
                .Now();
            
            Verify.That(pmod.UsernameVisible).IsFalse()
                .Now();
            
            Verify.That(pmod.PasswordVisible).IsFalse()
                .Now();
            
            Verify.That(pmod.RedirectButtonVisible).IsTrue()
                .Now();
            
        }

        [TestMethod]
        public void CanInvalidateBadPasswordConfirmation()
        {
            // Arrange
            string username = "ausername";
            string password = "apassword";
            string password_confirmation = "incorrect!";

            // Arrange, Act & Assert
            FailedCreationOperationHelper(username, password, password_confirmation); // bad password confirmation
        }

        [Ignore]
        [TestMethod]
        public void CanInvalidateBadUsername()
        {
            // Arrange
            string[] usernames = {
                                     "bad username", "!badusername", "@badusername",
                                     "#badusername", "$badusername", "%badusername",
                                     "^badusername", "&badusername", "*badusername",
                                     "(badusername", ")badusername", "=badusername", 
                                     "+badusername", "{badusername", "}badusername", 
                                     ",badusername", ";badusername", ">badusername",
                                     "<badusername", "/badusername", ":badusername",
                                     "'badusername", "\"badusername", "[badusername",
                                     "]badusername", "|badusername", "\badusername",
                                     "!badusername`", "~badusername", "ábadusername",
                                     "ébadusername", "íbadusername", "óbadusername",
                                     "úbadusername", "♫badusername", 
                                     "12345678901234567", // max 16 chars
                                     "123" // min 4 chars
                                 };
            string password = "apassword";
            string password_confirmation = "apassword";
            foreach (string username in usernames)
            {
                FailedCreationOperationHelper(username, password, password_confirmation);
            }
        }

        [Ignore]
        [TestMethod]
        public void CanInvalidateBadPassword()
        {
            // Arrange
            string username = "new_account";
            string[] passwords = {
                                    "1234567",// min 7 chars
                                    "123456789012345678901234567890123" // max 32 chars
                                 };
            foreach (string password in passwords)
            {
                FailedCreationOperationHelper(username, password, password);
            }
        }

        [Ignore]
        [TestMethod]
        public void CanInvalidateTakenUsername()
        {
            // Arrange
            string username = "marcel";
            string password = "password";
            string password_confirmation = "password";

            // AAA
            this.FailedCreationOperationHelper(username, password, password_confirmation);
        }

        private void FailedCreationOperationHelper(string username, string password, string password_confirmation)
        {
            Contract.Requires(!String.IsNullOrEmpty(username), "username is null or empty.");
            Contract.Requires(!String.IsNullOrEmpty(password), "password is null or empty.");
            Contract.Requires(!String.IsNullOrEmpty(password_confirmation), "password_confirmation is null or empty.");

            // Arrange
            ICreateAccountPresentation pmod = this.CreateInstance();

            // Act
            pmod.Username = username;
            pmod.Password = password;
            pmod.PasswordConfirmation = password_confirmation;
            pmod.CreateAccount();

            // Assert
            Verify.That(pmod.CreationSuccess).IsFalse()
                .Now();

            Verify.That(pmod.RedirectButtonVisible).IsFalse()
                .Now();

            Verify.That(pmod.ResultMessageVisible).IsTrue()
                .Now();

            Verify.That(pmod.ResultMessage).IsNotNull()
                .Now();

            Verify.That(pmod.CreateButtonVisible).IsTrue()
                .Now();

            Verify.That(pmod.PasswordConfirmationVisible).IsTrue()
                .Now();

            Verify.That(pmod.UsernameVisible).IsTrue()
                .Now();

            Verify.That(pmod.PasswordVisible).IsTrue()
                .Now();
        }
    }
}
