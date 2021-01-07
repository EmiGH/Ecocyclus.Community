using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Units
{
    public class Magnitude
    {
        internal Magnitude(Int64 idMagnitude, Security.Credential credential)
        {
            _IdMagnitude = idMagnitude;
            _Credential = credential;
        }

        #region Private Fields

        private Int64 _IdMagnitude;
        private Security.Credential _Credential;

        #endregion

        #region Public Properties

        public Int64 IdMagnitude
        { get { return _IdMagnitude; } }

        #endregion

        #region Methods

        public Dictionary<Int64, Unit> GetUnits()
        { return new Handlers.Units().ItemsByMagnitude(_IdMagnitude, _Credential); }

        #endregion
    }
}
