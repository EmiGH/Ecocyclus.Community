using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Metrics
{
    public class MetricComposite
    {
        private Dictionary<String, MetricComponent> _Components;

        private Double _Max;
        private Double _Min;
        private Double _Sum;
        private Double _Avg;
        private Int32 _Count;

        private Auxiliaries.Units.Unit _Unit;

        internal MetricComposite(List<KeyValuePair<String, Double>> values, Auxiliaries.Units.Unit unit)
        {
            _Components = new Dictionary<string, MetricComponent>();
            _Count = values.Count;
            _Unit = unit;

            if (_Count > 0)
            {
                _Sum = values.Sum(e => e.Value);
                _Avg = _Sum / _Count;
                foreach (KeyValuePair<String, Double> _item in values)
                {
                    _Components.Add(_item.Key, new MetricComponent(_item.Key, _item.Value, _Sum == 0 ? 0 : _item.Value / _Sum * 100));
                    if (_Max < _item.Value) _Max = _item.Value;
                    if (_Min > _item.Value) _Min = _item.Value;
                }
            }
        }

        public Int32 Count
        { get { return _Count; } }
        public Double Max
        { get { return _Max; } }
        public Double Min
        { get { return _Min; } }
        public Double Avg
        { get { return _Avg; } }
        public Double Sum
        { get { return _Sum; } }
        public Auxiliaries.Units.Unit Unit
        { get { return _Unit; } }

        public Dictionary<String, MetricComponent> Components
        { get { return _Components; } }

    }
}
