﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Threading;
using System.Globalization;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class WaterMeterLoads
    {
        internal WaterMeterLoads()
        { }

        #region Read Methods

        internal Dictionary<Int64, Objects.Sites.Meters.Series.WaterLoad> ReadAll(Int64 idMeter, Security.Credential credential)
        {
            Storage.WaterMeterLoads _dbSeries = new Storage.WaterMeterLoads();
            Dictionary<Int64, Objects.Sites.Meters.Series.WaterLoad> _loads = new Dictionary<long, Objects.Sites.Meters.Series.WaterLoad>();

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadAll(idMeter, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_loads.ContainsKey(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _idLanguage)
                    {
                        _loads.Remove(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _loads.Add(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), new Library.Objects.Sites.Meters.Series.WaterLoad(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), Convert.ToDateTime(_dbRecord["DateFrom"]), Convert.ToDateTime(_dbRecord["DateTo"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValueInput"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), credential));
                }
            }
            return _loads;
        }
        internal Dictionary<Int64, Objects.Sites.Meters.Series.WaterLoad> ReadAll(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.WaterMeterLoads _dbSeries = new Storage.WaterMeterLoads();
            Dictionary<Int64, Objects.Sites.Meters.Series.WaterLoad> _loads = new Dictionary<long, Objects.Sites.Meters.Series.WaterLoad>();

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadAll(idMeter, _idLanguage, from, to);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_loads.ContainsKey(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _idLanguage)
                    {
                        _loads.Remove(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _loads.Add(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), new Library.Objects.Sites.Meters.Series.WaterLoad(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), Convert.ToDateTime(_dbRecord["DateFrom"]), Convert.ToDateTime(_dbRecord["DateTo"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValueInput"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), credential));
                }
            }

            return _loads;
        }
        internal Objects.Sites.Meters.Series.WaterLoad ReadById(Int64 idLoad, Security.Credential credential)
        {
            Storage.WaterMeterLoads _dbSeries = new Storage.WaterMeterLoads();
            Objects.Sites.Meters.Series.WaterLoad _load = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadById(idLoad, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                {
                    _load = new Library.Objects.Sites.Meters.Series.WaterLoad(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), Convert.ToDateTime(_dbRecord["DateFrom"]), Convert.ToDateTime(_dbRecord["DateTo"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValueInput"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), credential);
                    _insert = false;
                }
                if (_insert)
                    _load = new Library.Objects.Sites.Meters.Series.WaterLoad(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), Convert.ToDateTime(_dbRecord["DateFrom"]), Convert.ToDateTime(_dbRecord["DateTo"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValueInput"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), credential);
            }
            return _load;
        }
        internal Objects.Sites.Meters.Series.WaterLoad ReadPreviousLoad(Int64 idMeter, Int64 idCurrentLoad, Security.Credential credential)
        {
            Storage.WaterMeterLoads _dbSeries = new Storage.WaterMeterLoads();
            Objects.Sites.Meters.Series.WaterLoad _load = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadPrevious(idMeter, idCurrentLoad, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                {
                    _load = new Library.Objects.Sites.Meters.Series.WaterLoad(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), Convert.ToDateTime(_dbRecord["DateFrom"]), Convert.ToDateTime(_dbRecord["DateTo"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValueInput"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), credential);
                    _insert = false;
                }
                if (_insert)
                    _load = new Library.Objects.Sites.Meters.Series.WaterLoad(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), Convert.ToDateTime(_dbRecord["DateFrom"]), Convert.ToDateTime(_dbRecord["DateTo"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValueInput"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), credential);
            }
            return _load;
        }
        internal Objects.Sites.Meters.Series.WaterLoad ReadNextLoad(Int64 idMeter, Int64 idCurrentLoad, Security.Credential credential)
        {
            Storage.WaterMeterLoads _dbSeries = new Storage.WaterMeterLoads();
            Objects.Sites.Meters.Series.WaterLoad _load = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadNext(idMeter, idCurrentLoad, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                {
                    _load = new Library.Objects.Sites.Meters.Series.WaterLoad(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), Convert.ToDateTime(_dbRecord["DateFrom"]), Convert.ToDateTime(_dbRecord["DateTo"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValueInput"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), credential);
                    _insert = false;
                }
                if (_insert)
                    _load = new Library.Objects.Sites.Meters.Series.WaterLoad(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), Convert.ToDateTime(_dbRecord["DateFrom"]), Convert.ToDateTime(_dbRecord["DateTo"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValueInput"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), credential);
            }
            return _load;
        }
        internal Objects.Sites.Meters.Series.WaterLoad ReadFirstLoad(Int64 idMeter, Security.Credential credential)
        {
            Storage.WaterMeterLoads _dbSeries = new Storage.WaterMeterLoads();
            Objects.Sites.Meters.Series.WaterLoad _load = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadFirst(idMeter, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                {
                    _load = new Library.Objects.Sites.Meters.Series.WaterLoad(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), Convert.ToDateTime(_dbRecord["DateFrom"]), Convert.ToDateTime(_dbRecord["DateTo"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValueInput"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), credential);
                    _insert = false;
                }
                if (_insert)
                    _load = new Library.Objects.Sites.Meters.Series.WaterLoad(Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), Convert.ToDateTime(_dbRecord["DateFrom"]), Convert.ToDateTime(_dbRecord["DateTo"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValueInput"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), credential);
            }
            return _load;
        }
        
        #endregion
        
        #region Write Methods

        internal void Add(Objects.Sites.Meters.WaterMeter meter, List<Objects.Sites.Meters.Series.WaterData> data, Security.Credential credential)
        {
            //Dates Check
            DataLoadCheckDates(((Objects.Sites.SiteMine)meter.Site).LoadTimeRange, meter.GetNextDate(), data);
            
            Storage.WaterMeterLoads _dbLoad = new Storage.WaterMeterLoads();
            Storage.WaterMeterSeries _dbSerie = new Storage.WaterMeterSeries();

            Int64 _idMeter = meter.IdMeter;
            Int64 _idOperator = ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator;
            Objects.Auxiliaries.Units.Unit _unit = meter.DefaultUnit;

            //Site
            Objects.Sites.SiteMine _site = (Objects.Sites.SiteMine)meter.Site;

            //Save Loop
            Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _ef = meter.EmissionFactor;
            using (TransactionScope _scope = new TransactionScope())
            {
                //Time unit for meter frequency
                Objects.Auxiliaries.Units.TimeUnit.Units _meterFrequencyTimeUnit = (Objects.Auxiliaries.Units.TimeUnit.Units)Enum.Parse(typeof(Objects.Auxiliaries.Units.TimeUnit.Units), meter.FrequencyUnit.ToString());
                  
                foreach (Objects.Sites.Meters.Series.WaterData item in data)
                {
                    Double _value = item.Value;
                    Double _valuePerDay = _value / item.Days;
                    Double _valuePattern = _unit.ToPattern(_valuePerDay);
                    Double _totalCO2 = _ef.TotalCO2(_valuePattern);

                    Int64 _idLoad = _dbLoad.Create(_idMeter, _idOperator, item.From, item.To, _value, _value, _unit.IdUnit, _ef.Value, _ef.IdEmissionFactor);
                    for (int i = 0; i < item.Days; i++)
                    {
                        _dbSerie.Create(_idLoad, item.From.AddDays(i), _valuePerDay, _valuePattern, _totalCO2);
                    }

                    //Resolve Overdue and evaluate targets
                    DateTime _monthStart = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(item.From, 1, _meterFrequencyTimeUnit);
                    DateTime _monthEnd = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(item.To, 1, _meterFrequencyTimeUnit);

                    new Storage.SiteExceptionMeters().ResolveForElectricityMeter(_idMeter, _monthStart, _monthEnd);
                    new SiteTargets().EvaluteTargetWater(_site, _monthStart, _monthEnd, credential);
                }

                //Finally check overdue and target current status
                new SiteTargets().CheckTargetStatus(_site.IdSite);
                new Sites().UpdateScheduleStatus(_site.IdSite);


                _scope.Complete();
            }

        }
        internal void Add(Objects.Sites.Meters.WaterMeterPhysical meter, List<Objects.Sites.Meters.Series.WaterData> data, Security.Credential credential)
        {
            //Dates & Values Check
            DataLoadCheckDatesAndReading(((Objects.Sites.SiteMine)meter.Site).LoadTimeRange, meter.GetNextDate(), meter.LastReading, data);
            
            Storage.WaterMeterLoads _dbLoad = new Storage.WaterMeterLoads();
            Storage.WaterMeterSeries _dbSerie = new Storage.WaterMeterSeries();

            Int64 _idMeter = meter.IdMeter;
            Int64 _idOperator = ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator;
            Objects.Auxiliaries.Units.Unit _unit = meter.DefaultUnit;

            //Site
            Objects.Sites.SiteMine _site = (Objects.Sites.SiteMine)meter.Site;

            //Save Loop
            Double _lastReading = meter.LastReading;
            Boolean _hasValue = meter.HasValue();

            Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _ef = meter.EmissionFactor;
            using (TransactionScope _scope = new TransactionScope())
            {
                //Time unit for meter frequency
                Objects.Auxiliaries.Units.TimeUnit.Units _meterFrequencyTimeUnit = (Objects.Auxiliaries.Units.TimeUnit.Units)Enum.Parse(typeof(Objects.Auxiliaries.Units.TimeUnit.Units), meter.FrequencyUnit.ToString());
                  
                foreach (Objects.Sites.Meters.Series.WaterData item in data)
                {
                    
                    Double _value = item.Value - _lastReading;
                    Double _valuePerDay = _value / item.Days;
                    Double _valuePattern = _unit.ToPattern(_valuePerDay);
                    Double _totalCO2 = _ef.TotalCO2(_valuePattern);
                    
                    Int64 _idLoad = _dbLoad.Create(_idMeter, _idOperator, item.From, item.To, _value, item.Value, _unit.IdUnit, _ef.Value, _ef.IdEmissionFactor);
                    for (int i = 0; i < item.Days; i++)
                    {
                        _dbSerie.Create(_idLoad, item.From.AddDays(i), _valuePerDay, _valuePattern, _totalCO2);
                    }
                    _hasValue = true;
                    _lastReading = item.Value;

                    //Resolve Overdue and evaluate targets
                    DateTime _monthStart = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(item.From, 1, _meterFrequencyTimeUnit);
                    DateTime _monthEnd = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(item.To, 1, _meterFrequencyTimeUnit);

                    new Storage.SiteExceptionMeters().ResolveForElectricityMeter(_idMeter, _monthStart, _monthEnd);
                    new SiteTargets().EvaluteTargetWater(_site, _monthStart, _monthEnd, credential);
                }

                //Finally check overdue and target current status
                new SiteTargets().CheckTargetStatus(_site.IdSite);
                new Sites().UpdateScheduleStatus(_site.IdSite);

                _scope.Complete();
            }
        }
        internal void Remove(Objects.Sites.Meters.WaterMeter meter, DateTime from)
        {
            Storage.WaterMeterLoads _dbSeries = new Storage.WaterMeterLoads();

            try
            {
                _dbSeries.Delete(meter.IdMeter, from);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Objects.Sites.Meters.WaterMeter meter, Objects.Sites.Meters.Series.WaterLoad load, Double data, Int64 idUnit, Security.Credential credential)
        {
            Storage.WaterMeterLoads _dbLoad = new Storage.WaterMeterLoads();
            Storage.WaterMeterSeries _dbSerie = new Storage.WaterMeterSeries();

            //Site
            Objects.Sites.SiteMine _site = (Objects.Sites.SiteMine)meter.Site;

            Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _ef = meter.EmissionFactor;

            int _days = (load.To - load.From).Days;
            Double _valuePerDay = data / _days;
            Double _valuePattern = meter.DefaultUnit.ToPattern(_valuePerDay);

            using (TransactionScope _scope = new TransactionScope())
            {
                _dbLoad.Update(load.IdLoad, ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator, data, data, idUnit, _ef.Value, _ef.IdEmissionFactor);
                for (int i = 0; i < _days; i++)
                {
                    _dbSerie.Update(load.IdLoad, _valuePerDay, _valuePattern, _ef.TotalCO2(_valuePattern));
                }

                //Evaluate targets
                Objects.Auxiliaries.Units.TimeUnit.Units _meterFrequencyTimeUnit = (Objects.Auxiliaries.Units.TimeUnit.Units)Enum.Parse(typeof(Objects.Auxiliaries.Units.TimeUnit.Units), meter.FrequencyUnit.ToString());
                DateTime _monthStart = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(load.From, 1, _meterFrequencyTimeUnit);
                DateTime _monthEnd = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(load.To, 1, _meterFrequencyTimeUnit);

                new SiteTargets().EvaluteTargetWater(_site, _monthStart, _monthEnd, credential);

                //Finally check overdue and target current status
                new SiteTargets().CheckTargetStatus(_site.IdSite);
                
                _scope.Complete();
            }
        }
        internal void Modify(Objects.Sites.Meters.WaterMeterPhysical meter, Objects.Sites.Meters.Series.WaterLoad load, Double data, Int64 idUnit, Security.Credential credential)
        {
            //Dates and Value Check
            Objects.Sites.Meters.Series.WaterLoad _next = ReadNextLoad(meter.IdMeter, load.IdLoad, credential);
            if (_next != null)
            {
                if (ReadFirstLoad(meter.IdMeter, credential).IdLoad == load.IdLoad)
                    DataLoadCheckReading(meter.InitialReading, _next.ValueInput, data);
                else
                    DataLoadCheckReading(ReadPreviousLoad(meter.IdMeter, load.IdLoad, credential).ValueInput, _next.ValueInput, data);
            }
            else
            {
                if (ReadFirstLoad(meter.IdMeter, credential).IdLoad == load.IdLoad)
                    DataLoadCheckReading(meter.InitialReading, data);
                else
                    DataLoadCheckReading(ReadPreviousLoad(meter.IdMeter, load.IdLoad, credential).ValueInput, data);
            }
               
            Storage.WaterMeterLoads _dbLoad = new Storage.WaterMeterLoads();
            Storage.WaterMeterSeries _dbSerie = new Storage.WaterMeterSeries();

            //Site
            Objects.Sites.SiteMine _site = (Objects.Sites.SiteMine)meter.Site;

            Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _ef = meter.EmissionFactor;

            int _days = (load.To - load.From).Days;

            Double _lastReading;
            Objects.Sites.Meters.Series.WaterLoad _previous = ReadPreviousLoad(meter.IdMeter, load.IdLoad, credential);
            if (_previous != null)
                _lastReading = _previous.ValueInput;
            else
                _lastReading = meter.InitialReading;

            Double _value = data - _lastReading;
            Double _valuePerDay = _value / _days;
            Double _valuePattern = meter.DefaultUnit.ToPattern(_valuePerDay);

            using (TransactionScope _scope = new TransactionScope())
            {
                _dbLoad.Update(load.IdLoad, ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator, _value, data, idUnit, _ef.Value, _ef.IdEmissionFactor);
                _dbSerie.Update(load.IdLoad, _valuePerDay, _valuePattern, _ef.TotalCO2(_valuePattern));

                //Update next reading
                Objects.Sites.Meters.Series.WaterLoad _nextLoad = ReadNextLoad(meter.IdMeter, load.IdLoad, credential);
                if (_nextLoad != null)
                {
                    _days = (_nextLoad.To - _nextLoad.From).Days;
                    _value = _nextLoad.ValueInput - data;
                    _valuePerDay = _value / _days;
                    _valuePattern = meter.DefaultUnit.ToPattern(_valuePerDay);

                    _dbLoad.Update(_nextLoad.IdLoad, ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator, _value, _nextLoad.ValueInput, idUnit, _ef.Value, _ef.IdEmissionFactor);
                    _dbSerie.Update(_nextLoad.IdLoad, _valuePerDay, _valuePattern, _ef.TotalCO2(_valuePattern));
                }

                //Evaluate targets
                Objects.Auxiliaries.Units.TimeUnit.Units _meterFrequencyTimeUnit = (Objects.Auxiliaries.Units.TimeUnit.Units)Enum.Parse(typeof(Objects.Auxiliaries.Units.TimeUnit.Units), meter.FrequencyUnit.ToString());
                DateTime _monthStart = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(load.From, 1, _meterFrequencyTimeUnit);
                DateTime _monthEnd = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(load.To, 1, _meterFrequencyTimeUnit);

                new SiteTargets().EvaluteTargetWater(_site, _monthStart, _monthEnd, credential);

                //Finally check overdue and target current status
                new SiteTargets().CheckTargetStatus(_site.IdSite);
                
                _scope.Complete();
            }
        }
        internal void Modify(Objects.Sites.Meters.WaterMeterPhysical meter, Objects.Sites.Meters.Series.WaterLoad load, Double previousValue, Double data, Int64 idUnit, Security.Credential credential)
        {
            //Dates and Value Check
            Objects.Sites.Meters.Series.WaterLoad _next = ReadNextLoad(meter.IdMeter, load.IdLoad, credential);
            if (_next != null)
            {
                if (ReadFirstLoad(meter.IdMeter, credential).IdLoad == load.IdLoad)
                    DataLoadCheckReading(meter.InitialReading, _next.ValueInput, data);
                else
                    DataLoadCheckReading(ReadPreviousLoad(meter.IdMeter, load.IdLoad, credential).ValueInput, _next.ValueInput, data);
            }
            else
            {
                if (ReadFirstLoad(meter.IdMeter, credential).IdLoad == load.IdLoad)
                    DataLoadCheckReading(meter.InitialReading, data);
                else
                    DataLoadCheckReading(ReadPreviousLoad(meter.IdMeter, load.IdLoad, credential).ValueInput, data);
            }

            Storage.WaterMeterLoads _dbLoad = new Storage.WaterMeterLoads();
            Storage.WaterMeterSeries _dbSerie = new Storage.WaterMeterSeries();

            //Site
            Objects.Sites.SiteMine _site = (Objects.Sites.SiteMine)meter.Site;

            Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _ef = meter.EmissionFactor;

            int _days = (load.To - load.From).Days;
            
            Double _value = data - previousValue;
            Double _valuePerDay = _value / _days;
            Double _valuePattern = meter.DefaultUnit.ToPattern(_valuePerDay);

            using (TransactionScope _scope = new TransactionScope())
            {
                _dbLoad.Update(load.IdLoad, ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator, _value, data, idUnit, _ef.Value, _ef.IdEmissionFactor);
                _dbSerie.Update(load.IdLoad, _valuePerDay, _valuePattern, _ef.TotalCO2(_valuePattern));

                //Update next reading
                Objects.Sites.Meters.Series.WaterLoad _nextLoad = ReadNextLoad(meter.IdMeter, load.IdLoad, credential);
                if (_nextLoad != null)
                {
                    _days = (_nextLoad.To - _nextLoad.From).Days;
                    _value = _nextLoad.ValueInput - data;
                    _valuePerDay = _value / _days;
                    _valuePattern = meter.DefaultUnit.ToPattern(_valuePerDay);

                    _dbLoad.Update(_nextLoad.IdLoad, ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator, _value, _nextLoad.ValueInput, idUnit, _ef.Value, _ef.IdEmissionFactor);
                    _dbSerie.Update(_nextLoad.IdLoad, _valuePerDay, _valuePattern, _ef.TotalCO2(_valuePattern));
                }

                //Evaluate targets
                Objects.Auxiliaries.Units.TimeUnit.Units _meterFrequencyTimeUnit = (Objects.Auxiliaries.Units.TimeUnit.Units)Enum.Parse(typeof(Objects.Auxiliaries.Units.TimeUnit.Units), meter.FrequencyUnit.ToString());
                DateTime _monthStart = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(load.From, 1, _meterFrequencyTimeUnit);
                DateTime _monthEnd = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(load.To, 1, _meterFrequencyTimeUnit);

                new SiteTargets().EvaluteTargetWater(_site, _monthStart, _monthEnd, credential);

                //Finally check overdue and target current status
                new SiteTargets().CheckTargetStatus(_site.IdSite);
                
                _scope.Complete();
            }
        }

        //Dates Check
        private void DataLoadCheckDatesAndReading(Objects.Auxiliaries.Units.TimeRange siteValidLoadRange, DateTime? nextDate, Double lastReading, Objects.Sites.Meters.Series.WaterData data)
        {
            DataLoadCheckDates(siteValidLoadRange, nextDate, data.From);
            DataLoadCheckReading(lastReading, data.Value);
        }
        private void DataLoadCheckDatesAndReading(Objects.Auxiliaries.Units.TimeRange siteValidLoadRange, DateTime? nextDate, Double lastReading, List<Objects.Sites.Meters.Series.WaterData> data)
        {
            DataLoadCheckDates(siteValidLoadRange, nextDate, data);
            DataLoadCheckReading(lastReading, data);
        }
        private void DataLoadCheckDates(Objects.Auxiliaries.Units.TimeRange siteValidLoadRange, DateTime? nextDate, DateTime loadDate)
        {
            MeterLoadFunctions.CheckDateValidity(siteValidLoadRange, loadDate);

            if (nextDate.HasValue)
                if (nextDate != loadDate)
                    throw new ApplicationException(Resources.Messages.NextDateInvalid);
        }
        private void DataLoadCheckDates(Objects.Auxiliaries.Units.TimeRange siteValidLoadRange, DateTime? nextDate, List<Objects.Sites.Meters.Series.WaterData> data)
        {
            DateTime _first = data.First().From;

            MeterLoadFunctions.CheckDateValidity(siteValidLoadRange, _first);

            if (nextDate.HasValue)
                if (nextDate != _first)
                    throw new ApplicationException(Resources.Messages.NextDateInvalid);

            for (int i = 0; i < data.Count - 1; i++)
            {
                if (data[i].To != data[i + 1].From)
                    throw new ApplicationException(Resources.Messages.DatesInvalid);
            }
        }

        //Reading Checks
        private void DataLoadCheckReading(Double lastReading, Double nextReading, Double data)
        {
            if (lastReading > data || nextReading < data)
                throw new ApplicationException(Resources.Messages.PhysicalMeterInvalidValue);

        }
        private void DataLoadCheckReading(Double lastReading, Double data)
        {
            if (lastReading > data)
                throw new ApplicationException(Resources.Messages.PhysicalMeterInvalidValue);

        }
        private void DataLoadCheckReading(Double lastReading, List<Objects.Sites.Meters.Series.WaterData> data)
        {
            if (lastReading > data.First().Value)
                throw new ApplicationException(Resources.Messages.PhysicalMeterInvalidValue);

            for (int i = 0; i < data.Count - 1; i++)
            {
                if (data[i].Value > data[i + 1].Value)
                    throw new ApplicationException(Resources.Messages.PhysicalMeterInvalidValue);
            }
        }
        
        #endregion
    }
}
