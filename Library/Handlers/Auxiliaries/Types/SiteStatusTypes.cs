using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class StatusTypes
    {
        internal StatusTypes() { }

        #region Read Functions

        internal Library.Objects.Auxiliaries.Types.StatusType Item(Int64 idStatusType, Security.Credential credential)
        {
            Storage.SiteStatusTypes _dbStatusTypes = new Storage.SiteStatusTypes();
            Library.Objects.Auxiliaries.Types.StatusType _statusType = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbStatusTypes.ReadById(idStatusType, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_statusType != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _statusType = new Library.Objects.Auxiliaries.Types.StatusType(idStatusType, Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _statusType = new Library.Objects.Auxiliaries.Types.StatusType(idStatusType, Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));

            }
            return _statusType;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.Types.StatusType> Items(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Types.StatusType> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Types.StatusType>();
            Storage.SiteStatusTypes _dbStatusTypes = new Storage.SiteStatusTypes();
            Library.Objects.Auxiliaries.Types.StatusType _statusType = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbStatusTypes.ReadAll(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdStatusType"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdStatusType"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _statusType = new Library.Objects.Auxiliaries.Types.StatusType(Convert.ToInt64(_dbRecord["IdStatusType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
                    _oItems.Add(_statusType.IdStatusType, _statusType);
                }
                _insert = true;
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Types.StatusType Add(String name, String description, Double ef, Int64 idIcon, Security.Credential credential)
        {
            Storage.SiteStatusTypes _dbStatusTypes = new Storage.SiteStatusTypes();
            String _defaultLanguage = new Languages().ItemDefault().IdLanguage;

            try
            {
                Int64 _idStatusType = _dbStatusTypes.Create(_defaultLanguage, name);
                return Item(_idStatusType, credential);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }

        }
        internal void Remove(Int64 idStatusType)
        {
            Storage.SiteStatusTypes _dbStatusTypes = new Storage.SiteStatusTypes();

            try
            {
                _dbStatusTypes.Delete(idStatusType);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idStatusType, Security.Credential credential, String name, String description, Double ef, Int64 idIcon)
        {
            Storage.SiteStatusTypes _dbStatusTypes = new Storage.SiteStatusTypes();
            String _defaultLanguage = new Languages().ItemDefault().IdLanguage;

            try
            {
                _dbStatusTypes.Update(idStatusType, _defaultLanguage, name, description, ef, idIcon);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }

        #endregion
    }
}
