using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class WasteSerie
    {
        internal WasteSerie(Int64 idSerie, Int64 idMeter, DateTime date, Int64 idWasteType, Double value, Double valuePattern, Double totalCO2, Auxiliaries.Units.Unit unit, Double EF, Int64 idWasteTypeEmissionFactor, Int64 idOperator, Security.Credential credential)
        {
            _IdMeter = idMeter;
            _IdSerie = idSerie;
            _Date = date;
            _IdWasteType = idWasteType;
            _Value = value;
            _ValuePattern = valuePattern;
            _TotalCO2 = totalCO2;
            _Unit = unit;
            _EF = EF;
            _IdWasteTypeEmissionFactor = idWasteTypeEmissionFactor;
            _IdOperator = idOperator;

            _Credential = credential;
        }
        internal WasteSerie(Int64 idSerie, Int64 idMeter, DateTime date, Int64 idWasteType, Double value, Double valuePattern, Double totalCO2, Auxiliaries.Units.Unit unit, Double EF, Int64 idWasteTypeEmissionFactor, Users.UserOperator userOperator, Security.Credential credential)
        {
            _IdMeter = idMeter;
            _IdSerie = idSerie;
            _Date = date;
            _IdWasteType = idWasteType;
            _Value = value;
            _ValuePattern = valuePattern;
            _TotalCO2 = totalCO2;
            _Unit = unit;
            _EF = EF;
            _IdWasteTypeEmissionFactor = idWasteTypeEmissionFactor;
            _Operator = userOperator;

            _Credential = credential;
        }

        #region Private Fields

        private Int64 _IdSerie;
        private Int64 _IdOperator;
        private Users.UserOperator _Operator;
        private Int64 _IdMeter;
        private Int64 _IdWasteType;
        private Int64 _IdWasteTypeEmissionFactor;
        private Auxiliaries.Units.Unit _Unit;

        private DateTime _Date;
        
        private Double _Value;
        private Double _ValuePattern;
        private Double _TotalCO2;
        private Double _EF;

        private Security.Credential _Credential;

        #endregion

        #region Public Properties

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
        public Meters.WasteMeter Meter
        { get { return new Handlers.WasteMeters().Item(_IdMeter, _Credential); } }

        public Int64 IdSerie
        { get { return _IdSerie; } }
        public DateTime Date
        { get { return _Date; } }
        public Auxiliaries.Types.WasteType WasteType
        { get { return new Handlers.WasteTypes().Item(_IdWasteType, _Credential); } }
        public Double ValuePattern
        { get { return _ValuePattern; } }
        public Double Value
        { get { return _Value; } }
        public Auxiliaries.Units.Unit Unit
        { get { return _Unit; } }
        public Double EF
        { get { return _EF; } }
        public Auxiliaries.EmissionFactors.WasteTypeEmissionFactor EmissionFactor
        { get { return new Handlers.WasteTypeEmissionFactors().Item(_IdWasteTypeEmissionFactor, _Credential); } }
        public Double TotalCO2
        { get { return _TotalCO2; } }

        #endregion

        #region Public Methods


        #endregion
    }
}
