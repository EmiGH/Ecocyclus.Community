using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters
{
    public abstract class Meter
    {
        #region Private fields

        private Int64 _IdMeter;
        private Int64 _IdSite;
        private String _Identification;
        private Int64 _IdDefaultUnit;

        private Security.Credential _Credential;

        #endregion

        internal Meter(Int64 idMeter, Int64 idSite, String identification, Int64 idDefaultUnit, Security.Credential credential)
        {
            _Credential = credential;

            _IdMeter = idMeter;
            _IdSite = idSite;
            _Identification = identification;
            _IdDefaultUnit = idDefaultUnit;
        }

        #region Public Properties

        internal protected Security.Credential Credential
        { get { return _Credential; } }

        public Int64 IdMeter
        {
            get { return _IdMeter; }
        }
        public String Identification
        {
            get { return _Identification; }
        }
        public Site Site
        {
            get { return new Handlers.Sites().ItemByOperator(_IdSite, _Credential); }
        }

        #endregion

        #region Public Methods

        public Auxiliaries.Units.Unit DefaultUnit
        { get { return new Handlers.Units().Item(_IdDefaultUnit, Credential); } }
        

        public abstract DateTime? GetLastDate();

        public abstract Metrics.MetricPeriod GetStatistics();
        public abstract Metrics.MetricPeriod GetStatistics(DateTime from, DateTime to);

        #endregion
    }
}
