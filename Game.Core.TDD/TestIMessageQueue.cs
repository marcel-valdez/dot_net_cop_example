namespace Game.Core.Tests
{
    using System.Linq;
    using DependencyLocation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestingTools.Core;
    using TestingTools.Extensions;

    [TestClass]
    public class TestIMessageQueue
    {
        [ClassInitialize]
        public static void Init(TestContext tc)
        {
            Initializer.ReleaseDependencies();
            Initializer.LoadDependencies();
        }

        IMessageQueue GetInstance()
        {
            return Dependency.Locator.Create<IMessageQueue>();
        }

        [TestMethod]
        public void CanKnowItHasPending()
        {
            // Arrange
            IMessageQueue q = GetInstance();
            object chan = new object();

            // Act
            q.Push(chan, "message");

            // Assert
            Verify.That(q.HasPending(chan))
                    .IsTrue()
                    .Now();

            Verify.That(q.Pop<string>(chan)).IsEqualTo("message")
                .Now();

            Verify.That(q.HasPending(chan)).IsFalse()
                .Now();
        }

        [TestMethod]
        public void CanStoreAllMessages()
        {
            // Arrange
            IMessageQueue q = GetInstance();
            object chan = new object();

            // Act
            q.Push(chan, "message1");
            q.Push(chan, "message2");
            q.Push(chan, "message3");

            // Assert
            Verify.That(q.Pop<string>(chan)).IsEqualTo("message1")
                .Now();
            Verify.That(q.Pop<string>(chan)).IsEqualTo("message2")
                .Now();
            Verify.That(q.Pop<string>(chan)).IsEqualTo("message3")
                .Now();
        }

        [TestMethod]
        public void CanStoreCurrentChannels()
        {
            // Arrange
            IMessageQueue q = GetInstance();
            object chan1 = new object();
            object chan2 = new object();
            object chan3 = new object();

            // Act
            q.Push(chan1, "message1");
            q.Push(chan2, "message2");
            q.Push(chan3, "message3");

            // Assert
            Verify.That(q.Channels).IsOfSize(3)
                .Now();
            Verify.That(q.Channels)
                .IsTrueForAll(
                    chan => new object[] { chan1, chan2, chan3 }.Contains(chan))
                .Now();
        }

        [TestMethod]
        public void CanGetmessagesOnAllChannels()
        {
            // Arrange
            IMessageQueue q = GetInstance();
            object chan1 = new object();
            object chan2 = new object();
            object chan3 = new object();

            // Act
            q.Push(chan1, "message1");
            q.Push(chan2, "message2");
            q.Push(chan3, "message3");

            // Assert
            Verify.That(q.Pop<string>()).IsEqualTo("message1")
                .Now();
            Verify.That(q.Pop<string>()).IsEqualTo("message2")
                .Now();
            Verify.That(q.Pop<string>()).IsEqualTo("message3")
                .Now();
        }

        [TestMethod]
        public void CanPushMessage()
        {
            // Arrange
            IMessageQueue q = GetInstance();
            object chan = new object();

            // Act
            q.Push(chan, "message");

            // Assert
            Verify.That(q.Pop<string>(chan))
                .IsEqualTo("message")
                .Now();
        }
    }
}
