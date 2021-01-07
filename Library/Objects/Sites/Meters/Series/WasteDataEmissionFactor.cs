using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class WasteDataEmissionFactor
    {
        private Int64 _IdWasteType;
        private Security.Credential _Credential;
        private Int64 _IdWasteTypeEmissionFactor;
        private DataEmissionFactor _newEmissionFactor;

        public WasteDataEmissionFactor(Int64 idWasteType, String idLanguage, Auxiliaries.Geographic.Country country, Double value, String name, String description, List<KeyValuePair<String, String>> descriptions, Users.UserOperatorMe I)
        {
            _newEmissionFactor = new DataEmissionFactor(country, value, description, descriptions);
            _IdWasteType = idWasteType;
            _Credential = I.Credential;
        }
        public WasteDataEmissionFactor(Int64 idWasteTypeEmissionFactor, Users.UserOperatorMe I)
        {
            _IdWasteTypeEmissionFactor = idWasteTypeEmissionFactor;
            _Credential = I.Credential;
        }

        public Boolean IsNew
        { get { return _IdWasteTypeEmissionFactor>0; } }

        //New emission factor
        public DataEmissionFactor NewEmissionFactor
        { get { return _newEmissionFactor; } }
        public Auxiliaries.Types.WasteType WasteType
        {
            get {
                if (IsNew)
                    return new Handlers.WasteTypes().Item(_IdWasteType, _Credential);
                return null;
            }
        }

        //Existing emission Factor
        public Auxiliaries.EmissionFactors.WasteTypeEmissionFactor WasteTypeEmissionFactor
        {
            get
            {
                if (_IdWasteTypeEmissionFactor > 0)
                    return new Handlers.WasteTypeEmissionFactors().Item(_IdWasteTypeEmissionFactor, _Credential);
                return null;
            }
        }
    }
}
