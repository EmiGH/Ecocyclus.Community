using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Metrics
{
    public class Metric
    {
        private Double _Min;
        private Double _Max;
        private Double _Sum;
        private Double _Avg;
        private Double _MinCO2;
        private Double _MaxCO2;
        private Double _SumCO2;
        private Double _AvgCO2;
        private Auxiliaries.Units.Unit _Unit;

        internal Metric(Double min, Double max, Double sum, Double avg, Double minCO2, Double maxCO2, Double sumCO2, Double avgCO2, Auxiliaries.Units.Unit unit)
        {
            _Min = min;
            _Max = max;
            _Sum = sum;
            _Avg = avg;
            _MinCO2 = minCO2;
            _MaxCO2 = maxCO2;
            _SumCO2 = sumCO2;
            _AvgCO2 = avgCO2;

            _Unit = unit;
        }
        internal Metric(Auxiliaries.Units.Unit unit)
        {
            _Min = 0;
            _Max = 0;
            _Sum = 0;
            _Avg = 0;
            _MinCO2 = 0;
            _MaxCO2 = 0;
            _SumCO2 = 0;
            _AvgCO2 = 0;

            _Unit = unit;
        }

        public Double Min
        { get { return _Min; } }
        public Double Max
        { get { return _Max; } }
        public Double Sum
        { get { return _Sum; } }
        public Double Avg
        { get { return _Avg; } }
        public Double MinCO2
        { get { return _MinCO2; } }
        public Double MaxCO2
        { get { return _MaxCO2; } }
        public Double SumCO2
        { get { return _SumCO2; } }
        public Double AvgCO2
        { get { return _AvgCO2; } }

        public Auxiliaries.Units.Unit Unit
        { get { return _Unit; } }


    }
}

