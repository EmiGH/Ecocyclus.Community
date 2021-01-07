using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class TransportData
    {
        private Int32 _Index;
        private DateTime _Date;
        private String _PlateNumber;
        private Boolean _IsRoundtrip;
        private Double _Value;
        private Auxiliaries.Types.TransportType _TransportType;
        private Auxiliaries.Units.Unit _Unit;


        public TransportData(Int32 index, DateTime date, String plateNumber, Boolean isRoundtrip, Auxiliaries.Types.TransportType transportType, Double value, Auxiliaries.Units.Unit unit)
        {
            _Index = index;
            _Date = date;
            _PlateNumber = plateNumber;
            _IsRoundtrip = isRoundtrip;
            _TransportType = transportType;
            _Unit = unit;
            _Value = value;
        }
        public TransportData(Int32 index, DateTime date, String plateNumber, Boolean isRoundtrip, Auxiliaries.Types.TransportType transportType)
        {
            _Index = index;
            _Date = date;
            _PlateNumber = plateNumber;
            _IsRoundtrip = isRoundtrip;
            _TransportType = transportType;
        }

        public Int32 Index
        { get { return _Index; } }
        public DateTime Date
        { get { return _Date; } }
        public String PlateNumber
        { get { return _PlateNumber; } }
        public Boolean IsRoundtrip
        { get { return _IsRoundtrip; } }
        public virtual  Double Value
        { get { return _Value; } }
        public virtual Auxiliaries.Units.Unit Unit
        { get { return _Unit; } }
        public Auxiliaries.Types.TransportType TransportType
        { get { return _TransportType; } }

    }
}
