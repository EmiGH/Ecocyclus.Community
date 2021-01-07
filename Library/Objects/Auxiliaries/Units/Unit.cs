using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Units
{
    public class Unit
    {
        internal Unit(Int64 idUnit, Int64 idMagnitude, String name, String symbol, Double numerator, Double denominator, Double exponent, Double constant, Boolean isPattern, Security.Credential credential)
        {
            _Credential = credential;

            _IdUnit = idUnit;
            _IdMagnitude = idMagnitude;
            _Symbol = symbol;
            _Numerator = numerator;
            _Denominator = denominator;
            _Exponent = exponent;
            _Constant = constant;
            _IsPattern = isPattern;

            _LanguageOption = new UnitLanguageOption(credential.CurrentLanguage.IdLanguage, name);
        }

        #region Private Fields

        private Security.Credential _Credential;

        private Int64 _IdUnit;
        private Int64 _IdMagnitude;
        private String _Symbol;
        private Double _Numerator;
        private Double _Denominator;
        private Double _Exponent;
        private Double _Constant;
        private Boolean _IsPattern;

        private UnitLanguageOption _LanguageOption;
        
        #endregion
        
        #region Public Properties

        public Int64 IdUnit
        { get { return _IdUnit; } }
        public Magnitude Magnitude
        { get { return new Magnitude(_IdMagnitude,_Credential); } }
        public String Symbol
        { get { return _Symbol; } }
        public String Name
        { get { return _LanguageOption.Name; } }
        public Double Numerator
        { get { return _Numerator; } }
        public Double Denominator
        { get { return _Denominator; } }
        public Double Exponent
        { get { return _Exponent; } }
        public Double Constant
        { get { return _Constant; } }
        public Boolean IsPattern 
        { get { return _IsPattern; } }

        public UnitLanguageOption LanguageOption
        { get { return _LanguageOption; } }
        public Dictionary<String, UnitLanguageOption> LanguageOptions
        { get { return new Handlers.UnitLanguageOptions().Items(_IdUnit); } }

        #endregion

        #region Public Methods
                
        public Double ToPattern(Double value)
        {
            if (_IsPattern)
                return value;

            return Math.Pow(value * _Numerator / _Denominator, _Exponent) + _Constant;
        }
        private Double FromPattern(Double patternValue)
        {
            if (_IsPattern)
                return patternValue;

            return Common.Utilities.Root(patternValue - _Constant, _Exponent) * _Denominator / _Numerator;
        }

        public Double ToUnit(Double value, Unit unit)
        {
            return unit.FromPattern(ToPattern(value));
        }

        #endregion
    }
}

