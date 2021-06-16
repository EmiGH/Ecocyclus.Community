using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class ElectricityMeters
    {
        internal ElectricityMeters() 
        {
        }
        
        #region Read Functions

        internal Library.Objects.Sites.Meters.ElectricityMeter Item(Int64 idMeter, Security.Credential credential)
        {
            Storage.ElectricityMeters _dbMeters = new Storage.ElectricityMeters();
            Library.Objects.Sites.Meters.ElectricityMeter _meter = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbMeters.ReadById(idMeter, _idLanguage);

            Boolean _insert = true; 
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_meter!=null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _meter = Library.Objects.Sites.Meters.fElectricityMeter.CreateMeter(idMeter, Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential);
                        _insert = false;
                    }
                    
                }
                if (_insert)
                    _meter = Library.Objects.Sites.Meters.fElectricityMeter.CreateMeter(idMeter, Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential);

                _insert = true;
            }
            return _meter;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Meters.ElectricityMeter> Items(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Meters.ElectricityMeter> _oItems = new Dictionary<Int64, Library.Objects.Sites.Meters.ElectricityMeter>();
            Storage.ElectricityMeters _dbMeters = new Storage.ElectricityMeters();
            Library.Objects.Sites.Meters.ElectricityMeter _meter = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbMeters.ReadAll(idSite, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdSiteElectricityMeter"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdSiteElectricityMeter"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _meter = Library.Objects.Sites.Meters.fElectricityMeter.CreateMeter(Convert.ToInt64(_dbRecord["IdSiteElectricityMeter"]), Convert.ToInt64(_dbRecord["IdSite"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToString(_dbRecord["Description"]), Convert.ToDateTime(Common.CastNullValues(_dbRecord["InitialDate"], DateTime.MinValue)), Convert.ToDouble(Common.CastNullValues(_dbRecord["InitialReading"], -1)), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdEmissionFactor"], 0)), Convert.ToInt16(_dbRecord["IdUnit"]), Convert.ToBoolean(_dbRecord["IsPhysicalMeter"]), Convert.ToInt16(_dbRecord["FrequencyQuantity"]), Convert.ToInt16(_dbRecord["FrequencyUnit"]), Convert.ToInt16(_dbRecord["AlertBeforeDays"]), Convert.ToInt16(_dbRecord["AlertAfterDays"]), Convert.ToBoolean(_dbRecord["AlertOnStart"]), credential);
                    _oItems.Add(_meter.IdMeter, _meter);
                }
                _insert=true;
                
            }
            return _oItems;
        }

        internal Boolean HasValues(Int64 idMeter)
        {
            Storage.ElectricityMeters _dbMeters = new Storage.ElectricityMeters();
            return _dbMeters.HasValues(idMeter);
        }
        internal DateTime? NextDate(Int64 idMeter)
        {
            Storage.ElectricityMeters _dbMeters = new Storage.ElectricityMeters();
            return _dbMeters.NextDate(idMeter);
        }
        internal DateTime? LastDate(Int64 idMeter)
        {
            Storage.ElectricityMeters _dbMeters = new Storage.ElectricityMeters();
            return _dbMeters.LastDate(idMeter);
        }
        internal Double LastReading(Int64 idMeter)
        {
            Storage.ElectricityMeters _dbMeters = new Storage.ElectricityMeters();
            return _dbMeters.LastReading(idMeter);
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Sites.Meters.ElectricityMeter Add(Objects.Sites.Site site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPhysical, DateTime initialDate, Double initialReading, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart, Security.Credential credential)
        {
            Storage.ElectricityMeters _dbMeters = new Storage.ElectricityMeters();
            Storage.ElectricityMeterLanguageOptions _dbLanguageOptions = new Storage.ElectricityMeterLanguageOptions();
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
        internal Library.Objects.Sites.Meters.ElectricityMeter Add(Objects.Sites.Site site, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPhysical, DateTime initialDate, Double initialReading, Objects.Sites.Meters.Series.ElectricityDataEmissionFactor ef, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart, Security.Credential credential)
        {
            Storage.ElectricityMeters _dbMeters = new Storage.ElectricityMeters();
            Storage.ElectricityMeterLanguageOptions _dbLanguageOptions = new Storage.ElectricityMeterLanguageOptions();
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
                        new ElectricityEmissionFactors().Add(_idEmissionFactor, true, credential);
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
        internal void Remove(Objects.Sites.Meters.ElectricityMeter meter)
        {
            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    //Delete All Series
                    new ElectricityMeterLoads().Remove(meter, DateTime.MinValue);
                    
                    //Delete All Custom Emission Factors
                    Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor _emissionFactor = meter.EmissionFactor;
                    if (_emissionFactor.IsPropietary)
                        new ElectricityEmissionFactors().Remove(_emissionFactor.IdEmissionFactor, _emissionFactor.IdEmissionFactor);

                    Storage.ElectricityMeters _dbMeters = new Storage.ElectricityMeters();
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
        internal void Modify(Objects.Sites.Meters.ElectricityMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Objects.Sites.Meters.Series.ElectricityDataEmissionFactor ef, Objects.Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart, Security.Credential credential)
        {
            if (HasValues(meter.IdMeter))
                if (meter.DefaultUnit.Magnitude.IdMagnitude != unit.Magnitude.IdMagnitude)
                    throw new ApplicationException(Resources.Messages.ErrorCannotChangeMagnitude);
            
            Storage.ElectricityMeters _dbMeters = new Storage.ElectricityMeters();
            Storage.ElectricityMeterLanguageOptions _dbLanguageOptions = new Storage.ElectricityMeterLanguageOptions();
            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            Int64 _idMeter = meter.IdMeter;
            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            Int64 _newIdElectricityEmissionFactor;
            Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor _previousElectricityEmissionFactor = meter.EmissionFactor;

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    //Save Emission Factor
                    if (ef.IsNew)
                    {
                        //try to delete previous one
                        if(_previousElectricityEmissionFactor.IsPropietary)
                            new ElectricityEmissionFactors().Remove(_previousElectricityEmissionFactor.IdEmissionFactor, _previousElectricityEmissionFactor.IdEmissionFactor);

                        //create a new propietary one
                        Objects.Sites.Meters.Series.DataEmissionFactor _newEmissionFactor = ef.NewEmissionFactor;
                        _newIdElectricityEmissionFactor = new EmissionFactors().Add(_newEmissionFactor.Country.IdCountry, _newEmissionFactor.Value, _newEmissionFactor.Description, credential).IdEmissionFactor;
                        foreach (KeyValuePair<String, String> _item in _newEmissionFactor.Descriptions)
                        {
                            new EmissionFactorLanguageOptions().Add(_newIdElectricityEmissionFactor, _item.Key, _item.Value);
                        }
                        new ElectricityEmissionFactors().Add(_newIdElectricityEmissionFactor, true, credential);
                    }
                    else
                    {
                        _newIdElectricityEmissionFactor = ef.EmissionFactor.IdEmissionFactor;

                        //If ef is changed then try to delete previous one
                        if (_newIdElectricityEmissionFactor != _previousElectricityEmissionFactor.IdEmissionFactor)
                            new ElectricityEmissionFactors().Remove(_previousElectricityEmissionFactor.IdEmissionFactor, meter.EmissionFactor.IdEmissionFactor);
                    }

                    //Language Options
                    _dbLanguageOptions.DeleteAll(_idMeter);

                    //Update Meter
                    _dbMeters.Update(_idMeter, _defaultLanguage.IdLanguage, identification, description, _newIdElectricityEmissionFactor, unit.IdUnit, frequencyQuantity, frequencyUnit, alertBefore, alertAfter, alertOnStart);
                    
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
        internal void Modify(Objects.Sites.Meters.ElectricityMeter meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, Objects.Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart, Security.Credential credential)
        {
            if (HasValues(meter.IdMeter))
                if (meter.DefaultUnit.Magnitude.IdMagnitude != unit.Magnitude.IdMagnitude)
                    throw new ApplicationException(Resources.Messages.ErrorCannotChangeMagnitude);

            Storage.ElectricityMeters _dbMeters = new Storage.ElectricityMeters();
            Storage.ElectricityMeterLanguageOptions _dbLanguageOptions = new Storage.ElectricityMeterLanguageOptions();
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
        internal void Modify(Objects.Sites.Meters.ElectricityMeterPhysical meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, DateTime initialDate, Double initialReading, Objects.Sites.Meters.Series.ElectricityDataEmissionFactor ef, Objects.Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart, Security.Credential credential)
        {
            if (HasValues(meter.IdMeter))
                if (meter.DefaultUnit.Magnitude.IdMagnitude != unit.Magnitude.IdMagnitude)
                    throw new ApplicationException(Resources.Messages.ErrorCannotChangeMagnitude);

            if(meter.HasValue() && meter.InitialDate != initialDate)
                throw new ApplicationException(Resources.Messages.ErrorCannotChangeInitialDate);

            Storage.ElectricityMeters _dbMeters = new Storage.ElectricityMeters();
            Storage.ElectricityMeterLanguageOptions _dbLanguageOptions = new Storage.ElectricityMeterLanguageOptions();
            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            Int64 _idMeter = meter.IdMeter;
            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            Int64 _newIdElectricityEmissionFactor;
            Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor _previousElectricityEmissionFactor = meter.EmissionFactor;

            try
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    //Save Emission Factor
                    if (ef.IsNew)
                    {
                        //try to delete previous one
                        if (_previousElectricityEmissionFactor.IsPropietary)
                            new ElectricityEmissionFactors().Remove(_previousElectricityEmissionFactor.IdEmissionFactor, _previousElectricityEmissionFactor.IdEmissionFactor);

                        //create a new propietary one
                        Objects.Sites.Meters.Series.DataEmissionFactor _newEmissionFactor = ef.NewEmissionFactor;
                        _newIdElectricityEmissionFactor = new EmissionFactors().Add(_newEmissionFactor.Country.IdCountry, _newEmissionFactor.Value, _newEmissionFactor.Description, credential).IdEmissionFactor;
                        foreach (KeyValuePair<String, String> _item in _newEmissionFactor.Descriptions)
                        {
                            new EmissionFactorLanguageOptions().Add(_newIdElectricityEmissionFactor, _item.Key, _item.Value);
                        }
                        new ElectricityEmissionFactors().Add(_newIdElectricityEmissionFactor, true, credential);
                    }
                    else
                    {
                        _newIdElectricityEmissionFactor = ef.EmissionFactor.IdEmissionFactor;

                        //If ef is changed then try to delete previous one
                        if (_newIdElectricityEmissionFactor != _previousElectricityEmissionFactor.IdEmissionFactor)
                            new ElectricityEmissionFactors().Remove(_previousElectricityEmissionFactor.IdEmissionFactor, meter.EmissionFactor.IdEmissionFactor);
                    }

                    //Language Options
                    _dbLanguageOptions.DeleteAll(_idMeter);
                    
                    //Check initial reading before change
                    Double _oldReading = meter.InitialReading;
                                        
                    //Update Meter
                    _dbMeters.Update(_idMeter, _defaultLanguage.IdLanguage, identification, description, initialDate, initialReading, _newIdElectricityEmissionFactor, unit.IdUnit, frequencyQuantity, frequencyUnit, alertBefore, alertAfter, alertOnStart);
                    
                    //Update first load if change initial reading
                    if (_oldReading != initialReading)
                    {
                        ElectricityMeterLoads _loads = new ElectricityMeterLoads();
                        Objects.Sites.Meters.Series.ElectricityLoad _first = _loads.ReadFirstLoad(_idMeter, credential);
                        if (_first != null)
                        {
                            _loads.Modify(meter, _first, initialReading, _first.ValueInput, _first.Unit.IdUnit, credential);
                        }
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
        internal void Modify(Objects.Sites.Meters.ElectricityMeterPhysical meter, String identification, String description, List<KeyValuePair<String, String>> descriptionTranslations, DateTime initialDate, Double initialReading, Objects.Auxiliaries.Units.Unit unit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBefore, Int16 alertAfter, Boolean alertOnStart, Security.Credential credential)
        {
            //Check Magnitude
            if (HasValues(meter.IdMeter))
                if (meter.DefaultUnit.Magnitude.IdMagnitude != unit.Magnitude.IdMagnitude)
                    throw new ApplicationException(Resources.Messages.ErrorCannotChangeMagnitude);

            if (meter.HasValue() && meter.InitialDate != initialDate)
                throw new ApplicationException(Resources.Messages.ErrorCannotChangeInitialDate);

            Storage.ElectricityMeters _dbMeters = new Storage.ElectricityMeters();
            Storage.ElectricityMeterLanguageOptions _dbLanguageOptions = new Storage.ElectricityMeterLanguageOptions();
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
                        ElectricityMeterLoads _loads = new ElectricityMeterLoads();
                        Objects.Sites.Meters.Series.ElectricityLoad _first = _loads.ReadFirstLoad(_idMeter, credential);
                        if (_first != null)
                        {
                            _loads.Modify(meter, _first, initialReading, _first.ValueInput, _first.Unit.IdUnit, credential);
                        }
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
