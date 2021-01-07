using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;


namespace CSI.Library.Handlers
{
    internal class UnitLanguageOptions
    {
        internal UnitLanguageOptions() { }


        #region Read Functions

        internal Library.Objects.Auxiliaries.Units.UnitLanguageOption Item(Int64 idUnit, String idLanguage)
        {
            Storage.UnitLanguageOptions _dbUnitLanguageOptions = new Storage.UnitLanguageOptions();
            Library.Objects.Auxiliaries.Units.UnitLanguageOption _electricityUnitLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUnitLanguageOptions.ReadById(idUnit, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _electricityUnitLanguageOption = new Library.Objects.Auxiliaries.Units.UnitLanguageOption(idLanguage, Convert.ToString(_dbRecord["Name"]));
            }
            return _electricityUnitLanguageOption;
        }
        internal Dictionary<String, Library.Objects.Auxiliaries.Units.UnitLanguageOption> Items(Int64 idUnit)
        {
            Dictionary<String, Library.Objects.Auxiliaries.Units.UnitLanguageOption> _oItems = new Dictionary<String, Library.Objects.Auxiliaries.Units.UnitLanguageOption>();
            Storage.UnitLanguageOptions _dbUnitLanguageOptions = new Storage.UnitLanguageOptions();
            Library.Objects.Auxiliaries.Units.UnitLanguageOption _electricityUnitLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbUnitLanguageOptions.ReadAll(idUnit);

            String _idLanguage;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _idLanguage = Convert.ToString(_dbRecord["IdLanguage"]);
                _electricityUnitLanguageOption = new Library.Objects.Auxiliaries.Units.UnitLanguageOption(_idLanguage, Convert.ToString(_dbRecord["Name"]));

                _oItems.Add(_idLanguage, _electricityUnitLanguageOption);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Units.UnitLanguageOption Add(Int64 idUnit, String idLanguage, String name)
        {
            Storage.UnitLanguageOptions _dbUnitLanguageOptions = new Storage.UnitLanguageOptions();

            try
            {
                _dbUnitLanguageOptions.Create(idUnit, idLanguage, name);
                return new Library.Objects.Auxiliaries.Units.UnitLanguageOption(idLanguage, name);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idUnit, String idLanguage)
        {
            Storage.UnitLanguageOptions _dbUnitLanguageOptions = new Storage.UnitLanguageOptions();

            try
            {
                _dbUnitLanguageOptions.Delete(idUnit, idLanguage);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idUnit, String idLanguage, String name)
        {
            Storage.UnitLanguageOptions _dbUnitLanguageOptions = new Storage.UnitLanguageOptions();

            try
            {
                _dbUnitLanguageOptions.Update(idUnit, idLanguage, name);
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
