using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.EmissionFactors
{
    public class FuelMeterEmissionFactor
    {
        internal FuelMeterEmissionFactor(Int64 idFuelMeterEmissionFactor, Int64 idFuelTypeEmissionFactor, Security.Credential credential)
        {
            _IdFuelMeterEmissionFactor = idFuelMeterEmissionFactor;
            _IdFuelTypeEmissionFactor = idFuelTypeEmissionFactor;
            _Credential = credential;            
        }

        #region Private Fields

        private Int64 _IdFuelMeterEmissionFactor;
        private Int64 _IdFuelTypeEmissionFactor;
        private Security.Credential _Credential;

        #endregion

        #region Public Properties

        public Int64 IdFuelMeterEmissionFactor
        { get { return _IdFuelMeterEmissionFactor; } }
        public Auxiliaries.EmissionFactors.FuelTypeEmissionFactor FuelTypeEmissionFactor
        { get { return new Handlers.FuelTypeEmissionFactors().Item(_IdFuelTypeEmissionFactor, _Credential); } }
        
        #endregion
    }
}
