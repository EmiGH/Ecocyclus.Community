using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Exceptions
{
    public class ExceptionMeterFuel : ExceptionMeter
    {
        internal ExceptionMeterFuel(Int64 idException, DateTime date, Meters.FuelMeter meter)
            : base(idException, date, meter)
        {

            _Meter = meter;

        }

        #region Private Properties

        private Meters.FuelMeter _Meter;

        #endregion
        
        #region Public Properties

        private Meters.FuelMeter Meter
        { get { return _Meter; } }

        #endregion
    }
}
