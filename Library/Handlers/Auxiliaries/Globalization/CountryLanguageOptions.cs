using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class CountryLanguageOptions
    {
        internal CountryLanguageOptions() { }
        
        #region Read Functions

        internal Library.Objects.Auxiliaries.Geographic.CountryLanguageOption Item(Int64 idCountry, String idLanguage)
        {
            Storage.CountryLanguageOptions _dbCountryLanguageOptions = new Storage.CountryLanguageOptions();
            Library.Objects.Auxiliaries.Geographic.CountryLanguageOption _countryLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCountryLanguageOptions.ReadById(idCountry, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _countryLanguageOption = new Library.Objects.Auxiliaries.Geographic.CountryLanguageOption(idLanguage, Convert.ToString(_dbRecord["Name"]));
            }
            return _countryLanguageOption;
        }
        internal Dictionary<String, Library.Objects.Auxiliaries.Geographic.CountryLanguageOption> Items(Int64 idCountry)
        {
            Dictionary<String, Library.Objects.Auxiliaries.Geographic.CountryLanguageOption> _oItems = new Dictionary<String, Library.Objects.Auxiliaries.Geographic.CountryLanguageOption>();
            Storage.CountryLanguageOptions _dbCountryLanguageOptions = new Storage.CountryLanguageOptions();
            Library.Objects.Auxiliaries.Geographic.CountryLanguageOption _countryLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCountryLanguageOptions.ReadAll(idCountry);

            String _idLanguage;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _idLanguage = Convert.ToString(_dbRecord["IdLanguage"]);
                _countryLanguageOption = new Library.Objects.Auxiliaries.Geographic.CountryLanguageOption(_idLanguage, Convert.ToString(_dbRecord["Name"]));

                _oItems.Add(_idLanguage, _countryLanguageOption);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Geographic.CountryLanguageOption Add(Int64 idCountry, String idLanguage, String name)
        {
            Storage.CountryLanguageOptions _dbCountryLanguageOptions = new Storage.CountryLanguageOptions();

            try
            {
                _dbCountryLanguageOptions.Create(idCountry, idLanguage, name);
                return new Objects.Auxiliaries.Geographic.CountryLanguageOption(idLanguage, name);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idCountry, String idLanguage)
        {
            Storage.CountryLanguageOptions _dbCountryLanguageOptions = new Storage.CountryLanguageOptions();

            try
            {
                _dbCountryLanguageOptions.Delete(idCountry, idLanguage);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idCountry, String idLanguage, String name)
        {
            Storage.CountryLanguageOptions _dbCountryLanguageOptions = new Storage.CountryLanguageOptions();

            try
            {
                _dbCountryLanguageOptions.Update(idCountry, idLanguage, name);
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
