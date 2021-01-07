using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Handlers
{
    internal class FuelSeries
    {
        internal FuelSeries() { }

        #region Read Methods

        #region Meter

        internal Objects.Metrics.MetricPeriod ReadMeterStatistics(Int64 idMeter, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterStatistics(idMeter);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricPeriod ReadMeterStatistics(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterStatistics(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricPeriod ReadMeterStatistics(Int64 idMeter, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterStatistics(idMeter, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricPeriod ReadMeterStatistics(Int64 idMeter, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterStatistics(idMeter, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricComposite ReadMeterStatisticsByTypes(Int64 idMeter, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            List<KeyValuePair<String, Double>> _statistics = new List<KeyValuePair<string, double>>();

            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterStatisticsByTypes(idMeter);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _statistics.Add(new KeyValuePair<string, double>(Convert.ToString(_dbRecord[0]), Convert.ToDouble(Common.CastNullValues(_dbRecord["Sum"], 0))));
            }
            return new Objects.Metrics.MetricComposite(_statistics, _unit);
        }
        internal Objects.Metrics.MetricComposite ReadMeterStatisticsByTypes(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            List<KeyValuePair<String, Double>> _statistics = new List<KeyValuePair<string, double>>();

            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterStatisticsByTypes(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _statistics.Add(new KeyValuePair<string, double>(Convert.ToString(_dbRecord[0]), Convert.ToDouble(Common.CastNullValues(_dbRecord["Sum"], 0))));
            }
            return new Objects.Metrics.MetricComposite(_statistics, _unit);
        }
        internal Objects.Metrics.MetricComposite ReadMeterStatisticsCO2ByTypes(Int64 idMeter, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            List<KeyValuePair<String, Double>> _statistics = new List<KeyValuePair<string, double>>();

            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterStatisticsByTypes(idMeter);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _statistics.Add(new KeyValuePair<string, double>(Convert.ToString(_dbRecord["Name"]), Convert.ToDouble(Common.CastNullValues(_dbRecord["SumCO2"], 0))));
            }
            return new Objects.Metrics.MetricComposite(_statistics, _unit);
        }
        internal Objects.Metrics.MetricComposite ReadMeterStatisticsCO2ByTypes(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            List<KeyValuePair<String, Double>> _statistics = new List<KeyValuePair<string, double>>();

            Objects.Auxiliaries.Units.Unit _unit = new CO2Units().ItemPattern(credential);
            
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterStatisticsByTypes(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _statistics.Add(new KeyValuePair<string, double>(Convert.ToString(_dbRecord["Name"]), Convert.ToDouble(Common.CastNullValues(_dbRecord["SumCO2"], 0))));
            }
            return new Objects.Metrics.MetricComposite(_statistics, _unit);
        }


        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterDaily(Int64 idMeter, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterDaily(idMeter);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterDaily(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterDaily(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterDaily(Int64 idMeter, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterDaily(idMeter, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterDaily(Int64 idMeter, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterDaily(idMeter, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterWeekly(Int64 idMeter, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterWeekly(idMeter);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterWeekly(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterWeekly(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterWeekly(Int64 idMeter, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterWeekly(idMeter, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterWeekly(Int64 idMeter, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterWeekly(idMeter, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterMonthly(Int64 idMeter, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterMonthly(idMeter);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterMonthly(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterMonthly(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterMonthly(Int64 idMeter, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterMonthly(idMeter, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterMonthly(Int64 idMeter, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterMonthly(idMeter, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterYearly(Int64 idMeter, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterYearly(idMeter);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterYearly(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterYearly(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterYearly(Int64 idMeter, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterYearly(idMeter, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadMeterYearly(Int64 idMeter, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForMeter(idMeter), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterYearly(idMeter, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        
        internal Objects.Auxiliaries.Units.Unit ReadMeterFuelUnit(Int64 idMeter, Security.Credential credential)
        {
            return new Units().Item(new Storage.FuelSeries().ReadUnitForMeter(idMeter), credential);
        }
        
        #endregion

        #region Site

        internal Objects.Metrics.MetricPeriod ReadSiteStatistics(Int64 idSite, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteStatistics(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricPeriod ReadSiteStatistics(Int64 idSite, DateTime from, DateTime to, Security.Credential credential)
        {

            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteStatistics(idSite, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricPeriod ReadSiteStatistics(Int64 idSite, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteStatistics(idSite, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricPeriod ReadSiteStatistics(Int64 idSite, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {

            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteStatistics(idSite, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricComposite ReadSiteStatisticsByTypes(Int64 idSite, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            List<KeyValuePair<String, Double>> _statistics = new List<KeyValuePair<string, double>>();
            
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteStatisticsByTypes(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _statistics.Add(new KeyValuePair<string, double>(Convert.ToString(_dbRecord["Type"]), Convert.ToDouble(Common.CastNullValues(_dbRecord["SumCO2"], 0))));
            }
            return new Objects.Metrics.MetricComposite(_statistics, _unit);
        }
        internal Objects.Metrics.MetricComposite ReadSiteStatisticsByTypes(Int64 idSite, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            List<KeyValuePair<String, Double>> _statistics = new List<KeyValuePair<string, double>>();

            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteStatisticsByTypes(idSite, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _statistics.Add(new KeyValuePair<string, double>(Convert.ToString(_dbRecord["Type"]), Convert.ToDouble(Common.CastNullValues(_dbRecord["SumCO2"], 0))));
            }
            return new Objects.Metrics.MetricComposite(_statistics, _unit);
        }
       

        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteDaily(Int64 idSite, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteDaily(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteDaily(Int64 idSite, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteDaily(idSite, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteDaily(Int64 idSite, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteDaily(idSite, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteDaily(Int64 idSite, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteDaily(idSite, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteWeekly(Int64 idSite, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteWeekly(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteWeekly(Int64 idSite, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteWeekly(idSite, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteWeekly(Int64 idSite, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteWeekly(idSite, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteWeekly(Int64 idSite, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteWeekly(idSite, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteMonthly(Int64 idSite, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteMonthly(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteMonthly(Int64 idSite, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteMonthly(idSite, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteMonthly(Int64 idSite, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteMonthly(idSite, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteMonthly(Int64 idSite, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteMonthly(idSite, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteYearly(Int64 idSite, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteYearly(idSite);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteYearly(Int64 idSite, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteYearly(idSite, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteYearly(Int64 idSite, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteYearly(idSite, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadSiteYearly(Int64 idSite, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForSite(idSite), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadSiteYearly(idSite, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }

        internal Objects.Auxiliaries.Units.Unit ReadSiteFuelUnit(Int64 idSite, Security.Credential credential)
        {
            return new Units().Item(new Storage.FuelSeries().ReadUnitForSite(idSite), credential);
        }
        
        #endregion

        #region Company

        internal Objects.Metrics.MetricPeriod ReadCompanyStatistics(Int64 idCompany, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyStatistics(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricPeriod ReadCompanyStatistics(Int64 idCompany, DateTime from, DateTime to, Security.Credential credential)
        {

            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyStatistics(idCompany, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricPeriod ReadCompanyStatistics(Int64 idCompany, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyStatistics(idCompany, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }
        internal Objects.Metrics.MetricPeriod ReadCompanyStatistics(Int64 idCompany, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {

            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Objects.Metrics.MetricPeriod _statistics = new Objects.Metrics.MetricPeriod(_unit);

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyStatistics(idCompany, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (Convert.ToInt64(_dbRecord["Count"]) > 0)
                    _statistics = new Library.Objects.Metrics.MetricPeriod(Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit);
            }
            return _statistics;
        }

        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyDaily(Int64 idCompany, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyDaily(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyDaily(Int64 idCompany, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyDaily(idCompany, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyDaily(Int64 idCompany, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyDaily(idCompany, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyDaily(Int64 idCompany, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyDaily(idCompany, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyWeekly(Int64 idCompany, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyWeekly(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyWeekly(Int64 idCompany, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyWeekly(idCompany, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyWeekly(Int64 idCompany, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyWeekly(idCompany, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyWeekly(Int64 idCompany, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyWeekly(idCompany, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyMonthly(Int64 idCompany, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyMonthly(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyMonthly(Int64 idCompany, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyMonthly(idCompany, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyMonthly(Int64 idCompany, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyMonthly(idCompany, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyMonthly(Int64 idCompany, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyMonthly(idCompany, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyYearly(Int64 idCompany, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyYearly(idCompany);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyYearly(Int64 idCompany, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyYearly(idCompany, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyYearly(Int64 idCompany, Int64 idFuelType, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyYearly(idCompany, idFuelType);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }
        internal Dictionary<DateTime, Objects.Metrics.MetricInstant> ReadCompanyYearly(Int64 idCompany, Int64 idFuelType, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.FuelSeries _dbSeries = new Storage.FuelSeries();
            Objects.Auxiliaries.Units.Unit _unit = new Units().Item(_dbSeries.ReadUnitForCompany(idCompany), credential);
            Dictionary<DateTime, Objects.Metrics.MetricInstant> _serie = new Dictionary<DateTime, Objects.Metrics.MetricInstant>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadCompanyYearly(idCompany, idFuelType, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToDateTime(_dbRecord["Date"]), new Library.Objects.Metrics.MetricInstant(Convert.ToDateTime(_dbRecord["Date"]), Convert.ToDouble(_dbRecord["Min"]), Convert.ToDouble(_dbRecord["Max"]), Convert.ToDouble(_dbRecord["Sum"]), Convert.ToDouble(_dbRecord["Avg"]), Convert.ToDouble(_dbRecord["MinCO2"]), Convert.ToDouble(_dbRecord["MaxCO2"]), Convert.ToDouble(_dbRecord["SumCO2"]), Convert.ToDouble(_dbRecord["AvgCO2"]), _unit));
            }
            return _serie;
        }

        internal Objects.Auxiliaries.Units.Unit ReadCompanyFuelUnit(Int64 idCompany, Security.Credential credential)
        {
            return new Units().Item(new Storage.FuelSeries().ReadUnitForCompany(idCompany), credential);
        }
        
        #endregion

        #endregion
    }
}
