namespace Game.Core
{
    using System.Diagnostics.Contracts;
    using System.Collections.Generic;

    /// <summary>
    /// Responsabilidad:
    /// Funcionar como buffer de mensajes, para ser revisados después
    /// de que los mismos han sido recibidos
    /// </summary>
    [ContractClass(typeof(IMessageQueueCodeContract))]
    public interface IMessageQueue
    {
        /// <summary>
        /// Gets the max length of a channel's history.
        /// </summary>
        /// <value>
        /// The max length of a channel's history.
        /// </value>
        [Pure]
        int MaxChannelHistoryLength
        {
            get;
        }

        /// <summary>
        /// Gets the channels.
        /// </summary>
        [Pure]
        IEnumerable<object> Channels
        {
            get;
        }

        /// <summary>
        /// Pushes a message to the specified channel.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="channel">The channel.</param>
        /// <param name="message">The message.</param>
        void Push<TMessage>(object channel, TMessage message);

        /// <summary>
        /// Determines whether the specified channel has pending messages of type <typeparamref name="TMessage"/>.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="channel">The channel.</param>
        /// <returns>
        ///   <c>true</c> if the specified channel has pending; otherwise, <c>false</c>.
        /// </returns>
        [Pure]
        bool HasPending<TMessage>(object channel = null);

        /// <summary>
        /// Determines whether the specified channel has pending messages.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>
        ///   <c>true</c> if the specified channel has pending messages; otherwise, <c>false</c>.
        /// </returns>
        [Pure]
        bool HasPending(object channel = null);

        /// <summary>
        /// Gets the messages for a given channel.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="channel">The channel.</param>
        /// <returns>The list of messages, changes made to the list get
        /// reflected to the actual messages in the queue</returns>
        [Pure]
        TMessage Pop<TMessage>(object channel = null);
    }
}
