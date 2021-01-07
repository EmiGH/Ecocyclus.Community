using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class WasteMeters
    {
        internal WasteMeters()
        {
        }

        #region Read Functions

        internal Library.Objects.Sites.Meters.WasteMeter Item(Int64 idMeter, Security.Credential credential)
        {
            Storage.WasteMeters _dbMeters = new Storage.WasteMeters();
            Library.Objects.Sites.Meters.WasteMeter _meter = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbMeters.ReadById(idMeter, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_meter != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _meter = new Library.Objects.Sites.Meters.WasteMeter(idMeter, Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential);
                        _insert = false;
                    }
                }
                if (_insert)
                    _meter = new Library.Objects.Sites.Meters.WasteMeter(idMeter, Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential);

            }
            return _meter;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Meters.WasteMeter> Items(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Meters.WasteMeter> _oItems = new Dictionary<Int64, Library.Objects.Sites.Meters.WasteMeter>();
            Storage.WasteMeters _dbMeters = new Storage.WasteMeters();
            Library.Objects.Sites.Meters.WasteMeter _meter = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbMeters.ReadAll(idSite, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdSiteWasteMeter"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdSiteWasteMeter"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _meter = new Library.Objects.Sites.Meters.WasteMeter(Convert.ToInt64(_dbRecord["IdSiteWasteMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential);
                    _oItems.Add(_meter.IdMeter, _meter);
                }
                _insert = true;

            }
            return _oItems;
        }

        internal DateTime? LastDate(Int64 idMeter)
        {
            Storage.WasteMeters _dbMeters = new Storage.WasteMeters();
            return _dbMeters.LastDate(idMeter);
        }
        
        #endregion

        #region Write Functions

        internal Library.Objects.Sites.Meters.WasteMeter Add(Int64 idSite, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Int64 idDefaultUnit, List<Objects.Sites.Meters.Series.WasteDataEmissionFactor> emissionFactors, Security.Credential credential)
        {
            try
            {
                Library.Objects.Sites.Meters.WasteMeter _meter;
                using (TransactionScope _scope = new TransactionScope())
                {
                    _meter = Add(idSite, identification, description, descriptionTranslations, idDefaultUnit, credential);

                    //Emission Factors
                    new WasteMeterEmissionFactors().Add(_meter.IdMeter, emissionFactors, credential);

                    _scope.Complete();

                }

                return _meter;
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedMeter);
                else
                    throw sqlex;
            }

        }
        internal Library.Objects.Sites.Meters.WasteMeter Add(Int64 idSite, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Int64 idDefaultUnit, Security.Credential credential)
        {
            Storage.WasteMeters _dbMeters = new Storage.WasteMeters();
            Storage.WasteMeterLanguageOptions _dbLanguageOptions = new Storage.WasteMeterLanguageOptions();
            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            try
            {
                Int64 _idMeter;
                using (TransactionScope _scope = new TransactionScope())
                {
                    //Meter
                    _idMeter = _dbMeters.Create(idSite, _defaultLanguage.IdLanguage, identification, description, idDefaultUnit);

                    //Descriptions
                    foreach (KeyValuePair<String, String> _item in descriptionTranslations)
                    {
                        if (_item.Value != "")
                            _dbLanguageOptions.Create(_idMeter, _item.Key, _item.Value);
                    }
                    _scope.Complete();

                }

                return Item(_idMeter, credential);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedMeter);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Objects.Sites.Meters.WasteMeter meter)
        {
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    //Delete All Series
                    new WasteMeterSeries().RemoveAll(meter.IdMeter);

                    //Delete All Custom Emission Factors
                    WasteMeterEmissionFactors _wasteMeterEmissionFactors = new WasteMeterEmissionFactors();
                    foreach (Objects.Sites.Meters.EmissionFactors.WasteMeterEmissionFactor _emissionFactor in meter.GetEmissionFactors().Values)
                    {
                        Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _wasteTypeEmissionFactor = _emissionFactor.WasteTypeEmissionFactor;
                        _wasteMeterEmissionFactors.Remove(_emissionFactor.IdWasteMeterEmissionFactor, _wasteTypeEmissionFactor.IdWasteTypeEmissionFactor, _wasteTypeEmissionFactor.IdEmissionFactor);
                    }

                    //Delete meter
                    Storage.WasteMeters _dbMeters = new Storage.WasteMeters();
                    _dbMeters.Delete(meter.IdMeter);

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
        internal void Modify(Library.Objects.Sites.Meters.WasteMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Int64 idDefaultUnit, List<Objects.Sites.Meters.Series.WasteDataEmissionFactor> emissionFactors, Security.Credential credential)
        {
            Storage.WasteMeters _dbMeters = new Storage.WasteMeters();
            Storage.WasteMeterLanguageOptions _dbLanguageOptions = new Storage.WasteMeterLanguageOptions();
            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            try
            {
                Int64 _idMeter = meter.IdMeter;

                using (TransactionScope _scope = new TransactionScope())
                {
                    //Emission Factors
                    new WasteMeterEmissionFactors().Modify(meter, emissionFactors, credential);

                    //Meter
                    Modify(meter, identification, description, descriptionTranslations, idDefaultUnit, credential);
                    _scope.Complete();

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
        internal void Modify(Library.Objects.Sites.Meters.WasteMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Int64 idDefaultUnit, Security.Credential credential)
        {
            Storage.WasteMeters _dbMeters = new Storage.WasteMeters();
            Storage.WasteMeterLanguageOptions _dbLanguageOptions = new Storage.WasteMeterLanguageOptions();
            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            try
            {
                Int64 _idMeter = meter.IdMeter;

                using (TransactionScope _scope = new TransactionScope())
                {
                    //Meter
                    _dbMeters.Update(_idMeter, _defaultLanguage.IdLanguage, identification, description, idDefaultUnit);

                    //Descriptions
                    _dbLanguageOptions.DeleteAll(_idMeter);
                    _dbLanguageOptions.Create(_idMeter, _defaultLanguage.IdLanguage, description);

                    foreach (KeyValuePair<String, String> _item in descriptionTranslations)
                    {
                        if (_item.Value != "")
                            _dbLanguageOptions.Create(_idMeter, _item.Key, _item.Value);
                    }
                    _scope.Complete();


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

        #endregion
    }
}
