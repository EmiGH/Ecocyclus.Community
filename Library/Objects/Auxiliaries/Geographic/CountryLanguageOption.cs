using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Geographic
{
    public class CountryLanguageOption
    {
        internal CountryLanguageOption(String idLanguage, String name)
        {
            _IdLanguage = idLanguage;
            _Name = name;
        }
        
        #region Private Fields
                
        private String _IdLanguage;
        private String _Name;

        #endregion

        #region Public Properties

        public Globalization.Language Language
        { get { return new Handlers.Languages().Item(_IdLanguage); } }
        public String Name
        { get { return _Name; } }
        
        #endregion

        #region Public Methods

        
        #endregion
    }
}
