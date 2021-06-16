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
    internal class WasteTypeEmissionFactors
    {
        internal WasteTypeEmissionFactors() { }
        
        #region Read Functions

        internal Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor Item(Int64 idWasteTypeEmissionFactor, Security.Credential credential)
        {
            Storage.WasteTypeEmissionFactors _dbEmissionFactors = new Storage.WasteTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _wastesTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadById(idWasteTypeEmissionFactor, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_wastesTypeEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _wastesTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor(idWasteTypeEmissionFactor, Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.WasteType(Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _wastesTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor(idWasteTypeEmissionFactor, Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.WasteType(Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);

            }
            return _wastesTypeEmissionFactor;
        }
        internal Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor Item(Int64 idWasteType, Int64 idCountry, Security.Credential credential)
        {
            Storage.WasteTypeEmissionFactors _dbEmissionFactors = new Storage.WasteTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _wasteTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadDefault(idWasteType, idCountry, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_wasteTypeEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _wasteTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.WasteType(Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _wasteTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.WasteType(Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);

            }
            return _wasteTypeEmissionFactor;
        }
        internal Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor ItemGlobal(Int64 idWasteType, Security.Credential credential)
        {
            Storage.WasteTypeEmissionFactors _dbEmissionFactors = new Storage.WasteTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _wasteTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadGlobal(idWasteType, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_wasteTypeEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _wasteTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.WasteType(Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _wasteTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.WasteType(Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);

            }
            return _wasteTypeEmissionFactor;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor> Items(Int64 idWastesType, Int64 idCountry, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor>();
            Storage.WasteTypeEmissionFactors _dbWastesTypeEmissionFactors = new Storage.WasteTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _wasteTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbWastesTypeEmissionFactors.ReadAll(idWastesType, idCountry, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdWastesTypeEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdWastesTypeEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _wasteTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.WasteType(Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_wasteTypeEmissionFactor.IdWasteTypeEmissionFactor, _wasteTypeEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor> Items(Int64 idWastesType, Int64 idCountry, Int64 idCompany, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor>();
            Storage.WasteTypeEmissionFactors _dbWastesTypeEmissionFactors = new Storage.WasteTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _wasteTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbWastesTypeEmissionFactors.ReadAll(idWastesType, idCountry, idCompany, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdWastesTypeEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdWastesTypeEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _wasteTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.WasteType(Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_wasteTypeEmissionFactor.IdWasteTypeEmissionFactor, _wasteTypeEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor> ItemsDefault(Int64 idCountry, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor>();
            Storage.WasteTypeEmissionFactors _dbWasteTypeEmissionFactors = new Storage.WasteTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _fuelTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbWasteTypeEmissionFactors.ReadAllDefault(idCountry, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.WasteType(Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_fuelTypeEmissionFactor.IdWasteTypeEmissionFactor, _fuelTypeEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor> ItemsGlobal(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor>();
            Storage.WasteTypeEmissionFactors _dbWasteTypeEmissionFactors = new Storage.WasteTypeEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _fuelTypeEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbWasteTypeEmissionFactors.ReadAllGlobal(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _fuelTypeEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor(Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Convert.ToInt64(_dbRecord["IdEmissionFactor"]), new Objects.Auxiliaries.Types.WasteType(Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdIcon"], 0))), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_fuelTypeEmissionFactor.IdWasteTypeEmissionFactor, _fuelTypeEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        
        internal Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country> Countries(Int64 idWastesType, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country>();
            Storage.WasteTypeEmissionFactors _dbWastesTypeEmissionFactors = new Storage.WasteTypeEmissionFactors();
            Library.Objects.Auxiliaries.Geographic.Country _country = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbWastesTypeEmissionFactors.ReadCountries(idWastesType, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"],0))))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
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

        internal Boolean IsUsed(Int64 idWasteTypeEmissionFactor)
        {
            Storage.WasteTypeEmissionFactors _dbMeters = new Storage.WasteTypeEmissionFactors();
            return _dbMeters.IsUsed(idWasteTypeEmissionFactor);
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor Add(Int64 idEmissionFactor, Int64 idWasteType, Boolean isPropietary, Security.Credential credential)
        {
            Storage.WasteTypeEmissionFactors _dbWasteTypeEmissionFactor = new Storage.WasteTypeEmissionFactors();
            try
            {
                Int64 _idEmissionFactor = _dbWasteTypeEmissionFactor.Create(idEmissionFactor, idWasteType, isPropietary);
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
        internal void Remove(Int64 idWasteTypeEmissionFactor, Int64 idEmissionFactor)
        {
            try
            {
                if (!IsUsed(idWasteTypeEmissionFactor))
                    using (TransactionScope _scope = new TransactionScope())
                    {
                        new Storage.WasteTypeEmissionFactors().Delete(idWasteTypeEmissionFactor);
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
