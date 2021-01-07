using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters
{
    public class WasteMeterLanguageOption
    {
        internal WasteMeterLanguageOption(String idLanguage, String description)
        {
            _IdLanguage = idLanguage;
            _Description = description;
        }

        #region Private Fields

        private String _IdLanguage;
        private String _Description;

        #endregion

        #region Public Properties

        public Auxiliaries.Globalization.Language Language
        { get { return new Handlers.Languages().Item(_IdLanguage); } }
        public String Description
        { get { return _Description; } }

        #endregion

        #region Public Methods


        #endregion
    }
}
