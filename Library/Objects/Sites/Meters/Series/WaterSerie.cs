using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class WaterSerie
    {
        internal WaterSerie(Int64 idSerie, Int64 idMeter, Int64 idLoad, DateTime day, Double value, Double valuePattern, Double totalCO2, Security.Credential credential)
        {
            _IdMeter = idMeter;
            _IdSerie = idSerie;
            _IdLoad = idLoad;
            _Day = day;
            _Value = value;
            _ValuePattern = valuePattern;
            _TotalCO2 = totalCO2;

            _Credential = credential;
        }

        #region Private Fields

        private Int64 _IdSerie;
        private Int64 _IdMeter;
        private Int64 _IdLoad;
        private DateTime _Day;
        private Double _Value;
        private Double _ValuePattern;
        private Double _TotalCO2;

        private Security.Credential _Credential;

        #endregion

        #region Public Properties

        public Meters.WaterMeter Meter
        { get { return new Handlers.WaterMeters().Item(_IdMeter, _Credential); } }
        public WaterLoad Load
        { get { return new Handlers.WaterMeterLoads().ReadById(_IdLoad, _Credential); } }

        public Int64 IdSerie
        { get { return _IdSerie; } }
        public DateTime Day
        { get { return _Day; } }
        public Double Value
        { get { return _Value; } }
        public Double ValuePattern
        { get { return _ValuePattern; } }
        public Double TotalCO2
        { get { return _TotalCO2; } }

        #endregion

        #region Public Methods


        #endregion
    }


}
