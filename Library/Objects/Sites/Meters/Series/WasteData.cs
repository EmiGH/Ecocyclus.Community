using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class WasteData
    {
        private Int32 _Index;
        private DateTime _Date;
        private Auxiliaries.Types.WasteType _WasteType;
        private Double _Value;
        private Auxiliaries.Units.Unit _Unit;

        public WasteData(Int32 index, DateTime date, Auxiliaries.Types.WasteType wasteType, Double value, Auxiliaries.Units.Unit unit)
        {
            _Index = index;
            _Date = date;
            _WasteType = wasteType;
            _Value = value;
            _Unit = unit;
        }

        public Int32 Index
        { get { return _Index; } }
        public DateTime Date
        { get { return _Date; } }
        public Double Value
        { get { return _Value; } }
        public Auxiliaries.Units.Unit Unit
        { get { return _Unit; } }
        public Auxiliaries.Types.WasteType WasteType
        { get { return _WasteType; } }

    }
}
