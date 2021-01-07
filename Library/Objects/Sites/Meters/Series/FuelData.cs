using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class FuelData
    {
        private Int32 _Index;
        private DateTime _Date;
        private Auxiliaries.Types.FuelType _FuelType;
        private Double _Value;
        private Auxiliaries.Units.Unit _Unit;


        public FuelData(Int32 index, DateTime date, Auxiliaries.Types.FuelType fuelType, Double value, Auxiliaries.Units.Unit unit)
        {
            _Index = index;
            _Date = date;
            _FuelType = fuelType;
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
        public Auxiliaries.Types.FuelType FuelType
        { get { return _FuelType; } }
                
    }
}
