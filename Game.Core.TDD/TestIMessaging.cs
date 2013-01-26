﻿namespace Game.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DependencyLocation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestingTools.Core;
    using TestingTools.Extensions;

    [TestClass]
    public class TestIMessaging
    {
        IMessaging CreateInstance()
        {
            return Dependency.Locator.GetSingleton<IMessaging>();
        }

        [ClassInitialize]
        public static void Init(TestContext tc)
        {
            Initializer.ReleaseDependencies();
            Initializer.LoadDependencies();
        }

        [TestMethod]
        public void CanPublishAndSubscribeToChannel()
        {
            // Arrange
            IMessaging target = CreateInstance();
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

        [TestMethod]
        public void CanPublishAndSubscribeConditionallyToChannel()
        {
            // Arrange
            IMessaging target = CreateInstance();
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
            Verify.That(messages.Keys.AsEnumerable()).DoesContain(channel, "No messages were received?").Now();
            Verify.That(messages[channel].AsEnumerable()).IsOfSize(2).Now();
            Verify.That(messages[channel][0]).IsEqualTo("message").Now();
            Verify.That(messages[channel][1]).IsEqualTo("message1").Now();
            // Reset
            target.Unsubscribe(channel, handler);

        }

        [TestMethod]
        public void CanUnsubscribe()
        {
            // Arrange
            IMessaging target = CreateInstance();

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