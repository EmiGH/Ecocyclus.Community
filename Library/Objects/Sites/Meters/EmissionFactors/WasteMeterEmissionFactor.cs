using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.EmissionFactors
{
    public class WasteMeterEmissionFactor
    {
        internal WasteMeterEmissionFactor(Int64 idWasteMeterEmissionFactor, Int64 idWasteTypeEmissionFactor, Security.Credential credential)
        {
            _IdWasteMeterEmissionFactor = idWasteMeterEmissionFactor;
            _IdWasteTypeEmissionFactor = idWasteTypeEmissionFactor;
            _Credential = credential;
        }

        #region Private Fields

        private Int64 _IdWasteMeterEmissionFactor;
        private Int64 _IdWasteTypeEmissionFactor;
        private Security.Credential _Credential;

        #endregion

        #region Public Properties

        public Int64 IdWasteMeterEmissionFactor
        { get { return _IdWasteMeterEmissionFactor; } }
        public Auxiliaries.EmissionFactors.WasteTypeEmissionFactor WasteTypeEmissionFactor
        { get { return new Handlers.WasteTypeEmissionFactors().Item(_IdWasteTypeEmissionFactor, _Credential); } }
        
        #endregion
    }
}
