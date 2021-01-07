using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Globalization
{
    public class Language
    {
        #region Private Fields

        private String _IdLanguage;
        private String _Name;
        private Boolean _IsDefault;
        private Boolean _Enable;

        #endregion
        
        #region public Properties

        public String IdLanguage
        {
            get { return _IdLanguage; }
        }
        public String Name
        {
            get { return _Name; }
        }
        public Boolean IsDefault
        {
            get { return _IsDefault; }
        }
        public Boolean Enable
        {
            get { return _Enable; }
        }

        #endregion

        internal Language(String idLanguage, String name, Boolean isDefault, Boolean enable)
        {
            _IdLanguage = idLanguage;
            _Name = name;
            _IsDefault = isDefault;
            _Enable = enable;

        }

        #region Static Public Methods

        #endregion
    }
}
