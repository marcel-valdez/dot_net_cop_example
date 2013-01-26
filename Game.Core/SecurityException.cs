using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Game.Logic
{
    public class SecurityException : Exception
    {
        public SecurityException()
        {
        }
        public SecurityException(string message)
            : base(message)
        {   
        }
        public SecurityException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
        protected SecurityException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {   
        }
    }
}
