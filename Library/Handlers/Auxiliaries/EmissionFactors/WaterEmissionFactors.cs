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
    internal class WaterEmissionFactors
    {
        internal WaterEmissionFactors() { }
        
        #region Read Functions

        internal Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor Item(Int64 idEmissionFactor, Security.Credential credential)
        {
            Storage.WaterEmissionFactors _dbEmissionFactors = new Storage.WaterEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _waterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadById(idEmissionFactor, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_waterEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _waterEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor(idEmissionFactor, Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _waterEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor(idEmissionFactor, Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);

            }
            return _waterEmissionFactor;
        }
        internal Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor ItemDefault(Int64 idCountry, Security.Credential credential)
        {
            Storage.WaterEmissionFactors _dbEmissionFactors = new Storage.WaterEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _waterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadDefault(idCountry, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_waterEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _waterEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor(Convert.ToInt64(_dbRecord["IdEmissionFactor"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _waterEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor(Convert.ToInt64(_dbRecord["IdEmissionFactor"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);

            }
            return _waterEmissionFactor;
        }
        internal Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor ItemGlobal(Security.Credential credential)
        {
            Storage.WaterEmissionFactors _dbEmissionFactors = new Storage.WaterEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _waterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadGlobal(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_waterEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _waterEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor(Convert.ToInt64(_dbRecord["IdEmissionFactor"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _waterEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor(Convert.ToInt64(_dbRecord["IdEmissionFactor"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);

            }
            return _waterEmissionFactor;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor> Items(Int64 idCountry, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor>();
            Storage.WaterEmissionFactors _dbWaterEmissionFactors = new Storage.WaterEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _waterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbWaterEmissionFactors.ReadAll(idCountry, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _idLanguage)
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
                    _waterEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor(Convert.ToInt64(_dbRecord["IdEmissionFactor"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_waterEmissionFactor.IdEmissionFactor, _waterEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor> Items(Int64 idCountry, Int64 idCompany, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor>();
            Storage.WaterEmissionFactors _dbWaterEmissionFactors = new Storage.WaterEmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _waterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbWaterEmissionFactors.ReadAll(idCountry, idCompany, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _idLanguage)
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
                    _waterEmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor(Convert.ToInt64(_dbRecord["IdEmissionFactor"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["EFDescription"]), Convert.ToBoolean(_dbRecord["IsPropietary"]), credential);
                    _oItems.Add(_waterEmissionFactor.IdEmissionFactor, _waterEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        
        internal Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country> Countries(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.Geographic.Country>();
            Storage.WaterEmissionFactors _dbWaterEmissionFactors = new Storage.WaterEmissionFactors();
            Library.Objects.Auxiliaries.Geographic.Country _country = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbWaterEmissionFactors.ReadCountries(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"],0))))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _idLanguage)
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

        internal Boolean IsUsed(Int64 idWaterEmissionFactor)
        {
            Storage.WaterEmissionFactors _dbMeters = new Storage.WaterEmissionFactors();
            return _dbMeters.IsUsed(idWaterEmissionFactor);
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor Add(Int64 idEmissionFactor, Boolean isPropietary, Security.Credential credential)
        {
            Storage.WaterEmissionFactors _dbWaterEmissionFactor = new Storage.WaterEmissionFactors();
            try
            {
                Int64 _idEmissionFactor = _dbWaterEmissionFactor.Create(idEmissionFactor, isPropietary);
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
        internal void Remove(Int64 idWaterEmissionFactor, Int64 idEmissionFactor)
        {
            try
            {
                if (!IsUsed(idWaterEmissionFactor))
                    using (TransactionScope _scope = new TransactionScope())
                    {
                        new Storage.WaterEmissionFactors().Delete(idWaterEmissionFactor);
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
