using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Metrics
{
    public class MetricInstant : Metric
    {
        private DateTime _Instant;

        internal MetricInstant(DateTime instant, Double min, Double max, Double sum, Double avg, Double minCO2, Double maxCO2, Double sumCO2, Double avgCO2, Auxiliaries.Units.Unit unit)
            : base(min, max, sum, avg, minCO2, maxCO2, sumCO2, avgCO2, unit)
        {
            _Instant = instant;
        }

        public DateTime Instant
        { get { return _Instant; } }
    }
}
