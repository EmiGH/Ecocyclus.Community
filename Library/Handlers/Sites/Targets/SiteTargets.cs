using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Transactions;
using System.Globalization;
using System.Threading;

namespace CSI.Library.Handlers
{
    internal class SiteTargets
    {
        internal SiteTargets() 
        {
        }

        #region Read Functions

        internal Library.Objects.Sites.Targets Item(Int64 idSite, Security.Credential credential)
        {
            Storage.SiteTargets _dbTargets = new Storage.SiteTargets();
            Library.Objects.Sites.Targets _targets = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbTargets.ReadById(idSite);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _targets = new Library.Objects.Sites.Targets(Convert.ToDouble(_dbRecord["ElectricityConsumption"]), Convert.ToInt64(_dbRecord["IdElectricityUnit"]), Convert.ToDouble(_dbRecord["ElectricityCO2"]), Convert.ToDouble(_dbRecord["FuelConsumption"]), Convert.ToInt64(_dbRecord["IdFuelUnit"]), Convert.ToDouble(_dbRecord["FuelCO2"]), Convert.ToDouble(_dbRecord["TransportConsumption"]), Convert.ToInt64(_dbRecord["IdTransportUnit"]), Convert.ToDouble(_dbRecord["TransportCO2"]), Convert.ToDouble(_dbRecord["WasteConsumption"]), Convert.ToInt64(_dbRecord["IdWasteUnit"]), Convert.ToDouble(_dbRecord["WasteCO2"]), Convert.ToDouble(_dbRecord["WaterConsumption"]), Convert.ToInt64(_dbRecord["IdWaterUnit"]), Convert.ToDouble(_dbRecord["WaterCO2"]), Convert.ToDouble(_dbRecord["TotalCO2"]), credential);
            }
            return _targets;
        }
        
        #endregion

        #region Write Functions

        internal void Modify(Int64 idSite, Double electricityConsumption, Int64 idElectricityUnit, Double electricityCO2, Double fuelConsumption, Int64 idFuelUnit, Double fuelCO2, Double transportConsumption, Int64 idTransportUnit, Double transportCO2, Double wasteConsumption, Int64 idWasteUnit, Double wasteCO2, Double waterConsumption, Int64 idWaterUnit, Double waterCO2, Double totalCO2)
        {
            //Update Target
            new Storage.SiteTargets().Update(idSite, electricityConsumption, idElectricityUnit, electricityCO2, fuelConsumption, idFuelUnit, fuelCO2, transportConsumption, idTransportUnit, transportCO2, wasteConsumption, idWasteUnit, wasteCO2, waterConsumption, idWaterUnit, waterCO2, totalCO2);
        }

        internal void EvaluteTargetWaste(Objects.Sites.SiteMine site, DateTime from, DateTime to, Security.Credential credential)
        {
            Objects.Sites.Targets _target = site.Targets;
            Objects.Metrics.MetricInstant _stats = site.GetWasteMonthly(from, to).First().Value;

            Double _deltaConsumption = 0, _deltaCO2 = 0;
            if (_target.WasteConsumption > 0)
                _deltaConsumption = _stats.Unit.ToPattern(_stats.Sum) - _target.WasteUnit.ToPattern(_target.WasteConsumption);
            if (_target.WasteCO2 > 0)
                _deltaCO2 = _stats.SumCO2 - _target.WasteCO2;

            String _unit = _stats.Unit.Symbol;
            String _unitCO2 = new CO2Units().ItemPattern(credential).Symbol;

            System.Globalization.DateTimeFormatInfo _info = new System.Globalization.DateTimeFormatInfo();
            String _monthName = _info.GetMonthName(from.Month);
            String _yearName = from.Year.ToString();

            String _preMessage = Resources.Messages.NotificationSiteTargetSurpassedMessage;
            _preMessage = _preMessage.Replace("[site]", site.Title);
            _preMessage = _preMessage.Replace("[month]", _monthName);
            _preMessage = _preMessage.Replace("[year]", _yearName);

            //Save current culture
            CultureInfo _currentCultureInfo = Thread.CurrentThread.CurrentCulture;

            ///////////////////////////////
            // Waste
            ///////////////////////////////
            if (_deltaConsumption > 0)
            {
                String _message = _preMessage;
                _message = _message.Replace("[amount]", _deltaConsumption.ToString());
                _message = _message.Replace("[unit]", _unit);

                //Write Exception
                new SiteExceptionTargets().Add(site.IdSite, (Int16)Objects.Common.Data.Protocols.Waste);

                //Write Notifications for site managers
                foreach (Objects.Users.UserOperator _item in site.GetManagers().Values)
                {
                    //Set Language 
                    CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                    Thread.CurrentThread.CurrentCulture = _cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                    //Write Notification
                    new Notifications().Add(_item.IdUser, Resources.Messages.NotificationSiteTargetSurpassedTitle, _message.Replace("[protocol]", Resources.Data.Waste), credential);
                }
            }

            ///////////////////////////////
            // CO2
            ///////////////////////////////
            if (_deltaCO2 > 0)
            {
                String _message = _preMessage;
                _message = _message.Replace("[amount]", _deltaCO2.ToString());
                _message = _message.Replace("[unit]", _unitCO2);

                //Write Exception
                new SiteExceptionTargets().Add(site.IdSite, (Int16)Objects.Common.Data.Protocols.CO2);

                //Write Notifications for site managers
                foreach (Objects.Users.UserOperator _item in site.GetManagers().Values)
                {
                    //Set Language 
                    CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                    Thread.CurrentThread.CurrentCulture = _cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                    //Write Notification
                    new Notifications().Add(_item.IdUser, Resources.Messages.NotificationSiteTargetSurpassedTitle, _message.Replace("[protocol]", Resources.Data.CO2), credential);
                }
            }

            //Restore culture
            Thread.CurrentThread.CurrentCulture = _currentCultureInfo;
            Thread.CurrentThread.CurrentUICulture = _currentCultureInfo;
        }
        internal void EvaluteTargetTransport(Objects.Sites.SiteMine site, DateTime from, DateTime to, Security.Credential credential)
        {
            Objects.Sites.Targets _target = site.Targets;
            Objects.Metrics.MetricInstant _stats = site.GetTransportMonthly(from, to).First().Value;

            Double _deltaConsumption = 0, _deltaCO2 = 0;
            if (_target.TransportConsumption > 0)
                _deltaConsumption = _stats.Unit.ToPattern(_stats.Sum) - _target.TransportUnit.ToPattern(_target.TransportConsumption);
            if (_target.TransportCO2 > 0)
                _deltaCO2 = _stats.SumCO2 - _target.TransportCO2;

            String _unit = _stats.Unit.Symbol;
            String _unitCO2 = new CO2Units().ItemPattern(credential).Symbol;

            System.Globalization.DateTimeFormatInfo _info = new System.Globalization.DateTimeFormatInfo();
            String _monthName = _info.GetMonthName(from.Month);
            String _yearName = from.Year.ToString();

            String _preMessage = Resources.Messages.NotificationSiteTargetSurpassedMessage;
            _preMessage = _preMessage.Replace("[site]", site.Title);
            _preMessage = _preMessage.Replace("[month]", _monthName);
            _preMessage = _preMessage.Replace("[year]", _yearName);

            //Save current culture
            CultureInfo _currentCultureInfo = Thread.CurrentThread.CurrentCulture;

            ///////////////////////////////
            // Transport
            ///////////////////////////////
            if (_deltaConsumption > 0)
            {
                String _message = _preMessage;
                _message = _message.Replace("[amount]", _deltaConsumption.ToString());
                _message = _message.Replace("[unit]", _unit);

                //Write Exception
                new SiteExceptionTargets().Add(site.IdSite, (Int16)Objects.Common.Data.Protocols.Transport);

                //Write Notifications for site managers
                foreach (Objects.Users.UserOperator _item in site.GetManagers().Values)
                {
                    //Set Language 
                    CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                    Thread.CurrentThread.CurrentCulture = _cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                    //Write Notification
                    new Notifications().Add(_item.IdUser, Resources.Messages.NotificationSiteTargetSurpassedTitle, _message.Replace("[protocol]", Resources.Data.Transport), credential);
                }

            }

            ///////////////////////////////
            // CO2
            ///////////////////////////////
            if (_deltaCO2 > 0)
            {
                String _message = _preMessage;
                _message = _message.Replace("[amount]", _deltaCO2.ToString());
                _message = _message.Replace("[unit]", _unitCO2);

                //Write Exception
                new SiteExceptionTargets().Add(site.IdSite, (Int16)Objects.Common.Data.Protocols.CO2);

                //Write Notifications for site managers
                foreach (Objects.Users.UserOperator _item in site.GetManagers().Values)
                {
                    //Set Language 
                    CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                    Thread.CurrentThread.CurrentCulture = _cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                    //Write Notification
                    new Notifications().Add(_item.IdUser, Resources.Messages.NotificationSiteTargetSurpassedTitle, _message.Replace("[protocol]", Resources.Data.CO2), credential);
                }
            }

            //Restore culture
            Thread.CurrentThread.CurrentCulture = _currentCultureInfo;
            Thread.CurrentThread.CurrentUICulture = _currentCultureInfo;
        }
        internal void EvaluteTargetElectricity(Objects.Sites.SiteMine site, DateTime from, DateTime to, Security.Credential credential)
        {
            Objects.Sites.Targets _target = site.Targets;
            Objects.Metrics.MetricInstant _stats = site.GetElectricityMonthly(from, to).First().Value;

            Double _deltaConsumption = 0, _deltaCO2 = 0;
            if (_target.ElectricityConsumption > 0)
                _deltaConsumption = _stats.Unit.ToPattern(_stats.Sum) - _target.ElectricityUnit.ToPattern(_target.ElectricityConsumption);
            if (_target.ElectricityCO2 > 0)
                _deltaCO2 = _stats.SumCO2 - _target.ElectricityCO2;

            String _unit = _stats.Unit.Symbol;
            String _unitCO2 = new CO2Units().ItemPattern(credential).Symbol;

            System.Globalization.DateTimeFormatInfo _info = new System.Globalization.DateTimeFormatInfo();
            String _monthName = _info.GetMonthName(from.Month);
            String _yearName = from.Year.ToString();

            String _preMessage = Resources.Messages.NotificationSiteTargetSurpassedMessage;
            _preMessage = _preMessage.Replace("[site]", site.Title);
            _preMessage = _preMessage.Replace("[month]", _monthName);
            _preMessage = _preMessage.Replace("[year]", _yearName);

            //Save current culture
            CultureInfo _currentCultureInfo = Thread.CurrentThread.CurrentCulture;

            ///////////////////////////////
            // Electricity
            ///////////////////////////////
            if (_deltaConsumption > 0)
            {
                String _message = _preMessage;
                _message = _message.Replace("[amount]", _deltaConsumption.ToString());
                _message = _message.Replace("[unit]", _unit);

                //Write Exception
                new SiteExceptionTargets().Add(site.IdSite, (Int16)Objects.Common.Data.Protocols.Electricity);

                //Write Notifications for site managers
                foreach (Objects.Users.UserOperator _item in site.GetManagers().Values)
                {
                    //Set Language 
                    CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                    Thread.CurrentThread.CurrentCulture = _cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                    //Write Notification
                    new Notifications().Add(_item.IdUser, Resources.Messages.NotificationSiteTargetSurpassedTitle, _message.Replace("[protocol]", Resources.Data.Electricity), credential);
                }
            }

            ///////////////////////////////
            // CO2
            ///////////////////////////////
            if (_deltaCO2 > 0)
            {
                String _message = _preMessage;
                _message = _message.Replace("[amount]", _deltaCO2.ToString());
                _message = _message.Replace("[unit]", _unitCO2);

                //Write Exception
                new SiteExceptionTargets().Add(site.IdSite, (Int16)Objects.Common.Data.Protocols.CO2);

                //Write Notifications for site managers
                foreach (Objects.Users.UserOperator _item in site.GetManagers().Values)
                {
                    //Set Language 
                    CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                    Thread.CurrentThread.CurrentCulture = _cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                    //Write Notification
                    new Notifications().Add(_item.IdUser, Resources.Messages.NotificationSiteTargetSurpassedTitle, _message.Replace("[protocol]", Resources.Data.CO2), credential);
                }
            }

            //Restore culture
            Thread.CurrentThread.CurrentCulture = _currentCultureInfo;
            Thread.CurrentThread.CurrentUICulture = _currentCultureInfo;
        }
        internal void EvaluteTargetFuel(Objects.Sites.SiteMine site, DateTime from, DateTime to, Security.Credential credential)
        {
            Objects.Sites.Targets _target = site.Targets;
            Objects.Metrics.MetricInstant _stats = site.GetFuelsMonthly(from, to).First().Value;

            Double _deltaConsumption = 0, _deltaCO2 = 0;
            if (_target.FuelConsumption > 0)
                _deltaConsumption = _stats.Unit.ToPattern(_stats.Sum) - _target.FuelUnit.ToPattern(_target.FuelConsumption);
            if (_target.FuelCO2 > 0)
                _deltaCO2 = _stats.SumCO2 - _target.FuelCO2;

            String _unit = _stats.Unit.Symbol;
            String _unitCO2 = new CO2Units().ItemPattern(credential).Symbol;

            System.Globalization.DateTimeFormatInfo _info = new System.Globalization.DateTimeFormatInfo();
            String _monthName = _info.GetMonthName(from.Month);
            String _yearName = from.Year.ToString();

            String _preMessage = Resources.Messages.NotificationSiteTargetSurpassedMessage;
            _preMessage = _preMessage.Replace("[site]", site.Title);
            _preMessage = _preMessage.Replace("[month]", _monthName);
            _preMessage = _preMessage.Replace("[year]", _yearName);

            //Save current culture
            CultureInfo _currentCultureInfo = Thread.CurrentThread.CurrentCulture;

            ///////////////////////////////
            // Fuel
            ///////////////////////////////
            if (_deltaConsumption > 0)
            {
                String _message = _preMessage;
                _message = _message.Replace("[amount]", _deltaConsumption.ToString());
                _message = _message.Replace("[unit]", _unit);

                //Write Exception
                new SiteExceptionTargets().Add(site.IdSite, (Int16)Objects.Common.Data.Protocols.Fuel);

                //Write Notifications for site managers
                foreach (Objects.Users.UserOperator _item in site.GetManagers().Values)
                {
                    //Set Language 
                    CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                    Thread.CurrentThread.CurrentCulture = _cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                    //Write Notification
                    new Notifications().Add(_item.IdUser, Resources.Messages.NotificationSiteTargetSurpassedTitle, _message.Replace("[protocol]", Resources.Data.Fuel), credential);
                }
            }

            ///////////////////////////////
            // CO2
            ///////////////////////////////
            if (_deltaCO2 > 0)
            {
                String _message = _preMessage;
                _message = _message.Replace("[amount]", _deltaCO2.ToString());
                _message = _message.Replace("[unit]", _unitCO2);

                //Write Exception
                new SiteExceptionTargets().Add(site.IdSite, (Int16)Objects.Common.Data.Protocols.CO2);

                //Write Notifications for site managers
                foreach (Objects.Users.UserOperator _item in site.GetManagers().Values)
                {
                    //Set Language 
                    CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                    Thread.CurrentThread.CurrentCulture = _cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                    //Write Notification
                    new Notifications().Add(_item.IdUser, Resources.Messages.NotificationSiteTargetSurpassedTitle, _message.Replace("[protocol]", Resources.Data.CO2), credential);
                }
            }

            //Restore culture
            Thread.CurrentThread.CurrentCulture = _currentCultureInfo;
            Thread.CurrentThread.CurrentUICulture = _currentCultureInfo;
        }
        internal void EvaluteTargetWater(Objects.Sites.SiteMine site, DateTime from, DateTime to, Security.Credential credential)
        {
            Objects.Sites.Targets _target = site.Targets;
            Objects.Metrics.MetricInstant _stats = site.GetWaterMonthly(from, to).First().Value;
            
            Double _deltaConsumption=0, _deltaCO2=0;
            if(_target.WaterConsumption>0)
                 _deltaConsumption = _stats.Unit.ToPattern(_stats.Sum) - _target.WaterUnit.ToPattern(_target.WaterConsumption);
            if(_target.WaterCO2>0)
                _deltaCO2 = _stats.SumCO2 - _target.WaterCO2;

            String _unit = _stats.Unit.Symbol;
            String _unitCO2 = new CO2Units().ItemPattern(credential).Symbol;

            System.Globalization.DateTimeFormatInfo _info = new System.Globalization.DateTimeFormatInfo();
            String _monthName = _info.GetMonthName(from.Month);
            String _yearName = from.Year.ToString();

            String _preMessage = Resources.Messages.NotificationSiteTargetSurpassedMessage;
            _preMessage = _preMessage.Replace("[site]", site.Title);
            _preMessage = _preMessage.Replace("[month]", _monthName);
            _preMessage = _preMessage.Replace("[year]", _yearName);

            //Save current culture
            CultureInfo _currentCultureInfo = Thread.CurrentThread.CurrentCulture;

            ///////////////////////////////
            // Water
            ///////////////////////////////
            if (_deltaConsumption > 0)
            {
                String _message = _preMessage;
                _message = _message.Replace("[amount]", _deltaConsumption.ToString());
                _message = _message.Replace("[unit]", _unit);

                //Write Exception
                new SiteExceptionTargets().Add(site.IdSite, (Int16)Objects.Common.Data.Protocols.Water);

                //Write Notifications for site managers
                foreach (Objects.Users.UserOperator _item in site.GetManagers().Values)
                {
                    //Set Language 
                    CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                    Thread.CurrentThread.CurrentCulture = _cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                    //Write Notification
                    new Notifications().Add(_item.IdUser, Resources.Messages.NotificationSiteTargetSurpassedTitle, _message.Replace("[protocol]", Resources.Data.Water), credential);
                }

            }

            ///////////////////////////////
            // CO2
            ///////////////////////////////
            if (_deltaCO2 > 0)
            {
                String _message = _preMessage;
                _message = _message.Replace("[amount]", _deltaCO2.ToString());
                _message = _message.Replace("[unit]", _unitCO2);

                //Write Exception
                new SiteExceptionTargets().Add(site.IdSite, (Int16)Objects.Common.Data.Protocols.CO2);

                //Write Notifications for site managers
                foreach (Objects.Users.UserOperator _item in site.GetManagers().Values)
                {
                    //Set Language 
                    CultureInfo _cultureInfo = new CultureInfo(_item.Language.IdLanguage);

                    Thread.CurrentThread.CurrentCulture = _cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = _cultureInfo;

                    //Write Notification
                    new Notifications().Add(_item.IdUser, Resources.Messages.NotificationSiteTargetSurpassedTitle, _message.Replace("[protocol]", Resources.Data.CO2), credential);
                }

            }

            //Restore culture
            Thread.CurrentThread.CurrentCulture = _currentCultureInfo;
            Thread.CurrentThread.CurrentUICulture = _currentCultureInfo;
        }

        internal void CheckTargetStatus(Int64 idSite)
        {
            DateTime _monthStart = Objects.Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(DateTime.Now, 1, Objects.Auxiliaries.Units.TimeUnit.Units.Month);
            DateTime _monthEnd = Objects.Auxiliaries.Units.TimeRange.GetNormalizedEndDate(DateTime.Now, 1, Objects.Auxiliaries.Units.TimeUnit.Units.Month);

            UpdateTargetStatus(idSite, (new SiteExceptionTargets().Items(idSite, _monthStart, _monthEnd).Count>0));
                
        }
        private void UpdateTargetStatus(Int64 idSite, Boolean surpassed)
        {
            //Update Target
            new Storage.SiteTargets().UpdateStatus(idSite, surpassed);
        }

        #endregion
    }
}
