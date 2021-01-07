using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class SiteLanguageOptions
    {
        internal SiteLanguageOptions() { }
        
        #region Read Functions

        internal Library.Objects.Sites.SiteLanguageOption Item(Int64 idSite, String idLanguage)
        {
            Storage.SiteLanguageOptions _dbSiteLanguageOptions = new Storage.SiteLanguageOptions();
            Library.Objects.Sites.SiteLanguageOption _siteLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSiteLanguageOptions.ReadById(idSite, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _siteLanguageOption = new Library.Objects.Sites.SiteLanguageOption(idLanguage, Convert.ToString(_dbRecord["Description"]));
            }
            return _siteLanguageOption;
        }
        internal Dictionary<String, Library.Objects.Sites.SiteLanguageOption> Items(Int64 idSite)
        {
            Dictionary<String, Library.Objects.Sites.SiteLanguageOption> _oItems = new Dictionary<String, Library.Objects.Sites.SiteLanguageOption>();
            Storage.SiteLanguageOptions _dbSiteLanguageOptions = new Storage.SiteLanguageOptions();
            Library.Objects.Sites.SiteLanguageOption _siteLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSiteLanguageOptions.ReadAll(idSite);

            String _idLanguage;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _idLanguage = Convert.ToString(_dbRecord["IdLanguage"]);
                _siteLanguageOption = new Library.Objects.Sites.SiteLanguageOption(_idLanguage, Convert.ToString(_dbRecord["Description"]));

                _oItems.Add(_idLanguage, _siteLanguageOption);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Sites.SiteLanguageOption Add(Int64 idSite, String idLanguage, String description)
        {
            Storage.SiteLanguageOptions _dbSiteLanguageOptions = new Storage.SiteLanguageOptions();

            try
            {
                _dbSiteLanguageOptions.Create(idSite, idLanguage, description);
                return new Library.Objects.Sites.SiteLanguageOption(idLanguage, description);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }            
        }
        internal void Remove(Int64 idSite, String idLanguage)
        {
            Storage.SiteLanguageOptions _dbSiteLanguageOptions = new Storage.SiteLanguageOptions();

            try
            {
                _dbSiteLanguageOptions.Delete(idSite, idLanguage);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idSite, String idLanguage, String description)
        {
            Storage.SiteLanguageOptions _dbSiteLanguageOptions = new Storage.SiteLanguageOptions();

            try
            {
                _dbSiteLanguageOptions.Update(idSite, idLanguage, description);
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
