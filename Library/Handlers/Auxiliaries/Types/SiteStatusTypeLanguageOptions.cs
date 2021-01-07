using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class SiteStatusTypeLanguageOptions
    {
        internal SiteStatusTypeLanguageOptions() { }

        #region Read Functions

        internal Library.Objects.Auxiliaries.Types.StatusTypeLanguageOption Item(Int64 idSiteStatusType, String idLanguage)
        {
            Storage.SiteStatusTypeLanguageOptions _dbSiteStatusTypeLanguageOptions = new Storage.SiteStatusTypeLanguageOptions();
            Library.Objects.Auxiliaries.Types.StatusTypeLanguageOption _siteStatusTypeLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSiteStatusTypeLanguageOptions.ReadById(idSiteStatusType, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _siteStatusTypeLanguageOption = new Library.Objects.Auxiliaries.Types.StatusTypeLanguageOption(idLanguage, Convert.ToString(_dbRecord["Name"]));
            }
            return _siteStatusTypeLanguageOption;
        }
        internal Dictionary<String, Library.Objects.Auxiliaries.Types.StatusTypeLanguageOption> Items(Int64 idSiteStatusType)
        {
            Dictionary<String, Library.Objects.Auxiliaries.Types.StatusTypeLanguageOption> _oItems = new Dictionary<String, Library.Objects.Auxiliaries.Types.StatusTypeLanguageOption>();
            Storage.SiteStatusTypeLanguageOptions _dbSiteStatusTypeLanguageOptions = new Storage.SiteStatusTypeLanguageOptions();
            Library.Objects.Auxiliaries.Types.StatusTypeLanguageOption _siteStatusTypeLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSiteStatusTypeLanguageOptions.ReadAll(idSiteStatusType);

            String _idLanguage;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _idLanguage = Convert.ToString(_dbRecord["IdLanguage"]);
                _siteStatusTypeLanguageOption = new Library.Objects.Auxiliaries.Types.StatusTypeLanguageOption(_idLanguage, Convert.ToString(_dbRecord["Name"]));

                _oItems.Add(_idLanguage, _siteStatusTypeLanguageOption);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Types.StatusTypeLanguageOption Add(Int64 idSiteStatusType, String idLanguage, String name)
        {
            Storage.SiteStatusTypeLanguageOptions _dbSiteStatusTypeLanguageOptions = new Storage.SiteStatusTypeLanguageOptions();

            try
            {
                _dbSiteStatusTypeLanguageOptions.Create(idSiteStatusType, idLanguage, name);
                return new Objects.Auxiliaries.Types.StatusTypeLanguageOption(idLanguage, name);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idSiteStatusType, String idLanguage)
        {
            Storage.SiteStatusTypeLanguageOptions _dbSiteStatusTypeLanguageOptions = new Storage.SiteStatusTypeLanguageOptions();

            try
            {
                _dbSiteStatusTypeLanguageOptions.Delete(idSiteStatusType, idLanguage);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idSiteStatusType, String idLanguage, String name)
        {
            Storage.SiteStatusTypeLanguageOptions _dbSiteStatusTypeLanguageOptions = new Storage.SiteStatusTypeLanguageOptions();

            try
            {
                _dbSiteStatusTypeLanguageOptions.Update(idSiteStatusType, idLanguage, name);
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
