using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class TransportDataDistance : TransportData
    {
        public TransportDataDistance(Int32 index, DateTime date, String plateNumber, Boolean isRoundtrip, Auxiliaries.Types.TransportType transportType, Double value, Auxiliaries.Units.Unit unit)
            :base(index, date, plateNumber, isRoundtrip, transportType, value, unit)
        {
        }

    }
}
