using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class SiteExceptionTargets
    {
        internal SiteExceptionTargets()
        { }

        #region Read Functions

        internal Library.Objects.Sites.Exceptions.ExceptionTarget Item(Int64 idException)
        {
            Storage.SiteExceptionTarget _dbExceptions = new Storage.SiteExceptionTarget();
            Library.Objects.Sites.Exceptions.ExceptionTarget _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadById(idException);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionTarget(Convert.ToInt64(_dbRecord["IdSiteExceptionTarget"]), Convert.ToDateTime(_dbRecord["Date"]));
            }
            return _exception;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionTarget> Items(Int64 idSite)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionTarget> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionTarget>();
            Storage.SiteExceptionTarget _dbExceptions = new Storage.SiteExceptionTarget();
            Library.Objects.Sites.Exceptions.ExceptionTarget _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAll(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionTarget(Convert.ToInt64(_dbRecord["IdSiteExceptionTarget"]), Convert.ToDateTime(_dbRecord["Date"]));
                _oItems.Add(_exception.IdException, _exception);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionTarget> Items(Int64 idSite, DateTime from, DateTime to)
        {
            Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionTarget> _oItems = new Dictionary<Int64, Library.Objects.Sites.Exceptions.ExceptionTarget>();
            Storage.SiteExceptionTarget _dbExceptions = new Storage.SiteExceptionTarget();
            Library.Objects.Sites.Exceptions.ExceptionTarget _exception = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExceptions.ReadAll(idSite, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _exception = new Library.Objects.Sites.Exceptions.ExceptionTarget(Convert.ToInt64(_dbRecord["IdSiteExceptionTarget"]), Convert.ToDateTime(_dbRecord["Date"]));
                _oItems.Add(_exception.IdException, _exception);
            }
            return _oItems;
        }
        
        #endregion

        #region Write Functions

        internal Library.Objects.Sites.Exceptions.ExceptionTarget Add(Int64 idSite, Int16 protocol)
        {
            Storage.SiteExceptionTarget _dbExceptions = new Storage.SiteExceptionTarget();

            Int64 _idException = _dbExceptions.Create(idSite, protocol);
            return Item(_idException);

        }
        internal void Remove(Int64 idException)
        {
            Storage.SiteExceptionTarget _dbExceptions = new Storage.SiteExceptionTarget();

            try
            {
                _dbExceptions.Delete(idException);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }

        #endregion
    }
}
