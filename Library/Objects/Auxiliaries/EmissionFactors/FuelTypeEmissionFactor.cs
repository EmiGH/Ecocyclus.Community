using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.EmissionFactors
{
    public class FuelTypeEmissionFactor : EmissionFactor
    {
        internal FuelTypeEmissionFactor(Int64 idFuelTypeEmissionFactor, Int64 idEmissionFactor, Auxiliaries.Types.FuelType fuelType, Int64 idCountry, Double value, Auxiliaries.Units.Unit unit, String description, Boolean isPropietary, Security.Credential credential)
            :base(idEmissionFactor, idCountry, value, unit, description, credential)
        {
            _IdFuelTypeEmissionFactor = idFuelTypeEmissionFactor;
            _FuelType = fuelType;
            _IsPropietary = isPropietary;
        }

        #region Private Fields

        private Boolean _IsPropietary;
        private Auxiliaries.Types.FuelType _FuelType;
        private Int64 _IdFuelTypeEmissionFactor;

        #endregion

        #region Public Properties

        public Int64 IdFuelTypeEmissionFactor
        { get { return _IdFuelTypeEmissionFactor; } }
        public Auxiliaries.Types.FuelType FuelType
        { get { return _FuelType; } }
        internal Boolean IsPropietary
        { get { return _IsPropietary; } }

        #endregion
    }
}
