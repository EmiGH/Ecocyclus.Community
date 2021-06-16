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
    internal class WasteMeterEmissionFactors
    {
         internal WasteMeterEmissionFactors() { }
        
        #region Read Functions

        internal Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor Item(Int64 idWasteMeterEmissionFactor, Security.Credential credential)
        {
            Storage.WasteMeterEmissionFactors _dbEmissionFactors = new Storage.WasteMeterEmissionFactors();
            Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor _wastesMeterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadById(idWasteMeterEmissionFactor, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_wastesMeterEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _wastesMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor(idWasteMeterEmissionFactor, Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _wastesMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor(idWasteMeterEmissionFactor, Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), credential);

            }
            return _wastesMeterEmissionFactor;
        }
        internal Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor Item(Int64 idWasteMeter, Int64 idWasteType, Security.Credential credential)
        {
            Storage.WasteMeterEmissionFactors _dbEmissionFactors = new Storage.WasteMeterEmissionFactors();
            Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor _wastesMeterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbEmissionFactors.ReadById(idWasteMeter, idWasteType, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_wastesMeterEmissionFactor != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _wastesMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor(Convert.ToInt64(_dbRecord["IdWasteMeterEmissionFactor"]), Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _wastesMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor(Convert.ToInt64(_dbRecord["IdWasteMeterEmissionFactor"]), Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), credential);

            }
            return _wastesMeterEmissionFactor;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor> Items(Int64 idWasteMeter, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor> _oItems = new Dictionary<Int64, Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor>();
            Storage.WasteMeterEmissionFactors _dbWasteMeterEmissionFactors = new Storage.WasteMeterEmissionFactors();
            Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor _wasteMeterEmissionFactor = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbWasteMeterEmissionFactors.ReadAll(idWasteMeter, _idLanguage);

            Boolean _insert=true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdWasteMeterEmissionFactor"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdWasteMeterEmissionFactor"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _wasteMeterEmissionFactor = new Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor(Convert.ToInt64(_dbRecord["IdWasteMeterEmissionFactor"]), Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), credential);
                    _oItems.Add(_wasteMeterEmissionFactor.IdWasteMeterEmissionFactor, _wasteMeterEmissionFactor);
                }
                _insert=true;
            }
            return _oItems;
        }

        #endregion

        #region Write Functions

        internal void Add(Int64 idMeter, List<Objects.Sites.Meters.Series.WasteDataEmissionFactor> emissionFactors, Security.Credential credential)
        {
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    foreach (Objects.Sites.Meters.Series.WasteDataEmissionFactor _item in emissionFactors)
                    {
                        Int64 _idWasteTypeEmissionFactor;
                        if (_item.IsNew)
                        {
                            Objects.Sites.Meters.Series.DataEmissionFactor _newEF = _item.NewEmissionFactor;
                            Int64 _idEmissionFactor = new EmissionFactors().Add(_newEF.Country.IdCountry, _newEF.Value, _newEF.Description, credential).IdEmissionFactor;
                            foreach (KeyValuePair<String, String> _description in _newEF.Descriptions)
                            {
                                new EmissionFactorLanguageOptions().Add(_idEmissionFactor, _description.Key, _description.Value);
                            }
                            _idWasteTypeEmissionFactor = new WasteTypeEmissionFactors().Add(_idEmissionFactor, _item.WasteType.IdWasteType, true, credential).IdEmissionFactor;
                        }
                        else
                        {
                            _idWasteTypeEmissionFactor = _item.WasteTypeEmissionFactor.IdEmissionFactor;
                        }
                        new WasteMeterEmissionFactors().Add(idMeter, _idWasteTypeEmissionFactor, credential);
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
        internal Library.Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor Add(Int64 idWasteMeter, Int64 idWasteTypeEmissionFactor, Security.Credential credential)
        {
            Storage.WasteMeterEmissionFactors _dbWasteMeterEmissionFactor = new Storage.WasteMeterEmissionFactors();
            try
            {

                Int64 _idEmissionFactor = _dbWasteMeterEmissionFactor.Create(idWasteMeter, idWasteTypeEmissionFactor);
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
        internal void Remove(Int64 idWasteMeterEmissionFactor, Int64 idWasteTypeEmissionFactor, Int64 idEmissionFactor)
        {
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    new Storage.WasteMeterEmissionFactors().Delete(idWasteMeterEmissionFactor);
                    new WasteTypeEmissionFactors().Remove(idWasteTypeEmissionFactor, idEmissionFactor);

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
        internal void Modify(Objects.Sites.Meters.WasteMeter meter, List<Objects.Sites.Meters.Series.WasteDataEmissionFactor> emissionFactors, Security.Credential credential)
        {
            try
            {
                Int64 _idMeter = meter.IdMeter;

                using (TransactionScope _scope = new TransactionScope())
                {
                    WasteMeterEmissionFactors _wasteMeterEmissionFactors = new WasteMeterEmissionFactors();
                    DeleteUnusedEmissionFactors(meter, emissionFactors);

                    foreach (Objects.Sites.Meters.Series.WasteDataEmissionFactor _item in GetNewEmissionFactors(meter, emissionFactors))
                    {
                        Int64 _idWasteTypeEmissionFactor;
                        if (_item.IsNew)
                        {
                            Objects.Sites.Meters.Series.DataEmissionFactor _newEF = _item.NewEmissionFactor;
                            Int64 _idEmissionFactor = new EmissionFactors().Add(_newEF.Country.IdCountry, _newEF.Value, _newEF.Description, credential).IdEmissionFactor;
                            foreach (KeyValuePair<String, String> _description in _newEF.Descriptions)
                            {
                                new EmissionFactorLanguageOptions().Add(_idEmissionFactor, _description.Key, _description.Value);
                            }
                            _idWasteTypeEmissionFactor = new WasteTypeEmissionFactors().Add(_idEmissionFactor, _item.WasteType.IdWasteType, true, credential).IdEmissionFactor;
                        }
                        else
                        {
                            _idWasteTypeEmissionFactor = _item.WasteTypeEmissionFactor.IdEmissionFactor;
                        }
                        _wasteMeterEmissionFactors.Add(meter.IdMeter, _idWasteTypeEmissionFactor, credential);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DeleteUnusedEmissionFactors(Objects.Sites.Meters.WasteMeter meter, List<Objects.Sites.Meters.Series.WasteDataEmissionFactor> emissionFactors)
        {
            WasteMeterEmissionFactors _wasteMeterEmissionFactors = new WasteMeterEmissionFactors();
            Dictionary<Int64, Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor> _existingEmissionFactors = meter.GetEmissionFactors();
            Boolean _found;

            foreach (Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor _existingEF in _existingEmissionFactors.Values)
            {
                _found = false;
                foreach (Objects.Sites.Meters.Series.WasteDataEmissionFactor _newEF in emissionFactors)
                {
                    if (!_newEF.IsNew)
                    {
                        if (_existingEF.IdWasteMeterEmissionFactor == _newEF.WasteTypeEmissionFactor.IdEmissionFactor)
                        {
                            _found = true;
                            break;
                        }
                    }
                }
                if (!_found)
                {
                    Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _wasteTypeEmissionFactor = _existingEF.WasteTypeEmissionFactor;
                    if (_wasteTypeEmissionFactor.IsPropietary)
                        _wasteMeterEmissionFactors.Remove(_existingEF.IdWasteMeterEmissionFactor, _wasteTypeEmissionFactor.IdEmissionFactor, _wasteTypeEmissionFactor.IdEmissionFactor);
                }
            }

        }
        private List<Objects.Sites.Meters.Series.WasteDataEmissionFactor> GetNewEmissionFactors(Objects.Sites.Meters.WasteMeter meter, List<Objects.Sites.Meters.Series.WasteDataEmissionFactor> allEmissionFactors)
        {
            List<Objects.Sites.Meters.Series.WasteDataEmissionFactor> _wasteMeterEmissionFactors = new List<Objects.Sites.Meters.Series.WasteDataEmissionFactor>();

            foreach (Objects.Sites.Meters.Series.WasteDataEmissionFactor _newEF in allEmissionFactors)
            {
                if (_newEF.IsNew)
                    _wasteMeterEmissionFactors.Add(_newEF);
                else
                {
                    Boolean _found = false;

                    foreach (Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor _existingEF in meter.GetEmissionFactors().Values)
                    {
                        if (_existingEF.IdWasteMeterEmissionFactor == _newEF.WasteTypeEmissionFactor.IdEmissionFactor)
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
