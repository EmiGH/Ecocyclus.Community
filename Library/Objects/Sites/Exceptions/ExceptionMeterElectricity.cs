using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Exceptions
{
    public class ExceptionMeterElectricity : ExceptionMeter
    {
        internal ExceptionMeterElectricity(Int64 idException, DateTime date, Meters.ElectricityMeter meter)
            :base(idException, date, meter)
        {

            _Meter = meter;

        }

        #region Private Properties

        private Meters.ElectricityMeter _Meter;

        #endregion
        
        #region Public Properties

        private Meters.ElectricityMeter Meter
        { get { return _Meter; } }

        #endregion
    }
}
