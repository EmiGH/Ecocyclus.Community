using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class EmissionFactors
    {
        internal EmissionFactors() { }

        #region Read Functions

        internal Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor Item(Int64 idEmissionFactor, Security.Credential credential)
        {
            Storage.EmissionFactors _dbEmissionFactors = new Storage.EmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor _emissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadById(idEmissionFactor, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_emissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _emissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor(idEmissionFactor, Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["Description"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _emissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor(idEmissionFactor, Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["Description"]), credential);

            }
            return _emissionFactor;
        }
        internal Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor> Items(Int64 idCountry, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor>();
            Storage.EmissionFactors _dbEmissionFactors = new Storage.EmissionFactors();
            Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor _EmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadAll(_idLanguage);

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
                    _EmissionFactor = new Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor(Convert.ToInt64(_dbRecord["IdEmissionFactor"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)), Convert.ToDouble(_dbRecord["Value"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToString(_dbRecord["Description"]), credential);
                    _oItems.Add(_EmissionFactor.IdEmissionFactor, _EmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }

        internal Boolean IsUsed(Int64 idEmissionFactor)
        {
            Storage.EmissionFactors _dbMeters = new Storage.EmissionFactors();
            return _dbMeters.IsUsed(idEmissionFactor);
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor Add(Int64 idCountry, Double value, String description, Security.Credential credential)
        {
            Storage.EmissionFactors _dbEmissionFactor = new Storage.EmissionFactors();
            String _defaultLanguage = new Languages().ItemDefault().IdLanguage;

            try
            {
                Int64 _idEmissionFactor = _dbEmissionFactor.Create(_defaultLanguage, idCountry, value, description);
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
        internal void Remove(Int64 idEmissionFactor)
        {
            try
            {
                if(!IsUsed(idEmissionFactor))
                    new Storage.EmissionFactors().Delete(idEmissionFactor);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idEmissionFactor, Int64 idCountry, Double value, String name, String description)
        {
            Storage.EmissionFactors _dbEmissionFactor = new Storage.EmissionFactors();
            String _defaultLanguage = new Languages().ItemDefault().IdLanguage;

            try
            {
                _dbEmissionFactor.Update(idEmissionFactor, _defaultLanguage, idCountry, value, description);
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
