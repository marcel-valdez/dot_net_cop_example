/*namespace Game.Logic
{
    using Game.Logic.Model;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Reflection;

    public static class RequestContext
    {

        /// <summary>
        /// Gets or sets the context getter.
        /// </summary>
        /// <value>
        /// The context getter.
        /// </value>
        public static Func<IDictionary> ContextGetter
        {
            get;
            set;
        }

        internal static Entities Model
        {
            get
            {
                if (ContextGetter()["context"] == null)
                {
                    ContextGetter().Add("context", new Entities());
                }

                return (Entities)ContextGetter()["context"];
            }
        }
    }
}
*/