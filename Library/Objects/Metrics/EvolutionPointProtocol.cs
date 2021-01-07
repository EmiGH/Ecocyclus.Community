using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Metrics
{
    public class EvolutionPointProtocol
    {
        private String _Name;
        private Double _Sum;
        private Double _SumCO2;
        private Double _ShareCO2;

        internal EvolutionPointProtocol(String name)
        {

        }
        internal EvolutionPointProtocol(String name, Double sum, Double sumCO2, Double shareCO2)
        {
            _Name = name;
            _Sum = sum;
            _SumCO2 = sumCO2;
            _ShareCO2 = shareCO2;
        }
        internal EvolutionPointProtocol(String name, Double sum)
        {
            _Name = name;
            _Sum = sum;
            _SumCO2 = 0;
            _ShareCO2 = 0;
        }

        public String Name
        { get { return _Name; } }
        public Double Sum
        { get { return _Sum; } }
        public Double SumCO2
        { get { return _SumCO2; } }
        public Double ShareCO2
        { get { return _ShareCO2; } }

        internal void Update(Double sum, Double sumCO2, Double shareCO2)
        {
            _Sum = sum;
            _SumCO2 = sumCO2;
            _ShareCO2 = shareCO2;
        }
        internal void Update(Double sum)
        {
            _Sum = sum;
        }
        internal void UpdateCO2(Double sumCO2, Double shareCO2)
        {
            _SumCO2 = sumCO2;
            _ShareCO2 = shareCO2;
        }
    }
}
