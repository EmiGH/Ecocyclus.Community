using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Globalization;
using System.Data.SqlClient;

namespace CSI.Library.Handlers
{
    internal class WasteMeterSeries
    {
        internal WasteMeterSeries()
        { }

        #region Read Methods

        internal Dictionary<Int64, Objects.Sites.Meters.Series.WasteSerie> Items(Int64 idMeter, Security.Credential credential)
        {
            Storage.WasteMeterSeries _dbSeries = new Storage.WasteMeterSeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.WasteSerie> _series = new Dictionary<long, Objects.Sites.Meters.Series.WasteSerie>();

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadAll(idMeter, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_series.ContainsKey(Convert.ToInt64(_dbRecord["IdSiteWasteMeterSerie"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _series.Remove(Convert.ToInt64(_dbRecord["IdSiteWasteMeterSerie"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _series.Add(Convert.ToInt64(_dbRecord["IdSiteWasteMeterSerie"]), new Library.Objects.Sites.Meters.Series.WasteSerie(Convert.ToInt64(_dbRecord["IdSiteWasteMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteWasteMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential));
                }
                _insert = true;
            } 
            return _series;
        }
        internal Dictionary<Int64, Objects.Sites.Meters.Series.WasteSerie> Items(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.WasteMeterSeries _dbSeries = new Storage.WasteMeterSeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.WasteSerie> _series = new Dictionary<long, Objects.Sites.Meters.Series.WasteSerie>();

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadAll(idMeter, _idLanguage, from, to);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_series.ContainsKey(Convert.ToInt64(_dbRecord["IdSiteWasteMeterSerie"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _series.Remove(Convert.ToInt64(_dbRecord["IdSiteWasteMeterSerie"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _series.Add(Convert.ToInt64(_dbRecord["IdSiteWasteMeterSerie"]), new Library.Objects.Sites.Meters.Series.WasteSerie(Convert.ToInt64(_dbRecord["IdSiteWasteMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteWasteMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential));
                }
                _insert = true;
            }

            return _series;
        }
        internal Objects.Sites.Meters.Series.WasteSerie Item(Int64 idSerie, Security.Credential credential)
        {
            Storage.WasteMeterSeries _dbSeries = new Storage.WasteMeterSeries();
            Objects.Sites.Meters.Series.WasteSerie _serie = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadById(idSerie, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                {
                    _serie = new Library.Objects.Sites.Meters.Series.WasteSerie(idSerie, Convert.ToInt64(_dbRecord["IdSiteWasteMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential);
                    _insert = false;
                }
                if (_insert)
                    _serie = new Library.Objects.Sites.Meters.Series.WasteSerie(idSerie, Convert.ToInt64(_dbRecord["IdSiteWasteMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential);

                _insert = true;
            }
            return _serie;
        }
        internal Objects.Sites.Meters.Series.WasteSerie Item(Int64 idSerie, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.WasteMeterSeries _dbSeries = new Storage.WasteMeterSeries();
            Objects.Sites.Meters.Series.WasteSerie _serie = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadById(idSerie, _idLanguage, from, to);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                {
                    _serie = new Library.Objects.Sites.Meters.Series.WasteSerie(idSerie, Convert.ToInt64(_dbRecord["IdSiteWasteMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential);
                    _insert = false;
                }
                if (_insert)
                    _serie = new Library.Objects.Sites.Meters.Series.WasteSerie(idSerie, Convert.ToInt64(_dbRecord["IdSiteWasteMeter"]), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), new Objects.Auxiliaries.Units.Unit(Convert.ToInt64(_dbRecord["IdUnit"]), Convert.ToInt64(_dbRecord["IdMagnitude"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["Numerator"]), Convert.ToDouble(_dbRecord["Denominator"]), Convert.ToDouble(_dbRecord["Exponent"]), Convert.ToDouble(_dbRecord["Constant"]), Convert.ToBoolean(_dbRecord["IsPattern"]), credential), Convert.ToDouble(_dbRecord["EF"]), Convert.ToInt64(_dbRecord["IdWasteTypeEmissionFactor"]), Objects.Users.fUserOperator.CreateOperatorOther(Convert.ToInt64(_dbRecord["IdCompanyUser"]),Convert.ToInt64(_dbRecord["IdUser"]), Convert.ToInt64(_dbRecord["IdCompany"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Firstname"]), Convert.ToString(_dbRecord["Lastname"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdPicture"],0)), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToBoolean(_dbRecord["IsManager"]), Convert.ToBoolean(_dbRecord["IsActive"]), credential), credential);

                _insert = true;
            } 
            return _serie;
        }

        internal Dictionary<Int64, Int64> Magnitudes(Int64 idMeter)
        {
            Storage.WasteMeterSeries _dbSeries = new Storage.WasteMeterSeries();
            Dictionary<Int64, Int64> _series = new Dictionary<Int64, Int64>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMagnitudes(idMeter);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _series.Add(Convert.ToInt64(_dbRecord["IdWasteType"]), Convert.ToInt64(_dbRecord["IdMagnitude"]));
            }

            return _series;
        }
        internal Int64 Magnitude(Int64 idMeter)
        {
            return new Storage.WasteMeterSeries().ReadMagnitude(idMeter);
        }

        #endregion

        #region Write Methods

        internal void Add(Objects.Sites.Meters.WasteMeter meter, List<Objects.Sites.Meters.Series.WasteData> data, Security.Credential credential)
        {
            //Check magnitudes consistency
            CheckUnits(meter, data);

            //Check date
            CheckSiteDate(((Objects.Sites.SiteMine)meter.Site).LoadTimeRange, data);

            Storage.WasteMeterSeries _dbSerie = new Storage.WasteMeterSeries();

            //Site
            Objects.Sites.SiteMine _site = (Objects.Sites.SiteMine)meter.Site;

            Int64 _idMeter = meter.IdMeter;
            Int64 _idOperator = ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator;

            using (TransactionScope _scope = new TransactionScope())
            {
                //Time unit for meter frequency
                Objects.Auxiliaries.Units.TimeUnit.Units _meterFrequencyTimeUnit = Objects.Auxiliaries.Units.TimeUnit.Units.Month;
                  
                foreach (Objects.Sites.Meters.Series.WasteData _item in data)
                {
                    Objects.Auxiliaries.Units.Unit _unit = _item.Unit;
                    Objects.Auxiliaries.Types.WasteType _wasteType = _item.WasteType;
                    Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _emissionFactor = meter.GetWasteTypeEmissionFactor(_wasteType.IdWasteType);

                    Double _valuePattern = _unit.ToPattern(_item.Value);
                    Double _totalCO2 = _emissionFactor.TotalCO2(_valuePattern);

                    _dbSerie.Create(meter.IdMeter, _item.Date, _wasteType.IdWasteType, _item.Value, _valuePattern, _unit.IdUnit, _emissionFactor.Value, _emissionFactor.IdWasteTypeEmissionFactor, _totalCO2, _idOperator);

                    //Evaluate Targets
                    DateTime _monthStart = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(_item.Date, 1, _meterFrequencyTimeUnit);
                    DateTime _monthEnd = Objects.Auxiliaries.Units.TimeRange.GetNormalizedEndDate(_item.Date, 1, _meterFrequencyTimeUnit);
                    new SiteTargets().EvaluteTargetWaste(_site, _monthStart, _monthEnd, credential);
                }

                //Finally check target current status
                new SiteTargets().CheckTargetStatus(_site.IdSite);

                _scope.Complete();
            }

        }
        internal void Remove(Int64 idSerie)
        {
            Storage.WasteMeterSeries _dbSeries = new Storage.WasteMeterSeries();

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
            Storage.WasteMeterSeries _dbSeries = new Storage.WasteMeterSeries();

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
        internal void Modify(Objects.Sites.Meters.WasteMeter meter, Int64 idSerie, Objects.Sites.Meters.Series.WasteData data, Security.Credential credential)
        {
            //Check magnitudes consistency
            CheckUnit(meter, data);

            //Check date
            CheckSiteDate(((Objects.Sites.SiteMine)meter.Site).LoadTimeRange, data.Date);
            
            Storage.WasteMeterSeries _dbSerie = new Storage.WasteMeterSeries();

            //Site
            Objects.Sites.SiteMine _site = (Objects.Sites.SiteMine)meter.Site;

            Objects.Auxiliaries.Units.Unit _unit = data.Unit;
            Objects.Auxiliaries.Types.WasteType _wasteType = data.WasteType;
            Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _emissionFactor = meter.GetWasteTypeEmissionFactor(_wasteType.IdWasteType);

            Double _valuePattern = _unit.ToPattern(data.Value);
            Double _totalCO2 = _emissionFactor.TotalCO2(_valuePattern);
            Int64 _idOperator = ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator;

            _dbSerie.Update(idSerie, data.Date, _wasteType.IdWasteType, data.Value, _valuePattern, _unit.IdUnit, _emissionFactor.Value, _emissionFactor.IdWasteTypeEmissionFactor, _totalCO2, _idOperator);

            //Evaluate targets
            Objects.Auxiliaries.Units.TimeUnit.Units _meterFrequencyTimeUnit = Objects.Auxiliaries.Units.TimeUnit.Units.Month;
            DateTime _monthStart = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(data.Date, 1, _meterFrequencyTimeUnit);
            DateTime _monthEnd = Objects.Auxiliaries.Units.TimeRange.GetNormalizedEndDate(data.Date, 1, _meterFrequencyTimeUnit);

            new SiteTargets().EvaluteTargetWaste(_site, _monthStart, _monthEnd, credential);

            //Finally check overdue and target current status
            new SiteTargets().CheckTargetStatus(_site.IdSite);
                
        }

        private void CheckUnits(Objects.Sites.Meters.WasteMeter meter, List<Objects.Sites.Meters.Series.WasteData> data)
        {
            //First check to see if same wastetypes have different magnitudes
            foreach (Objects.Sites.Meters.Series.WasteData _item in data)
                foreach (Objects.Sites.Meters.Series.WasteData _item2 in data)
                    if (_item2.WasteType.IdWasteType == _item.WasteType.IdWasteType)
                        if (_item2.Unit.Magnitude.IdMagnitude != _item.Unit.Magnitude.IdMagnitude)
                            throw new ApplicationException(Resources.Messages.ErrorCannotUseDifferentMagnitudes);

            //Then check with existing values in the serie
            Dictionary<Int64, Int64> _magnitudes = Magnitudes(meter.IdMeter);
            foreach (Objects.Sites.Meters.Series.WasteData _item in data)
            {
                Int64 _idMagnitude;
                if (_magnitudes.TryGetValue(_item.WasteType.IdWasteType, out _idMagnitude))
                {
                    if (_idMagnitude != _item.Unit.Magnitude.IdMagnitude)
                        throw new ApplicationException(Resources.Messages.ErrorCannotUseDifferentMagnitudes);
                }
            }
        }
        private void CheckUnit(Objects.Sites.Meters.WasteMeter meter, Objects.Sites.Meters.Series.WasteData data)
        {
            //Then check with existing values in the serie
            Int64 _idExistingMagnitude = Magnitude(meter.IdMeter);
            if (_idExistingMagnitude > 0)
                if (_idExistingMagnitude != data.Unit.Magnitude.IdMagnitude)
                    throw new ApplicationException(Resources.Messages.ErrorCannotUseDifferentMagnitudes);

        }
        private void CheckSiteDate(Objects.Auxiliaries.Units.TimeRange siteValidLoadRange, List<Objects.Sites.Meters.Series.WasteData> data)
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
