using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    class TransportTypeEmissionFactors
    {
        internal TransportTypeEmissionFactors() { }
        
        #region Read Functions

        internal Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor Item(Int64 idTransportTypeEmissionFactor, Security.Credential credential)
        {
            Storage.TransportTypeEmissionFactors _dbEmissionFactors = new Storage.TransportTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor _transportsTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadById(idTransportTypeEmissionFactor, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_transportsTypeEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _transportsTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor(idTransportTypeEmissionFactor, Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.TransportType(Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _transportsTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor(idTransportTypeEmissionFactor, Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.TransportType(Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);

                _insert = true;
            }
            return _transportsTypeEmissionFactor;
        }
        internal Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor Item(Int64 idTransportType, Int64 idCountry, Security.Credential credential)
        {
            Storage.TransportTypeEmissionFactors _dbEmissionFactors = new Storage.TransportTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor _transportTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadDefault(idTransportType, idCountry, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_transportTypeEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _transportTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.TransportType(Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _transportTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.TransportType(Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);

                _insert = true;
            }
            return _transportTypeEmissionFactor;
        }
        internal Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor ItemGlobal(Int64 idTransportType, Security.Credential credential)
        {
            Storage.TransportTypeEmissionFactors _dbEmissionFactors = new Storage.TransportTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor _transportTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadGlobal(idTransportType, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_transportTypeEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _transportTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.TransportType(Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _transportTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.TransportType(Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);

                _insert = true;
            }
            return _transportTypeEmissionFactor;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor> Items(Int64 idTransportType, Int64 idCountry, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor>();
            Storage.TransportTypeEmissionFactors _dbTransportTypeEmissionFactors = new Storage.TransportTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor _transportTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbTransportTypeEmissionFactors.ReadAll(idTransportType, idCountry, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _transportTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.TransportType(Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_transportTypeEmissionFactor.IdTransportTypeEmissionFactor, _transportTypeEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor> Items(Int64 idTransportType, Int64 idCountry, Int64 idCompany, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor>();
            Storage.TransportTypeEmissionFactors _dbTransportTypeEmissionFactors = new Storage.TransportTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor _transportTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbTransportTypeEmissionFactors.ReadAll(idTransportType, idCountry, idCompany, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _transportTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.TransportType(Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_transportTypeEmissionFactor.IdTransportTypeEmissionFactor, _transportTypeEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor> ItemsDefault(Int64 idCountry, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor>();
            Storage.TransportTypeEmissionFactors _dbTransportTypeEmissionFactors = new Storage.TransportTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor _fuelTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbTransportTypeEmissionFactors.ReadAllDefault(idCountry, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.TransportType(Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_fuelTypeEmissionFactor.IdTransportTypeEmissionFactor, _fuelTypeEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor> ItemsGlobal(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor>();
            Storage.TransportTypeEmissionFactors _dbTransportTypeEmissionFactors = new Storage.TransportTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor _fuelTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbTransportTypeEmissionFactors.ReadAllGlobal(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.TransportType(Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_fuelTypeEmissionFactor.IdTransportTypeEmissionFactor, _fuelTypeEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country> Countries(Int64 idTransportType, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country>();
            Storage.TransportTypeEmissionFactors _dbTransportTypeEmissionFactors = new Storage.TransportTypeEmissionFactors();
            Library.Objects.Auxiliaries.Geographic.Country _country = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbTransportTypeEmissionFactors.ReadCountries(idTransportType, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"],0))))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"],0)));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _country = new Library.Objects.Auxiliaries.Geographic.Country(Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Code"]), Convert.ToString(_dbRecord["PhoneCode"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential);
                    _oItems.Add(_country.IdCountry, _country);
                }
                _insert = true;
            }
            return _oItems;
        }

        internal Boolean IsUsed(Int64 idTransportTypeEmissionFactor)
        {
            Storage.TransportTypeEmissionFactors _dbMeters = new Storage.TransportTypeEmissionFactors();
            return _dbMeters.IsUsed(idTransportTypeEmissionFactor);
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor Add(Int64 idEmissionFactor, Int64 idTransportType, Boolean isPropietary, Security.Credential credential)
        {
            Storage.TransportTypeEmissionFactors _dbTransportTypeEmissionFactor = new Storage.TransportTypeEmissionFactors();
            try
            {
                Int64 _idEmissionFactor = _dbTransportTypeEmissionFactor.Create(idEmissionFactor, idTransportType, isPropietary);
                return Item(_idEmissionFactor, credential);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idTransportTypeEmissionFactor, Int64 idEmissionFactor)
        {
            try
            {
                if (!IsUsed(idTransportTypeEmissionFactor))
                    using (TransactionScope _scope = new TransactionScope())
                    {
                        new Storage.TransportTypeEmissionFactors().Delete(idTransportTypeEmissionFactor);
                        new EmissionFactors().Remove(idEmissionFactor);

                        _scope.Complete();
                    }
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }

        #endregion
    }
}
