using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Types
{
    public class WasteTypeLanguageOption
    {
        internal WasteTypeLanguageOption(String idLanguage, String name, String description)
        {
            _IdLanguage = idLanguage;
            _Name = name;
            _Description = description;
        }

        #region Private Fields

        private String _IdLanguage;
        private String _Name;
        private String _Description;

        #endregion

        #region Public Properties

        public Globalization.Language Language
        { get { return new Handlers.Languages().Item(_IdLanguage); } }
        public String Name
        { get { return _Name; } }
        public String Description
        { get { return _Description; } }

        #endregion

        #region Public Methods


        #endregion
    }
}
