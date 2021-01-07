using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Auxiliaries.Units
{
    public class Cost
    {
        private Double _Value;
        private Currency _Currency;

        internal Cost(Double value, Currency currency)
        {
            _Value = value;
            _Currency = currency;
        }

        public Double Value
        { get { return _Value; } }
        public Currency Currency
        { get { return _Currency; } }
    }
}
