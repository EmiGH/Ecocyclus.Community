using System;

namespace CSI.Library.Objects.Auxiliaries.EmissionFactors
{
    public class FleetTypeEmissionFactor : EmissionFactor
    {
        internal FleetTypeEmissionFactor(Int64 idFleetTypeEmissionFactor, Int64 idEmissionFactor, Auxiliaries.Types.FleetType fleetType, Int64 idCountry, Double value, Auxiliaries.Units.Unit unit, String description, Boolean isPropietary, Security.Credential credential)
            : base(idEmissionFactor, idCountry, value, unit, description, credential)
        {
            _IdFleetTypeEmissionFactor = idFleetTypeEmissionFactor;
            _FleetType = fleetType;
            _IsPropietary = isPropietary;
        }

        #region Private Fields

        private Boolean _IsPropietary;
        private Auxiliaries.Types.FleetType _FleetType;
        private Int64 _IdFleetTypeEmissionFactor;

        #endregion

        #region Public Properties

        public Int64 IdFleetTypeEmissionFactor
        { get { return _IdFleetTypeEmissionFactor; } }
        public Auxiliaries.Types.FleetType FleetType
        { get { return _FleetType; } }
        internal Boolean IsPropietary
        { get { return _IsPropietary; } }

        #endregion
    }
}
