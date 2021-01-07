using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Geographic
{
    public class Country
    {
        internal Country(Int64 idCountry, String name, String code, String phoneCode, String paymentSystemCode, Security.Credential credential)
        {
            _Credential = credential;

            _IdCountry = idCountry;
            _Code = code;
            _PhoneCode = phoneCode;
            _PaymentSystemCode = paymentSystemCode;
        
            _LanguageOption = new CountryLanguageOption(credential.CurrentLanguage.IdLanguage, name);
        }

        #region Private Fields

        private Security.Credential _Credential;

        private Int64 _IdCountry;
        private String _Code;
        private String _PhoneCode;
        private String _PaymentSystemCode;

        private CountryLanguageOption _LanguageOption;

        #endregion
        
        #region Public Properties

        public Int64 IdCountry
        { get { return _IdCountry; } }
        public String Code
        { get { return _Code; } }
        public String PhoneCode
        { get { return _PhoneCode; } }
        public String PaymentSystemCode
        { get { return _PaymentSystemCode; } }

        public CountryLanguageOption LanguageOption
        { get { return _LanguageOption; } }
        public Dictionary<String, CountryLanguageOption> LanguageOptions
        { get { return new Handlers.CountryLanguageOptions().Items(_IdCountry); } }

        #endregion

        #region Public Methods

        #region EF

        public Dictionary<Int64, EmissionFactors.ElectricityEmissionFactor> GetEmissionFactorsForElectricity()
        {
            return new Handlers.ElectricityEmissionFactors().Items(_IdCountry, _Credential);
        }
        public Dictionary<Int64, EmissionFactors.ElectricityEmissionFactor> GetEmissionFactorsForElectricity(Int64 idCompany)
        {
            return new Handlers.ElectricityEmissionFactors().Items(_IdCountry, idCompany, _Credential);
        }
        public Dictionary<Int64, EmissionFactors.WaterEmissionFactor> GetEmissionFactorsForWater()
        {
            return new Handlers.WaterEmissionFactors().Items(_IdCountry, _Credential);
        }
        public Dictionary<Int64, EmissionFactors.WaterEmissionFactor> GetEmissionFactorsForWater(Int64 idCompany)
        {
            return new Handlers.WaterEmissionFactors().Items(_IdCountry, idCompany, _Credential);
        }
        public Dictionary<Int64, EmissionFactors.TransportTypeEmissionFactor> GetEmissionFactorsForTransportType(Int64 idTransportType)
        {
            return new Handlers.TransportTypeEmissionFactors().Items(idTransportType, _IdCountry, _Credential);
        }
        public Dictionary<Int64, EmissionFactors.TransportTypeEmissionFactor> GetEmissionFactorsForTransportType(Int64 idCompany, Int64 idTransportType)
        {
            return new Handlers.TransportTypeEmissionFactors().Items(idTransportType, _IdCountry, idCompany, _Credential);
        }
        public Dictionary<Int64, EmissionFactors.FuelTypeEmissionFactor> GetEmissionFactorsForFuelType(Int64 idFuelType)
        {
            return new Handlers.FuelTypeEmissionFactors().Items(idFuelType, _IdCountry, _Credential);
        }
        public Dictionary<Int64, EmissionFactors.FuelTypeEmissionFactor> GetEmissionFactorsForFuelType(Int64 idCompany, Int64 idFuelType)
        {
            return new Handlers.FuelTypeEmissionFactors().Items(idFuelType, _IdCountry, idCompany, _Credential);
        }
        public Dictionary<Int64, EmissionFactors.WasteTypeEmissionFactor> GetEmissionFactorsForWasteType(Int64 idWasteType)
        {
            return new Handlers.WasteTypeEmissionFactors().Items(idWasteType, _IdCountry, _Credential);
        }
        public Dictionary<Int64, EmissionFactors.WasteTypeEmissionFactor> GetEmissionFactorsForWasteType(Int64 idCompany, Int64 idWasteType)
        {
            return new Handlers.WasteTypeEmissionFactors().Items(idWasteType, _IdCountry, idCompany, _Credential);
        }

        #endregion

        #endregion
    }
}
