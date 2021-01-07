using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class CurrencyLanguageOptions
    {
        internal CurrencyLanguageOptions() { }


        #region Read Functions

        internal Library.Objects.Auxiliaries.Units.CurrencyLanguageOption Item(Int64 idCurrency, String idLanguage)
        {
            Storage.CurrencyLanguageOptions _dbCurrencyLanguageOptions = new Storage.CurrencyLanguageOptions();
            Library.Objects.Auxiliaries.Units.CurrencyLanguageOption _electricityCurrencyLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCurrencyLanguageOptions.ReadById(idCurrency, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _electricityCurrencyLanguageOption = new Library.Objects.Auxiliaries.Units.CurrencyLanguageOption(idLanguage, Convert.ToString(_dbRecord["Name"]));
            }
            return _electricityCurrencyLanguageOption;
        }
        internal Dictionary<String, Library.Objects.Auxiliaries.Units.CurrencyLanguageOption> Items(Int64 idCurrency)
        {
            Dictionary<String, Library.Objects.Auxiliaries.Units.CurrencyLanguageOption> _oItems = new Dictionary<String, Library.Objects.Auxiliaries.Units.CurrencyLanguageOption>();
            Storage.CurrencyLanguageOptions _dbCurrencyLanguageOptions = new Storage.CurrencyLanguageOptions();
            Library.Objects.Auxiliaries.Units.CurrencyLanguageOption _electricityCurrencyLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCurrencyLanguageOptions.ReadAll(idCurrency);

            String _idLanguage;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _idLanguage = Convert.ToString(_dbRecord["IdLanguage"]);
                _electricityCurrencyLanguageOption = new Library.Objects.Auxiliaries.Units.CurrencyLanguageOption(_idLanguage, Convert.ToString(_dbRecord["Name"]));

                _oItems.Add(_idLanguage, _electricityCurrencyLanguageOption);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Units.CurrencyLanguageOption Add(Int64 idCurrency, String idLanguage, String name)
        {
            Storage.CurrencyLanguageOptions _dbCurrencyLanguageOptions = new Storage.CurrencyLanguageOptions();

            try
            {
                _dbCurrencyLanguageOptions.Create(idCurrency, idLanguage, name);
                return new Library.Objects.Auxiliaries.Units.CurrencyLanguageOption(idLanguage, name);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idCurrency, String idLanguage)
        {
            Storage.CurrencyLanguageOptions _dbCurrencyLanguageOptions = new Storage.CurrencyLanguageOptions();

            try
            {
                _dbCurrencyLanguageOptions.Delete(idCurrency, idLanguage);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idCurrency, String idLanguage, String name)
        {
            Storage.CurrencyLanguageOptions _dbCurrencyLanguageOptions = new Storage.CurrencyLanguageOptions();

            try
            {
                _dbCurrencyLanguageOptions.Update(idCurrency, idLanguage, name);
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
