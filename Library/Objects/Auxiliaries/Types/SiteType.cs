using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Types
{
    public class SiteType
    {
        internal SiteType(Int64 idSiteType, String idlanguage, String name, String description)
        {
            _IdSiteType = idSiteType;
            _languageOption = new SiteTypeLanguageOption(idlanguage, name, description);

        }

        #region Private Fields

        private Int64 _IdSiteType;
        private SiteTypeLanguageOption _languageOption;

        #endregion

        #region Public Properties

        public Int64 IdSiteType
        { get { return _IdSiteType; } }
        public String Name
        { get { return _languageOption.Name; } }
        public String Description
        { get { return _languageOption.Description; } }
        
        #endregion

        #region Public Methods

        public Dictionary<String, SiteTypeLanguageOption> LanguageOptions()
        {
            return new Handlers.SiteTypeLanguageOptions().Items(_IdSiteType);
        }

        #endregion
    }
}
