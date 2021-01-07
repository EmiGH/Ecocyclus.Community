using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class TransportTypeLanguageOptions
    {
        internal TransportTypeLanguageOptions() { }
        
        #region Read Functions

        internal Library.Objects.Auxiliaries.Types.TransportTypeLanguageOption Item(Int64 idTransportType, String idLanguage)
        {
            Storage.TransportTypeLanguageOptions _dbTransportTypeLanguageOptions = new Storage.TransportTypeLanguageOptions();
            Library.Objects.Auxiliaries.Types.TransportTypeLanguageOption _transportTypeLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbTransportTypeLanguageOptions.ReadById(idTransportType, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _transportTypeLanguageOption = new Library.Objects.Auxiliaries.Types.TransportTypeLanguageOption(idLanguage, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
            }
            return _transportTypeLanguageOption;
        }
        internal Dictionary<String, Library.Objects.Auxiliaries.Types.TransportTypeLanguageOption> Items(Int64 idTransportType)
        {
            Dictionary<String, Library.Objects.Auxiliaries.Types.TransportTypeLanguageOption> _oItems = new Dictionary<String, Library.Objects.Auxiliaries.Types.TransportTypeLanguageOption>();
            Storage.TransportTypeLanguageOptions _dbTransportTypeLanguageOptions = new Storage.TransportTypeLanguageOptions();
            Library.Objects.Auxiliaries.Types.TransportTypeLanguageOption _transportTypeLanguageOption = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbTransportTypeLanguageOptions.ReadAll(idTransportType);

            String _idLanguage;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _idLanguage = Convert.ToString(_dbRecord["IdLanguage"]);
                _transportTypeLanguageOption = new Library.Objects.Auxiliaries.Types.TransportTypeLanguageOption(_idLanguage, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

                _oItems.Add(_idLanguage, _transportTypeLanguageOption);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Types.TransportTypeLanguageOption Add(Int64 idTransportType, String idLanguage, String name, String description)
        {
            Storage.TransportTypeLanguageOptions _dbTransportTypeLanguageOptions = new Storage.TransportTypeLanguageOptions();

            try
            {
                _dbTransportTypeLanguageOptions.Create(idTransportType, idLanguage, name, description);
                return new Objects.Auxiliaries.Types.TransportTypeLanguageOption(idLanguage, name, description);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idTransportType, String idLanguage)
        {
            Storage.TransportTypeLanguageOptions _dbTransportTypeLanguageOptions = new Storage.TransportTypeLanguageOptions();

            try
            {
                _dbTransportTypeLanguageOptions.Delete(idTransportType, idLanguage);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idTransportType, String idLanguage, String name, String description)
        {
            Storage.TransportTypeLanguageOptions _dbTransportTypeLanguageOptions = new Storage.TransportTypeLanguageOptions();

            try
            {
                _dbTransportTypeLanguageOptions.Update(idTransportType, idLanguage, name, description);
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
