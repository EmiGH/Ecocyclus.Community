using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Exceptions
{
    public class ExceptionMeterWater : ExceptionMeter
    {
        internal ExceptionMeterWater(Int64 idException, DateTime date, Meters.WaterMeter meter)
            : base(idException, date, meter)
        {

            _Meter = meter;

        }

        #region Private Properties

        private Meters.WaterMeter _Meter;

        #endregion

        #region Public Properties

        private Meters.WaterMeter Meter
        { get { return _Meter; } }

        #endregion
    }
}
