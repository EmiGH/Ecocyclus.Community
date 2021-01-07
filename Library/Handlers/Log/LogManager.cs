using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Handlers
{
    internal class LogManager
    {
        internal LogManager() { }

        internal void LogError(Int64 idUser, String error)
        {
            new Storage.LogManager().LogError(idUser, error);
        }
    }
}
