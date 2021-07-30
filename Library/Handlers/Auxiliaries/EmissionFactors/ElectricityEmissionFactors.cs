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
    internal class ElectricityEmissionFactors
    {
        internal ElectricityEmissionFactors() { }
        
        #region Read Functions

        internal Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor Item(Int64 idEmissionFactor, Security.Credential credential)
        {
            Storage.ElectricityEmissionFactors _dbEmissionFactors = new Storage.ElectricityEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor _electricityEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadById(idEmissionFactor, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_electricityEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _electricityEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor(idEmissionFactor, Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _electricityEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor(idEmissionFactor, Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);

                _insert = true;
            }
            return _electricityEmissionFactor;
        }
        internal Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor ItemDefault(Int64 idCountry, Security.Credential credential)
        {
            Storage.ElectricityEmissionFactors _dbEmissionFactors = new Storage.ElectricityEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor _electricityEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadDefault(idCountry, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_electricityEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _electricityEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor(Convert.ToInt64(_dbRecord["IdEmissionFactor"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _electricityEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor(Convert.ToInt64(_dbRecord["IdEmissionFactor"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                
                _insert = true;
            }
            return _electricityEmissionFactor;
        }
        internal Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor ItemGlobal(Security.Credential credential)
        {
            Storage.ElectricityEmissionFactors _dbEmissionFactors = new Storage.ElectricityEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor _electricityEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadGlobal(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_electricityEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _electricityEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor(Convert.ToInt64(_dbRecord["IdEmissionFactor"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _electricityEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor(Convert.ToInt64(_dbRecord["IdEmissionFactor"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);

                _insert = true;
            }
            return _electricityEmissionFactor;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor> Items(Int64 idCountry, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor>();
            Storage.ElectricityEmissionFactors _dbElectricityEmissionFactors = new Storage.ElectricityEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor _electricityEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbElectricityEmissionFactors.ReadAll(idCountry, _idLanguage);

            Boolean _insert=true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _electricityEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor(Convert.ToInt64(_dbRecord["IdEmissionFactor"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_electricityEmissionFactor.IdEmissionFactor, _electricityEmissionFactor);
                }
                _insert=true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor> Items(Int64 idCountry, Int64 idCompany, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor>();
            Storage.ElectricityEmissionFactors _dbElectricityEmissionFactors = new Storage.ElectricityEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor _electricityEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbElectricityEmissionFactors.ReadAll(idCountry, idCompany, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _electricityEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor(Convert.ToInt64(_dbRecord["IdEmissionFactor"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_electricityEmissionFactor.IdEmissionFactor, _electricityEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country> Countries(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country>();
            Storage.ElectricityEmissionFactors _dbElectricityEmissionFactors = new Storage.ElectricityEmissionFactors();
            Library.Objects.Auxiliaries.Geographic.Country _country = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbElectricityEmissionFactors.ReadCountries(_idLanguage);

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

        internal Boolean IsUsed(Int64 idElectricityEmissionFactor)
        {
            Storage.ElectricityEmissionFactors _dbMeters = new Storage.ElectricityEmissionFactors();
            return _dbMeters.IsUsed(idElectricityEmissionFactor);
        }

        #endregion

        #region Write Functions
        
        internal Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor Add(Int64 idEmissionFactor, Boolean isPropietary, Security.Credential credential)
        {
            Storage.ElectricityEmissionFactors _dbElectricityEmissionFactor = new Storage.ElectricityEmissionFactors();

            try
            {
                Int64 _idEmissionFactor = _dbElectricityEmissionFactor.Create(idEmissionFactor, isPropietary);
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
        internal void Remove(Int64 idElectricityEmissionFactor, Int64 idEmissionFactor)
        {
            try
            {
                if (!IsUsed(idElectricityEmissionFactor))
                    using (TransactionScope _scope = new TransactionScope())
                    {
                        new Storage.ElectricityEmissionFactors().Delete(idElectricityEmissionFactor);
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
