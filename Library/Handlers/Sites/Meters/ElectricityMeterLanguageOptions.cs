using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class ElectricityMeterLanguageOption
    {
        internal ElectricityMeterLanguageOption() { }
        
        #region Read Functions

        internal Library.Objects.Sites.Meters.ElectricityMeterLanguageOption Item(Int64 idMeter, String idLanguage)
        {
            Storage.ElectricityMeterLanguageOptions _dbElectricityMeterLanguageOptions = new Storage.ElectricityMeterLanguageOptions();
            Library.Objects.Sites.Meters.ElectricityMeterLanguageOption _electricityMeterLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbElectricityMeterLanguageOptions.ReadById(idMeter, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _electricityMeterLanguageOption = new Library.Objects.Sites.Meters.ElectricityMeterLanguageOption(idLanguage, Convert.ToString(_dbRecord["Description"]));
            }
            return _electricityMeterLanguageOption;
        }
        internal Dictionary<String, Library.Objects.Sites.Meters.ElectricityMeterLanguageOption> Items(Int64 idMeter)
        {
            Dictionary<String, Library.Objects.Sites.Meters.ElectricityMeterLanguageOption> _oItems = new Dictionary<String, Library.Objects.Sites.Meters.ElectricityMeterLanguageOption>();
            Storage.ElectricityMeterLanguageOptions _dbElectricityMeterLanguageOptions = new Storage.ElectricityMeterLanguageOptions();
            Library.Objects.Sites.Meters.ElectricityMeterLanguageOption _electricityMeterLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbElectricityMeterLanguageOptions.ReadAll(idMeter);

            String _idLanguage;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _idLanguage = Convert.ToString(_dbRecord["IdLanguage"]);
                _electricityMeterLanguageOption = new Library.Objects.Sites.Meters.ElectricityMeterLanguageOption(_idLanguage, Convert.ToString(_dbRecord["Description"]));
            
                _oItems.Add(_idLanguage, _electricityMeterLanguageOption);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Sites.Meters.ElectricityMeterLanguageOption Add(Int64 idMeter, String idLanguage, String name, String description)
        {
            Storage.ElectricityMeterLanguageOptions _dbElectricityMeterLanguageOptions = new Storage.ElectricityMeterLanguageOptions();

            try
            {
                _dbElectricityMeterLanguageOptions.Create(idMeter, idLanguage, description);
                return new Library.Objects.Sites.Meters.ElectricityMeterLanguageOption(idLanguage, description);
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
            Storage.ElectricityMeterLanguageOptions _dbElectricityMeterLanguageOptions = new Storage.ElectricityMeterLanguageOptions();

            try
            {
                _dbElectricityMeterLanguageOptions.Delete(idMeter, idLanguage);
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
            Storage.ElectricityMeterLanguageOptions _dbElectricityMeterLanguageOptions = new Storage.ElectricityMeterLanguageOptions();

            try
            {
                _dbElectricityMeterLanguageOptions.Update(idMeter, idLanguage, description);
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
