using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Payments
{
    public class Payment
    {
        internal Payment(Int64 idSitePayment, Int64 idOperator, DateTime timestamp, DateTime from, DateTime to, Double amount, Auxiliaries.Units.Currency currency, String idTransaction, String data, Security.Credential credential)
        {
            _IdPayment = idSitePayment;
            _IdOperator = idOperator;
            _Timestamp = timestamp;
            _From = from;
            _To = to;
            _Amount = amount;
            _Currency = currency;
            _IdTransaction = idTransaction;
            _Data = data;

            _Credential = credential;
        }

        #region Private Properties

        private Security.Credential _Credential;

        private Int64 _IdPayment;
        private Int64 _IdOperator;
        private DateTime _Timestamp;
        private DateTime _From;
        private DateTime _To;
        private Double _Amount;
        Auxiliaries.Units.Currency _Currency;
        private String _IdTransaction;
        private String _Data;

        #endregion

        #region Public Properties

        public Int64 IdPayment
        { get { return _IdPayment; } }
        public Objects.Users.UserOperatorCoworker Operator
        { get { return (Users.UserOperatorCoworker)new Handlers.Operators().Item(_IdOperator, _Credential); } }
        public DateTime Timestamp
        { get { return _Timestamp; } }
        public DateTime From
        { get { return _From; } }
        public DateTime To
        { get { return _To; } }
        public Double Amount
        { get { return _Amount; } }
        public Auxiliaries.Units.Currency Currency
        { get { return _Currency; } }
        public String IdTransaction
        { get { return _IdTransaction; } }
        public String Data
        { get { return _Data; } }

        #endregion

    }
}
