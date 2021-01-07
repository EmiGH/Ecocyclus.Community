using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class FuelMeterLanguageOptions
    {
        internal FuelMeterLanguageOptions() { }

        #region Read Functions

        internal Library.Objects.Sites.Meters.FuelMeterLanguageOption Item(Int64 idMeter, String idLanguage)
        {
            Storage.FuelMeterLanguageOptions _dbFuelMeterLanguageOptions = new Storage.FuelMeterLanguageOptions();
            Library.Objects.Sites.Meters.FuelMeterLanguageOption _electricityMeterLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFuelMeterLanguageOptions.ReadById(idMeter, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _electricityMeterLanguageOption = new Library.Objects.Sites.Meters.FuelMeterLanguageOption(idLanguage, Convert.ToString(_dbRecord["Description"]));
            }
            return _electricityMeterLanguageOption;
        }
        internal Dictionary<String, Library.Objects.Sites.Meters.FuelMeterLanguageOption> Items(Int64 idMeter)
        {
            Dictionary<String, Library.Objects.Sites.Meters.FuelMeterLanguageOption> _oItems = new Dictionary<String, Library.Objects.Sites.Meters.FuelMeterLanguageOption>();
            Storage.FuelMeterLanguageOptions _dbFuelMeterLanguageOptions = new Storage.FuelMeterLanguageOptions();
            Library.Objects.Sites.Meters.FuelMeterLanguageOption _electricityMeterLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFuelMeterLanguageOptions.ReadAll(idMeter);

            String _idLanguage;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _idLanguage = Convert.ToString(_dbRecord["IdLanguage"]);
                _electricityMeterLanguageOption = new Library.Objects.Sites.Meters.FuelMeterLanguageOption(_idLanguage, Convert.ToString(_dbRecord["Description"]));

                _oItems.Add(_idLanguage, _electricityMeterLanguageOption);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Sites.Meters.FuelMeterLanguageOption Add(Int64 idMeter, String idLanguage, String name, String description)
        {
            Storage.FuelMeterLanguageOptions _dbFuelMeterLanguageOptions = new Storage.FuelMeterLanguageOptions();

            try
            {
                _dbFuelMeterLanguageOptions.Create(idMeter, idLanguage, description);
                return new Library.Objects.Sites.Meters.FuelMeterLanguageOption(idLanguage, description);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idMeter, String idLanguage)
        {
            Storage.FuelMeterLanguageOptions _dbFuelMeterLanguageOptions = new Storage.FuelMeterLanguageOptions();

            try
            {
                _dbFuelMeterLanguageOptions.Delete(idMeter, idLanguage);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idMeter, String idLanguage, String name, String description)
        {
            Storage.FuelMeterLanguageOptions _dbFuelMeterLanguageOptions = new Storage.FuelMeterLanguageOptions();

            try
            {
                _dbFuelMeterLanguageOptions.Update(idMeter, idLanguage, description);
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
