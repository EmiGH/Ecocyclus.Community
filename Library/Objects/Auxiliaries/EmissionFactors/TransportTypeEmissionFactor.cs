using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.EmissionFactors
{
    public class TransportTypeEmissionFactor: EmissionFactor
    {

        internal TransportTypeEmissionFactor(Int64 idTransportTypeEmissionFactor, Int64 idEmissionFactor, Auxiliaries.Types.TransportType transportType, Int64 idCountry, Double value, Auxiliaries.Units.Unit unit, String description, Boolean isPropietary, Security.Credential credential)
            : base(idEmissionFactor, idCountry, value, unit, description, credential)
        {
            _IdTransportTypeEmissionFactor = idTransportTypeEmissionFactor;
            _TransportType = transportType;
            _IsPropietary = isPropietary;
        }

        #region Private Fields

        private Boolean _IsPropietary;
        private Auxiliaries.Types.TransportType _TransportType;
        private Int64 _IdTransportTypeEmissionFactor;

        #endregion

        #region Public Properties

        public Int64 IdTransportTypeEmissionFactor
        { get { return _IdTransportTypeEmissionFactor; } }
        public Auxiliaries.Types.TransportType TransportType
        { get { return _TransportType; } }
        internal Boolean IsPropietary
        { get { return _IsPropietary; } }

        #endregion
    }
}
