using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Units
{
    public class CurrencyLanguageOption
    {
        internal CurrencyLanguageOption(String idLanguage, String name)
        {
            _IdLanguage = idLanguage;
            _Name = name;
        }

        #region Private Fields

        private String _IdLanguage;
        private String _Name;

        #endregion

        #region Public Properties

        public String IdLanguage
        { get { return _IdLanguage; } }
        public String Name
        { get { return _Name; } }


        #endregion
    }
}
