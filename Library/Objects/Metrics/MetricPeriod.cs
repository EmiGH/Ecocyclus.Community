using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Metrics
{
    public class MetricPeriod : Metric
    {
        private DateTime _From;
        private DateTime _To;

        internal MetricPeriod(DateTime from, DateTime to, Double min, Double max, Double sum, Double avg, Double minCO2, Double maxCO2, Double sumCO2, Double avgCO2, Auxiliaries.Units.Unit unit)
            : base(min, max, sum, avg, minCO2, maxCO2, sumCO2, avgCO2, unit)
        {
            _From = from;
            _To = to;
        }
        internal MetricPeriod(Auxiliaries.Units.Unit unit)
            :base(unit)
        {
            _From = DateTime.MinValue;
            _To = DateTime.MaxValue;
        }

        public DateTime From
        { get { return _From; } }
        public DateTime To
        { get { return _To; } }
    }
}
