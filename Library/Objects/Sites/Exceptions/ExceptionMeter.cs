using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Exceptions
{
    public abstract class ExceptionMeter : Exception
    {
        internal ExceptionMeter(Int64 idException, DateTime date, Meters.Meter meter)
            :base(idException, date)
        {
            _Meter = meter;
        }

        #region Private Properties

        private Meters.Meter _Meter;

        #endregion

        #region Public Properties

        private Meters.Meter Meter
        { get { return _Meter; } }

        #endregion
    }
}
