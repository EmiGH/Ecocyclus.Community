using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Globalization;
using System.Threading;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class FuelMeterSeries
    {
        internal FuelMeterSeries()
        { }

        #region Read Methods

        internal Dictionary<Int64, Objects.Sites.Meters.Series.FuelSerie> Items(Int64 idMeter, Security.Credential credential)
        {
            Storage.FuelMeterSeries _dbSeries = new Storage.FuelMeterSeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.FuelSerie> _series = new Dictionary<long, Objects.Sites.Meters.Series.FuelSerie>();

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadAll(idMeter, _idLanguage);

            Boolean _insert = true; 
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_series.ContainsKey(Convert.ToInt64(_dbRecord["IdSiteFuelMeterSerie"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _series.Remove(Convert.ToInt64(_dbRecord["IdSiteFuelMeterSerie"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _series.Add(Convert.ToInt64(_dbRecord["IdSiteFuelMeterSerie"]), new Library.Objects.Sites.Meters.Series.FuelSerie(Convert.ToInt64(_dbRecord["IdSiteFuelMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteFuelMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential));
                }
                _insert = true;
            }

            return _series;
        }
        internal Dictionary<Int64, Objects.Sites.Meters.Series.FuelSerie> Items(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelMeterSeries _dbSeries = new Storage.FuelMeterSeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.FuelSerie> _series = new Dictionary<long, Objects.Sites.Meters.Series.FuelSerie>();

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadAll(idMeter, _idLanguage, from, to);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_series.ContainsKey(Convert.ToInt64(_dbRecord["IdSiteFuelMeterSerie"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _series.Remove(Convert.ToInt64(_dbRecord["IdSiteFuelMeterSerie"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _series.Add(Convert.ToInt64(_dbRecord["IdSiteFuelMeterSerie"]), new Library.Objects.Sites.Meters.Series.FuelSerie(Convert.ToInt64(_dbRecord["IdSiteFuelMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteFuelMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential));
                }
                _insert = true;
            }

            return _series;
        }
        internal Objects.Sites.Meters.Series.FuelSerie Item(Int64 idSerie, Security.Credential credential)
        {
            Storage.FuelMeterSeries _dbSeries = new Storage.FuelMeterSeries();
            Objects.Sites.Meters.Series.FuelSerie _serie = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadById(idSerie, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                {
                    _serie = new Library.Objects.Sites.Meters.Series.FuelSerie(idSerie, Convert.ToInt64(_dbRecord["IdSiteFuelMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential);
                    _insert = false;
                }
                if (_insert)
                    _serie = new Library.Objects.Sites.Meters.Series.FuelSerie(idSerie, Convert.ToInt64(_dbRecord["IdSiteFuelMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential);

                _insert = true;
            }
            return _serie;
        }
        internal Objects.Sites.Meters.Series.FuelSerie Item(Int64 idSerie, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelMeterSeries _dbSeries = new Storage.FuelMeterSeries();
            Objects.Sites.Meters.Series.FuelSerie _serie = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadById(idSerie, _idLanguage, from, to);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                {
                    _serie = new Library.Objects.Sites.Meters.Series.FuelSerie(idSerie, Convert.ToInt64(_dbRecord["IdSiteFuelMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential);
                    _insert = false;
                }
                if (_insert)
                    _serie = new Library.Objects.Sites.Meters.Series.FuelSerie(idSerie, Convert.ToInt64(_dbRecord["IdSiteFuelMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdFuelTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential);

                _insert = true;
            }
            return _serie;
        }

        internal Dictionary<Int64, Int64> Magnitudes(Int64 idMeter)
        {
            Storage.FuelMeterSeries _dbSeries = new Storage.FuelMeterSeries();
            Dictionary<Int64, Int64> _series = new Dictionary<Int64, Int64>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMagnitudes(idMeter);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _series.Add(Convert.ToInt64(_dbRecord["IdFuelType"]), Convert.ToInt64(_dbRecord["IdMagnitude"]));
            }

            return _series;
        }
        internal Int64 Magnitude(Int64 idMeter)
        {
            return new Storage.FuelMeterSeries().ReadMagnitude(idMeter);
        }

        #endregion

        #region Write Methods

        internal void Add(Objects.Sites.Meters.FuelMeter meter, List<Objects.Sites.Meters.Series.FuelData> data, Security.Credential credential)
        {
            //Check magnitudes consistency
            CheckUnits(meter, data);
            
            //Check date
            CheckSiteDate(((Objects.Sites.SiteMine)meter.Site).LoadTimeRange, data);
            
            Storage.FuelMeterSeries _dbSerie = new Storage.FuelMeterSeries();
        
            Int64 _idMeter = meter.IdMeter;
            Int64 _idOperator = ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator;

            //Site
            Objects.Sites.SiteMine _site = (Objects.Sites.SiteMine)meter.Site;

            using (TransactionScope _scope = new TransactionScope())
            {
                //Time unit for meter frequency
                Objects.Auxiliaries.Units.TimeUnit.Units _meterFrequencyTimeUnit = Objects.Auxiliaries.Units.TimeUnit.Units.Month;
                  
                foreach (Objects.Sites.Meters.Series.FuelData _item in data)
                {
                    Objects.Auxiliaries.Units.Unit _unit = _item.Unit;
                    Objects.Auxiliaries.Types.FuelType _fuelType = _item.FuelType;
                    Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor _emissionFactor = meter.GetFuelTypeEmissionFactor(_fuelType.IdFuelType);

                    Double _valuePattern = _unit.ToPattern(_item.Value);
                    Double _totalCO2 = _emissionFactor.TotalCO2(_valuePattern);

                    _dbSerie.Create(meter.IdMeter, _item.Date, _fuelType.IdFuelType, _item.Value, _valuePattern, _unit.IdUnit, _emissionFactor.Value, _emissionFactor.IdFuelTypeEmissionFactor, _totalCO2, _idOperator);
                
                    //Evaluate Targets
                    DateTime _monthStart = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(_item.Date, 1, _meterFrequencyTimeUnit);
                    DateTime _monthEnd = Objects.Auxiliaries.Units.TimeRange.GetNormalizedEndDate(_item.Date, 1, _meterFrequencyTimeUnit);
                    new SiteTargets().EvaluteTargetFuel(_site, _monthStart, _monthEnd, credential);
                }

                //Finally check target current status
                new SiteTargets().CheckTargetStatus(_site.IdSite);

                _scope.Complete();
            }      
        
        }
        internal void Remove(Int64 idSerie)
        {
            Storage.FuelMeterSeries _dbSeries = new Storage.FuelMeterSeries();

            try
            {
                _dbSeries.Delete(idSerie);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void RemoveAll(Int64 idMeter)
        {
            Storage.FuelMeterSeries _dbSeries = new Storage.FuelMeterSeries();
            _dbSeries.DeleteAll(idMeter);
        }
        internal void Modify(Objects.Sites.Meters.FuelMeter meter, Int64 idSerie, Objects.Sites.Meters.Series.FuelData data, Security.Credential credential)
        {
            //Check magnitudes consistency
            CheckUnit(meter, data);
            
            //Check date
            CheckSiteDate(((Objects.Sites.SiteMine)meter.Site).LoadTimeRange, data.Date);

            Storage.FuelMeterSeries _dbSerie = new Storage.FuelMeterSeries();

            //Site
            Objects.Sites.SiteMine _site = (Objects.Sites.SiteMine)meter.Site;

            Objects.Auxiliaries.Units.Unit _unit = data.Unit;
            Objects.Auxiliaries.Types.FuelType _fuelType = data.FuelType;
            Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor _emissionFactor = meter.GetFuelTypeEmissionFactor(_fuelType.IdFuelType);
            
            Double _valuePattern = _unit.ToPattern(data.Value);
            Double _totalCO2 = _emissionFactor.TotalCO2(_valuePattern);
            Int64 _idOperator = ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator;

            _dbSerie.Update(idSerie, data.Date, _fuelType.IdFuelType, data.Value, _valuePattern, _unit.IdUnit, _emissionFactor.Value, _emissionFactor.IdFuelTypeEmissionFactor, _totalCO2, _idOperator);
            
            //Evaluate targets
            Objects.Auxiliaries.Units.TimeUnit.Units _meterFrequencyTimeUnit = Objects.Auxiliaries.Units.TimeUnit.Units.Month;
            DateTime _monthStart = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(data.Date, 1, _meterFrequencyTimeUnit);
            DateTime _monthEnd = Objects.Auxiliaries.Units.TimeRange.GetNormalizedEndDate(data.Date, 1, _meterFrequencyTimeUnit);

            new SiteTargets().EvaluteTargetFuel(_site, _monthStart, _monthEnd, credential);

            //Finally check overdue and target current status
            new SiteTargets().CheckTargetStatus(_site.IdSite);
                
        }

        private void CheckUnits(Objects.Sites.Meters.FuelMeter meter, List<Objects.Sites.Meters.Series.FuelData> data)
        {
            //First check to see if same fueltypes have different magnitudes
            foreach (Objects.Sites.Meters.Series.FuelData _item in data)
                foreach (Objects.Sites.Meters.Series.FuelData _item2 in data)
                    if(_item2.FuelType.IdFuelType == _item.FuelType.IdFuelType)
                        if(_item2.Unit.Magnitude.IdMagnitude != _item.Unit.Magnitude.IdMagnitude)
                            throw new ApplicationException(Resources.Messages.ErrorCannotUseDifferentMagnitudes); 

            //Then check with existing values in the serie
            Dictionary<Int64, Int64> _magnitudes = Magnitudes(meter.IdMeter);
            foreach (Objects.Sites.Meters.Series.FuelData _item in data)
            {
                Int64 _idMagnitude;
                if (_magnitudes.TryGetValue(_item.FuelType.IdFuelType, out _idMagnitude))
                {
                    if (_idMagnitude != _item.Unit.Magnitude.IdMagnitude)
                        throw new ApplicationException(Resources.Messages.ErrorCannotUseDifferentMagnitudes);
                }
            }
        }
        private void CheckUnit(Objects.Sites.Meters.FuelMeter meter, Objects.Sites.Meters.Series.FuelData data)
        {
            //Then check with existing values in the serie
            Int64 _idExistingMagnitude = Magnitude(meter.IdMeter);
            if(_idExistingMagnitude >0)
                if (_idExistingMagnitude != data.Unit.Magnitude.IdMagnitude)
                    throw new ApplicationException(Resources.Messages.ErrorCannotUseDifferentMagnitudes);
            
        }
        
        private void CheckSiteDate(Objects.Auxiliaries.Units.TimeRange siteValidLoadRange, List<Objects.Sites.Meters.Series.FuelData> data)
        {
            for (int i = 0; i < data.Count; i++)
                MeterLoadFunctions.CheckDateValidity(siteValidLoadRange, data[i].Date);
        }
        private void CheckSiteDate(Objects.Auxiliaries.Units.TimeRange siteValidLoadRange, DateTime loadDate)
        {
            MeterLoadFunctions.CheckDateValidity(siteValidLoadRange, loadDate);
        }
        
        #endregion
    }
}