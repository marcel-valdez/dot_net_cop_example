namespace Game.Core.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using DependencyLocation;  
  using TestingTools.Core;
  using TestingTools.Extensions;
  using NUnit.Framework;

  [TestFixture]
  public class TestIMessaging
  {
    static IMessaging GetTarget()
    {
      return Dependency.Locator.GetSingleton<IMessaging>();
    }

    [SetUp]
    public void Init()
    {
      Initializer.ReleaseDependencies();
      Initializer.LoadDependencies();
    }

    [Test]
    public void CanPublishAndSubscribeToChannel()
    {
      // Arrange
      IMessaging target = GetTarget();
      object channel = new object();
      Dictionary<object, List<string>> messages = new Dictionary<object, List<string>>();
      Action<object, string> handler = (chan, msg) =>
      {
        Verify.That(chan).IsEqualTo(channel, "The received channel should be the same as the expected one!").Now();

        if (!messages.ContainsKey(chan))
        {
          messages.Add(chan, new List<string>());
        }

        messages[chan].Add(msg);
      };

      // Act
      target.Subscribe(channel, handler);
      target.Publish(channel, "message");
      target.Publish(channel, new object());

      // Assert
      System.Threading.Thread.Sleep(100);
      Verify.That(messages.Keys.AsEnumerable()).DoesContain(channel, "No messages were received?").Now();
      Verify.That(messages[channel].AsEnumerable()).IsOfSize(1).Now();
      Verify.That(messages[channel][0]).IsEqualTo("message").Now();

      // Reset
      target.Unsubscribe(channel, handler);
    }

    [Test]
    public void CanPublishAndSubscribeConditionallyToChannel()
    {
      // Arrange
      IMessaging target = GetTarget();
      object channel = new object();
      Dictionary<object, List<string>> messages = new Dictionary<object, List<string>>();
      Action<object, string> handler = (chan, msg) =>
      {
        if (!messages.ContainsKey(chan))
        {
          messages.Add(chan, new List<string>());
        }
        messages[chan].Add(msg);
      };
      Predicate<string> filter = (msg) => msg.Contains("mess");
      // Act
      target.Subscribe<string>(channel, handler, filter);
      target.Publish(channel, "message");
      target.Publish(channel, "message1");
      target.Publish(channel, "mesage");
      target.Publish(channel, new object());
      // Assert
      System.Threading.Thread.Sleep(200);      
      CollectionAssert.Contains(messages.Keys, channel);
      Assert.AreEqual(2, messages[channel].Count);
      CollectionAssert.Contains(messages[channel], "message");
      CollectionAssert.Contains(messages[channel], "message1");
      CollectionAssert.DoesNotContain(messages[channel], "mesage");
      //Assert.Equals(messages[channel][0], "message");
      //Assert.Equals(messages[channel][1], "message1");
      // Reset
      target.Unsubscribe(channel, handler);

    }

    [Test]
    public void CanUnsubscribe()
    {
      // Arrange
      IMessaging target = GetTarget();

      object channel = new object();
      Dictionary<object, List<string>> messages = new Dictionary<object, List<string>>();
      Action<object, string> handler = (chan, msg) =>
      {
        if (!messages.ContainsKey(chan))
        {
          messages.Add(chan, new List<string>());
        }

        messages[chan].Add(msg);
      };

      // Act
      target.Subscribe<string>(channel, handler);
      target.Publish(channel, "message");
      target.Unsubscribe(channel, handler);
      target.Publish(channel, "message1");
      target.Publish(channel, "mesage");
      target.Publish(channel, new object());

      // Assert
      System.Threading.Thread.Sleep(100);
      Verify.That(messages.Keys.AsEnumerable()).DoesContain(channel, "No messages were received?").Now();
      Verify.That(messages[channel].AsEnumerable()).IsOfSize(1).Now();

      // Reset
    }
  }
}
