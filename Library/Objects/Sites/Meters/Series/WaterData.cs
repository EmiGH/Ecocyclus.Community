using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters.Series
{
    public class WaterData
    {
        private DateTime _From;
        private DateTime _To;
        private Double _Value;

        public WaterData(DateTime from, DateTime to, Double value)
        {
            if (from >= to)
                throw new ApplicationException(Resources.Messages.DataLoadInvalidPeriod);

            _From = from;
            _To = to;
            _Value = value;
        }

        public DateTime From
        { get { return _From; } }
        public DateTime To
        { get { return _To; } }
        public Double Value
        { get { return _Value; } }

        public Int32 Days
        { get { return ((TimeSpan)(_To - _From)).Days; } }
    }
}
