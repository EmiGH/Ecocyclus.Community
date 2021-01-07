using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Geographic
{
    public class Location
    {
        internal Location(String address, Position position)
        {
            _Address = address;
            _Position = position;

        }

        #region Private Fields

        private String _Address;
        private Position _Position;
        
        #endregion

        #region Public Properties

        public String Address
        { get { return _Address; } }
        public Position Position
        { get { return _Position; } }

        #endregion
    }
}
