using System;
using System.Collections.Generic;

namespace CSI.Library.Objects.Auxiliaries.Types
{
    public class FleetType
    {
        internal FleetType(Int64 idFleetType, String idlanguage, String name, String description, Int64 idIcon)
        {
            _IdFleetType = idFleetType;
            _languageOption = new FleetTypeLanguageOption(idlanguage, name, description);
            _IdIcon = idIcon;

        }

        #region Private Fields

        private Int64 _IdFleetType;
        private FleetTypeLanguageOption _languageOption;
        private Int64 _IdIcon;

        #endregion

        #region Public Properties

        public Int64 IdFleetType
        { get { return _IdFleetType; } }
        public String Name
        { get { return _languageOption.Name; } }
        public String Description
        { get { return _languageOption.Description; } }
        public Auxiliaries.Files.File Icon
        { get { return new Handlers.Files().Item(_IdIcon); } }

        #endregion

        #region Public Methods

        public Dictionary<String, FleetTypeLanguageOption> LanguageOptions()
        {
            return new Handlers.FleetTypeLanguageOptions().Items(_IdFleetType);
        }

        #endregion
    }
}
