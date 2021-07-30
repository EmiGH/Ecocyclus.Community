using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class PaymentScales
    {
        internal PaymentScales() { }

        #region Read Functions

        internal Library.Objects.Sites.Payments.PaymentScale Item(Int64 idPaymentScale, Security.Credential credential)
        {
            Storage.SitePaymentScales _dbSitePayments = new Storage.SitePaymentScales();
            Library.Objects.Sites.Payments.PaymentScale _paymentScale = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSitePayments.ReadById(idPaymentScale, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_paymentScale != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                    {
                        _paymentScale = new Library.Objects.Sites.Payments.PaymentScale(idPaymentScale, Convert.ToInt64(_dbRecord["MinValue"]), Convert.ToInt64(_dbRecord["MaxValue"]), Convert.ToDouble(_dbRecord["Amount"]), new Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential), Convert.ToInt32(_dbRecord["MonthsFree"]), Convert.ToDouble(_dbRecord["FirstPayment"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _paymentScale = new Library.Objects.Sites.Payments.PaymentScale(idPaymentScale, Convert.ToInt64(_dbRecord["MinValue"]), Convert.ToInt64(_dbRecord["MaxValue"]), Convert.ToDouble(_dbRecord["Amount"]), new Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential), Convert.ToInt32(_dbRecord["MonthsFree"]), Convert.ToDouble(_dbRecord["FirstPayment"]));

                _insert = true;
            }
            return _paymentScale;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Payments.PaymentScale> Items(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Payments.PaymentScale> _oItems = new Dictionary<Int64, Library.Objects.Sites.Payments.PaymentScale>();
            Storage.SitePaymentScales _dbSitePayments = new Storage.SitePaymentScales();
            Library.Objects.Sites.Payments.PaymentScale _payment = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSitePayments.ReadAll(_idLanguage);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _payment = new Library.Objects.Sites.Payments.PaymentScale(Convert.ToInt64(_dbRecord["IdSitePaymentScale"]), Convert.ToInt64(_dbRecord["MinValue"]), Convert.ToInt64(_dbRecord["MaxValue"]), Convert.ToDouble(_dbRecord["Amount"]), new Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential), Convert.ToInt32(_dbRecord["MonthsFree"]), Convert.ToDouble(_dbRecord["FirstPayment"]));
                _oItems.Add(_payment.IdPaymentScale, _payment);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Payments.PaymentScale> ItemsByCountry(Int64 idCountry, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Payments.PaymentScale> _oItems = new Dictionary<Int64, Library.Objects.Sites.Payments.PaymentScale>();
            Storage.SitePaymentScales _dbSitePayments = new Storage.SitePaymentScales();
            Library.Objects.Sites.Payments.PaymentScale _payment = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSitePayments.ReadByCountry(idCountry, _idLanguage);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _payment = new Library.Objects.Sites.Payments.PaymentScale(Convert.ToInt64(_dbRecord["IdSitePaymentScale"]), Convert.ToInt64(_dbRecord["MinValue"]), Convert.ToInt64(_dbRecord["MaxValue"]), Convert.ToDouble(_dbRecord["Amount"]), new Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential), Convert.ToInt32(_dbRecord["MonthsFree"]), Convert.ToDouble(_dbRecord["FirstPayment"]));
                _oItems.Add(_payment.IdPaymentScale, _payment);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Payments.PaymentScale> ItemsGlobal(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Payments.PaymentScale> _oItems = new Dictionary<Int64, Library.Objects.Sites.Payments.PaymentScale>();
            Storage.SitePaymentScales _dbSitePayments = new Storage.SitePaymentScales();
            Library.Objects.Sites.Payments.PaymentScale _payment = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSitePayments.ReadGlobal(_idLanguage);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _payment = new Library.Objects.Sites.Payments.PaymentScale(Convert.ToInt64(_dbRecord["IdSitePaymentScale"]), Convert.ToInt64(_dbRecord["MinValue"]), Convert.ToInt64(_dbRecord["MaxValue"]), Convert.ToDouble(_dbRecord["Amount"]), new Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential), Convert.ToInt32(_dbRecord["MonthsFree"]), Convert.ToDouble(_dbRecord["FirstPayment"]));
                _oItems.Add(_payment.IdPaymentScale, _payment);
            }
            return _oItems;
        }
        internal Library.Objects.Sites.Payments.PaymentScale Match(Int64 idCountry, Double siteValue, Security.Credential credential)
        {
            Storage.SitePaymentScales _dbSitePayments = new Storage.SitePaymentScales();
            Library.Objects.Sites.Payments.PaymentScale _paymentScale = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSitePayments.FindMatch(idCountry, siteValue, _idLanguage);

            Boolean _insert=false;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //There is a scale already loaded
                if (_paymentScale != null)
                {
                    //Then check to see if this new record is country based
                    if (Convert.ToInt64(Common.CastNullValues(_dbRecord["IdCountry"], 0)) == idCountry)
                    {
                        //If it is a country based record for the scale then ckeck the language of the unit
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                        {
                            //If country and language match replace scale
                            _insert = true;
                        }
                        else
                            //If language differs then check the amount to see if match 
                            if (_paymentScale.Amount != Convert.ToDouble(_dbRecord["Amount"]))
                            {
                                //If amounts do not match then previous scale its not country based so load new country based record 
                                _insert = true;
                            }
                    }
                    else
                    {
                        //If amounts concurr then check language
                        if (_paymentScale.Amount != Convert.ToDouble(_dbRecord["Amount"]))
                        {
                            if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage.ToUpper())
                            {
                                //Amounts are the same then load new language specific scale
                                _insert = true;
                            }

                        }
                    }
                }
                else
                { 
                    //Load a first scale no matter what
                    _insert = true; 
                }

                if (_insert)
                    _paymentScale = new Library.Objects.Sites.Payments.PaymentScale(Convert.ToInt64(_dbRecord["IdSitePaymentScale"]), Convert.ToInt64(_dbRecord["MinValue"]), Convert.ToInt64(_dbRecord["MaxValue"]), Convert.ToDouble(_dbRecord["Amount"]), new Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential), Convert.ToInt32(_dbRecord["MonthsFree"]), Convert.ToDouble(_dbRecord["FirstPayment"]));

                _insert = true;
            }
            return _paymentScale;
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Sites.Payments.PaymentScale Add(Int64 idCountry, Int64 minValue, Int64 maxValue, Double amount, Int64 idCurrency, Int32 monthsFree, Double firstPayment, Security.Credential credential)
        {
            Storage.SitePaymentScales _dbSitePaymentScales = new Storage.SitePaymentScales();
            
            try
            {
                Int64 _idSitePaymentScale = _dbSitePaymentScales.Create(idCountry, minValue, maxValue, amount, idCurrency, monthsFree, firstPayment);
                return Item(_idSitePaymentScale, credential);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }

        }
        internal void Remove(Int64 idSitePaymentScale)
        {
            Storage.SitePaymentScales _dbSitePayments = new Storage.SitePaymentScales();

            try
            {
                _dbSitePayments.Delete(idSitePaymentScale);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        
        #endregion
    }
}
