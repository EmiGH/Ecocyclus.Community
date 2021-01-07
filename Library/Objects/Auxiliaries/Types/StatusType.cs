using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Types
{
    public class StatusType
    {
        internal StatusType(Int64 idStatusType, String idlanguage, String name)
        {
            _IdStatusType = idStatusType;
            _languageOption = new StatusTypeLanguageOption(idlanguage, name);

        }

        #region Private Fields

        private Int64 _IdStatusType;
        private StatusTypeLanguageOption _languageOption;

        #endregion

        #region Public Properties

        public Int64 IdStatusType
        { get { return _IdStatusType; } }
        public String Name
        { get { return _languageOption.Name; } }
        
        #endregion

        #region Public Methods

        public Dictionary<String, StatusTypeLanguageOption> LanguageOptions()
        {
            return new Handlers.SiteStatusTypeLanguageOptions().Items(_IdStatusType);
        }

        #endregion

    }
}
