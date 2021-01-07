using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class WaterLoad
    {
        private Int64 _IdLoad;
        private Int64 _IdOperator;
        private Users.UserOperator _Operator;
        private Int64 _IdMeter;
        private Auxiliaries.Units.Unit _Unit;

        private DateTime _From;
        private DateTime _To;

        private Double _Value;
        private Double _ValueInput;
        private Double _EF;

        private Security.Credential _Credential;

        internal WaterLoad(Int64 idLoad, Int64 idMeter, Int64 idOperator, DateTime from, DateTime to, Double value, Double valueInput, Auxiliaries.Units.Unit unit, Double ef, Security.Credential credential)
        {
            _IdLoad = idLoad;
            _IdOperator = idOperator;
            _IdMeter = idMeter;
            _Unit = unit;

            _From = from;
            _To = to;

            _Value = value;
            _ValueInput = valueInput;
            _EF = ef;

            _Credential = credential;
        }
        internal WaterLoad(Int64 idLoad, Int64 idMeter, Users.UserOperator userOperator, DateTime from, DateTime to, Double value, Double valueInput, Auxiliaries.Units.Unit unit, Double ef, Security.Credential credential)
        {
            _IdLoad = idLoad;
            _Operator = userOperator;
            _IdMeter = idMeter;
            _Unit = unit;

            _From = from;
            _To = to;

            _Value = value;
            _ValueInput = valueInput;
            _EF = ef;

            _Credential = credential;
        }

        public Int64 IdLoad
        { get { return _IdLoad; } }
        public Users.UserOperator Operator
        {
            get
            {
                if (_Operator == null)
                {
                    _Operator = new Handlers.Operators().Item(_IdOperator, _Credential);
                }
                return _Operator;
            }
        }
        public Meters.WaterMeter Meter
        { get { return new Handlers.WaterMeters().Item(_IdMeter, _Credential); } }

        public DateTime From
        { get { return _From; } }
        public DateTime To
        { get { return _To; } }
        public Double ValueInput
        { get { return _ValueInput; } }
        public Double Value
        { get { return _Value; } }
        public Auxiliaries.Units.Unit Unit
        { get { return _Unit; } }
        public Double EF
        { get { return _EF; } }
        public Double TotalCO2
        { get { return _EF * Unit.ToPattern(_Value); } }
    }
}
