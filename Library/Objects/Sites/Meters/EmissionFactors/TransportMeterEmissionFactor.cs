using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.EmissionFactors
{
    public class TransportMeterEmissionFactor
    {
        internal TransportMeterEmissionFactor(Int64 idTransportMeterEmissionFactor, Int64 idTransportTypeEmissionFactor, Security.Credential credential)
        {
            _IdTransportMeterEmissionFactor = idTransportMeterEmissionFactor;
            _IdTransportTypeEmissionFactor = idTransportTypeEmissionFactor;
            _Credential = credential;
        }

        #region Private Fields

        private Int64 _IdTransportMeterEmissionFactor;
        private Int64 _IdTransportTypeEmissionFactor;
        private Security.Credential _Credential;

        #endregion

        #region Public Properties

        public Int64 IdTransportMeterEmissionFactor
        { get { return _IdTransportMeterEmissionFactor; } }
        public Auxiliaries.EmissionFactors.TransportTypeEmissionFactor TransportTypeEmissionFactor
        { get { return new Handlers.TransportTypeEmissionFactors().Item(_IdTransportTypeEmissionFactor, _Credential); } }

        #endregion
    }
}
