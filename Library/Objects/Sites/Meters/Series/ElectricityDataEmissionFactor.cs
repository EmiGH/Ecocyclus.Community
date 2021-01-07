using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class ElectricityDataEmissionFactor
    {
        private Security.Credential _Credential;
        private Int64 _IdEmissionFactor;
        private DataEmissionFactor _newEmissionFactor;
        
        public ElectricityDataEmissionFactor(Auxiliaries.Geographic.Country country, Double value, String description, List<KeyValuePair<String, String>> descriptions, Users.UserOperatorMe I)
        {
            _newEmissionFactor = new DataEmissionFactor(country, value, description, descriptions);
            _Credential = I.Credential;
        }
        public ElectricityDataEmissionFactor(Int64 idEmissionFactor, Users.UserOperatorMe I)
        {
            _IdEmissionFactor = idEmissionFactor;
            _Credential = I.Credential;
        }

        public Boolean IsNew
        { get { return _IdEmissionFactor==0; } }

        //New emission factor
        public DataEmissionFactor NewEmissionFactor
        { get { return _newEmissionFactor; } }

        //Existing emission Factor
        public Auxiliaries.EmissionFactors.EmissionFactor EmissionFactor
        {
            get
            {
                if (_IdEmissionFactor > 0)
                    return new Handlers.ElectricityEmissionFactors().Item(_IdEmissionFactor, _Credential);
                return null;
            }
        }

    }
}
