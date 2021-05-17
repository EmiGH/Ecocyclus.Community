using System;

namespace CSI.Library.Objects.Sites.Meters.EmissionFactors
{
    public class FleetMeterEmissionFactor
    {
        internal FleetMeterEmissionFactor(Int64 idFleetMeterEmissionFactor, Int64 idFleetTypeEmissionFactor, Security.Credential credential)
        {
            _IdFleetMeterEmissionFactor = idFleetMeterEmissionFactor;
            _IdFleetTypeEmissionFactor = idFleetTypeEmissionFactor;
            _Credential = credential;
        }

        #region Private Fields

        private Int64 _IdFleetMeterEmissionFactor;
        private Int64 _IdFleetTypeEmissionFactor;
        private Security.Credential _Credential;

        #endregion

        #region Public Properties

        public Int64 IdFleetMeterEmissionFactor
        { get { return _IdFleetMeterEmissionFactor; } }
        public Auxiliaries.EmissionFactors.FleetTypeEmissionFactor FleetTypeEmissionFactor
        { get { return new Handlers.FleetTypeEmissionFactors().Item(_IdFleetTypeEmissionFactor, _Credential); } }

        #endregion
    }
}
