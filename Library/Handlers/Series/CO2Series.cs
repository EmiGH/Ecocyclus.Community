using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Handlers
{
    internal class CO2Series
    {
        internal CO2Series() { }

        #region Read Methods

        #region Site

        internal Objects.Metrics.Evolution ReadSiteEvolution(Int64 idSite, Int16 interval, Objects.Auxiliaries.Units.TimeUnit.Units timeUnit, Security.Credential credential)
        {
            Objects.Auxiliaries.Units.TimeRange _timeRange = new Storage.CO2Series().ReadSiteDatesRange(idSite);

            return ReadSiteEvolution(idSite, _timeRange, interval, timeUnit, credential);
        }
        internal Objects.Metrics.Evolution ReadSiteEvolution(Int64 idSite, Objects.Auxiliaries.Units.TimeRange timeRange, Int16 interval, Objects.Auxiliaries.Units.TimeUnit.Units timeUnit, Security.Credential credential)
        {
            Objects.Auxiliaries.Units.Unit _electricityUnit = new ElectricitySeries().ReadSiteElectricityUnit(idSite, credential);
            Objects.Auxiliaries.Units.Unit _fuelUnit = new FuelSeries().ReadSiteFuelUnit(idSite, credential);
            Objects.Auxiliaries.Units.Unit _transportUnit = new TransportSeries().ReadSiteTransportUnit(idSite, credential);
            Objects.Auxiliaries.Units.Unit _wasteUnit = new WasteSeries().ReadSiteWasteUnit(idSite, credential);
            Objects.Auxiliaries.Units.Unit _waterUnit = new WaterSeries().ReadSiteWaterUnit(idSite, credential);
            Objects.Auxiliaries.Units.Unit _co2Unit = ReadSiteCO2Unit(idSite, credential);

            Objects.Metrics.Evolution _evolution = new Objects.Metrics.Evolution(timeRange, interval, timeUnit, _electricityUnit, _fuelUnit, _transportUnit, _wasteUnit, _waterUnit, _co2Unit);

            //Get Electricity
            foreach (Objects.Metrics.MetricInstant _item in new ElectricitySeries().ReadSiteMonthly(idSite, timeRange.Start, timeRange.End, credential).Values)
		        _evolution.Update(_item.Instant, Objects.Common.Data.Protocols.Electricity, _item.Sum, _item.SumCO2);

            //Get Fuel
            foreach (Objects.Metrics.MetricInstant _item in new FuelSeries().ReadSiteMonthly(idSite, timeRange.Start, timeRange.End, credential).Values)
                _evolution.Update(_item.Instant, Objects.Common.Data.Protocols.Fuel, _item.Sum, _item.SumCO2);
            
            //Get Transport
            foreach (Objects.Metrics.MetricInstant _item in new TransportSeries().ReadSiteMonthly(idSite, timeRange.Start, timeRange.End, credential).Values)
                _evolution.Update(_item.Instant, Objects.Common.Data.Protocols.Transport, _item.Sum, _item.SumCO2);
                        
            //Get Waste
            foreach (Objects.Metrics.MetricInstant _item in new WasteSeries().ReadSiteMonthly(idSite, timeRange.Start, timeRange.End, credential).Values)
                _evolution.Update(_item.Instant, Objects.Common.Data.Protocols.Waste, _item.Sum, _item.SumCO2);
            
            //Get Water
            foreach (Objects.Metrics.MetricInstant _item in new WaterSeries().ReadSiteMonthly(idSite, timeRange.Start, timeRange.End, credential).Values)
                _evolution.Update(_item.Instant, Objects.Common.Data.Protocols.Water, _item.Sum, _item.SumCO2);

            return _evolution;
        }

        internal Objects.Metrics.MetricPeriod ReadSiteStatistics(Int64 idSite, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteStatistics(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), 0,0,0,0, Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricPeriod ReadSiteStatistics(Int64 idSite, DateTime from, DateTime to, Security.Credential credential)
        {

            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteStatistics(idSite, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), 0,0,0,0, Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricComposite ReadSiteStatisticsByProtocols(Int64 idSite, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            List<KeyValuePair<String, Double>> _statistics = new List<KeyValuePair<string, double>>();

            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteStatisticsByProtocols(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _statistics.Add(new KeyValuePair<string, double>(Resources.Data.ResourceManager.GetString(Convert.ToString(_dbRecord["Protocol"])), Convert.ToDouble(Common.CastNullValues(_dbRecord["SumCO2"], 0))));
            }
            return new Objects.Metrics.MetricComposite(_statistics,_unit);
        }
        internal Objects.Metrics.MetricComposite ReadSiteStatisticsByProtocols(Int64 idSite, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            List<KeyValuePair<String, Double>> _statistics = new List<KeyValuePair<string, double>>();

            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteStatisticsByProtocols(idSite, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _statistics.Add(new KeyValuePair<string, double>(Resources.Data.ResourceManager.GetString(Convert.ToString(_dbRecord["Protocol"])), Convert.ToDouble(Common.CastNullValues(_dbRecord["SumCO2"], 0))));
            }
            return new Objects.Metrics.MetricComposite(_statistics, _unit);
        }
       
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteDaily(Int64 idSite, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteDaily(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteDaily(Int64 idSite, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteDaily(idSite, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteWeekly(Int64 idSite, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteWeekly(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteWeekly(Int64 idSite, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteWeekly(idSite, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteMonthly(Int64 idSite, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteMonthly(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteMonthly(Int64 idSite, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteMonthly(idSite, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteYearly(Int64 idSite, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteYearly(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteYearly(Int64 idSite, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteYearly(idSite, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }

        internal Objects.Auxiliaries.Units.Unit ReadSiteCO2Unit(Int64 idSite, Security.Credential credential)
        {
            return new CO2Units().ItemPattern(credential);
        }
        
        #endregion

        #region Company

        internal Objects.Metrics.MetricPeriod ReadCompanyStatistics(Int64 idCompany, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyStatistics(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), 0,0,0,0, Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricPeriod ReadCompanyStatistics(Int64 idCompany, DateTime from, DateTime to, Security.Credential credential)
        {

            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyStatistics(idCompany, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), 0,0,0,0, Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricComposite ReadCompanyStatisticsByProtocols(Int64 idCompany, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            List<KeyValuePair<String, Double>> _statistics = new List<KeyValuePair<string, double>>();

            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyStatisticsByProtocols(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _statistics.Add(new KeyValuePair<string, double>(Resources.Data.ResourceManager.GetString(Convert.ToString(_dbRecord["Protocol"])), Convert.ToDouble(Common.CastNullValues(_dbRecord["SumCO2"], 0))));
            }
            return new Objects.Metrics.MetricComposite(_statistics, _unit);
        }
        internal Objects.Metrics.MetricComposite ReadCompanyStatisticsByProtocols(Int64 idCompany, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            List<KeyValuePair<String, Double>> _statistics = new List<KeyValuePair<string, double>>();

            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyStatisticsByProtocols(idCompany, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _statistics.Add(new KeyValuePair<string, double>(Resources.Data.ResourceManager.GetString(Convert.ToString(_dbRecord["Protocol"])), Convert.ToDouble(Common.CastNullValues(_dbRecord["SumCO2"], 0))));
            }
            return new Objects.Metrics.MetricComposite(_statistics, _unit);
        }
        internal Objects.Metrics.MetricComposite ReadCompanyStatisticsBySites(Int64 idCompany, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            List<KeyValuePair<String, Double>> _statistics = new List<KeyValuePair<string, double>>();

            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyStatisticsBySites(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _statistics.Add(new KeyValuePair<string, double>(Convert.ToString(_dbRecord["Site"]), Convert.ToDouble(Common.CastNullValues(_dbRecord["SumCO2"], 0))));
            }
            return new Objects.Metrics.MetricComposite(_statistics, _unit);
        }
        internal Objects.Metrics.MetricComposite ReadCompanyStatisticsBySites(Int64 idCompany, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            List<KeyValuePair<String, Double>> _statistics = new List<KeyValuePair<string, double>>();

            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyStatisticsBySites(idCompany, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _statistics.Add(new KeyValuePair<string, double>(Convert.ToString(_dbRecord["Site"]), Convert.ToDouble(Common.CastNullValues(_dbRecord["SumCO2"], 0))));
            }
            return new Objects.Metrics.MetricComposite(_statistics, _unit);
        }

        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyDaily(Int64 idCompany, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyDaily(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyDaily(Int64 idCompany, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyDaily(idCompany, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyWeekly(Int64 idCompany, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyWeekly(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyWeekly(Int64 idCompany, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyWeekly(idCompany, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyMonthly(Int64 idCompany, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyMonthly(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyMonthly(Int64 idCompany, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyMonthly(idCompany, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyYearly(Int64 idCompany, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyYearly(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyYearly(Int64 idCompany, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.CO2Series _dbSeries = new Storage.CO2Series();
            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyYearly(idCompany, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }

        internal Objects.Auxiliaries.Units.Unit ReadCompanyCO2Unit(Int64 idCompany, Security.Credential credential)
        {
            return new CO2Units().ItemPattern(credential);
        }
        
        #endregion

        #endregion

    }
}
