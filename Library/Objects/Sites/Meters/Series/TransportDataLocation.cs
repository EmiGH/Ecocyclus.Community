using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class TransportDataLocation : TransportData
    {
        public TransportDataLocation(Int32 index, DateTime date, String address, String plateNumber, Boolean isRoundtrip, Auxiliaries.Geographic.Position siteLocation, Auxiliaries.Geographic.Position otherLocation, Auxiliaries.Types.TransportType transportType, Users.UserOperatorMe I)
            : base(index, date, plateNumber, isRoundtrip, transportType)
        {
            _Address = address;
            _IsRoundtrip = isRoundtrip;
            _Location = otherLocation;

            Objects.Auxiliaries.Units.Unit _UnitGeo = new Handlers.Units().ItemForSQL(I.Credential);
            _Unit = new Handlers.Units().ItemGeoDefault(I.Credential);

            _Value = _UnitGeo.ToPattern((Double)otherLocation.ToSqlGeography.STDistance(siteLocation.ToSqlGeography));
        }
        public TransportDataLocation(Int32 index, DateTime date, String address, String plateNumber, Boolean isRoundtrip, Auxiliaries.Geographic.Position siteLocation, Auxiliaries.Geographic.Position otherLocation, Auxiliaries.Types.TransportType transportType, Double valueInMts, Users.UserOperatorMe I)
            : base(index, date, plateNumber, isRoundtrip, transportType)
        {
            _Address = address;
            _IsRoundtrip = isRoundtrip;
            _Location = otherLocation;

            _Unit = new Handlers.Units().ItemForGoogle(I.Credential); 

            _Value = valueInMts;
        }

        private String _Address;
        private Boolean _IsRoundtrip;
        private Auxiliaries.Geographic.Position _Location;
        private Double _Value;
        private Auxiliaries.Units.Unit _Unit;

        public String Address
        { get { return _Address; } }
        public Auxiliaries.Geographic.Position Location
        { get { return _Location; } }
        public override Double Value
        { get { return _Value; } }
        public override Auxiliaries.Units.Unit Unit
        { get { return _Unit; } }
    }
}
