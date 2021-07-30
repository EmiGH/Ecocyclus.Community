using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class TransportMeters
    {
        internal TransportMeters()
        {
        }

        #region Read Functions

        internal Library.Objects.Sites.Meters.TransportMeter Item(Int64 idMeter, Security.Credential credential)
        {
            Storage.TransportMeters _dbMeters = new Storage.TransportMeters();
            Library.Objects.Sites.Meters.TransportMeter _meter = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbMeters.ReadById(idMeter, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_meter != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _meter = new Library.Objects.Sites.Meters.TransportMeter(idMeter, Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential);
                        _insert = false;
                    }
                }
                if (_insert)
                    _meter = new Library.Objects.Sites.Meters.TransportMeter(idMeter, Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential);

                _insert = true;
            }
            return _meter;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Meters.TransportMeter> Items(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Meters.TransportMeter> _oItems = new Dictionary<Int64, Library.Objects.Sites.Meters.TransportMeter>();
            Storage.TransportMeters _dbMeters = new Storage.TransportMeters();
            Library.Objects.Sites.Meters.TransportMeter _meter = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbMeters.ReadAll(idSite, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdSiteTransportMeter"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdSiteTransportMeter"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _meter = new Library.Objects.Sites.Meters.TransportMeter(Convert.ToInt64(_dbRecord["IdSiteTransportMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(_dbRecord["IdDefaultUnit"]), credential);
                    _oItems.Add(_meter.IdMeter, _meter);
                }
                _insert = true;

            }
            return _oItems;
        }

        internal DateTime? LastDate(Int64 idMeter)
        {
            Storage.TransportMeters _dbMeters = new Storage.TransportMeters();
            return _dbMeters.LastDate(idMeter);
        }


        #endregion

        #region Write Functions

        internal Library.Objects.Sites.Meters.TransportMeter Add(Int64 idSite, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Int64 idDefaultUnit, List<Objects.Sites.Meters.Series.TransportDataEmissionFactor> emissionFactors, Security.Credential credential)
        {
            try
            {
                Library.Objects.Sites.Meters.TransportMeter _meter;
                using (TransactionScope _scope = new TransactionScope())
                {
                    _meter = Add(idSite, identification, description, descriptionTranslations, idDefaultUnit, credential);

                    //Emission Factors
                    new TransportMeterEmissionFactors().Add(_meter.IdMeter, emissionFactors, credential);

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
        internal Library.Objects.Sites.Meters.TransportMeter Add(Int64 idSite, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Int64 idDefaultUnit, Security.Credential credential)
        {
            Storage.TransportMeters _dbMeters = new Storage.TransportMeters();
            Storage.TransportMeterLanguageOptions _dbLanguageOptions = new Storage.TransportMeterLanguageOptions();
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
        internal void Remove(Objects.Sites.Meters.TransportMeter meter)
        {
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    //Delete All Series
                    new TransportMeterSeries().RemoveAll(meter.IdMeter);

                    //Delete All Custom Emission Factors
                    TransportMeterEmissionFactors _transportMeterEmissionFactors = new TransportMeterEmissionFactors();
                    foreach (Objects.Sites.Meters.EmissionFactors.TransportMeterEmissionFactor _emissionFactor in meter.GetEmissionFactors().Values)
                    {
                        Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor _transportTypeEmissionFactor = _emissionFactor.TransportTypeEmissionFactor;
                        _transportMeterEmissionFactors.Remove(_emissionFactor.IdTransportMeterEmissionFactor, _transportTypeEmissionFactor.IdTransportTypeEmissionFactor, _transportTypeEmissionFactor.IdEmissionFactor);
                    }

                    //Delete meter
                    Storage.TransportMeters _dbMeters = new Storage.TransportMeters();
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
        internal void Modify(Library.Objects.Sites.Meters.TransportMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Int64 idDefaultUnit, List<Objects.Sites.Meters.Series.TransportDataEmissionFactor> emissionFactors, Security.Credential credential)
        {
            Storage.TransportMeters _dbMeters = new Storage.TransportMeters();
            Storage.TransportMeterLanguageOptions _dbLanguageOptions = new Storage.TransportMeterLanguageOptions();
            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            try
            {
                Int64 _idMeter = meter.IdMeter;

                using (TransactionScope _scope = new TransactionScope())
                {
                    //Emission Factors
                    new TransportMeterEmissionFactors().Modify(meter, emissionFactors, credential);

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
        internal void Modify(Library.Objects.Sites.Meters.TransportMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Int64 idDefaultUnit, Security.Credential credential)
        {
            Storage.TransportMeters _dbMeters = new Storage.TransportMeters();
            Storage.TransportMeterLanguageOptions _dbLanguageOptions = new Storage.TransportMeterLanguageOptions();
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
