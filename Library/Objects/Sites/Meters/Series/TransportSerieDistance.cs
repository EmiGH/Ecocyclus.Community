using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class TransportSerieDistance : TransportSerie
    {
        internal TransportSerieDistance(Int64 idSerie, Int64 idMeter, DateTime date, String plateNumber, Boolean isRoundtrip, Int64 idTransportType, Double value, Double valuePattern, Double totalCO2, Auxiliaries.Units.Unit unit, Double EF, Int64 idTransportTypeEmissionFactor, Int64 idOperator, Security.Credential credential)
            : base(idSerie, idMeter, date, plateNumber, isRoundtrip, idTransportType, value, valuePattern, totalCO2, unit, EF, idTransportTypeEmissionFactor, idOperator, credential)
        { }
        internal TransportSerieDistance(Int64 idSerie, Int64 idMeter, DateTime date, String plateNumber, Boolean isRoundtrip, Int64 idTransportType, Double value, Double valuePattern, Double totalCO2, Auxiliaries.Units.Unit unit, Double EF, Int64 idTransportTypeEmissionFactor, Users.UserOperator userOperator, Security.Credential credential)
            : base(idSerie, idMeter, date, plateNumber, isRoundtrip, idTransportType, value, valuePattern, totalCO2, unit, EF, idTransportTypeEmissionFactor, userOperator, credential)
        { }
    }
}
