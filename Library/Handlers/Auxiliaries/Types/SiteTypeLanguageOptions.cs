using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class SiteTypeLanguageOptions
    {
        internal SiteTypeLanguageOptions() { }

        #region Read Functions

        internal Library.Objects.Auxiliaries.Types.SiteTypeLanguageOption Item(Int64 idSiteType, String idLanguage)
        {
            Storage.SiteTypeLanguageOptions _dbSiteTypeLanguageOptions = new Storage.SiteTypeLanguageOptions();
            Library.Objects.Auxiliaries.Types.SiteTypeLanguageOption _siteSiteTypeLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSiteTypeLanguageOptions.ReadById(idSiteType, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _siteSiteTypeLanguageOption = new Library.Objects.Auxiliaries.Types.SiteTypeLanguageOption(idLanguage, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
            }
            return _siteSiteTypeLanguageOption;
        }
        internal Dictionary<String, Library.Objects.Auxiliaries.Types.SiteTypeLanguageOption> Items(Int64 idSiteType)
        {
            Dictionary<String, Library.Objects.Auxiliaries.Types.SiteTypeLanguageOption> _oItems = new Dictionary<String, Library.Objects.Auxiliaries.Types.SiteTypeLanguageOption>();
            Storage.SiteTypeLanguageOptions _dbSiteTypeLanguageOptions = new Storage.SiteTypeLanguageOptions();
            Library.Objects.Auxiliaries.Types.SiteTypeLanguageOption _siteSiteTypeLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSiteTypeLanguageOptions.ReadAll(idSiteType);

            String _idLanguage;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _idLanguage = Convert.ToString(_dbRecord["IdLanguage"]);
                _siteSiteTypeLanguageOption = new Library.Objects.Auxiliaries.Types.SiteTypeLanguageOption(_idLanguage, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

                _oItems.Add(_idLanguage, _siteSiteTypeLanguageOption);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Types.SiteTypeLanguageOption Add(Int64 idSiteType, String idLanguage, String name, String description)
        {
            Storage.SiteTypeLanguageOptions _dbSiteTypeLanguageOptions = new Storage.SiteTypeLanguageOptions();

            try
            {
                _dbSiteTypeLanguageOptions.Create(idSiteType, idLanguage, name, description);
                return new Objects.Auxiliaries.Types.SiteTypeLanguageOption(idLanguage, name, description);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idSiteType, String idLanguage)
        {
            Storage.SiteTypeLanguageOptions _dbSiteTypeLanguageOptions = new Storage.SiteTypeLanguageOptions();

            try
            {
                _dbSiteTypeLanguageOptions.Delete(idSiteType, idLanguage);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idSiteType, String idLanguage, String name, String description)
        {
            Storage.SiteTypeLanguageOptions _dbSiteTypeLanguageOptions = new Storage.SiteTypeLanguageOptions();

            try
            {
                _dbSiteTypeLanguageOptions.Update(idSiteType, idLanguage, name, description);
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
