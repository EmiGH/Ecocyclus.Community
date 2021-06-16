using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class FuelMeterEmissionFactors
    {
        internal FuelMeterEmissionFactors() { }
        
        #region Read Functions

        internal Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor Item(Int64 idFuelMeterEmissionFactor, Security.Credential credential)
        {
            Storage.FuelMeterEmissionFactors _dbEmissionFactors = new Storage.FuelMeterEmissionFactors();
            Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor _fuelsMeterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadById(idFuelMeterEmissionFactor, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_fuelsMeterEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _fuelsMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor(idFuelMeterEmissionFactor, Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _fuelsMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor(idFuelMeterEmissionFactor, Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), credential);

            }
            return _fuelsMeterEmissionFactor;
        }
        internal Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor Item(Int64 idFuelMeter, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelMeterEmissionFactors _dbEmissionFactors = new Storage.FuelMeterEmissionFactors();
            Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor _fuelsMeterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadById(idFuelMeter, idFuelType, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_fuelsMeterEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _fuelsMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor(Convert.ToInt64(_dbRecord["IdFuelMeterEmissionFactor"]), Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _fuelsMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor(Convert.ToInt64(_dbRecord["IdFuelMeterEmissionFactor"]), Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), credential);

            }
            return _fuelsMeterEmissionFactor;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor> Items(Int64 idFuelMeter, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor>();
            Storage.FuelMeterEmissionFactors _dbFuelMeterEmissionFactors = new Storage.FuelMeterEmissionFactors();
            Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor _fuelMeterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbFuelMeterEmissionFactors.ReadAll(idFuelMeter, _idLanguage);

            Boolean _insert=true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdFuelMeterEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdFuelMeterEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _fuelMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor(Convert.ToInt64(_dbRecord["IdFuelMeterEmissionFactor"]), Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), credential);
                    _oItems.Add(_fuelMeterEmissionFactor.IdFuelMeterEmissionFactor, _fuelMeterEmissionFactor);
                }
                _insert=true;
            }
            return _oItems;
        }
                
        #endregion

        #region Write Functions

        internal void Add(Int64 idMeter, List<Objects.Sites.Meters.Series.FuelDataEmissionFactor> emissionFactors, Security.Credential credential)
        {
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    foreach (Objects.Sites.Meters.Series.FuelDataEmissionFactor _item in emissionFactors)
                    {
                        Int64 _idFuelTypeEmissionFactor;
                        if (_item.IsNew)
                        {
                            Objects.Sites.Meters.Series.DataEmissionFactor _newEF = _item.NewEmissionFactor;
                            Int64 _idEmissionFactor = new EmissionFactors().Add(_newEF.Country.IdCountry, _newEF.Value, _newEF.Description, credential).IdEmissionFactor;
                            foreach (KeyValuePair<String, String> _description in _newEF.Descriptions)
                            {
                                new EmissionFactorLanguageOptions().Add(_idEmissionFactor, _description.Key, _description.Value);
                            }
                            _idFuelTypeEmissionFactor = new FuelTypeEmissionFactors().Add(_idEmissionFactor, _item.FuelType.IdFuelType, true, credential).IdEmissionFactor;
                        }
                        else
                        {
                            _idFuelTypeEmissionFactor = _item.FuelTypeEmissionFactor.IdEmissionFactor;
                        }
                        new FuelMeterEmissionFactors().Add(idMeter, _idFuelTypeEmissionFactor, credential);
                    }
                }
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedMeter);
                else
                    throw sqlex;
            }
        }
        internal Library.Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor Add(Int64 idFuelMeter, Int64 idFuelTypeEmissionFactor, Security.Credential credential)
        {
            Storage.FuelMeterEmissionFactors _dbFuelMeterEmissionFactor = new Storage.FuelMeterEmissionFactors();
            try
            {

                Int64 _idEmissionFactor = _dbFuelMeterEmissionFactor.Create(idFuelMeter, idFuelTypeEmissionFactor);
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
        internal void Remove(Int64 idFuelMeterEmissionFactor, Int64 idFuelTypeEmissionFactor, Int64 idEmissionFactor)
        {
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    new Storage.FuelMeterEmissionFactors().Delete(idFuelMeterEmissionFactor);
                    new FuelTypeEmissionFactors().Remove(idFuelTypeEmissionFactor, idEmissionFactor);

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
        internal void Modify(Objects.Sites.Meters.FuelMeter meter, List<Objects.Sites.Meters.Series.FuelDataEmissionFactor> emissionFactors, Security.Credential credential)
        {
            try
            {
                Int64 _idMeter = meter.IdMeter;

                using (TransactionScope _scope = new TransactionScope())
                {
                    FuelMeterEmissionFactors _wasteMeterEmissionFactors = new FuelMeterEmissionFactors();
                    DeleteUnusedEmissionFactors(meter, emissionFactors);

                    foreach (Objects.Sites.Meters.Series.FuelDataEmissionFactor _item in GetNewEmissionFactors(meter, emissionFactors))
                    {
                        Int64 _idFuelTypeEmissionFactor;
                        if (_item.IsNew)
                        {
                            Objects.Sites.Meters.Series.DataEmissionFactor _newEF = _item.NewEmissionFactor;
                            Int64 _idEmissionFactor = new EmissionFactors().Add(_newEF.Country.IdCountry, _newEF.Value, _newEF.Description, credential).IdEmissionFactor;
                            foreach (KeyValuePair<String, String> _description in _newEF.Descriptions)
                            {
                                new EmissionFactorLanguageOptions().Add(_idEmissionFactor, _description.Key, _description.Value);
                            }
                            _idFuelTypeEmissionFactor = new FuelTypeEmissionFactors().Add(_idEmissionFactor, _item.FuelType.IdFuelType, true, credential).IdEmissionFactor;
                        }
                        else
                        {
                            _idFuelTypeEmissionFactor = _item.FuelTypeEmissionFactor.IdEmissionFactor;
                        }
                        _wasteMeterEmissionFactors.Add(meter.IdMeter, _idFuelTypeEmissionFactor, credential);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private void DeleteUnusedEmissionFactors(Objects.Sites.Meters.FuelMeter meter, List<Objects.Sites.Meters.Series.FuelDataEmissionFactor> emissionFactors)
        {
            FuelMeterEmissionFactors _wasteMeterEmissionFactors = new FuelMeterEmissionFactors();
            Dictionary<Int64, Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor> _existingEmissionFactors = meter.GetEmissionFactors();
            Boolean _found;

            foreach (Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor _existingEF in _existingEmissionFactors.Values)
            {
                _found = false;
                foreach (Objects.Sites.Meters.Series.FuelDataEmissionFactor _newEF in emissionFactors)
                {
                    if (!_newEF.IsNew)
                    {
                        if (_existingEF.IdFuelMeterEmissionFactor == _newEF.FuelTypeEmissionFactor.IdEmissionFactor)
                        {
                            _found = true;
                            break;
                        }
                    }
                }
                if (!_found)
                {
                    Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor _fuelTypeEmissionFactor = _existingEF.FuelTypeEmissionFactor;
                    if (_fuelTypeEmissionFactor.IsPropietary)
                        _wasteMeterEmissionFactors.Remove(_existingEF.IdFuelMeterEmissionFactor, _fuelTypeEmissionFactor.IdEmissionFactor, _fuelTypeEmissionFactor.IdEmissionFactor);
                }
            }

        }
        private List<Objects.Sites.Meters.Series.FuelDataEmissionFactor> GetNewEmissionFactors(Objects.Sites.Meters.FuelMeter meter, List<Objects.Sites.Meters.Series.FuelDataEmissionFactor> allEmissionFactors)
        {
            List<Objects.Sites.Meters.Series.FuelDataEmissionFactor> _wasteMeterEmissionFactors = new List<Objects.Sites.Meters.Series.FuelDataEmissionFactor>();

            foreach (Objects.Sites.Meters.Series.FuelDataEmissionFactor _newEF in allEmissionFactors)
            {
                if (_newEF.IsNew)
                    _wasteMeterEmissionFactors.Add(_newEF);
                else
                {
                    Boolean _found = false;

                    foreach (Objects.Sites.Meters.EmissionFactors.FuelMeterEmissionFactor _existingEF in meter.GetEmissionFactors().Values)
                    {
                        if (_existingEF.IdFuelMeterEmissionFactor == _newEF.FuelTypeEmissionFactor.IdEmissionFactor)
                        {
                            _found = true;
                            break;
                        }
                    }
                    if (!_found)
                    {
                        _wasteMeterEmissionFactors.Add(_newEF);
                        _found = false;
                    }
                }

            }
            return _wasteMeterEmissionFactors;
        }

        #endregion
    }
}
