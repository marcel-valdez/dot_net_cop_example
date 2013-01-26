namespace Game.Core
{
    using System.Diagnostics.Contracts;

    [ContractClassFor(typeof(ILog))]
    internal abstract class ILogCodeContract : Game.Core.ILog
    {
        public bool AddToLog(System.Exception exc)
        {
            Contract.Requires(exc != null);
            return default(bool);
        }
    
        public bool AddToLog(System.Exception exc, string message)
        {
            Contract.Requires(exc != null);
            Contract.Requires(message != null);
            return default(bool);
        }
    
        public bool AddToLog(string message)
        {
            Contract.Requires(!string.IsNullOrEmpty(message));
            return default(bool);
        }

        #region ILog Members


        public bool AddToLog(string message, System.Exception exc)
        {
            Contract.Requires(exc != null);
            Contract.Requires(message != null);
            return default(bool);
        }

        public string Path
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
                return default(string);
            }

            set
            {
                Contract.Requires(!string.IsNullOrEmpty(value));
            }
        }

        public bool DebugOn
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        #endregion
    }
}
