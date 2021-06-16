using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Types;
using System.Data.SqlClient;
using System.Transactions;

namespace CSI.Library.Handlers
{
    internal class Sites
    {
        internal Sites() { }
        
        #region Read Functions

        internal Library.Objects.Sites.Site ItemByOperator(Int64 idSite, Security.Credential credential)
        {
            Storage.Sites _dbSites = new Storage.Sites();
            Library.Objects.Sites.Site _site = null;
            Int64 _idOperator = ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator;

            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSites.ReadByIdByOperator(idSite, _idOperator, _idLanguage);
            
            Boolean _insert = true; 
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_site != null)
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _site = Library.Objects.Sites.fSite.CreateSite(Convert.ToInt64(_dbRecord["idCompany"]), idSite, new Objects.Auxiliaries.Types.SiteType(Convert.ToInt64(_dbRecord["idSiteType"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["TypeName"]), Convert.ToString(_dbRecord["TypeDescription"])), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToDateTime(_dbRecord["Start"]), Convert.ToInt32(_dbRecord["Weeks"]), new Objects.Auxiliaries.Units.TimeRange(Convert.ToDateTime(_dbRecord["ValidLoadFrom"]), Convert.ToDateTime(_dbRecord["ValidLoadTo"])), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Number"]), new Objects.Auxiliaries.Geographic.Contact(new Objects.Auxiliaries.Geographic.Location(Convert.ToString(_dbRecord["Location"]), new Objects.Auxiliaries.Geographic.Position(SqlGeography.Parse(Convert.ToString(_dbRecord["Position"])))), Convert.ToString(_dbRecord["Telephone"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Website"]), Convert.ToString(_dbRecord["Facebook"]), Convert.ToString(_dbRecord["Twitter"])), Convert.ToInt64(_dbRecord["IdCountry"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToInt64(_dbRecord["idCurrency"]), Convert.ToDouble(_dbRecord["FloorSpace"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["Units"], 0)), Convert.ToString(_dbRecord["Client"]), Convert.ToString(_dbRecord["Agent"]), Convert.ToString(_dbRecord["Contractor"]), Convert.ToString(_dbRecord["Responsible"]), Convert.ToString(_dbRecord["Manager"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["isPublic"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdImage"], 0)), Convert.ToBoolean(_dbRecord["LoadOverdue"]), Convert.ToBoolean(_dbRecord["TargetSurpassed"]), Convert.ToBoolean(_dbRecord["IsClosed"]), credential);
                        _insert = false;
                    }
                }
                if (_insert)
                    _site = Library.Objects.Sites.fSite.CreateSite(Convert.ToInt64(_dbRecord["idCompany"]), idSite, new Objects.Auxiliaries.Types.SiteType(Convert.ToInt64(_dbRecord["idSiteType"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["TypeName"]), Convert.ToString(_dbRecord["TypeDescription"])), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToDateTime(_dbRecord["Start"]), Convert.ToInt32(_dbRecord["Weeks"]), new Objects.Auxiliaries.Units.TimeRange(Convert.ToDateTime(_dbRecord["ValidLoadFrom"]), Convert.ToDateTime(_dbRecord["ValidLoadTo"])), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Number"]), new Objects.Auxiliaries.Geographic.Contact(new Objects.Auxiliaries.Geographic.Location(Convert.ToString(_dbRecord["Location"]), new Objects.Auxiliaries.Geographic.Position(SqlGeography.Parse(Convert.ToString(_dbRecord["Position"])))), Convert.ToString(_dbRecord["Telephone"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Website"]), Convert.ToString(_dbRecord["Facebook"]), Convert.ToString(_dbRecord["Twitter"])), Convert.ToInt64(_dbRecord["IdCountry"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToInt64(_dbRecord["idCurrency"]), Convert.ToDouble(_dbRecord["FloorSpace"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["Units"], 0)), Convert.ToString(_dbRecord["Client"]), Convert.ToString(_dbRecord["Agent"]), Convert.ToString(_dbRecord["Contractor"]), Convert.ToString(_dbRecord["Responsible"]), Convert.ToString(_dbRecord["Manager"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["isPublic"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdImage"], 0)), Convert.ToBoolean(_dbRecord["LoadOverdue"]), Convert.ToBoolean(_dbRecord["TargetSurpassed"]), Convert.ToBoolean(_dbRecord["IsClosed"]), credential);
            }

            return _site;
        }
        internal Dictionary<Int64, Library.Objects.Sites.Site> ItemsByOperator(Security.Credential credential)
        {
            Dictionary<Int64, Library.Objects.Sites.Site> _sites = new Dictionary<long, Objects.Sites.Site>();
            Library.Objects.Sites.Site _site = null;
            Storage.Sites _dbSites = new Storage.Sites();
            
            Int64 _idOperator = ((Objects.Users.UserOperator)credential.CurrentUser).IdOperator;
            String _idLanguage = credential.CurrentLanguage.IdLanguage;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSites.ReadAllByOperator(_idOperator, _idLanguage);
            
            Boolean _insert = true; 
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_sites.ContainsKey(Convert.ToInt64(_dbRecord["IdSite"])))
                {
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() == _idLanguage)
                    {
                        _sites.Remove(Convert.ToInt64(_dbRecord["IdSite"]));
                    }
                    else
                    {
                        _insert = false;
                    }
                }
                if (_insert)
                {
                    _site = Library.Objects.Sites.fSite.CreateSite(Convert.ToInt64(_dbRecord["idCompany"]), Convert.ToInt64(_dbRecord["idSite"]), new Objects.Auxiliaries.Types.SiteType(Convert.ToInt64(_dbRecord["idSiteType"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["TypeName"]), Convert.ToString(_dbRecord["TypeDescription"])), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToDateTime(_dbRecord["Timestamp"]), Convert.ToDateTime(_dbRecord["Start"]), Convert.ToInt32(_dbRecord["Weeks"]), new Objects.Auxiliaries.Units.TimeRange(Convert.ToDateTime(_dbRecord["ValidLoadFrom"]), Convert.ToDateTime(_dbRecord["ValidLoadTo"])), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Number"]), new Objects.Auxiliaries.Geographic.Contact(new Objects.Auxiliaries.Geographic.Location(Convert.ToString(_dbRecord["Location"]), new Objects.Auxiliaries.Geographic.Position(SqlGeography.Parse(Convert.ToString(_dbRecord["Position"])))), Convert.ToString(_dbRecord["Telephone"]), Convert.ToString(_dbRecord["Email"]), Convert.ToString(_dbRecord["Website"]), Convert.ToString(_dbRecord["Facebook"]), Convert.ToString(_dbRecord["Twitter"])), Convert.ToInt64(_dbRecord["IdCountry"]), Convert.ToDouble(_dbRecord["Value"]), Convert.ToInt64(_dbRecord["idCurrency"]), Convert.ToDouble(_dbRecord["FloorSpace"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["Units"], 0)), Convert.ToString(_dbRecord["Client"]), Convert.ToString(_dbRecord["Agent"]), Convert.ToString(_dbRecord["Contractor"]), Convert.ToString(_dbRecord["Responsible"]), Convert.ToString(_dbRecord["Manager"]), Convert.ToString(_dbRecord["Description"]), Convert.ToBoolean(_dbRecord["isPublic"]), Convert.ToInt64(Common.CastNullValues(_dbRecord["IdImage"], 0)), Convert.ToBoolean(_dbRecord["LoadOverdue"]), Convert.ToBoolean(_dbRecord["TargetSurpassed"]), Convert.ToBoolean(_dbRecord["IsClosed"]), credential);
                    _sites.Add(_site.IdSite, _site); 
                }
                
            }
            return _sites;
        }
        
        internal Int32 MetersQuantity(Int64 idSite)
        {
            Storage.Sites _dbMeters = new Storage.Sites();
            return _dbMeters.MetersQuantity(idSite);
        }

        #endregion

        #region Write Functions

        internal Library.Objects.Sites.Site Add(Int64 idCompany, Int64 idSiteType, DateTime start, Int32 weeks, String title, String number, String location, Objects.Auxiliaries.Geographic.Position position, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String telephone, String email, String website, String facebook, String twitter, String client, String agent, String contractor, String responsible, String manager, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPublic, Int64 idImage, Security.Credential credential)
        {
            Storage.Sites _dbSites = new Storage.Sites();
            Storage.SiteLanguageOptions _dbLanguageOptions = new Storage.SiteLanguageOptions();

            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            try
            {
                Int64 _idSite;
                using (TransactionScope _scope = new TransactionScope())
                {
                    _idSite = _dbSites.Create(_defaultLanguage.IdLanguage, idCompany, idSiteType, start, weeks, title, number, location, position, idCountry, value, idCurrency, floorSpace, units, telephone, email, website, facebook, twitter, client, agent, contractor, responsible, manager, description, isPublic, idImage);
                    foreach (KeyValuePair<String, String> _item in descriptionTranslations)
                        _dbLanguageOptions.Create(_idSite, _item.Key, _item.Value);
    
                    DateTime _from = new DateTime(start.Year, start.Month, 1);
                    DateTime _to = new DateTime(start.Year+20, start.Month, DateTime.DaysInMonth(start.Year, start.Month));

                    ModifyValidLoadRange(_idSite, _from, _to);

                    _scope.Complete();
                }

                return ItemByOperator(_idSite, credential);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedSite);
                else
                    throw sqlex;
            }
        }
        internal void Remove(Int64 idSite)
        {
            Storage.Sites _dbSites = new Storage.Sites();

            try
            {
                _dbSites.Delete(idSite);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 547)
                    throw new ApplicationException(Resources.Messages.ErrorCannotDeleteExistingRelationship);
                else
                    throw sqlex;
            }
        }
        internal void Modify(Int64 idSite, Int64 idSiteType, DateTime start, Int32 weeks, String title, String number, String location, Objects.Auxiliaries.Geographic.Position position, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String telephone, String email, String website, String facebook, String twitter, String client, String agent, String contractor, String responsible, String manager, String description, List<KeyValuePair<String, String>> descriptionTranslations, Boolean isPublic, Int64 idImage)
        {
            Objects.Auxiliaries.Globalization.Language _defaultLanguage = new Languages().ItemDefault();

            Storage.SiteLanguageOptions _dbLanguageOptions = new Storage.SiteLanguageOptions();
            Storage.Sites _dbSites = new Storage.Sites();
            
            try 
            {
                using (TransactionScope _scope = new TransactionScope())
                {
                    _dbLanguageOptions.DeleteAll(idSite);
                    _dbSites.Update(_defaultLanguage.IdLanguage, idSite, idSiteType, start, weeks, title, number, location, position, idCountry, value, idCurrency, floorSpace, units, telephone, email, website, facebook, twitter, client, agent, contractor, responsible, manager, description, isPublic, idImage);
                    _dbLanguageOptions.Create(idSite, _defaultLanguage.IdLanguage, description);
                    foreach (KeyValuePair<String, String> _item in descriptionTranslations)
                        _dbLanguageOptions.Create(idSite, _item.Key, _item.Value);
    
                    _scope.Complete();
                }
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 || sqlex.Number == 2627)
                    throw new ApplicationException(Resources.Messages.DuplicatedSite);
                else
                    throw sqlex;
            }
        }
        internal void ModifyValidLoadRange(Int64 idSite, DateTime newStart, DateTime newEnd)
        {
            Storage.Sites _dbSites = new Storage.Sites();
            _dbSites.UpdateValidLoadRange(idSite, newStart, newEnd);
        }

        internal void UpdateStatus(Int64 idSite, Boolean isClosed)
        {
            //Update Management Status
            new Storage.Sites().UpdateStatus(idSite, isClosed);
        }
        internal void UpdateScheduleStatus(Int64 idSite)
        {
            //Update overdue status
            new Storage.Sites().UpdateScheduleStatus(idSite);
        }

        #region Automated Service

        internal void DoService(Security.Credential credential)
        {
            UpdateScheduleOverdue(credential);
            UpdateScheduleNotice(credential);
            UpdateScheduleStatus();
            new Notifications().Notify(credential);

        }

        private void UpdateScheduleStatus()
        {
            //Update Target
            new Storage.Sites().UpdateScheduleStatus();
        }
        private void UpdateScheduleOverdue(Security.Credential credential)
        {
            Storage.Sites _dbSites = new Storage.Sites();
            
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSites.ReadAllOverdue((Int16)Objects.Auxiliaries.Units.TimeUnit.Units.Year, (Int16)Objects.Auxiliaries.Units.TimeUnit.Units.Month, (Int16)Objects.Auxiliaries.Units.TimeUnit.Units.Week, (Int16)Objects.Auxiliaries.Units.TimeUnit.Units.Day);
            
            String _title = Resources.Messages.NotificationSiteMeterOverdueTitle;
            String _message = Resources.Messages.NotificationSiteMeterOverdueMessage;
            
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                switch (Convert.ToInt16(_dbRecord["Protocol"]))
                {
                    case 1:
                        new Handlers.SiteExceptionMeters().AddForElectricity(Convert.ToInt64(_dbRecord["IdMeter"]), credential);
                        break;

                    case 2:
                        new Handlers.SiteExceptionMeters().AddForWater(Convert.ToInt64(_dbRecord["IdMeter"]), credential);
                        break;

                    default:
                        break;
                }

                //TODO: Objects.Companies.CompanyAdministered _company = new Companies().ItemForAdministrator(Convert.ToInt64(_dbRecord["IdCompany"]), credential);
                
                //String _messageSite = _message.Replace("[meter]", Convert.ToString(_dbRecord["Identification"])).Replace("[site]", Convert.ToString(_dbRecord["Title"]));

                ////Write Notifications for company managers
                //foreach (Objects.Users.UserOperator _operator in _company.Managers.Values)
                //{
                //    //Set Language 
                //    CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                //    Thread.CurrentThread.CurrentCulture = _cultureInfo;
                //    Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                //    //Write Notification
                //    new Notifications().Add(_operator.IdUser, _title, _messageSite, credential);
                //}

                //foreach (Objects.Sites.SiteAdministered _site in _company.Site(Convert.ToInt64(_dbRecord["IdSite"])))
                //{
                //    //Write Notifications for site managers
                //    foreach (Objects.Users.UserOperator _operator in site.GetManagers().Values)
                //    {
                //        //Set Language 
                //        CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                //        Thread.CurrentThread.CurrentCulture = _cultureInfo;
                //        Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                //        //Write Notification
                //        new Notifications().Add(_operator.IdUser, _title, _messageSite, credential);
                //    }
                //} 
            }
        }
        private void UpdateScheduleNotice(Security.Credential credential)
        {
            Storage.Sites _dbSites = new Storage.Sites();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSites.ReadAllNotice((Int16)Objects.Auxiliaries.Units.TimeUnit.Units.Year, (Int16)Objects.Auxiliaries.Units.TimeUnit.Units.Month, (Int16)Objects.Auxiliaries.Units.TimeUnit.Units.Week, (Int16)Objects.Auxiliaries.Units.TimeUnit.Units.Day);

            String _title = Resources.Messages.NotificationSiteMeterNoticeTitle;
            String _message = Resources.Messages.NotificationSiteMeterNoticeMessage;

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //Objects.Companies.CompanyAdministered _company = new Companies().ItemForAdministrator(Convert.ToInt64(_dbRecord["IdCompany"]), credential);

                //String _messageSite = _message.Replace("[meter]", Convert.ToString(_dbRecord["Identification"])).Replace("[site]", Convert.ToString(_dbRecord["Title"]));

                ////Write Notifications for company managers
                //foreach (Objects.Users.UserOperator _operator in _company.Managers.Values)
                //{
                //    //Set Language 
                //    CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                //    Thread.CurrentThread.CurrentCulture = _cultureInfo;
                //    Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                //    //Write Notification
                //    new Notifications().Add(_operator.IdUser, _title, _messageSite, credential);
                //}

                //foreach (Objects.Sites.SiteAdministered _site in _company.Site(Convert.ToInt64(_dbRecord["IdSite"])))
                //{
                //    //Write Notifications for site managers
                //    foreach (Objects.Users.UserOperator _operator in site.GetManagers().Values)
                //    {
                //        //Set Language 
                //        CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                //        Thread.CurrentThread.CurrentCulture = _cultureInfo;
                //        Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                //        //Write Notification
                //        new Notifications().Add(_operator.IdUser, _title, _messageSite, credential);
                //    }
                //}
            }


        }

        #endregion

        #endregion

    }
}
