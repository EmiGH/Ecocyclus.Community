using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class TransportDataEmissionFactor
    {
        private Int64 _IdTransportType;
        private Security.Credential _Credential;
        private Int64 _IdTransportTypeEmissionFactor;
        private DataEmissionFactor _newEmissionFactor;

        public TransportDataEmissionFactor(Int64 idTransportType, Auxiliaries.Geographic.Country country, Double value, String description, List<KeyValuePair<String, String>> descriptions, Users.UserOperatorMe I)
        {
            _newEmissionFactor = new DataEmissionFactor(country, value, description, descriptions);
            _IdTransportType = idTransportType;
            _Credential = I.Credential;

        }
        public TransportDataEmissionFactor(Int64 idTransportTypeEmissionFactor, Users.UserOperatorMe I)
        {
            _IdTransportTypeEmissionFactor = idTransportTypeEmissionFactor;
            _Credential = I.Credential;
        }

        public Boolean IsNew
        { get { return _IdTransportTypeEmissionFactor > 0; } }

        //New emission factor
        public DataEmissionFactor NewEmissionFactor
        { get { return _newEmissionFactor; } }
        public Auxiliaries.Types.TransportType TransportType
        {
            get
            {
                if (IsNew)
                    return new Handlers.TransportTypes().Item(_IdTransportType, _Credential);
                return null;
            }
        }

        //Existing emission Factor
        public Auxiliaries.EmissionFactors.TransportTypeEmissionFactor TransportTypeEmissionFactor
        {
            get
            {
                if (_IdTransportTypeEmissionFactor > 0)
                    return new Handlers.TransportTypeEmissionFactors().Item(_IdTransportTypeEmissionFactor, _Credential);
                return null;
            }
        }
    }
}
