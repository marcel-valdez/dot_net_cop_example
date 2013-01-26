namespace Game.Core
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Responsabilidad:
    /// Funcionar como el 'sink' de mensajes
    /// </summary>
    [ContractClass(typeof(IMessagingCodeContract))]
    public interface IMessaging : IDisposable
    {
        /// <summary>
        /// Subsribes to the specified channel.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="channel">The channel.</param>
        /// <param name="handler">The handler.</param>
        /// <param name="filter">The filter.</param>
        void Subscribe<TMessage>(object channel, Action<object, TMessage> handler, Predicate<TMessage> filter);

        /// <summary>
        /// Subscribes the specified channel, and only receives messages of 
        /// type <typeparamref name="TMessage"/>
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="channel">The channel.</param>
        /// <param name="handler">The handler.</param>
        void Subscribe<TMessage>(object channel, Action<object, TMessage> handler);
        
        /// <summary>
        /// Unsubscribes from the specified channel.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message to stop receiving.</typeparam>
        /// <param name="channel">The channel.</param>
        void Unsubscribe<TMessage>(object channel, Action<object, TMessage> handler);

        /// <summary>
        /// Publishes the specified channel.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="channel">The channel.</param>
        /// <param name="message">The message.</param>
        void Publish<TMessage>(object channel, TMessage message);
    }
}
