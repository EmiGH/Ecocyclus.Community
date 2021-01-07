using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.EmissionFactors
{
    public class WasteTypeEmissionFactor : EmissionFactor
    {
        internal WasteTypeEmissionFactor(Int64 idWasteTypeEmissionFactor, Int64 idEmissionFactor, Auxiliaries.Types.WasteType wasteType, Int64 idCountry, Double value, Auxiliaries.Units.Unit unit, String description, Boolean isPropietary, Security.Credential credential)
            : base(idEmissionFactor, idCountry, value, unit, description, credential)
        {
            _IdWasteTypeEmissionFactor = idWasteTypeEmissionFactor;
            _WasteType = wasteType;
            _IsPropietary = isPropietary;
        }

        #region Private Fields

        private Boolean _IsPropietary;
        private Auxiliaries.Types.WasteType _WasteType;
        private Int64 _IdWasteTypeEmissionFactor;

        #endregion

        #region Public Properties

        public Int64 IdWasteTypeEmissionFactor
        { get { return _IdWasteTypeEmissionFactor; } }
        public Auxiliaries.Types.WasteType WasteType
        { get { return _WasteType; } }
        internal Boolean IsPropietary
        { get { return _IsPropietary; } }

        #endregion

    }
}
