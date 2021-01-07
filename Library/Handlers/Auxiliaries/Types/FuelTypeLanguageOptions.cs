using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class FuelTypeLanguageOptions
    {
        internal FuelTypeLanguageOptions() { }

        #region Read Functions

        internal Library.Objects.Auxiliaries.Types.FuelTypeLanguageOption Item(Int64 idFuelType, String idLanguage)
        {
            Storage.FuelTypeLanguageOptions _dbFuelTypeLanguageOptions = new Storage.FuelTypeLanguageOptions();
            Library.Objects.Auxiliaries.Types.FuelTypeLanguageOption _transportTypeLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFuelTypeLanguageOptions.ReadById(idFuelType, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _transportTypeLanguageOption = new Library.Objects.Auxiliaries.Types.FuelTypeLanguageOption(idLanguage, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
            }
            return _transportTypeLanguageOption;
        }
        internal Dictionary<String, Library.Objects.Auxiliaries.Types.FuelTypeLanguageOption> Items(Int64 idFuelType)
        {
            Dictionary<String, Library.Objects.Auxiliaries.Types.FuelTypeLanguageOption> _oItems = new Dictionary<String, Library.Objects.Auxiliaries.Types.FuelTypeLanguageOption>();
            Storage.FuelTypeLanguageOptions _dbFuelTypeLanguageOptions = new Storage.FuelTypeLanguageOptions();
            Library.Objects.Auxiliaries.Types.FuelTypeLanguageOption _transportTypeLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFuelTypeLanguageOptions.ReadAll(idFuelType);

            String _idLanguage;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _idLanguage = Convert.ToString(_dbRecord["IdLanguage"]);
                _transportTypeLanguageOption = new Library.Objects.Auxiliaries.Types.FuelTypeLanguageOption(_idLanguage, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

                _oItems.Add(_idLanguage, _transportTypeLanguageOption);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Types.FuelTypeLanguageOption Add(Int64 idFuelType, String idLanguage, String name, String description)
        {
            Storage.FuelTypeLanguageOptions _dbFuelTypeLanguageOptions = new Storage.FuelTypeLanguageOptions();

            try
            {
                _dbFuelTypeLanguageOptions.Create(idFuelType, idLanguage, name, description);
                return new Objects.Auxiliaries.Types.FuelTypeLanguageOption(idLanguage, name, description);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idFuelType, String idLanguage)
        {
            Storage.FuelTypeLanguageOptions _dbFuelTypeLanguageOptions = new Storage.FuelTypeLanguageOptions();

            try
            {
                _dbFuelTypeLanguageOptions.Delete(idFuelType, idLanguage);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idFuelType, String idLanguage, String name, String description)
        {
            Storage.FuelTypeLanguageOptions _dbFuelTypeLanguageOptions = new Storage.FuelTypeLanguageOptions();

            try
            {
                _dbFuelTypeLanguageOptions.Update(idFuelType, idLanguage, name, description);
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
