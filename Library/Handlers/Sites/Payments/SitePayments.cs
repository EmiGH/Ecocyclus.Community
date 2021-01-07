using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    public class SitePayments
    {
        internal SitePayments() { }

        #region Read Functions

        internal Library.Objects.Sites.Payments.Payment Item(Int64 idSitePayment, Security.Credential credential)
        {
            Storage.SitePayments _dbSitePayments = new Storage.SitePayments();
            Library.Objects.Sites.Payments.Payment _payment = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSitePayments.ReadById(idSitePayment, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_payment != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _payment = new Library.Objects.Sites.Payments.Payment(Convert.ToInt64(_dbRecord["IdSitePayment"]), Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Amount"]), new Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential), Convert.ToString(_dbRecord["IdTransaction"]), Convert.ToString(Common.CastNullValues(_dbRecord["Data"], "")), credential);
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                    _payment = new Library.Objects.Sites.Payments.Payment(Convert.ToInt64(_dbRecord["IdSitePayment"]), Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Amount"]), new Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential), Convert.ToString(_dbRecord["IdTransaction"]), Convert.ToString(Common.CastNullValues(_dbRecord["Data"], "")), credential);

            }
            return _payment;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Payments.Payment> Items(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Payments.Payment> _oItems = new Dictionary<Int64, Library.Objects.Sites.Payments.Payment>();
            Storage.SitePayments _dbSitePayments = new Storage.SitePayments();
            Library.Objects.Sites.Payments.Payment _payment = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSitePayments.ReadApproved(_idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdSitePayment"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdSitePayment"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _payment = new Library.Objects.Sites.Payments.Payment(Convert.ToInt64(_dbRecord["IdSitePayment"]), Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Amount"]), new Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential), Convert.ToString(_dbRecord["IdTransaction"]), Convert.ToString(Common.CastNullValues(_dbRecord["Data"], "")), credential);
                    _oItems.Add(_payment.IdPayment, _payment);
                }
            }
            return _oItems;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Payments.Payment> ItemsBySite(Int64 idSite, Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Payments.Payment> _oItems = new Dictionary<Int64, Library.Objects.Sites.Payments.Payment>();
            Storage.SitePayments _dbSitePayments = new Storage.SitePayments();
            Library.Objects.Sites.Payments.Payment _payment = null;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSitePayments.ReadApprovedBySite(idSite, _idLanguage);

            Boolean _insert = true;
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdSitePayment"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _idLanguage)
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdSitePayment"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _payment = new Library.Objects.Sites.Payments.Payment(Convert.ToInt64(_dbRecord["IdSitePayment"]), Convert.ToInt64(_dbRecord["IdCompanyUser"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToDateTime(_dbRecord["From"]), Convert.ToDateTime(_dbRecord["To"]), Convert.ToDouble(_dbRecord["Amount"]), new Objects.Auxiliaries.Units.Currency(Convert.ToInt64(_dbRecord["IdCurrency"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Symbol"]), Convert.ToDouble(_dbRecord["ConversionIndex"]), Convert.ToBoolean(_dbRecord["IsPattern"]), Convert.ToString(_dbRecord["PaymentSystemCode"]), credential), Convert.ToString(_dbRecord["IdTransaction"]), Convert.ToString(Common.CastNullValues(_dbRecord["Data"], "")), credential);
                    _oItems.Add(_payment.IdPayment, _payment);
                }
            }
            return _oItems;
        }
        
        #endregion

        #region Write Functions

        internal Library.Objects.Sites.Payments.Payment Add(Objects.Sites.SiteMine site, Int64 idOperator, DateTime from, DateTime to, Double amount, Int64 idCurrency, String idTransaction, String data, Security.Credential credential)
        {
            Storage.SitePayments _dbSitePayments = new Storage.SitePayments();
            
            try
            {
                DateTime _from, _to;
                _from = new DateTime(from.Year, from.Month, 1);
                _to = new DateTime(to.Year, to.Month, DateTime.DaysInMonth(to.Year, to.Month));

                Objects.Sites.Payments.PaymentScale _scale = new PaymentScales().Match(site.Country.IdCountry, site.Value, credential);
                
                //Setup Fee
                Double _setupUpFee=0;
                if (new Handlers.SitePayments().ItemsBySite(site.IdSite, credential).Count == 0)
                    _setupUpFee = _scale.FirstPayment;

                //Check amount
                Int32 _months = ((_to.Year - _from.Year) * 12) + _to.Month - _from.Month;
                Double _total = Math.Round(_scale.Amount*_months+_setupUpFee,2);
                
                if (_total != amount)
                   throw new ApplicationException(Resources.Messages.PaymentErrorDifferentAmount);
                
                //Check Dates
                if(from>=to)
                    throw new ApplicationException(Resources.Messages.PaymentErrorWrongDates);

                DateTime _newStart=site.LoadTimeRange.Start, _newEnd=site.LoadTimeRange.End;
                //if from is greater than current end date for valid load then the load range is expanding to the future
                if (_from > site.LoadTimeRange.End)
                {
                    //The new start date must be one day after the current end date
                    if (_newEnd.AddDays(1) != _from)
                        throw new ApplicationException(Resources.Messages.PaymentErrorWrongDates);
                }
                //else, if to is lower than current start date for valid load then the load range is expanding to the past
                else
                {
                    //The new end date must be one day earlier than the current from date
                    if (_to.AddDays(1) != _newStart)
                        throw new ApplicationException(Resources.Messages.PaymentErrorWrongDates);
                }
                //Add Payment
                Int64 _idSitePayment = _dbSitePayments.Create(site.IdSite, idOperator, _from, _to, _total, _scale.Currency.IdCurrency, idTransaction, data);
                
                //Send administration mail
                Objects.Notifications.Mailer _mailer = new Objects.Notifications.Mailer();
                Handlers.Mail _config = new Handlers.Mail();

                String _body = Resources.Messages.PaymentAdministrationMailBody;
                _body = _body.Replace("[company]", site.Company.Name);
                _body = _body.Replace("[period]", from.ToShortDateString() + " - " + to.ToShortDateString());
                _body = _body.Replace("[amount]", _scale.Currency.Symbol + " " + amount.ToString());
                _body = _body.Replace("[transaction]", idTransaction.ToString());
                _body += Resources.Messages.MailFooter;
                _mailer.Send(_config.Configuration().Receiver, Resources.Messages.PaymentAdministrationMailSubject, _body);

                return Item(_idSitePayment, credential);

            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedRecord);
                else
                    throw sqlex;
            }

        }
        internal Objects.Sites.Payments.Payment Modify(Objects.Sites.SiteMine site, Objects.Sites.Payments.Payment payment, String idTransaction, Boolean confirmed, DateTime confirmedDate, String confirmedMessage, Security.Credential credential)
        {
            Storage.SitePayments _dbPayments = new Storage.SitePayments();

            using (TransactionScope _scope = new TransactionScope())
            {
                _dbPayments.Update(payment.IdPayment, idTransaction, confirmed, confirmedDate, confirmedMessage);
                if (confirmed)
                {
                    DateTime _newStart = site.LoadTimeRange.Start, _newEnd = site.LoadTimeRange.End;

                    if (payment.From > site.LoadTimeRange.End)
                        _newEnd = payment.To;
                    else
                        _newStart = payment.From;

                    new Sites().ModifyValidLoadRange(site.IdSite, _newStart, _newEnd);
                }

                //Send administration mail
                Objects.Notifications.Mailer _mailer = new Objects.Notifications.Mailer();
                Handlers.Mail _config = new Handlers.Mail();

                String _body = Resources.Messages.PaymentConfirmedAdministrationMailBody;
                _body = _body.Replace("[company]", site.Company.Name);
                _body = _body.Replace("[transaction]", idTransaction.ToString());
                _body += Resources.Messages.MailFooter;
                _mailer.Send(_config.Configuration().Receiver, Resources.Messages.PaymentConfirmedAdministrationMailSubject, _body);

                _scope.Complete();
            }
            
            return Item(payment.IdPayment, credential);
        }

        #endregion
    }
}
