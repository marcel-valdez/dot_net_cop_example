namespace Game.Core
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(IMessageQueue))]
    internal abstract class IMessageQueueCodeContract : IMessageQueue
    {
        #region IMessageQueue Members
        public int MaxChannelHistoryLength
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() > 0);
                return default(int);
            }
        }

        
        public IEnumerable<object> Channels
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<object>>() != null);
                return default(IEnumerable<object>);
            }
        }

        
        public bool HasPending(object channel = null)
        {
            return default(bool);
        }

        public bool HasPending<TMessage>(object channel = null)
        {
            return default(bool);
        }

        public TMessage Pop<TMessage>(object channel = null)
        {
            Contract.EnsuresOnThrow<KeyNotFoundException>(!(this as IMessageQueue).HasPending<TMessage>(channel));
            return default(TMessage);
        }

        public void Push<TMessage>(object channel, TMessage message)
        {
            Contract.Requires(channel != null, "channel is null.");
        }

        #endregion
    }
}
