using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Payments
{
    public class PaymentScale
    {
        internal PaymentScale(Int64 idPaymentScale, Int64 minValue, Int64 maxValue, Double amount, Auxiliaries.Units.Currency currency, Int32 monthsFree, Double firstPayment)
        {
            _IdPaymentScale = idPaymentScale;
            _MinValue = minValue;
            _MaxValue = maxValue;
            _Amount = amount;
            _Currency = currency;
            _MonthsFree = monthsFree;
            _FirstPayment = firstPayment;
        }

        #region Private Properties

        private Int64 _IdPaymentScale;
        private Int64 _MinValue;
        private Int64 _MaxValue;
        private Double _Amount;
        Auxiliaries.Units.Currency _Currency;
        private Int32 _MonthsFree;
        private Double _FirstPayment;

        #endregion

        #region Public Properties

        public Int64 IdPaymentScale
        { get { return _IdPaymentScale; } }
        public Int64 MinValue
        { get { return _MinValue; } }
        public Int64 MaxValue
        { get { return _MaxValue; } }
        public Double Amount
        { get { return _Amount; } }
        public Auxiliaries.Units.Currency Currency
        { get { return _Currency; } }
        public Int32 MonthsFree
        { get { return _MonthsFree; } }
        public Double FirstPayment
        { get { return _FirstPayment; } }

        #endregion
    }
}
