namespace Game.Core
{
    using System;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(IMessaging))]
    internal abstract class IMessagingCodeContract : IMessaging
    {
        public void Publish<TMessage>(object channel, TMessage message)
        {
            Contract.Requires(channel != null, "channel is null.");
        }

        public void Unsubscribe(object channel, Action<object, object> handler)
        {
            Contract.Requires(handler != null, "handler is null.");
            Contract.Requires(channel != null, "channel is null.");
        }

        public void Unsubscribe<TMessage>(object channel, Action<object, TMessage> handler)
        {
            Contract.Requires(handler != null, "handler is null.");
            Contract.Requires(channel != null, "channel is null.");
        }

        public void Subscribe(object channel, Action<object, object> handler)
        {
            Contract.Requires(handler != null, "handler is null.");
            Contract.Requires(channel != null, "channel is null.");
        }

        public void Subscribe<TMessage>(object channel, Action<object, TMessage> handler)
        {
            Contract.Requires(handler != null, "handler is null.");
            Contract.Requires(channel != null, "channel is null.");
        }

        public void Subscribe<TMessage>(object channel, Action<object, TMessage> handler, Predicate<TMessage> filter)
        {
            Contract.Requires(filter != null, "filter is null.");
            Contract.Requires(handler != null, "handler is null.");
            Contract.Requires(channel != null, "channel is null.");
        }

        public void Subscribe(object channel, Action<object, object> handler, Predicate<object> filter)
        {
            Contract.Requires(filter != null, "filter is null.");
            Contract.Requires(handler != null, "handler is null.");
            Contract.Requires(channel != null, "channel is null.");
        }

        #region IDisposable Members
        public void Dispose()
        {
        }
        #endregion
    }
}
