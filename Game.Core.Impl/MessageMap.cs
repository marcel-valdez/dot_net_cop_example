namespace Game.Core.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Game.Core;
    /// <summary>
    /// Stores messages in channels
    /// </summary>
    internal class MessageMap
    {
        private Dictionary<object, ChannelMessagesContainer> map;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageMap"/> class.
        /// </summary>
        public MessageMap()
        {
            this.map = new Dictionary<object, ChannelMessagesContainer>();
        }

        /// <summary>
        /// Gets the messages in a channel
        /// </summary>
        /// <param name="channel">The channel. Null for all channels</param>
        /// <returns>
        /// All messages of the type <typeparamref name="T"/> in the channel
        /// </returns>
        public IEnumerable<Tuple<object, object>> GetMessages(object channel = null)
        {
            lock (this.map)
            {
                foreach (var pair in this.map
                        .Where(pair => channel == null || pair.Key.Equals(channel))
                        .Select(pair => new
                        {
                            key = pair.Key,
                            container = pair.Value
                        }))
                {
                    foreach (Tuple<Type, object> match in pair.container.ToArray())
                    {
                        pair.container.Remove(match);
                        yield return Tuple.Create(pair.key, match.Item2);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the messages in a channel
        /// </summary>
        /// <param name="channel">The channel. Null for all channels</param>
        /// <returns>
        /// All messages of the type <typeparamref name="T"/> in the channel
        /// </returns>
        public IEnumerable<Tuple<object, T>> GetMessages<T>(object channel = null)
        {
            lock (this.map)
            {
                foreach (var pair in this.map
                        .Where(pair => channel == null || pair.Key.Equals(channel))
                        .Select(pair => new
                        {
                            key = pair.Key,
                            container = pair.Value
                        }))
                {
                    foreach (Tuple<Type, object> match in pair.container
                                                        .Where(
                                                            tuple => tuple.Item1.IsAssignableFrom(typeof(T)))
                                                        .ToArray())
                    {
                        pair.container.Remove(match);
                        yield return Tuple.Create(pair.key, (T)match.Item2);
                    }
                }
            }
        }


        public bool HasMessages
        {
            get
            {
                return this.map.Values.Any(val => val.HasElements);
            }
        }

        /// <summary>
        /// Adds a message to a given channel
        /// </summary>
        /// <typeparam name="T">The type of the message</typeparam>
        /// <param name="channel">The channel.</param>
        /// <param name="message">The message.</param>
        public void AddMessage<T>(object channel, T message)
        {
            lock (this.map)
            {
                if (!this.map.ContainsKey(channel))
                {
                    this.map.Add(channel, new ChannelMessagesContainer());
                }

                this.map[channel].Add(Tuple.Create(typeof(T), (object)message));
            }
        }
    }
}
