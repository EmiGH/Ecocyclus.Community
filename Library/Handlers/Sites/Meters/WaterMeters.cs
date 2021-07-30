using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class WaterMeters
    {
        internal WaterMeters() 
        {
        }


        #region Read Functions

        internal Library.Objects.Sites.Meters.WaterMeter Item(Int64 idMeter, Security.Credential credential)
        {
            Storage.WaterMeters _dbMeters = new Storage.WaterMeters();
            Library.Objects.Sites.Meters.WaterMeter _meter = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbMeters.ReadById(idMeter, _idLanguage);

            Boolean _insert = true; 
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_meter!=null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _meter = Library.Objects.Sites.Meters.fWaterMeter.CreateMeter(idMeter, Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential);
                        _insert = false;
                    }
                    
                }
                if (_insert)
                    _meter = Library.Objects.Sites.Meters.fWaterMeter.CreateMeter(idMeter, Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential);

                _insert = true;
            }
            return _meter;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Meters.WaterMeter> Items(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Meters.WaterMeter> _oItems = new Dictionary<Int64, Library.Objects.Sites.Meters.WaterMeter>();
            Storage.WaterMeters _dbMeters = new Storage.WaterMeters();
            Library.Objects.Sites.Meters.WaterMeter _meter = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbMeters.ReadAll(idSite, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdSiteWaterMeter"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _meter = Library.Objects.Sites.Meters.fWaterMeter.CreateMeter(Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential);
                    _oItems.Add(_meter.IdMeter, _meter);
                }
                _insert=true;
                
            }
            return _oItems;
        }

        internal Boolean HasValues(Int64 idMeter)
        {
            Storage.WaterMeters _dbMeters = new Storage.WaterMeters();
            return _dbMeters.HasValues(idMeter);
        }
        internal DateTime? NextDate(Int64 idMeter)
        {
            Storage.WaterMeters _dbMeters = new Storage.WaterMeters();
            return _dbMeters.NextDate(idMeter);
        }
        internal DateTime? LastDate(Int64 idMeter)
        {
            Storage.WaterMeters _dbMeters = new Storage.WaterMeters();
            return _dbMeters.LastDate(idMeter);
        }
        internal Double LastReading(Int64 idMeter)
        {
            Storage.WaterMeters _dbMeters = new Storage.WaterMeters();
            return _dbMeters.LastReading(idMeter);
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Sites.Meters.WaterMeter Add(Objects.Sites.Site site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPhysical, DateTime initialDate, Double initialReading, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart, Security.Credential credential)
        {
            Storage.WaterMeters _dbMeters = new Storage.WaterMeters();
            Storage.WaterMeterLanguageOptions _dbLanguageOptions = new Storage.WaterMeterLanguageOptions();
            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            try
            {
                Int64 _idMeter;
                using (TransactionScope _scope = new TransactionScope())
                {
                    //Save Meter
                    _idMeter = _dbMeters.Create(site.IdSite, _defaultLanguage.IdLanguage, identification, description, isPhysical, initialDate, initialReading, idUnit, frequencyQuantity, frequencyUnit, alertBefore, alertAfter, alertOnStart);
                    
                    foreach (KeyValuePair<String, String> _item in descriptionTranslations)
                    {
                        if (_item.Value != "")
                        {
                            _dbLanguageOptions.Create(_idMeter, _item.Key, _item.Value);
                        }
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
        internal Library.Objects.Sites.Meters.WaterMeter Add(Objects.Sites.Site site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPhysical, DateTime initialDate, Double initialReading, Objects.Sites.Meters.Series.WaterDataEmissionFactor ef, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart, Security.Credential credential)
        {
            Storage.WaterMeters _dbMeters = new Storage.WaterMeters();
            Storage.WaterMeterLanguageOptions _dbLanguageOptions = new Storage.WaterMeterLanguageOptions();
            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            try
            {
                Int64 _idMeter;

                using (TransactionScope _scope = new TransactionScope())
                {
                    //Save Emission Factor
                    Int64 _idEmissionFactor;
                    if (ef.IsNew)
                    {
                        //If not exists then create a propietary one
                        Objects.Sites.Meters.Series.DataEmissionFactor _newEmissionFactor = ef.NewEmissionFactor;
                        _idEmissionFactor = new EmissionFactors().Add(_newEmissionFactor.Country.IdCountry, _newEmissionFactor.Value, _newEmissionFactor.Description, credential).IdEmissionFactor;
                        foreach (KeyValuePair<String, String> _item in _newEmissionFactor.Descriptions)
                        {
                            new EmissionFactorLanguageOptions().Add(_idEmissionFactor, _item.Key, _item.Value);
                        }
                        new WaterEmissionFactors().Add(_idEmissionFactor, true, credential);
                    }
                    else
                        _idEmissionFactor = ef.EmissionFactor.IdEmissionFactor;

                    //Save Meter
                    _idMeter = _dbMeters.Create(site.IdSite, _defaultLanguage.IdLanguage, identification, description, isPhysical, initialDate, initialReading, _idEmissionFactor, idUnit, frequencyQuantity, frequencyUnit, alertBefore, alertAfter, alertOnStart);


                    foreach (KeyValuePair<String, String> _item in descriptionTranslations)
                    {
                        if (_item.Value != "")
                        {
                            _dbLanguageOptions.Create(_idMeter, _item.Key, _item.Value);
                        }
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
        internal void Remove(Objects.Sites.Meters.WaterMeter meter)
        {
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    //Delete All Series
                    new WaterMeterLoads().Remove(meter, DateTime.MinValue);
                    
                    //Delete All Custom Emission Factors
                    Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _emissionFactor = meter.EmissionFactor;
                    if (_emissionFactor.IsPropietary)
                        new WaterEmissionFactors().Remove(_emissionFactor.IdEmissionFactor, _emissionFactor.IdEmissionFactor);

                    Storage.WaterMeters _dbMeters = new Storage.WaterMeters();
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
        internal void Modify(Objects.Sites.Meters.WaterMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Objects.Sites.Meters.Series.WaterDataEmissionFactor ef, Objects.Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart, Security.Credential credential)
        {
            if (HasValues(meter.IdMeter))
                if (meter.DefaultUnit.Magnitude.IdMagnitude != unit.Magnitude.IdMagnitude)
                    throw new ApplicationException(Resources.Messages.ErrorCannotChangeMagnitude);
            
            Storage.WaterMeters _dbMeters = new Storage.WaterMeters();
            Storage.WaterMeterLanguageOptions _dbLanguageOptions = new Storage.WaterMeterLanguageOptions();
            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            Int64 _idMeter = meter.IdMeter;
            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            Int64 _newIdWaterEmissionFactor;
            Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _previousWaterEmissionFactor = meter.EmissionFactor;

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    //Save Emission Factor
                    if (ef.IsNew)
                    {
                        //try to delete previous one
                        if(_previousWaterEmissionFactor.IsPropietary)
                            new WaterEmissionFactors().Remove(_previousWaterEmissionFactor.IdEmissionFactor, _previousWaterEmissionFactor.IdEmissionFactor);

                        //create a new propietary one
                        Objects.Sites.Meters.Series.DataEmissionFactor _newEmissionFactor = ef.NewEmissionFactor;
                        _newIdWaterEmissionFactor = new EmissionFactors().Add(_newEmissionFactor.Country.IdCountry, _newEmissionFactor.Value, _newEmissionFactor.Description, credential).IdEmissionFactor;
                        foreach (KeyValuePair<String, String> _item in _newEmissionFactor.Descriptions)
                        {
                            new EmissionFactorLanguageOptions().Add(_newIdWaterEmissionFactor, _item.Key, _item.Value);
                        }
                        new WaterEmissionFactors().Add(_newIdWaterEmissionFactor, true, credential);
                    }
                    else
                    {
                        _newIdWaterEmissionFactor = ef.EmissionFactor.IdEmissionFactor;

                        //If ef is changed then try to delete previous one
                        if (_newIdWaterEmissionFactor != _previousWaterEmissionFactor.IdEmissionFactor)
                            new WaterEmissionFactors().Remove(_previousWaterEmissionFactor.IdEmissionFactor, meter.EmissionFactor.IdEmissionFactor);
                    }

                    //Language Options
                    _dbLanguageOptions.DeleteAll(_idMeter);

                    //Update Meter
                    _dbMeters.Update(_idMeter, _defaultLanguage.IdLanguage, identification, description, _newIdWaterEmissionFactor, unit.IdUnit, frequencyQuantity, frequencyUnit, alertBefore, alertAfter, alertOnStart);
                    
                    //New Translations
                    _dbLanguageOptions.Create(_idMeter, _defaultLanguage.IdLanguage, description);
                    foreach (KeyValuePair<String, String> _item in descriptionTranslations)
                    {
                        if (_item.Value != "")
                        {
                            _dbLanguageOptions.Create(_idMeter, _item.Key, _item.Value);
                        }
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
        internal void Modify(Objects.Sites.Meters.WaterMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Objects.Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart, Security.Credential credential)
        {
            if (HasValues(meter.IdMeter))
                if (meter.DefaultUnit.Magnitude.IdMagnitude != unit.Magnitude.IdMagnitude)
                    throw new ApplicationException(Resources.Messages.ErrorCannotChangeMagnitude);

            Storage.WaterMeters _dbMeters = new Storage.WaterMeters();
            Storage.WaterMeterLanguageOptions _dbLanguageOptions = new Storage.WaterMeterLanguageOptions();
            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            Int64 _idMeter = meter.IdMeter;
            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    //Language Options
                    _dbLanguageOptions.DeleteAll(_idMeter);

                    //Update Meter
                    _dbMeters.Update(_idMeter, _defaultLanguage.IdLanguage, identification, description, unit.IdUnit, frequencyQuantity, frequencyUnit, alertBefore, alertAfter, alertOnStart);

                    //New Translations
                    _dbLanguageOptions.Create(_idMeter, _defaultLanguage.IdLanguage, description);
                    foreach (KeyValuePair<String, String> _item in descriptionTranslations)
                    {
                        if (_item.Value != "")
                        {
                            _dbLanguageOptions.Create(_idMeter, _item.Key, _item.Value);
                        }
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
        internal void Modify(Objects.Sites.Meters.WaterMeterPhysical meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, DateTime initialDate, Double initialReading, Objects.Sites.Meters.Series.WaterDataEmissionFactor ef, Objects.Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart, Security.Credential credential)
        {
            if (HasValues(meter.IdMeter))
                if (meter.DefaultUnit.Magnitude.IdMagnitude != unit.Magnitude.IdMagnitude)
                    throw new ApplicationException(Resources.Messages.ErrorCannotChangeMagnitude);

            if (meter.HasValue() && meter.InitialDate != initialDate)
                throw new ApplicationException(Resources.Messages.ErrorCannotChangeInitialDate);

            Storage.WaterMeters _dbMeters = new Storage.WaterMeters();
            Storage.WaterMeterLanguageOptions _dbLanguageOptions = new Storage.WaterMeterLanguageOptions();
            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            Int64 _idMeter = meter.IdMeter;
            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            Int64 _newIdWaterEmissionFactor;
            Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _previousWaterEmissionFactor = meter.EmissionFactor;

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    //Save Emission Factor
                    if (ef.IsNew)
                    {
                        //try to delete previous one
                        if (_previousWaterEmissionFactor.IsPropietary)
                            new WaterEmissionFactors().Remove(_previousWaterEmissionFactor.IdEmissionFactor, _previousWaterEmissionFactor.IdEmissionFactor);

                        //create a new propietary one
                        Objects.Sites.Meters.Series.DataEmissionFactor _newEmissionFactor = ef.NewEmissionFactor;
                        _newIdWaterEmissionFactor = new EmissionFactors().Add(_newEmissionFactor.Country.IdCountry, _newEmissionFactor.Value, _newEmissionFactor.Description, credential).IdEmissionFactor;
                        foreach (KeyValuePair<String, String> _item in _newEmissionFactor.Descriptions)
                        {
                            new EmissionFactorLanguageOptions().Add(_newIdWaterEmissionFactor, _item.Key, _item.Value);
                        }
                        new WaterEmissionFactors().Add(_newIdWaterEmissionFactor, true, credential);
                    }
                    else
                    {
                        _newIdWaterEmissionFactor = ef.EmissionFactor.IdEmissionFactor;

                        //If ef is changed then try to delete previous one
                        if (_newIdWaterEmissionFactor != _previousWaterEmissionFactor.IdEmissionFactor)
                            new WaterEmissionFactors().Remove(_previousWaterEmissionFactor.IdEmissionFactor, meter.EmissionFactor.IdEmissionFactor);
                    }

                    //Language Options
                    _dbLanguageOptions.DeleteAll(_idMeter);
                    
                    //Check initial reading before change
                    Double _oldReading = meter.InitialReading;
                    
                    //Update Meter
                    _dbMeters.Update(_idMeter, _defaultLanguage.IdLanguage, identification, description, initialDate, initialReading, _newIdWaterEmissionFactor, unit.IdUnit, frequencyQuantity, frequencyUnit, alertBefore, alertAfter, alertOnStart);
                    
                    //Update first load if change initial reading
                    if (_oldReading != initialReading)
                    {
                        WaterMeterLoads _loads = new WaterMeterLoads();
                        Objects.Sites.Meters.Series.WaterLoad _first = _loads.ReadFirstLoad(_idMeter, credential);
                        if(_first!=null)
                            _loads.Modify(meter, _first, initialReading, _first.ValueInput, _first.Unit.IdUnit, credential);
                    }

                    //New Translations
                    _dbLanguageOptions.Create(_idMeter, _defaultLanguage.IdLanguage, description);
                    foreach (KeyValuePair<String, String> _item in descriptionTranslations)
                    {
                        if (_item.Value != "")
                        {
                            _dbLanguageOptions.Create(_idMeter, _item.Key, _item.Value);
                        }
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
        internal void Modify(Objects.Sites.Meters.WaterMeterPhysical meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, DateTime initialDate, Double initialReading, Objects.Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart, Security.Credential credential)
        {
            //Check Magnitude
            if (HasValues(meter.IdMeter))
                if (meter.DefaultUnit.Magnitude.IdMagnitude != unit.Magnitude.IdMagnitude)
                    throw new ApplicationException(Resources.Messages.ErrorCannotChangeMagnitude);

            if (meter.HasValue() && meter.InitialDate != initialDate)
                throw new ApplicationException(Resources.Messages.ErrorCannotChangeInitialDate);

            Storage.WaterMeters _dbMeters = new Storage.WaterMeters();
            Storage.WaterMeterLanguageOptions _dbLanguageOptions = new Storage.WaterMeterLanguageOptions();
            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            Int64 _idMeter = meter.IdMeter;

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {                   
                    //Language Options
                    _dbLanguageOptions.DeleteAll(_idMeter);

                    //Check initial reading before change
                    Double _oldReading = meter.InitialReading;
                    
                    //Update Meter
                    _dbMeters.Update(_idMeter, _defaultLanguage.IdLanguage, identification, description, initialDate, initialReading, unit.IdUnit, frequencyQuantity, frequencyUnit, alertBefore, alertAfter, alertOnStart);

                    //Update first load if change initial reading
                    if (_oldReading != initialReading)
                    {
                        WaterMeterLoads _loads = new WaterMeterLoads();
                        Objects.Sites.Meters.Series.WaterLoad _first = _loads.ReadFirstLoad(_idMeter, credential);
                        if(_first!=null)
                            _loads.Modify(meter, _first, initialReading, _first.ValueInput, _first.Unit.IdUnit, credential);
                    }

                    //New Translations
                    _dbLanguageOptions.Create(_idMeter, _defaultLanguage.IdLanguage, description);
                    foreach (KeyValuePair<String, String> _item in descriptionTranslations)
                    {
                        if (_item.Value != "")
                        {
                            _dbLanguageOptions.Create(_idMeter, _item.Key, _item.Value);
                        }
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
