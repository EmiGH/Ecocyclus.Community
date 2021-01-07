using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.EmissionFactors
{
    public class WaterEmissionFactor : EmissionFactor
    {
        internal WaterEmissionFactor(Int64 idEmissionFactor, Int64 idCountry, Double value, Auxiliaries.Units.Unit unit, String description, Boolean isPropietary, Security.Credential credential)
            : base(idEmissionFactor, idCountry, value, unit, description, credential)
        {
            _IsPropietary = isPropietary;
        }

        #region Private Fields

        private Boolean _IsPropietary;

        #endregion

        #region Public Properties

        internal Boolean IsPropietary
        { get { return _IsPropietary; } }

        #endregion
    }
}
