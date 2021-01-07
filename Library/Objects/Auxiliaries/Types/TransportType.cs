using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Types
{
    public class TransportType
    {
        internal TransportType(Int64 idTransportType, String idlanguage, String name, String description, Int64 idIcon)
        {
            _IdTransportType = idTransportType;
            _languageOption = new TransportTypeLanguageOption(idlanguage, name, description);
            _IdIcon = idIcon;
            
        }

        #region Private Fields

        private Int64 _IdTransportType;
        private TransportTypeLanguageOption _languageOption;
        private Int64 _IdIcon;
                
        #endregion

        #region Public Properties

        public Int64 IdTransportType
        { get { return _IdTransportType; } }
        public String Name
        { get { return _languageOption.Name; } }
        public String Description
        { get { return _languageOption.Description; } }
        public Auxiliaries.Files.File Icon
        { get { return new Handlers.Files().Item(_IdIcon); } }
        
        #endregion

        #region Public Methods

        public Dictionary<String, TransportTypeLanguageOption> LanguageOptions()
        {
            return new Handlers.TransportTypeLanguageOptions().Items(_IdTransportType);
        }

        #endregion
    }
}
