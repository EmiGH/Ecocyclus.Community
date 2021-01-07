using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Types
{
    public class FuelType
    {
        internal FuelType(Int64 idFuelType, String idlanguage, String name, String description, Int64 idIcon)
        {
            _IdFuelType = idFuelType;
            _languageOption = new FuelTypeLanguageOption(idlanguage, name, description);
            _IdIcon = idIcon;

        }

        #region Private Fields

        private Int64 _IdFuelType;
        private FuelTypeLanguageOption _languageOption;
        private Int64 _IdIcon;

        #endregion

        #region Public Properties

        public Int64 IdFuelType
        { get { return _IdFuelType; } }
        public String Name
        { get { return _languageOption.Name; } }
        public String Description
        { get { return _languageOption.Description; } }
        public Auxiliaries.Files.File Icon
        { get { return new Handlers.Files().Item(_IdIcon); } }

        #endregion

        #region Public Methods

        public Dictionary<String, FuelTypeLanguageOption> LanguageOptions()
        {
            return new Handlers.FuelTypeLanguageOptions().Items(_IdFuelType);
        }

        #endregion
    }
}
