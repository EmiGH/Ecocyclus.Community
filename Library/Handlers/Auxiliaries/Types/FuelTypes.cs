using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class FuelTypes
    {
        internal FuelTypes() { }

        #region Read Functions

        internal Library.Objects.Auxiliaries.Types.FuelType Item(Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelTypes _dbFuelTypes = new Storage.FuelTypes();
            Library.Objects.Auxiliaries.Types.FuelType _transportType = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFuelTypes.ReadById(idFuelType, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_transportType != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _transportType = new Library.Objects.Auxiliaries.Types.FuelType(idFuelType, Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0)));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _transportType = new Library.Objects.Auxiliaries.Types.FuelType(idFuelType, Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0)));

            }
            return _transportType;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.Types.FuelType> Items(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Types.FuelType> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Types.FuelType>();
            Storage.FuelTypes _dbFuelTypes = new Storage.FuelTypes();
            Library.Objects.Auxiliaries.Types.FuelType _FuelType = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFuelTypes.ReadAll(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdFuelType"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdFuelType"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _FuelType = new Library.Objects.Auxiliaries.Types.FuelType(Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0)));
                    _oItems.Add(_FuelType.IdFuelType, _FuelType);
                }
                _insert = true;
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Types.FuelType Add(String name, String description, Double ef, Int64 idIcon, Security.Credential credential)
        {
            Storage.FuelTypes _dbFuelTypes = new Storage.FuelTypes();
            String _defaultLanguage = new Languages().ItemDefault().IdLanguage;

            try
            {
                Int64 _idFuelType = _dbFuelTypes.Create(_defaultLanguage, name, description, ef, idIcon);
                return Item(_idFuelType, credential);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }

        }
        internal void Remove(Int64 idFuelType)
        {
            Storage.FuelTypes _dbFuelTypes = new Storage.FuelTypes();

            try
            {
                _dbFuelTypes.Delete(idFuelType);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idFuelType, Security.Credential credential, String name, String description, Double ef, Int64 idIcon)
        {
            Storage.FuelTypes _dbFuelTypes = new Storage.FuelTypes();
            String _defaultLanguage = new Languages().ItemDefault().IdLanguage;

            try
            {
                _dbFuelTypes.Update(idFuelType, _defaultLanguage, name, description, ef, idIcon);

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
