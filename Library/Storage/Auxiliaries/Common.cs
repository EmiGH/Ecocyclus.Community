using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Storage.Auxiliaries
{
    internal class Common
    {
        internal Common() { }

        internal static Object CastValueToNull<T>(object value, T defaultValue)
        {

            if (value.ToString() == "0" || value.ToString() == "") return DBNull.Value;
            return value;
        }
    }
}
