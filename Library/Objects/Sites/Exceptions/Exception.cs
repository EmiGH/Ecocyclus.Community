using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Exceptions
{
    public abstract class Exception
    {
        internal Exception(Int64 idException, DateTime date)
        {
            _IdException = idException;
            _Date = date;
        }

        #region Private Properties

        private Int64 _IdException;
        private DateTime _Date;

        #endregion

        #region Public Properties

        public Int64 IdException
        { get { return _IdException; } }
        public DateTime Date
        { get { return _Date; } }

        #endregion
    }
}
