using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class TransportTypes
    {
        internal TransportTypes() { }
        
        #region Read Functions

        internal Library.Objects.Auxiliaries.Types.TransportType Item(Int64 idTransportType, Security.Credential credential)
        {
            Storage.TransportTypes _dbTransportTypes = new Storage.TransportTypes();
            Library.Objects.Auxiliaries.Types.TransportType _transportType = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbTransportTypes.ReadById(idTransportType, _idLanguage);

            Boolean _insert=true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_transportType!=null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _transportType = new Library.Objects.Auxiliaries.Types.TransportType(idTransportType, Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0)));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _transportType = new Library.Objects.Auxiliaries.Types.TransportType(idTransportType, Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0)));

                _insert = true;
            }
            return _transportType;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.Types.TransportType> Items(Int64 idCountry, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Types.TransportType> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Types.TransportType>();
            Storage.TransportTypes _dbTransportTypes = new Storage.TransportTypes();
            Library.Objects.Auxiliaries.Types.TransportType _TransportType = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbTransportTypes.ReadAllByCountry(idCountry, _idLanguage);

            Boolean _insert=true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdTransportType"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdTransportType"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _TransportType = new Library.Objects.Auxiliaries.Types.TransportType(Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0)));
                    _oItems.Add(_TransportType.IdTransportType, _TransportType);
                }
                _insert=true;
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.Types.TransportType Add(String name, String description, Double ef, Int64 idIcon, Security.Credential credential)
        {
            Storage.TransportTypes _dbTransportTypes = new Storage.TransportTypes();
            String _defaultLanguage = new Languages().ItemDefault().IdLanguage;

            try 
            {
                Int64 _idTransportType = _dbTransportTypes.Create(_defaultLanguage, name, description, ef, idIcon);
                return Item(_idTransportType, credential);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
            
        }
        internal void Remove(Int64 idTransportType)
        {
            Storage.TransportTypes _dbTransportTypes = new Storage.TransportTypes();

            try
            {
                _dbTransportTypes.Delete(idTransportType);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idTransportType, Security.Credential credential, String name, String description, Double ef, Int64 idIcon)
        {
            Storage.TransportTypes _dbTransportTypes = new Storage.TransportTypes();
            String _defaultLanguage = new Languages().ItemDefault().IdLanguage;

            try
            {
                _dbTransportTypes.Update(idTransportType, _defaultLanguage, name, description, ef, idIcon);

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
