using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class WasteTypeLanguageOptions
    {
        internal WasteTypeLanguageOptions() { }

        #region Read Functions

        internal Library.Objects.Auxiliaries.Types.WasteTypeLanguageOption Item(Int64 idWasteType, String idLanguage)
        {
            Storage.WasteTypeLanguageOptions _dbWasteTypeLanguageOptions = new Storage.WasteTypeLanguageOptions();
            Library.Objects.Auxiliaries.Types.WasteTypeLanguageOption _transportTypeLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbWasteTypeLanguageOptions.ReadById(idWasteType, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _transportTypeLanguageOption = new Library.Objects.Auxiliaries.Types.WasteTypeLanguageOption(idLanguage, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
            }
            return _transportTypeLanguageOption;
        }
        internal Dictionary<String, Library.Objects.Auxiliaries.Types.WasteTypeLanguageOption> Items(Int64 idWasteType)
        {
            Dictionary<String, Library.Objects.Auxiliaries.Types.WasteTypeLanguageOption> _oItems = new Dictionary<String, Library.Objects.Auxiliaries.Types.WasteTypeLanguageOption>();
            Storage.WasteTypeLanguageOptions _dbWasteTypeLanguageOptions = new Storage.WasteTypeLanguageOptions();
            Library.Objects.Auxiliaries.Types.WasteTypeLanguageOption _transportTypeLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbWasteTypeLanguageOptions.ReadAll(idWasteType);

            String _idLanguage;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _idLanguage = Convert.ToString(_dbRecord["IdLanguage"]);
                _transportTypeLanguageOption = new Library.Objects.Auxiliaries.Types.WasteTypeLanguageOption(_idLanguage, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

                _oItems.Add(_idLanguage, _transportTypeLanguageOption);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Types.WasteTypeLanguageOption Add(Int64 idWasteType, String idLanguage, String name, String description)
        {
            Storage.WasteTypeLanguageOptions _dbWasteTypeLanguageOptions = new Storage.WasteTypeLanguageOptions();

            try
            {
                _dbWasteTypeLanguageOptions.Create(idWasteType, idLanguage, name, description);
                return new Objects.Auxiliaries.Types.WasteTypeLanguageOption(idLanguage, name, description);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idWasteType, String idLanguage)
        {
            Storage.WasteTypeLanguageOptions _dbWasteTypeLanguageOptions = new Storage.WasteTypeLanguageOptions();

            try
            {
                _dbWasteTypeLanguageOptions.Delete(idWasteType, idLanguage);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idWasteType, String idLanguage, String name, String description)
        {
            Storage.WasteTypeLanguageOptions _dbWasteTypeLanguageOptions = new Storage.WasteTypeLanguageOptions();

            try
            {
                _dbWasteTypeLanguageOptions.Update(idWasteType, idLanguage, name, description);
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
