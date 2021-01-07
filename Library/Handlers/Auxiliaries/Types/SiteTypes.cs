using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class SiteTypes
    {
        internal SiteTypes() { }

        #region Read Functions

        internal Library.Objects.Auxiliaries.Types.SiteType Item(Int64 idSiteType, Security.Credential credential)
        {
            Storage.SiteTypes _dbSiteTypes = new Storage.SiteTypes();
            Library.Objects.Auxiliaries.Types.SiteType _siteType = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSiteTypes.ReadById(idSiteType, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_siteType != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _siteType = new Library.Objects.Auxiliaries.Types.SiteType(idSiteType, Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _siteType = new Library.Objects.Auxiliaries.Types.SiteType(idSiteType, Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

            }
            return _siteType;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.Types.SiteType> Items(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Types.SiteType> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Types.SiteType>();
            Storage.SiteTypes _dbSiteTypes = new Storage.SiteTypes();
            Library.Objects.Auxiliaries.Types.SiteType _siteType = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSiteTypes.ReadAll(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdSiteType"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdSiteType"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _siteType = new Library.Objects.Auxiliaries.Types.SiteType(Convert.ToInt64(_dbRecord["IdSiteType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                    _oItems.Add(_siteType.IdSiteType, _siteType);
                }
                _insert = true;
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Types.SiteType Add(String name, String description, Double ef, Int64 idIcon, Security.Credential credential)
        {
            Storage.SiteTypes _dbSiteTypes = new Storage.SiteTypes();
            String _defaultLanguage = new Languages().ItemDefault().IdLanguage;

            try
            {
                Int64 _idSiteType = _dbSiteTypes.Create(_defaultLanguage, name, description, ef, idIcon);
                return Item(_idSiteType, credential);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }

        }
        internal void Remove(Int64 idSiteType)
        {
            Storage.SiteTypes _dbSiteTypes = new Storage.SiteTypes();

            try
            {
                _dbSiteTypes.Delete(idSiteType);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idSiteType, Security.Credential credential, String name, String description, Double ef, Int64 idIcon)
        {
            Storage.SiteTypes _dbSiteTypes = new Storage.SiteTypes();
            String _defaultLanguage = new Languages().ItemDefault().IdLanguage;

            try
            {
                _dbSiteTypes.Update(idSiteType, _defaultLanguage, name, description, ef, idIcon);

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
