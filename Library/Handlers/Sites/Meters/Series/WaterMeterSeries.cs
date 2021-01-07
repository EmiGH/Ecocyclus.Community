using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class WaterMeterSeries
    {
        internal WaterMeterSeries() { }
           
        #region Read Methods

        internal Dictionary<Int64, Objects.Sites.Meters.Series.WaterSerie> ReadDaily(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.WaterSeries _dbSeries = new Storage.WaterSeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.WaterSerie> _serie = new Dictionary<long, Objects.Sites.Meters.Series.WaterSerie>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterDaily(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToInt64(_dbRecord["IdSiteWaterMeterSerie"]), new Library.Objects.Sites.Meters.Series.WaterSerie(Convert.ToInt64(_dbRecord["IdSiteWaterMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToDateTime(_dbRecord["Day"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), credential));
            }
            return _serie;
        }
        internal Dictionary<Int64, Objects.Sites.Meters.Series.WaterSerie> ReadWeekly(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.WaterSeries _dbSeries = new Storage.WaterSeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.WaterSerie> _serie = new Dictionary<long, Objects.Sites.Meters.Series.WaterSerie>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterWeekly(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToInt64(_dbRecord["IdSiteWaterMeterSerie"]), new Library.Objects.Sites.Meters.Series.WaterSerie(Convert.ToInt64(_dbRecord["IdSiteWaterMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToDateTime(_dbRecord["Day"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), credential));
            }
            return _serie;
        }
        internal Dictionary<Int64, Objects.Sites.Meters.Series.WaterSerie> ReadMonthly(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.WaterSeries _dbSeries = new Storage.WaterSeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.WaterSerie> _serie = new Dictionary<long, Objects.Sites.Meters.Series.WaterSerie>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterMonthly(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToInt64(_dbRecord["IdSiteWaterMeterSerie"]), new Library.Objects.Sites.Meters.Series.WaterSerie(Convert.ToInt64(_dbRecord["IdSiteWaterMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToDateTime(_dbRecord["Day"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), credential));
            }
            return _serie;
        }
        internal Dictionary<Int64, Objects.Sites.Meters.Series.WaterSerie> ReadYearly(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.WaterSeries _dbSeries = new Storage.WaterSeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.WaterSerie> _serie = new Dictionary<long, Objects.Sites.Meters.Series.WaterSerie>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterYearly(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToInt64(_dbRecord["IdSiteWaterMeterSerie"]), new Library.Objects.Sites.Meters.Series.WaterSerie(Convert.ToInt64(_dbRecord["IdSiteWaterMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeter"]), Convert.ToInt64(_dbRecord["IdSiteWaterMeterLoad"]), Convert.ToDateTime(_dbRecord["Day"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), credential));
            }
            return _serie;
        }
                
        #endregion
    }
}
