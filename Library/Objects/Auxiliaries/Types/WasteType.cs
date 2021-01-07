using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Types
{
    public class WasteType
    {
        internal WasteType(Int64 idWasteType, String idlanguage, String name, String description, Int64 idIcon)
        {
            _IdWasteType = idWasteType;
            _languageOption = new WasteTypeLanguageOption(idlanguage, name, description);
            _IdIcon = idIcon;

        }

        #region Private Fields

        private Int64 _IdWasteType;
        private WasteTypeLanguageOption _languageOption;
        private Int64 _IdIcon;

        #endregion

        #region Public Properties

        public Int64 IdWasteType
        { get { return _IdWasteType; } }
        public String Name
        { get { return _languageOption.Name; } }
        public String Description
        { get { return _languageOption.Description; } }
        public Auxiliaries.Files.File Icon
        { get { return new Handlers.Files().Item(_IdIcon); } }

        #endregion

        #region Public Methods

        public Dictionary<String, WasteTypeLanguageOption> LanguageOptions()
        {
            return new Handlers.WasteTypeLanguageOptions().Items(_IdWasteType);
        }

        #endregion
    }
}
