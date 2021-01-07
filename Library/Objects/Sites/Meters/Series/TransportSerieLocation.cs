using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class TransportSerieLocation: TransportSerie
    {
        internal TransportSerieLocation(Int64 idSerie, Int64 idMeter, DateTime date, String address, Boolean isRoundtrip, Auxiliaries.Geographic.Position location, String plateNumber, Int64 idTransportType, Double value, Double valuePattern, Double totalCO2, Auxiliaries.Units.Unit unit, Double EF, Int64 idTransportTypeEmissionFactor, Int64 idOperator, Security.Credential credential)
            : base(idSerie, idMeter, date, plateNumber, isRoundtrip, idTransportType, value, valuePattern, totalCO2, unit, EF, idTransportTypeEmissionFactor, idOperator, credential)
        {
            _Address = address;
            _Location = location;

        }
        internal TransportSerieLocation(Int64 idSerie, Int64 idMeter, DateTime date, String address, Boolean isRoundtrip, Auxiliaries.Geographic.Position location, String plateNumber, Int64 idTransportType, Double value, Double valuePattern, Double totalCO2, Auxiliaries.Units.Unit unit, Double EF, Int64 idTransportTypeEmissionFactor, Users.UserOperator userOperator, Security.Credential credential)
            : base(idSerie, idMeter, date, plateNumber, isRoundtrip, idTransportType, value, valuePattern, totalCO2, unit, EF, idTransportTypeEmissionFactor, userOperator, credential)
        {
            _Address = address;
            _Location = location;

        }

        private String _Address;
        private Auxiliaries.Geographic.Position _Location;

        public String Address
        { get { return _Address; } }
        public Auxiliaries.Geographic.Position Location
        { get { return _Location; } }
    }
}
