using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class ElectricityMeterSeries
    {
        internal ElectricityMeterSeries() { }
           
        #region Read Methods

        internal Dictionary<Int64, Objects.Sites.Meters.Series.ElectricitySerie> ReadDaily(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.ElectricitySeries _dbSeries = new Storage.ElectricitySeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.ElectricitySerie> _serie = new Dictionary<long, Objects.Sites.Meters.Series.ElectricitySerie>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterDaily(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToInt64(_dbRecord["IdSiteElectricityMeterSerie"]), new Library.Objects.Sites.Meters.Series.ElectricitySerie(Convert.ToInt64(_dbRecord["IdSiteElectricityMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteElectricityMeter"]), Convert.ToInt64(_dbRecord["IdSiteElectricityMeterLoad"]), Convert.ToDateTime(_dbRecord["Day"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), credential));
            }
            return _serie;
        }
        internal Dictionary<Int64, Objects.Sites.Meters.Series.ElectricitySerie> ReadWeekly(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.ElectricitySeries _dbSeries = new Storage.ElectricitySeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.ElectricitySerie> _serie = new Dictionary<long, Objects.Sites.Meters.Series.ElectricitySerie>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterWeekly(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToInt64(_dbRecord["IdSiteElectricityMeterSerie"]), new Library.Objects.Sites.Meters.Series.ElectricitySerie(Convert.ToInt64(_dbRecord["IdSiteElectricityMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteElectricityMeter"]), Convert.ToInt64(_dbRecord["IdSiteElectricityMeterLoad"]), Convert.ToDateTime(_dbRecord["Day"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), credential));
            }
            return _serie;
        }
        internal Dictionary<Int64, Objects.Sites.Meters.Series.ElectricitySerie> ReadMonthly(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.ElectricitySeries _dbSeries = new Storage.ElectricitySeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.ElectricitySerie> _serie = new Dictionary<long, Objects.Sites.Meters.Series.ElectricitySerie>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterMonthly(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToInt64(_dbRecord["IdSiteElectricityMeterSerie"]), new Library.Objects.Sites.Meters.Series.ElectricitySerie(Convert.ToInt64(_dbRecord["IdSiteElectricityMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteElectricityMeter"]), Convert.ToInt64(_dbRecord["IdSiteElectricityMeterLoad"]), Convert.ToDateTime(_dbRecord["Day"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), credential));
            }
            return _serie;
        }
        internal Dictionary<Int64, Objects.Sites.Meters.Series.ElectricitySerie> ReadYearly(Int64 idMeter, DateTime from, DateTime to, Security.Credential credential)
        {
            Storage.ElectricitySeries _dbSeries = new Storage.ElectricitySeries();
            Dictionary<Int64, Objects.Sites.Meters.Series.ElectricitySerie> _serie = new Dictionary<long, Objects.Sites.Meters.Series.ElectricitySerie>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSeries.ReadMeterYearly(idMeter, from, to);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _serie.Add(Convert.ToInt64(_dbRecord["IdSiteElectricityMeterSerie"]), new Library.Objects.Sites.Meters.Series.ElectricitySerie(Convert.ToInt64(_dbRecord["IdSiteElectricityMeterSerie"]), Convert.ToInt64(_dbRecord["IdSiteElectricityMeter"]), Convert.ToInt64(_dbRecord["IdSiteElectricityMeterLoad"]), Convert.ToDateTime(_dbRecord["Day"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToDouble(_dbRecord["ValuePattern"]), Convert.ToDouble(_dbRecord["TotalCO2"]), credential));
            }
            return _serie;
        }
                
        #endregion
    }
}
