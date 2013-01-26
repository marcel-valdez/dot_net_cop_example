using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Core
{
    [System.Diagnostics.Contracts.ContractClass(typeof(ILogCodeContract))]
    public interface ILog
    {
        /// <summary>
        /// Adds to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        bool AddToLog(string message);

        /// <summary>
        /// Adds Exception and its inner exceptions to the log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exc">The exception.</param>
        /// <returns>true if successful.</returns>
        bool AddToLog(string message, Exception exc);

        /// <summary>
        /// Adds to log.
        /// </summary>
        /// <param name="exc">The exc.</param>
        /// <returns></returns>
        bool AddToLog(Exception exc);

        string Path
        {
            get;
            set;
        }

        bool DebugOn
        {
            get;
            set;
        }
    }
}
