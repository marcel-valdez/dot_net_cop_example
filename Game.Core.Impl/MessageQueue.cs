using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics.Contracts;

namespace Game.Core.Impl
{
    internal class MessageQueue : IMessageQueue
    {
        Dictionary<object, Dictionary<Type, Container<object>>> messages;
        private int mMaxHistoryLength;
        private Dictionary<object, int> messageCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageQueue"/> class.
        /// </summary>
        public MessageQueue()
            : this(10)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageQueue"/> class.
        /// </summary>
        /// <param name="maxHistoryPerChannel">The max history per channel.</param>
        public MessageQueue(int maxHistoryPerChannel = 10)
        {
            this.mMaxHistoryLength = maxHistoryPerChannel;
            this.messages = new Dictionary<object, Dictionary<Type, Container<object>>>();
            this.messageCount = new Dictionary<object, int>();
        }

        #region IMessageQueue Members
        public int MaxChannelHistoryLength
        {
            get
            {
                return this.mMaxHistoryLength;
            }
        }

        public IEnumerable<object> Channels
        {
            get
            {
                return this.messages.Keys;
            }
        }

        public void Push<TMessage>(object channel, TMessage message)
        {
            if (!this.messageCount.ContainsKey(channel))
            {
                this.messageCount.Add(channel, 0);
            }

            if (!this.messages.ContainsKey(channel))
            {
                this.messages.Add(channel, new Dictionary<Type, Container<object>>());
            }

            if (!this.messages[channel].ContainsKey(typeof(TMessage)))
            {
                this.messages[channel].Add(typeof(TMessage), new Container<object>());
            }

            if (this.messageCount[channel] == this.MaxChannelHistoryLength)
            {
                this.Pop<TMessage>(channel);
            }

            this.messageCount[channel]++;
            Contract.Assume(messages[channel] != null);
            this.messages[channel][typeof(TMessage)].Add(message as object);
        }

        public TMessage Pop<TMessage>(object channel = null)
        {
            Contract.Assume(channel == null || messages[channel][typeof(TMessage)] != null);
            Contract.Assume(channel == null || messages[channel][typeof(TMessage)].HasElements);

            Container<object> channelMessages;
            if (channel != null)
            {
                channelMessages = messages[channel][typeof(TMessage)];
            }
            else
            {
                var result = messages.Select(msgs => new
                {
                    chan = msgs.Key,
                    dictionary = msgs.Value
                })
                .First(pair => pair.dictionary.ContainsKey(typeof(TMessage))
                            && pair.dictionary[typeof(TMessage)].HasElements);
                channel = result.chan;
                channelMessages = result.dictionary[typeof(TMessage)];
            }

            object boxed = channelMessages.Pop();
            TMessage msg = boxed == null ? default(TMessage) : (TMessage)boxed;
            this.messageCount[channel]--;
            return msg;
        }

        public bool HasPending<TMessage>(object channel = null)
        {
            if (channel == null)
            {
                return this.messages.Values.SelectMany(val => val)
                    .Any(val => typeof(TMessage).Equals(val.Key) && val.Value.HasElements);
            }
            else
            {
                return this.messages.ContainsKey(channel)
                    && this.messages[channel].Any(
                            pair => typeof(TMessage).Equals(pair.Key)
                                 && pair.Value.HasElements);
            }
        }

        public bool HasPending(object channel = null)
        {
            if (channel == null)
            {
                return this.messageCount.Any(pair => pair.Value > 0);
            }

            return this.messageCount.ContainsKey(channel) && this.messageCount[channel] > 0;
        }

        [ContractInvariantMethod]
        private void Invariant()
        {
            Contract.Invariant(this.messageCount != null);
            Contract.Invariant(this.messages != null);
        }
        #endregion
    }
}
