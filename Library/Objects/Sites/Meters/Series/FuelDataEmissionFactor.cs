using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class FuelDataEmissionFactor
    {
        private Int64 _IdFuelType;
        private Security.Credential _Credential;
        private Int64 _IdFuelTypeEmissionFactor;
        private DataEmissionFactor _newEmissionFactor;
        
        public FuelDataEmissionFactor(Int64 idFuelType, Auxiliaries.Geographic.Country country, Double value, String description, List<KeyValuePair<String, String>> descriptions, Users.UserOperatorMe I)
        {
            _newEmissionFactor = new DataEmissionFactor(country, value, description, descriptions);
            _IdFuelType = idFuelType;
            _Credential = I.Credential;

        }
        public FuelDataEmissionFactor(Int64 idFuelTypeEmissionFactor, Users.UserOperatorMe I)
        {
            _IdFuelTypeEmissionFactor = idFuelTypeEmissionFactor;
            _Credential = I.Credential;
        }

        public Boolean IsNew
        { get { return _IdFuelTypeEmissionFactor>0; } }

        //New emission factor
        public DataEmissionFactor NewEmissionFactor
        { get { return _newEmissionFactor; } }
        public Auxiliaries.Types.FuelType FuelType
        {
            get {
                if (IsNew)
                    return new Handlers.FuelTypes().Item(_IdFuelType, _Credential);
                return null;
            }
        }

        //Existing emission Factor
        public Auxiliaries.EmissionFactors.FuelTypeEmissionFactor FuelTypeEmissionFactor
        {
            get
            {
                if (_IdFuelTypeEmissionFactor > 0)
                    return new Handlers.FuelTypeEmissionFactors().Item(_IdFuelTypeEmissionFactor, _Credential);
                return null;
            }
        }
    }
}
