using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class TransportMeterEmissionFactors
    {
        internal TransportMeterEmissionFactors() { }

        #region Read Functions

        internal Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor Item(Int64 idTransportMeterEmissionFactor, Security.Credential credential)
        {
            Storage.TransportMeterEmissionFactors _dbEmissionFactors = new Storage.TransportMeterEmissionFactors();
            Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor _transportsMeterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadById(idTransportMeterEmissionFactor, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_transportsMeterEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _transportsMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor(idTransportMeterEmissionFactor, Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _transportsMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor(idTransportMeterEmissionFactor, Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), credential);

                _insert = true;
            }
            return _transportsMeterEmissionFactor;
        }
        internal Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor Item(Int64 idTransportMeter, Int64 idTransportType, Security.Credential credential)
        {
            Storage.TransportMeterEmissionFactors _dbEmissionFactors = new Storage.TransportMeterEmissionFactors();
            Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor _transportsMeterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadById(idTransportMeter, idTransportType, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_transportsMeterEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _transportsMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor(Convert.ToInt64(_dbRecord["IdTransportMeterEmissionFactor"]), Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _transportsMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor(Convert.ToInt64(_dbRecord["IdTransportMeterEmissionFactor"]), Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), credential);

                _insert = true;
            }
            return _transportsMeterEmissionFactor;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor> Items(Int64 idTransportMeter, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor>();
            Storage.TransportMeterEmissionFactors _dbTransportMeterEmissionFactors = new Storage.TransportMeterEmissionFactors();
            Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor _transportMeterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbTransportMeterEmissionFactors.ReadAll(idTransportMeter, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdTransportMeterEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdTransportMeterEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _transportMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor(Convert.ToInt64(_dbRecord["IdTransportMeterEmissionFactor"]), Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), credential);
                    _oItems.Add(_transportMeterEmissionFactor.IdTransportMeterEmissionFactor, _transportMeterEmissionFactor);
                }
                _insert = true;
            }
            return _oItems;
        }
        
        #endregion

        #region Write Functions

        internal void Add(Int64 idMeter, List<Objects.Sites.Meters.Series.TransportDataEmissionFactor> emissionFactors, Security.Credential credential)
        {
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    foreach (Objects.Sites.Meters.Series.TransportDataEmissionFactor _item in emissionFactors)
                    {
                        Int64 _idTransportTypeEmissionFactor;
                        if (_item.IsNew)
                        {
                            Objects.Sites.Meters.Series.DataEmissionFactor _newEF = _item.NewEmissionFactor;
                            Int64 _idEmissionFactor = new EmissionFactors().Add(_newEF.Country.IdCountry, _newEF.Value, _newEF.Description, credential).IdEmissionFactor;
                            foreach (KeyValuePair<String, String> _description in _newEF.Descriptions)
                            {
                                new EmissionFactorLanguageOptions().Add(_idEmissionFactor, _description.Key, _description.Value);
                            }
                            _idTransportTypeEmissionFactor = new TransportTypeEmissionFactors().Add(_idEmissionFactor, _item.TransportType.IdTransportType, true, credential).IdEmissionFactor;
                        }
                        else
                        {
                            _idTransportTypeEmissionFactor = _item.TransportTypeEmissionFactor.IdEmissionFactor;
                        }
                        new TransportMeterEmissionFactors().Add(idMeter, _idTransportTypeEmissionFactor, credential);
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
        internal Library.Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor Add(Int64 idTransportMeter, Int64 idTransportTypeEmissionFactor, Security.Credential credential)
        {
            Storage.TransportMeterEmissionFactors _dbTransportMeterEmissionFactor = new Storage.TransportMeterEmissionFactors();
            try
            {

                Int64 _idEmissionFactor = _dbTransportMeterEmissionFactor.Create(idTransportMeter, idTransportTypeEmissionFactor);
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
        internal void Remove(Int64 idTransportMeterEmissionFactor, Int64 idTransportTypeEmissionFactor, Int64 idEmissionFactor)
        {
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    new Storage.TransportMeterEmissionFactors().Delete(idTransportMeterEmissionFactor);
                    new TransportTypeEmissionFactors().Remove(idTransportTypeEmissionFactor, idEmissionFactor);

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
        internal void Modify(Objects.Sites.Meters.TransportMeter meter, List<Objects.Sites.Meters.Series.TransportDataEmissionFactor> emissionFactors, Security.Credential credential)
        {
            try
            {
                Int64 _idMeter = meter.IdMeter;

                using (TransactionScope _scope = new TransactionScope())
                {
                    TransportMeterEmissionFactors _wasteMeterEmissionFactors = new TransportMeterEmissionFactors();
                    DeleteUnusedEmissionFactors(meter, emissionFactors);

                    foreach (Objects.Sites.Meters.Series.TransportDataEmissionFactor _item in GetNewEmissionFactors(meter, emissionFactors))
                    {
                        Int64 _idTransportTypeEmissionFactor;
                        if (_item.IsNew)
                        {
                            Objects.Sites.Meters.Series.DataEmissionFactor _newEF = _item.NewEmissionFactor;
                            Int64 _idEmissionFactor = new EmissionFactors().Add(_newEF.Country.IdCountry, _newEF.Value, _newEF.Description, credential).IdEmissionFactor;
                            foreach (KeyValuePair<String, String> _description in _newEF.Descriptions)
                            {
                                new EmissionFactorLanguageOptions().Add(_idEmissionFactor, _description.Key, _description.Value);
                            }
                            _idTransportTypeEmissionFactor = new TransportTypeEmissionFactors().Add(_idEmissionFactor, _item.TransportType.IdTransportType, true, credential).IdEmissionFactor;
                        }
                        else
                        {
                            _idTransportTypeEmissionFactor = _item.TransportTypeEmissionFactor.IdEmissionFactor;
                        }
                        _wasteMeterEmissionFactors.Add(meter.IdMeter, _idTransportTypeEmissionFactor, credential);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DeleteUnusedEmissionFactors(Objects.Sites.Meters.TransportMeter meter, List<Objects.Sites.Meters.Series.TransportDataEmissionFactor> emissionFactors)
        {
            TransportMeterEmissionFactors _wasteMeterEmissionFactors = new TransportMeterEmissionFactors();
            Dictionary<Int64, Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor> _existingEmissionFactors = meter.GetEmissionFactors();
            Boolean _found;

            foreach (Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor _existingEF in _existingEmissionFactors.Values)
            {
                _found = false;
                foreach (Objects.Sites.Meters.Series.TransportDataEmissionFactor _newEF in emissionFactors)
                {
                    if (!_newEF.IsNew)
                    {
                        if (_existingEF.IdTransportMeterEmissionFactor == _newEF.TransportTypeEmissionFactor.IdEmissionFactor)
                        {
                            _found = true;
                            break;
                        }
                    }
                }
                if (!_found)
                {
                    Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor _transportTypeEmissionFactor = _existingEF.TransportTypeEmissionFactor;
                    if (_transportTypeEmissionFactor.IsPropietary)
                        _wasteMeterEmissionFactors.Remove(_existingEF.IdTransportMeterEmissionFactor, _transportTypeEmissionFactor.IdEmissionFactor, _transportTypeEmissionFactor.IdEmissionFactor);
                }
            }

        }
        private List<Objects.Sites.Meters.Series.TransportDataEmissionFactor> GetNewEmissionFactors(Objects.Sites.Meters.TransportMeter meter, List<Objects.Sites.Meters.Series.TransportDataEmissionFactor> allEmissionFactors)
        {
            List<Objects.Sites.Meters.Series.TransportDataEmissionFactor> _wasteMeterEmissionFactors = new List<Objects.Sites.Meters.Series.TransportDataEmissionFactor>();

            foreach (Objects.Sites.Meters.Series.TransportDataEmissionFactor _newEF in allEmissionFactors)
            {
                if (_newEF.IsNew)
                    _wasteMeterEmissionFactors.Add(_newEF);
                else
                {
                    Boolean _found = false;

                    foreach (Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor _existingEF in meter.GetEmissionFactors().Values)
                    {
                        if (_existingEF.IdTransportMeterEmissionFactor == _newEF.TransportTypeEmissionFactor.IdEmissionFactor)
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
