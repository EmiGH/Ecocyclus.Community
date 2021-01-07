using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class TransportSerie
    {
        internal TransportSerie(Int64 idSerie, Int64 idMeter, DateTime date, String plateNumber, Boolean isRoundtrip, Int64 idTransportType, Double value, Double valuePattern, Double totalCO2, Auxiliaries.Units.Unit unit, Double EF, Int64 idTransportTypeEmissionFactor, Int64 idOperator, Security.Credential credential)
        {
            _IdMeter = idMeter;
            _IdSerie = idSerie;
            _Date = date;
            _PlateNumber = plateNumber;
            _IsRoundtrip = isRoundtrip;
            _IdTransportType = idTransportType;
            _Value = value;
            _ValuePattern = valuePattern;
            _TotalCO2 = totalCO2;
            _Unit = unit;
            _EF = EF;
            _IdTransportTypeEmissionFactor = idTransportTypeEmissionFactor;
            _IdOperator = idOperator;

            _Credential = credential;
        }
        internal TransportSerie(Int64 idSerie, Int64 idMeter, DateTime date, String plateNumber, Boolean isRoundtrip, Int64 idTransportType, Double value, Double valuePattern, Double totalCO2, Auxiliaries.Units.Unit unit, Double EF, Int64 idTransportTypeEmissionFactor, Users.UserOperator userOperator, Security.Credential credential)
        {
            _IdMeter = idMeter;
            _IdSerie = idSerie;
            _Date = date;
            _PlateNumber = plateNumber;
            _IsRoundtrip = isRoundtrip;
            _IdTransportType = idTransportType;
            _Value = value;
            _ValuePattern = valuePattern;
            _TotalCO2 = totalCO2;
            _Unit = unit;
            _EF = EF;
            _IdTransportTypeEmissionFactor = idTransportTypeEmissionFactor;
            _Operator = userOperator;

            _Credential = credential;
        }
        
        #region Private Fields

        private Int64 _IdSerie;
        private Int64 _IdOperator;
        private Users.UserOperator _Operator;
        private Int64 _IdMeter;
        private Int64 _IdTransportType;
        private Int64 _IdTransportTypeEmissionFactor;
        private Auxiliaries.Units.Unit _Unit;
        private DateTime _Date;
        private String _PlateNumber;
        private Boolean _IsRoundtrip;
        private Double _Value;
        private Double _ValuePattern;
        private Double _TotalCO2;
        private Double _EF;

        internal Security.Credential _Credential;

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
        public Meters.TransportMeter Meter
        { get { return new Handlers.TransportMeters().Item(_IdMeter, _Credential); } }

        public Int64 IdSerie
        { get { return _IdSerie; } }
        public DateTime Date
        { get { return _Date; } }
        
        public String PlateNumber
        { get { return _PlateNumber; } }
        public Auxiliaries.Types.TransportType TransportType
        { get { return new Handlers.TransportTypes().Item(_IdTransportType, _Credential); } }
        public Boolean IsRoundtrip
        { get { return _IsRoundtrip; } }
        public Double ValuePattern
        { get { return _ValuePattern; } }
        public Double Value
        { get { return _Value; } }
        public Auxiliaries.Units.Unit Unit
        { get { return _Unit; } }
        public Double EF
        { get { return _EF; } }
        public Auxiliaries.EmissionFactors.TransportTypeEmissionFactor EmissionFactor
        { get { return new Handlers.TransportTypeEmissionFactors().Item(_IdTransportTypeEmissionFactor, _Credential); } }
        public Double TotalCO2
        { get { return _TotalCO2; } }

        #endregion

        #region Public Methods


        #endregion
    }
}
