using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class WasteTypes
    {
        internal WasteTypes() { }

        #region Read Functions

        internal Library.Objects.Auxiliaries.Types.WasteType Item(Int64 idWasteType, Security.Credential credential)
        {
            Storage.WasteTypes _dbWasteTypes = new Storage.WasteTypes();
            Library.Objects.Auxiliaries.Types.WasteType _wasteType = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbWasteTypes.ReadById(idWasteType, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_wasteType != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _wasteType = new Library.Objects.Auxiliaries.Types.WasteType(idWasteType, Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0)));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _wasteType = new Library.Objects.Auxiliaries.Types.WasteType(idWasteType, Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0)));

                _insert = true;
            }
            return _wasteType;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.Types.WasteType> Items(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Types.WasteType> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Types.WasteType>();
            Storage.WasteTypes _dbWasteTypes = new Storage.WasteTypes();
            Library.Objects.Auxiliaries.Types.WasteType _WasteType = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbWasteTypes.ReadAll(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdWasteType"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdWasteType"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _WasteType = new Library.Objects.Auxiliaries.Types.WasteType(Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0)));
                    _oItems.Add(_WasteType.IdWasteType, _WasteType);
                }
                _insert = true;
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Types.WasteType Add(String name, String description, Double ef, Int64 idIcon, Security.Credential credential)
        {
            Storage.WasteTypes _dbWasteTypes = new Storage.WasteTypes();
            String _defaultLanguage = new Languages().ItemDefault().IdLanguage;

            try
            {
                Int64 _idWasteType = _dbWasteTypes.Create(_defaultLanguage, name, description, ef, idIcon);
                return Item(_idWasteType, credential);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }

        }
        internal void Remove(Int64 idWasteType)
        {
            Storage.WasteTypes _dbWasteTypes = new Storage.WasteTypes();

            try
            {
                _dbWasteTypes.Delete(idWasteType);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idWasteType, Security.Credential credential, String name, String description, Double ef, Int64 idIcon)
        {
            Storage.WasteTypes _dbWasteTypes = new Storage.WasteTypes();
            String _defaultLanguage = new Languages().ItemDefault().IdLanguage;

            try
            {
                _dbWasteTypes.Update(idWasteType, _defaultLanguage, name, description, ef, idIcon);

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
