using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Exceptions
{
    public class ExceptionMeterTransport : ExceptionMeter
    {
        internal ExceptionMeterTransport(Int64 idException, DateTime date, Meters.TransportMeter meter)
            : base(idException, date, meter)
        {

            _Meter = meter;

        }

        #region Private Properties

        private Meters.TransportMeter _Meter;

        #endregion

        #region Public Properties

        private Meters.TransportMeter Meter
        { get { return _Meter; } }

        #endregion
    }
}
