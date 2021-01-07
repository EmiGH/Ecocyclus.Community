using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.EmissionFactors
{
    public class EmissionFactor
    {
        internal EmissionFactor(Int64 idEmissionFactor, Int64 idCountry, Double value, Auxiliaries.Units.Unit unit, String description, Security.Credential credential)
        {
            _IdEmissionFactor = idEmissionFactor;
            _IdCountry = idCountry;
            _Value = value;
            _Unit = unit;

            _LanguageOption = new EmissionFactorLanguageOption(credential.CurrentLanguage.IdLanguage, description);
            _Credential = credential;
        }

        #region Private Fields

        internal Security.Credential _Credential;
        private Int64 _IdEmissionFactor;
        private Int64 _IdCountry;
        private Double _Value;
        private Auxiliaries.Units.Unit _Unit;
        
        private EmissionFactorLanguageOption _LanguageOption;

        #endregion
        
        #region Public Properties

        public Int64 IdEmissionFactor
        { get { return _IdEmissionFactor; } }
        public Double Value
        { get { return _Value; } }
        public Units.Unit Unit
        { get { return _Unit; } }
        public Auxiliaries.Geographic.Country Country
        { get { return new Handlers.Countries().Item(_IdCountry, _Credential); } }

        public EmissionFactorLanguageOption LanguageOption
        { get { return _LanguageOption; } }
        public Dictionary<String, EmissionFactorLanguageOption> LanguageOptions
        { get { return new Handlers.EmissionFactorLanguageOptions().Items(_IdEmissionFactor); } }

        #endregion

        #region Public Methods

        public Double TotalCO2(Double value)
        { return value * _Value; }

        public static Dictionary<Int64, Geographic.Country> GetCountriesForElectricity(Users.UserOperatorMe I)
        {
            return new Handlers.ElectricityEmissionFactors().Countries(I.Credential);
        }
        public static Dictionary<Int64, Geographic.Country> GetCountriesForWater(Users.UserOperatorMe I)
        {
            return new Handlers.WaterEmissionFactors().Countries(I.Credential);
        }
        public static Dictionary<Int64, Geographic.Country> GetCountriesForTransportType(Int64 idTransportType, Users.UserOperatorMe I)
        {
            return new Handlers.TransportTypeEmissionFactors().Countries(idTransportType, I.Credential);
        }
        public static Dictionary<Int64, Geographic.Country> GetCountriesForWasteType(Int64 idWasteType, Users.UserOperatorMe I)
        {
            return new Handlers.WasteTypeEmissionFactors().Countries(idWasteType, I.Credential);
        }
        public static Dictionary<Int64, Geographic.Country> GetCountriesForFuelType(Int64 idFuelType, Users.UserOperatorMe I)
        {
            return new Handlers.FuelTypeEmissionFactors().Countries(idFuelType, I.Credential);
        }

        #endregion
    }
}
