using System;

namespace CSI.Library.Objects.Sites.Exceptions
{
    public class ExceptionMeterFleet : ExceptionMeter
    {
        internal ExceptionMeterFleet(Int64 idException, DateTime date, Meters.FleetMeter meter)
            : base(idException, date, meter)
        {

            _Meter = meter;

        }

        #region Private Properties

        private Meters.FleetMeter _Meter;

        #endregion

        #region Public Properties

        private Meters.FleetMeter Meter
        { get { return _Meter; } }

        #endregion
    }
}
