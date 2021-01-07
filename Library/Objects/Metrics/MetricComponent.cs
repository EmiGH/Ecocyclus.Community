using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Metrics
{
    public class MetricComponent
    {
        private String _Name;
        private Double _Sum;
        private Double _Share;

        internal MetricComponent(String name, Double sum, Double share)
        {
            _Name = name;
            _Sum = sum;
            _Share = share;
        }

        public String Name
        { get { return _Name; } }
        public Double Sum
        { get { return _Sum; } }
        public Double Share
        { get { return _Share; } }
    }

}
