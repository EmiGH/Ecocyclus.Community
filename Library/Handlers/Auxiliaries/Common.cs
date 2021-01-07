using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Handlers
{
    internal class Common
    {
        internal Common() { }


        internal static T CastNullValues<T>(object value, T defaultValue)
        {
            if (value == DBNull.Value) return defaultValue;
            return (T)Convert.ChangeType(value, typeof(T));
        }

    }
}
