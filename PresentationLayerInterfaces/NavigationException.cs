using System.Runtime.Serialization;
namespace Game.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NavigationException : Exception
    {
        public NavigationException()
        {
            
        }
        public NavigationException(string message)
            : base(message)
        {
            
        }
        public NavigationException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
        protected NavigationException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            
        }
    }
}
