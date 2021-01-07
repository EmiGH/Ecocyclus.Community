using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.EmissionFactors
{
    public class EmissionFactorLanguageOption
    {
        internal EmissionFactorLanguageOption(String idLanguage, String description)
        {
            _IdLanguage = idLanguage;
            _Description = description;
        }

        #region Private Fields

        private String _IdLanguage;
        private String _Description;

        #endregion

        #region Public Properties

        public String IdLanguage
        { get { return _IdLanguage; } }
        public String Description
        { get { return _Description; } }


        #endregion
    }
}
