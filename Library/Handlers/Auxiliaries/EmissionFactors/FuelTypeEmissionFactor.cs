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
    class FuelTypeEmissionFactors
    {
        internal FuelTypeEmissionFactors() { }

        #region Read Functions

        internal Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor Item(Int64 idFuelTypeEmissionFactor, Security.Credential credential)
        {
            Storage.FuelTypeEmissionFactors _dbEmissionFactors = new Storage.FuelTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor _fuelTypeEmissionFactor = null;
            
            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadById(idFuelTypeEmissionFactor, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_fuelTypeEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor(idFuelTypeEmissionFactor, Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.FuelType(Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor(idFuelTypeEmissionFactor, Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.FuelType(Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                
                _insert = true;
            }
            return _fuelTypeEmissionFactor;
        }
        internal Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor Item(Int64 idFuelType, Int64 idCountry, Security.Credential credential)
        {
            Storage.FuelTypeEmissionFactors _dbEmissionFactors = new Storage.FuelTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor _fuelTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadDefault(idFuelType, idCountry, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_fuelTypeEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.FuelType(Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.FuelType(Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);

                _insert = true;
            }
            return _fuelTypeEmissionFactor;
        }
        internal Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor ItemGlobal(Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelTypeEmissionFactors _dbEmissionFactors = new Storage.FuelTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor _fuelTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadGlobal(idFuelType, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_fuelTypeEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.FuelType(Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.FuelType(Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);

                _insert = true;
            }
            return _fuelTypeEmissionFactor;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor> Items(Int64 idFuelsType, Int64 idCountry, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor>();
            Storage.FuelTypeEmissionFactors _dbFuelsTypeEmissionFactors = new Storage.FuelTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor _fuelTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFuelsTypeEmissionFactors.ReadAll(idFuelsType, idCountry, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdFuelsTypeEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdFuelsTypeEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.FuelType(Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_fuelTypeEmissionFactor.IdFuelTypeEmissionFactor, _fuelTypeEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor> Items(Int64 idFuelsType, Int64 idCountry, Int64 idCompany, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor>();
            Storage.FuelTypeEmissionFactors _dbFuelsTypeEmissionFactors = new Storage.FuelTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor _fuelTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFuelsTypeEmissionFactors.ReadAll(idFuelsType, idCountry, idCompany, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdFuelsTypeEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdFuelsTypeEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.FuelType(Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_fuelTypeEmissionFactor.IdFuelTypeEmissionFactor, _fuelTypeEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor> ItemsDefault(Int64 idCountry, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor>();
            Storage.FuelTypeEmissionFactors _dbFuelsTypeEmissionFactors = new Storage.FuelTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor _fuelTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFuelsTypeEmissionFactors.ReadAllDefault(idCountry, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.FuelType(Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_fuelTypeEmissionFactor.IdFuelTypeEmissionFactor, _fuelTypeEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor> ItemsGlobal(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor>();
            Storage.FuelTypeEmissionFactors _dbFuelsTypeEmissionFactors = new Storage.FuelTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor _fuelTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFuelsTypeEmissionFactors.ReadAllGlobal(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.FuelType(Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"],0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_fuelTypeEmissionFactor.IdFuelTypeEmissionFactor, _fuelTypeEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country> Countries(Int64 idFuelsType, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country>();
            Storage.FuelTypeEmissionFactors _dbFuelsTypeEmissionFactors = new Storage.FuelTypeEmissionFactors();
            Library.Objects.Auxiliaries.Geographic.Country _country = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFuelsTypeEmissionFactors.ReadCountries(idFuelsType, _idLanguage);

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
        
        internal Boolean IsUsed(Int64 idFuelTypeEmissionFactor)
        {
            Storage.FuelTypeEmissionFactors _dbMeters = new Storage.FuelTypeEmissionFactors();
            return _dbMeters.IsUsed(idFuelTypeEmissionFactor);
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor Add(Int64 idEmissionFactor, Int64 idFuelType, Boolean isPropietary, Security.Credential credential)
        {
            Storage.FuelTypeEmissionFactors _dbFuelTypeEmissionFactor = new Storage.FuelTypeEmissionFactors();
            try
            {
                Int64 _idEmissionFactor = _dbFuelTypeEmissionFactor.Create(idEmissionFactor, idFuelType, isPropietary);
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
        internal void Remove(Int64 idFuelTypeEmissionFactor, Int64 idEmissionFactor)
        {
            try
            {
                if (!IsUsed(idFuelTypeEmissionFactor))
                    using (TransactionScope _scope = new TransactionScope())
                    {
                        new Storage.FuelTypeEmissionFactors().Delete(idFuelTypeEmissionFactor);
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
