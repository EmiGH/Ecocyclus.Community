using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Units
{
    public class Currency
    {
        internal Currency(Int64 idCurrency, String name, String symbol, Double conversionIndex, Boolean isPattern, String paymentSystemCode, Security.Credential credential)
        {
            _Credential = credential;

            _IdCurrency = idCurrency;
            _Symbol = symbol;
            _ConversionIndex = conversionIndex;
            _IsPattern = isPattern;
            _PaymentSystemCode = paymentSystemCode;

            _LanguageOption = new CurrencyLanguageOption(credential.CurrentLanguage.IdLanguage, name);
        }

        #region Private Fields

        private Security.Credential _Credential;

        private Int64 _IdCurrency;
        private String _Symbol;
        private Double _ConversionIndex;
        private Boolean _IsPattern;
        private String _PaymentSystemCode;

        private CurrencyLanguageOption _LanguageOption;
        
        #endregion
        
        #region Public Properties

        public Int64 IdCurrency
        { get { return _IdCurrency; } }
        public String Symbol
        { get { return _Symbol; } }
        public String Name
        { get { return _LanguageOption.Name; } }
        public Double ConversionIndex
        { get { return _ConversionIndex; } }
        public Boolean IsPattern 
        { get { return _IsPattern; } }
        public String PaymentSystemCode
        { get { return _PaymentSystemCode; } }

        public CurrencyLanguageOption LanguageOption
        { get { return _LanguageOption; } }
        public Dictionary<String, CurrencyLanguageOption> LanguageOptions
        { get { return new Handlers.CurrencyLanguageOptions().Items(_IdCurrency); } }

        #endregion

        #region Public Methods
                
        public Double ToPattern(Double value)
        {
            if (_IsPattern)
                return value;

            return value*_ConversionIndex;
        }
        private Double FromPattern(Double patternValue)
        {
            if (_IsPattern)
                return patternValue;

            return patternValue/_ConversionIndex;
        }

        public Double ToCurrency(Double value, Currency currency)
        {
            return currency.FromPattern(ToPattern(value));
        }

        #endregion
    }
}
