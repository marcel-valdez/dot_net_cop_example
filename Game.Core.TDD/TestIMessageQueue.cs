namespace Game.Core.Tests
{
    using System.Linq;
    using DependencyLocation;
    
    using TestingTools.Core;
    using TestingTools.Extensions;
  using NUnit.Framework;

    [TestFixture]
    public class TestIMessageQueue
    {
        [SetUp]
        public static void Init()
        {
            Initializer.ReleaseDependencies();
            Initializer.LoadDependencies();
        }

        IMessageQueue GetInstance()
        {
            return Dependency.Locator.Create<IMessageQueue>();
        }

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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
