using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Exceptions
{
    public class ExceptionTarget : Exception
    {
        internal ExceptionTarget(Int64 idException, DateTime date)
            : base(idException, date)
        { }
    }
}
