using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CSI.Library.Security
{
    [Serializable]
    public class RegistrationException : System.Exception
    {
        public RegistrationException()
        : base()
        { }

        public RegistrationException(string message) :
            base(message)
        { }

        public RegistrationException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected RegistrationException(SerializationInfo info, StreamingContext context)
            : base (info, context)
        {
            
        }
    }

}
