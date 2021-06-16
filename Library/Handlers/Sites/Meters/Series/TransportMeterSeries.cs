using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;
using System.Data.SqlClient;
using System.Transactions;
using System.Device.Location;
using System.Globalization;
using System.Threading;

namespace CSI.Library.Handlers
{
    internal class TransportMeterSeries
    {
        internal TransportMeterSeries()
        { }

        #region Read Methods

        internal Dictionary<Int64, Objects.Sites.Meters.Series.TransportSerie> Items(Int64 idMeter, Security.Credential credential)
        {
            Storage.TransportMeterSeries _dbSeries = new Storage.TransportMeterSeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.TransportSerie> _series = new Dictionary<long, Objects.Sites.Meters.Series.TransportSerie>();

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadAll(idMeter, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_series.ContainsKey(Convert.ToInt64(_dbRecord["IdSiteTransportMeterSerie"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _series.Remove(Convert.ToInt64(_dbRecord["IdSiteTransportMeterSerie"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _series.Add(Convert.ToInt64(_dbRecord["IdSiteTransportMeterSerie"]), Create(Convert.ToInt64(_dbRecord["IdSiteTransportMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteTransportMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["PlateNumber"]), Convert.ToString(_dbRecord["Address"]), Convert.ToBoolean(_dbRecord["IsRoundtrip"]), Convert.ToString(_dbRecord["Location"]), Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["UnitName"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential));
                }
            }
            return _series;
        }
        internal Dictionary<Int64, Objects.Sites.Meters.Series.TransportSerie> Items(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.TransportMeterSeries _dbSeries = new Storage.TransportMeterSeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.TransportSerie> _series = new Dictionary<long, Objects.Sites.Meters.Series.TransportSerie>();

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadAll(idMeter, _idLanguage, from, to);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_series.ContainsKey(Convert.ToInt64(_dbRecord["IdSiteTransportMeterSerie"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _series.Remove(Convert.ToInt64(_dbRecord["IdSiteTransportMeterSerie"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _series.Add(Convert.ToInt64(_dbRecord["IdSiteTransportMeterSerie"]), Create(Convert.ToInt64(_dbRecord["IdSiteTransportMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteTransportMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["PlateNumber"]), Convert.ToString(_dbRecord["Address"]), Convert.ToBoolean(_dbRecord["IsRoundtrip"]), Convert.ToString(_dbRecord["Location"]), Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential));
                }
            }

            return _series;
        }
        internal Objects.Sites.Meters.Series.TransportSerie Item(Int64 idSerie, Security.Credential credential)
        {
            Storage.TransportMeterSeries _dbSeries = new Storage.TransportMeterSeries();
            Objects.Sites.Meters.Series.TransportSerie _serie = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadById(idSerie, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                {
                    _serie = Create(Convert.ToInt64(_dbRecord["IdSiteTransportMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteTransportMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["PlateNumber"]), Convert.ToString(_dbRecord["Address"]), Convert.ToBoolean(_dbRecord["IsRoundtrip"]), Convert.ToString(_dbRecord["Location"]), Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential);
                    _insert = false;
                }
                if (_insert)
                    _serie = Create(Convert.ToInt64(_dbRecord["IdSiteTransportMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteTransportMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["PlateNumber"]), Convert.ToString(_dbRecord["Address"]), Convert.ToBoolean(_dbRecord["IsRoundtrip"]), Convert.ToString(_dbRecord["Location"]), Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential);
            }
            return _serie;
        }
        internal Objects.Sites.Meters.Series.TransportSerie Item(Int64 idSerie, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.TransportMeterSeries _dbSeries = new Storage.TransportMeterSeries();
            Objects.Sites.Meters.Series.TransportSerie _serie = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadById(idSerie, _idLanguage, from, to);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                {
                    _serie = Create(Convert.ToInt64(_dbRecord["IdSiteTransportMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteTransportMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["PlateNumber"]), Convert.ToString(_dbRecord["Address"]), Convert.ToBoolean(_dbRecord["IsRoundtrip"]), Convert.ToString(_dbRecord["Location"]), Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential);
                    _insert = false;
                }
                if (_insert)
                    _serie = Create(Convert.ToInt64(_dbRecord["IdSiteTransportMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteTransportMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["PlateNumber"]), Convert.ToString(_dbRecord["Address"]), Convert.ToBoolean(_dbRecord["IsRoundtrip"]), Convert.ToString(_dbRecord["Location"]), Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdTransportTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential);
            }
            return _serie;
        }

        private Objects.Sites.Meters.Series.TransportSerie Create(Int64 idSiteTransportMeterSerie, Int64 idSiteTransportMeter, DateTime date, String plateNumber, String address, Boolean isRoundtrip, String origin, Int64 idTransportType, Double value, Double valuePattern, Double totalCO2, Objects.Auxiliaries.Units.Unit unit, Double ef, Int64 idTransportTypeEmissionFactor, Objects.Users.UserOperator userOperator, Security.Credential credential)
        {
            if (origin == "")
                return new Objects.Sites.Meters.Series.TransportSerieDistance(idSiteTransportMeterSerie, idSiteTransportMeter, date, plateNumber, isRoundtrip, idTransportType, value, valuePattern, totalCO2, unit, ef, idTransportTypeEmissionFactor, userOperator, credential);
            else
                return new Objects.Sites.Meters.Series.TransportSerieLocation(idSiteTransportMeterSerie, idSiteTransportMeter, date, address, isRoundtrip, new Objects.Auxiliaries.Geographic.Position(SqlGeography.Parse(Convert.ToString(origin))), plateNumber, idTransportType, value, valuePattern, totalCO2, unit, ef, idTransportTypeEmissionFactor, userOperator, credential); 
        }

        internal Dictionary<Int64, Int64> Magnitudes(Int64 idMeter)
        {
            Storage.TransportMeterSeries _dbSeries = new Storage.TransportMeterSeries();
            Dictionary<Int64, Int64> _series = new Dictionary<Int64, Int64>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMagnitudes(idMeter);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _series.Add(Convert.ToInt64(_dbRecord["IdTransportType"]), Convert.ToInt64(_dbRecord["IdMagnitude"]));
            }

            return _series;
        }
        internal Int64 Magnitude(Int64 idMeter)
        {
            return new Storage.TransportMeterSeries().ReadMagnitude(idMeter);
        }
        
        #endregion

        #region Write Methods

        internal void Add(Objects.Sites.Meters.TransportMeter meter, List<Objects.Sites.Meters.Series.TransportData> data, Security.Credential credential)
        {
            //Check magnitudes consistency
            CheckUnits(meter, data);

            //Check date
            CheckSiteDate(((Objects.Sites.SiteMine)meter.Site).LoadTimeRange, data);

            //Site
            Objects.Sites.SiteMine _site = (Objects.Sites.SiteMine)meter.Site;

            Int64 _idOperator = ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator;
            Objects.Auxiliaries.Types.TransportType _transportType;
            Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor _emissionFactor;

            Double _totalCO2;
            Double _valuePattern;
            Double _value;

            using (TransactionScope _scope = new TransactionScope())
            {
                //Time unit for meter frequency
                Objects.Auxiliaries.Units.TimeUnit.Units _meterFrequencyTimeUnit = Objects.Auxiliaries.Units.TimeUnit.Units.Month;
                  
                foreach (Objects.Sites.Meters.Series.TransportData _item in data)
                {
                    _value = _item.IsRoundtrip ? _item.Value*2 : _item.Value;

                    _transportType = _item.TransportType;
                    _emissionFactor = meter.GetTransportTypeEmissionFactor(_transportType.IdTransportType);
                    _valuePattern = _item.Unit.ToPattern(_value);
                    _totalCO2 = _emissionFactor.TotalCO2(_valuePattern);

                    if (_item is Objects.Sites.Meters.Series.TransportDataLocation)
                    {
                        //if its location then convert to default unit
                        Objects.Auxiliaries.Units.Unit _patternUnit = new Units().ItemGeoDefault(credential);
                
                        Objects.Sites.Meters.Series.TransportDataLocation _itemCasted = (Objects.Sites.Meters.Series.TransportDataLocation)_item;
                        Add(meter.IdMeter, _item.Date, _itemCasted.Address, _itemCasted.IsRoundtrip, _itemCasted.Location, _item.PlateNumber, _item.TransportType.IdTransportType, _valuePattern, _valuePattern, _patternUnit.IdUnit, _emissionFactor.Value, _emissionFactor.IdTransportTypeEmissionFactor, _totalCO2, _idOperator);
                    }
                    else
                    {
                        Objects.Sites.Meters.Series.TransportDataDistance _itemCasted = (Objects.Sites.Meters.Series.TransportDataDistance)_item;
                        Add(meter.IdMeter, _item.Date, _item.PlateNumber, _item.IsRoundtrip, _item.TransportType.IdTransportType, _value, _valuePattern, _item.Unit.IdUnit, _emissionFactor.Value, _emissionFactor.IdTransportTypeEmissionFactor, _totalCO2, _idOperator);
                    }

                    //Evaluate Targets
                    DateTime _monthStart = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(_item.Date, 1, _meterFrequencyTimeUnit);
                    DateTime _monthEnd = Objects.Auxiliaries.Units.TimeRange.GetNormalizedEndDate(_item.Date, 1, _meterFrequencyTimeUnit);
                    new SiteTargets().EvaluteTargetTransport(_site, _monthStart, _monthEnd, credential);
                }

                //Finally check target current status
                new SiteTargets().CheckTargetStatus(_site.IdSite);

                _scope.Complete();
            }  
        }
        internal void Remove(Int64 idSerie)
        {
            Storage.TransportMeterSeries _dbSeries = new Storage.TransportMeterSeries();

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
            Storage.TransportMeterSeries _dbSeries = new Storage.TransportMeterSeries();

            try
            {
                _dbSeries.DeleteAll(idMeter);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }

        }
        internal void Modify(Objects.Sites.Meters.TransportMeter meter, Int64 idSerie, Objects.Sites.Meters.Series.TransportData data, Security.Credential credential)
        {
            //Check magnitudes consistency
            CheckUnit(meter, data);

            //Check date
            CheckSiteDate(((Objects.Sites.SiteMine)meter.Site).LoadTimeRange, data.Date);
            
            Storage.TransportMeterSeries _dbSerie = new Storage.TransportMeterSeries();

            //Site
            Objects.Sites.SiteMine _site = (Objects.Sites.SiteMine)meter.Site;

            Int64 _idOperator = ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator;
            Objects.Auxiliaries.Types.TransportType _transportType;
            Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor _emissionFactor;

            Double _totalCO2;
            Double _valuePattern;
            Double _value;

            _value = data.IsRoundtrip ? data.Value * 2 : data.Value;
            _transportType = data.TransportType;
            _emissionFactor = meter.GetTransportTypeEmissionFactor(_transportType.IdTransportType);
            _valuePattern = data.Unit.ToPattern(_value);
            _totalCO2 = _emissionFactor.TotalCO2(_valuePattern);

            if (data is Objects.Sites.Meters.Series.TransportDataLocation)
            {
                //if its location then convert to default unit
                Objects.Auxiliaries.Units.Unit _patternUnit = new Units().ItemGeoDefault(credential);
                
                Objects.Sites.Meters.Series.TransportDataLocation _itemCasted = (Objects.Sites.Meters.Series.TransportDataLocation)data;
                _dbSerie.Update(idSerie, data.Date, _itemCasted.Address, data.IsRoundtrip, _itemCasted.Location, data.PlateNumber, _transportType.IdTransportType, _valuePattern, _valuePattern, _patternUnit.IdUnit, _emissionFactor.Value, _emissionFactor.IdTransportTypeEmissionFactor, _totalCO2, _idOperator);
            }
            else
            {
                Objects.Sites.Meters.Series.TransportDataDistance _itemCasted = (Objects.Sites.Meters.Series.TransportDataDistance)data;
                _dbSerie.Update(idSerie, data.Date, data.PlateNumber, data.IsRoundtrip, _transportType.IdTransportType, _value, _valuePattern, data.Unit.IdUnit, _emissionFactor.Value, _emissionFactor.IdTransportTypeEmissionFactor, _totalCO2, _idOperator);
            }

            //Evaluate targets
            Objects.Auxiliaries.Units.TimeUnit.Units _meterFrequencyTimeUnit = Objects.Auxiliaries.Units.TimeUnit.Units.Month;
            DateTime _monthStart = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(data.Date, 1, _meterFrequencyTimeUnit);
            DateTime _monthEnd = Objects.Auxiliaries.Units.TimeRange.GetNormalizedEndDate(data.Date, 1, _meterFrequencyTimeUnit);

            new SiteTargets().EvaluteTargetTransport(_site, _monthStart, _monthEnd, credential);

            //Finally check overdue and target current status
            new SiteTargets().CheckTargetStatus(_site.IdSite);
            
        }

        private void Add(Int64 idMeter, DateTime date, String address, Boolean isRoundtrip, Objects.Auxiliaries.Geographic.Position origin, String plateNumber, Int64 idTranportType, Double value, Double valuePattern, Int64 idUnit, Double emissionFactorValue, Int64 idEmissionFactor, Double totalCO2, Int64 idOperator)
        {
            Storage.TransportMeterSeries _dbSeries = new Storage.TransportMeterSeries();
            _dbSeries.Create(idMeter, date, address, isRoundtrip, origin, plateNumber, idTranportType, value, valuePattern, idUnit, emissionFactorValue, idEmissionFactor, totalCO2, idOperator);
            
        }
        private void Add(Int64 idMeter, DateTime date, String plateNumber, Boolean isRoundtrip, Int64 idTranportType, Double value, Double valuePattern, Int64 idUnit, Double emissionFactorValue, Int64 idEmissionFactor, Double totalCO2, Int64 idOperator)
        {
            Storage.TransportMeterSeries _dbSeries = new Storage.TransportMeterSeries();
            _dbSeries.Create(idMeter, date, plateNumber, isRoundtrip, idTranportType, value, valuePattern, idUnit, emissionFactorValue, idEmissionFactor, totalCO2, idOperator);
        }

        private void CheckUnits(Objects.Sites.Meters.TransportMeter meter, List<Objects.Sites.Meters.Series.TransportData> data)
        {
            //First check to see if same fueltypes have different magnitudes
            foreach (Objects.Sites.Meters.Series.TransportData _item in data)
                foreach (Objects.Sites.Meters.Series.TransportData _item2 in data)
                    if (_item2.TransportType.IdTransportType == _item.TransportType.IdTransportType)
                        if (_item2.Unit.Magnitude.IdMagnitude != _item.Unit.Magnitude.IdMagnitude)
                            throw new ApplicationException(Resources.Messages.ErrorCannotUseDifferentMagnitudes);

            //Then check with existing values in the serie
            Dictionary<Int64, Int64> _magnitudes = Magnitudes(meter.IdMeter);
            foreach (Objects.Sites.Meters.Series.TransportData _item in data)
            {
                Int64 _idMagnitude;
                if (_magnitudes.TryGetValue(_item.TransportType.IdTransportType, out _idMagnitude))
                {
                    if (_idMagnitude != _item.Unit.Magnitude.IdMagnitude)
                        throw new ApplicationException(Resources.Messages.ErrorCannotUseDifferentMagnitudes);
                }
            }
        }
        private void CheckUnit(Objects.Sites.Meters.TransportMeter meter, Objects.Sites.Meters.Series.TransportData data)
        {
            //Then check with existing values in the serie
            Int64 _idExistingMagnitude = Magnitude(meter.IdMeter);
            if (_idExistingMagnitude > 0)
                if (_idExistingMagnitude != data.Unit.Magnitude.IdMagnitude)
                    throw new ApplicationException(Resources.Messages.ErrorCannotUseDifferentMagnitudes);

        }
     
        private void CheckSiteDate(Objects.Auxiliaries.Units.TimeRange siteValidLoadRange, List<Objects.Sites.Meters.Series.TransportData> data)
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
