using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Exceptions
{
    public class ExceptionMeterWaste : ExceptionMeter
    {
        internal ExceptionMeterWaste(Int64 idException, DateTime date, Meters.WasteMeter meter)
            : base(idException, date, meter)
        {

            _Meter = meter;

        }

        #region Private Properties

        private Meters.WasteMeter _Meter;

        #endregion

        #region Public Properties

        private Meters.WasteMeter Meter
        { get { return _Meter; } }

        #endregion
    }
}
