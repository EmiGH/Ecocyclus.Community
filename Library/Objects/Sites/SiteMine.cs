using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites
{
    public class SiteMine : Site
    {
        internal SiteMine(Int64 idSite, Int64 idCompany, Auxiliaries.Types.SiteType type, String idLanguage, DateTime timestamp, DateTime start, Int32 weeks, Auxiliaries.Units.TimeRange loadTimeRange, String title, String number, Objects.Auxiliaries.Geographic.Contact contact, Int64 idCountry, Double value, Int64 idCurrency, Double floorSpace, Int64 units, String client, String agent, String contractor, String responsible, String manager, String description, Boolean isPublic, Int64 idImage, Boolean loadOverdue, Boolean targetSurpassed, Security.Credential credential)
            : base(idSite, idCompany, type, idLanguage, timestamp,start, weeks, title, number, contact, idCountry, value, idCurrency, floorSpace, units, client, agent, contractor, responsible, manager, description, isPublic, idImage, credential)
        {
            _LoadTimeRange = loadTimeRange;
            _LoadOverdue = LoadOverdue;
            _TargetSurpassed = targetSurpassed;
        }

        #region Public Properties

        private Auxiliaries.Units.TimeRange _LoadTimeRange;
        public Auxiliaries.Units.TimeRange LoadTimeRange
        { get { return _LoadTimeRange; } }

        private Boolean _LoadOverdue;
        public Boolean LoadOverdue
        { get { return _LoadOverdue; } }

        private Boolean _TargetSurpassed;
        public Boolean TargetSurpassed
        { get { return _TargetSurpassed; } }

        public new Companies.CompanyMine Company
        { get { return (Companies.CompanyMine)new Handlers.Companies().Item(_IdCompany, Credential); } }

        #endregion

        #region Public Methods

        #region Meters

        public Int32 MetersQuantity
        {
            get { return new Handlers.Sites().MetersQuantity(IdSite); }
        }
        public Dictionary<Int64, Meters.ElectricityMeter> ElectricityMeters
        {
            get { return new Handlers.ElectricityMeters().Items(IdSite, Credential); }
        }
        public Dictionary<Int64, Meters.WaterMeter> WaterMeters
        {
            get { return new Handlers.WaterMeters().Items(IdSite, Credential);  }
        }
        public Dictionary<Int64, Meters.FuelMeter> FuelMeters
        {
            get { return new Handlers.FuelMeters().Items(IdSite, Credential);  }
        }
        public Dictionary<Int64, Meters.WasteMeter> WasteMeters
        {
            get { return new Handlers.WasteMeters().Items(IdSite, Credential);  }
        }
        public Dictionary<Int64, Meters.TransportMeter> TransportMeters
        {
            get { return new Handlers.TransportMeters().Items(IdSite, Credential); }
        }
        
        #endregion

        #region Emission Factors

        public Auxiliaries.EmissionFactors.ElectricityEmissionFactor GetEmissionFactorForElectricity()
        {
            //return new Handlers.ElectricityEmissionFactors().ItemGlobal(Credential);
            return new Handlers.ElectricityEmissionFactors().ItemDefault(Country.IdCountry, Credential); 
        }
        public Dictionary<Int64, Auxiliaries.EmissionFactors.FuelTypeEmissionFactor> GetEmissionFactorsForFuels()
        {
            //return new Handlers.FuelTypeEmissionFactors().ItemsGlobal(Credential);
            return new Handlers.FuelTypeEmissionFactors().ItemsDefault(Country.IdCountry, Credential);
        }
        public Dictionary<Int64, Auxiliaries.EmissionFactors.TransportTypeEmissionFactor> GetEmissionFactorsForTransport()
        {
            //return new Handlers.TransportTypeEmissionFactors().ItemsGlobal(Credential);
            return new Handlers.TransportTypeEmissionFactors().ItemsDefault(Country.IdCountry, Credential); 
        }
        public Dictionary<Int64, Auxiliaries.EmissionFactors.WasteTypeEmissionFactor> GetEmissionFactorsForWaste()
        {
            //return new Handlers.WasteTypeEmissionFactors().ItemsGlobal(Credential);
            return new Handlers.WasteTypeEmissionFactors().ItemsDefault(Country.IdCountry, Credential); 
        
        }
        public Auxiliaries.EmissionFactors.WaterEmissionFactor GetEmissionFactorForWater()
        {
            //return new Handlers.WaterEmissionFactors().ItemGlobal(Credential);
            return new Handlers.WaterEmissionFactors().ItemDefault(Country.IdCountry, Credential); 
        }

        #endregion

        #region Metrics

        public Targets Targets
        { get { return new Handlers.SiteTargets().Item(IdSite, Credential); } }

        #region CO2

        public Metrics.Evolution GetCO2Evolution(DateTime start, DateTime end, Int16 interval, Auxiliaries.Units.TimeUnit.Units timeUnit)
        {
            return new Handlers.CO2Series().ReadSiteEvolution(IdSite, new Auxiliaries.Units.TimeRange(start, end), interval, timeUnit, Credential);
        }
        public Metrics.Evolution GetCO2Evolution(Int16 interval, Auxiliaries.Units.TimeUnit.Units timeUnit)
        {
            return new Handlers.CO2Series().ReadSiteEvolution(IdSite, interval, timeUnit, Credential);
        }
        public Metrics.Evolution GetCO2Evolution(Auxiliaries.Units.TimeRange timeRange, Int16 interval, Auxiliaries.Units.TimeUnit.Units timeUnit)
        {
            return new Handlers.CO2Series().ReadSiteEvolution(IdSite, timeRange, interval, timeUnit, Credential);
        }

        public Metrics.MetricPeriod GetCO2Statistics(DateTime from, DateTime to)
        { return new Handlers.CO2Series().ReadSiteStatistics(IdSite, from, to, Credential); }
        public Metrics.MetricPeriod GetCO2Statistics()
        { return new Handlers.CO2Series().ReadSiteStatistics(IdSite, Credential); }
        public Metrics.MetricComposite GetCO2StatisticsByProtocols(DateTime from, DateTime to)
        { return new Handlers.CO2Series().ReadSiteStatisticsByProtocols(IdSite, from, to, Credential); }
        public Metrics.MetricComposite GetCO2StatisticsByProtocols()
        { return new Handlers.CO2Series().ReadSiteStatisticsByProtocols(IdSite, Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Daily()
        { return new Handlers.CO2Series().ReadSiteDaily(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Daily(DateTime from, DateTime to)
        { return new Handlers.CO2Series().ReadSiteDaily(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Weekly()
        { return new Handlers.CO2Series().ReadSiteWeekly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Weekly(DateTime from, DateTime to)
        { return new Handlers.CO2Series().ReadSiteWeekly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Monthly()
        { return new Handlers.CO2Series().ReadSiteMonthly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Monthly(DateTime from, DateTime to)
        { return new Handlers.CO2Series().ReadSiteMonthly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Yearly()
        { return new Handlers.CO2Series().ReadSiteYearly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Yearly(DateTime from, DateTime to)
        { return new Handlers.CO2Series().ReadSiteYearly(IdSite, from, to, Credential); }

        #endregion

        #region Electricity

        public Metrics.MetricPeriod GetElectricityStatistics(DateTime from, DateTime to)
        { return new Handlers.ElectricitySeries().ReadSiteStatistics(IdSite, from, to, Credential); }
        public Metrics.MetricPeriod GetElectricityStatistics()
        { return new Handlers.ElectricitySeries().ReadSiteStatistics(IdSite, Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityDaily()
        { return new Handlers.ElectricitySeries().ReadSiteDaily(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityDaily(DateTime from, DateTime to)
        { return new Handlers.ElectricitySeries().ReadSiteDaily(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityWeekly()
        { return new Handlers.ElectricitySeries().ReadSiteWeekly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityWeekly(DateTime from, DateTime to)
        { return new Handlers.ElectricitySeries().ReadSiteWeekly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityMonthly()
        { return new Handlers.ElectricitySeries().ReadSiteMonthly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityMonthly(DateTime from, DateTime to)
        { return new Handlers.ElectricitySeries().ReadSiteMonthly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityYearly()
        { return new Handlers.ElectricitySeries().ReadSiteYearly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityYearly(DateTime from, DateTime to)
        { return new Handlers.ElectricitySeries().ReadSiteYearly(IdSite, from, to, Credential); }

        #endregion

        #region Fuels

        public Metrics.MetricPeriod GetFuelsStatistics(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadSiteStatistics(IdSite, from, to, Credential); }
        public Metrics.MetricPeriod GetFuelsStatistics()
        { return new Handlers.FuelSeries().ReadSiteStatistics(IdSite, Credential); }
        public Metrics.MetricPeriod GetFuelsStatistics(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadSiteStatistics(IdSite, idFuelType, from, to, Credential); }
        public Metrics.MetricPeriod GetFuelsStatistics(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadSiteStatistics(IdSite, idFuelType, Credential); }
        public Metrics.MetricComposite GetFuelsStatisticsByTypes(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadSiteStatisticsByTypes(IdSite, from, to, Credential); }
        public Metrics.MetricComposite GetFuelsStatisticsByTypes()
        { return new Handlers.FuelSeries().ReadSiteStatisticsByTypes(IdSite, Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsDaily()
        { return new Handlers.FuelSeries().ReadSiteDaily(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsDaily(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadSiteDaily(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsDaily(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadSiteDaily(IdSite, idFuelType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsDaily(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadSiteDaily(IdSite, idFuelType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsWeekly()
        { return new Handlers.FuelSeries().ReadSiteWeekly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsWeekly(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadSiteWeekly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsWeekly(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadSiteWeekly(IdSite, idFuelType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsWeekly(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadSiteWeekly(IdSite, idFuelType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsMonthly()
        { return new Handlers.FuelSeries().ReadSiteMonthly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsMonthly(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadSiteMonthly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsMonthly(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadSiteMonthly(IdSite, idFuelType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsMonthly(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadSiteMonthly(IdSite, idFuelType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsYearly()
        { return new Handlers.FuelSeries().ReadSiteYearly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsYearly(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadSiteYearly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsYearly(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadSiteYearly(IdSite, idFuelType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsYearly(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadSiteYearly(IdSite, idFuelType, from, to, Credential); }

        #endregion

        #region Transport

        public Metrics.MetricPeriod GetTransportStatistics(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadSiteStatistics(IdSite, from, to, Credential); }
        public Metrics.MetricPeriod GetTransportStatistics()
        { return new Handlers.TransportSeries().ReadSiteStatistics(IdSite, Credential); }
        public Metrics.MetricPeriod GetTransportStatistics(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadSiteStatistics(IdSite, idTransportType, from, to, Credential); }
        public Metrics.MetricPeriod GetTransportStatistics(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadSiteStatistics(IdSite, idTransportType, Credential); }
        public Metrics.MetricComposite GetTransportStatisticsByTypes(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadSiteStatisticsByTypes(IdSite, from, to, Credential); }
        public Metrics.MetricComposite GetTransportStatisticsByTypes()
        { return new Handlers.TransportSeries().ReadSiteStatisticsByTypes(IdSite, Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportDaily()
        { return new Handlers.TransportSeries().ReadSiteDaily(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportDaily(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadSiteDaily(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportDaily(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadSiteDaily(IdSite, idTransportType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportDaily(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadSiteDaily(IdSite, idTransportType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportWeekly()
        { return new Handlers.TransportSeries().ReadSiteWeekly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportWeekly(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadSiteWeekly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportWeekly(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadSiteWeekly(IdSite, idTransportType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportWeekly(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadSiteWeekly(IdSite, idTransportType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportMonthly()
        { return new Handlers.TransportSeries().ReadSiteMonthly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportMonthly(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadSiteMonthly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportMonthly(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadSiteMonthly(IdSite, idTransportType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportMonthly(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadSiteMonthly(IdSite, idTransportType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportYearly()
        { return new Handlers.TransportSeries().ReadSiteYearly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportYearly(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadSiteYearly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportYearly(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadSiteYearly(IdSite, idTransportType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportYearly(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadSiteYearly(IdSite, idTransportType, from, to, Credential); }

        #endregion

        #region Water

        public Metrics.MetricPeriod GetWaterStatistics(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadSiteStatistics(IdSite, from, to, Credential); }
        public Metrics.MetricPeriod GetWaterStatistics()
        { return new Handlers.WaterSeries().ReadSiteStatistics(IdSite, Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterDaily()
        { return new Handlers.WaterSeries().ReadSiteDaily(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterDaily(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadSiteDaily(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterWeekly()
        { return new Handlers.WaterSeries().ReadSiteWeekly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterWeekly(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadSiteWeekly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterMonthly()
        { return new Handlers.WaterSeries().ReadSiteMonthly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterMonthly(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadSiteMonthly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterYearly()
        { return new Handlers.WaterSeries().ReadSiteYearly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterYearly(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadSiteYearly(IdSite, from, to, Credential); }

        #endregion

        #region Waste

        public Metrics.MetricPeriod GetWasteStatistics(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadSiteStatistics(IdSite, from, to, Credential); }
        public Metrics.MetricPeriod GetWasteStatistics()
        { return new Handlers.WasteSeries().ReadSiteStatistics(IdSite, Credential); }
        public Metrics.MetricPeriod GetWasteStatistics(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadSiteStatistics(IdSite, idWasteType, from, to, Credential); }
        public Metrics.MetricPeriod GetWasteStatistics(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadSiteStatistics(IdSite, idWasteType, Credential); }
        public Metrics.MetricComposite GetWasteStatisticsByTypes(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadSiteStatisticsByTypes(IdSite, from, to, Credential); }
        public Metrics.MetricComposite GetWasteStatisticsByTypes()
        { return new Handlers.WasteSeries().ReadSiteStatisticsByTypes(IdSite, Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteDaily()
        { return new Handlers.WasteSeries().ReadSiteDaily(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteDaily(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadSiteDaily(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteDaily(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadSiteDaily(IdSite, idWasteType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteDaily(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadSiteDaily(IdSite, idWasteType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteWeekly()
        { return new Handlers.WasteSeries().ReadSiteWeekly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteWeekly(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadSiteWeekly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteWeekly(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadSiteWeekly(IdSite, idWasteType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteWeekly(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadSiteWeekly(IdSite, idWasteType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteMonthly()
        { return new Handlers.WasteSeries().ReadSiteMonthly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteMonthly(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadSiteMonthly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteMonthly(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadSiteMonthly(IdSite, idWasteType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteMonthly(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadSiteMonthly(IdSite, idWasteType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteYearly()
        { return new Handlers.WasteSeries().ReadSiteYearly(IdSite, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteYearly(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadSiteYearly(IdSite, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteYearly(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadSiteYearly(IdSite, idWasteType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteYearly(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadSiteYearly(IdSite, idWasteType, from, to, Credential); }

        #endregion

        #endregion

        #region Permissions

        public Dictionary<Int64, Permission> GetPermissionsGranted()
        {
            return new Handlers.Permissions().ItemsBySite(IdSite, Credential);
        }
        public Dictionary<Int64, Users.UserOperatorCoworker> GetPermissionsNotGranted()
        {
            return new Handlers.Permissions().UsersNotGranted(IdSite, Credential);
        }
        public Dictionary<Int64, Users.UserOperatorCoworker> GetManagers()
        {
            return new Handlers.Permissions().ItemsManagersBySite(IdSite, Credential);
        }
        internal Boolean IsGranted(Users.UserOperatorCoworker userOperator, Security.Authority.PermissionTypes permission)
        { 
            return new Handlers.Permissions().HasPermission(IdSite, userOperator.IdOperator, permission);
        }
        private Boolean IsGranted(Users.UserOperatorMe me, Security.Authority.PermissionTypes permission)
        { 
            return new Handlers.Permissions().HasPermission(IdSite, me.IdOperator, permission);
        }

        public Security.Authority.PermissionTypes CurrentPermission()
        {
            if (IsGranted((Users.UserOperatorMe)Credential.CurrentUser, Security.Authority.PermissionTypes.SiteManager))
                return Security.Authority.PermissionTypes.SiteManager;

            if (IsGranted((Users.UserOperatorMe)Credential.CurrentUser, Security.Authority.PermissionTypes.SiteOperator))
                return Security.Authority.PermissionTypes.SiteOperator;

            return Security.Authority.PermissionTypes.SiteReader;
        }

        #endregion

        #region Payments

        public Payments.PaymentScale PaymentScale
        {
            get { return new Handlers.PaymentScales().Match(Country.IdCountry, Value, Credential); }
        }
        public Dictionary<Int64, Payments.Payment> Payments()
        { return new Handlers.SitePayments().ItemsBySite(IdSite,Credential);}
        Payments.Payment Payment(Int64 idPayment)
        { return new Handlers.SitePayments().Item(idPayment, Credential); }

        #endregion

        #region Exceptions
        
        public Dictionary<Int64, Exceptions.ExceptionMeter> ExceptionsForOverdue()
        {
            return new Handlers.SiteExceptionMeters().Items(IdSite, Credential);
        }

        public Dictionary<Int64, Exceptions.ExceptionMeterElectricity> ExceptionsForOverdueOfElectricity()
        {
            return new Handlers.SiteExceptionMeters().ItemsByElectricity(IdSite, Credential);
        }
        public Exceptions.ExceptionMeterElectricity ExceptionForOverdueOfElectricity(Int64 idException)
        {
            return new Handlers.SiteExceptionMeters().ItemByElectricity(idException, Credential);
        }
        public Dictionary<Int64, Exceptions.ExceptionMeterFuel> ExceptionsForOverdueOfFuel()
        {
            return new Handlers.SiteExceptionMeters().ItemsByFuel(IdSite, Credential);
        }
        public Exceptions.ExceptionMeterFuel ExceptionForOverdueOfFuel(Int64 idException)
        {
            return new Handlers.SiteExceptionMeters().ItemByFuel(idException, Credential);
        }
        public Dictionary<Int64, Exceptions.ExceptionMeterTransport> ExceptionsForOverdueOfTransport()
        {
            return new Handlers.SiteExceptionMeters().ItemsByTransport(IdSite, Credential);
        }
        public Exceptions.ExceptionMeterTransport ExceptionForOverdueOfTransport(Int64 idException)
        {
            return new Handlers.SiteExceptionMeters().ItemByTransport(idException, Credential);
        }
        public Dictionary<Int64, Exceptions.ExceptionMeterWaste> ExceptionsForOverdueOfWaste()
        {
            return new Handlers.SiteExceptionMeters().ItemsByWaste(IdSite, Credential);
        }
        public Exceptions.ExceptionMeterWaste ExceptionForOverdueOfWaste(Int64 idException)
        {
            return new Handlers.SiteExceptionMeters().ItemByWaste(idException, Credential);
        }
        public Dictionary<Int64, Exceptions.ExceptionMeterWater> ExceptionsForOverdueOfWater()
        {
            return new Handlers.SiteExceptionMeters().ItemsByWater(IdSite, Credential);
        }
        public Exceptions.ExceptionMeterWater ExceptionForOverdueOfWater(Int64 idException)
        {
            return new Handlers.SiteExceptionMeters().ItemByWater(idException, Credential);
        }

        public Dictionary<Int64, Exceptions.ExceptionTarget> ExceptionsForTargetSurpassed()
        {
            return new Handlers.SiteExceptionTargets().Items(IdSite);
        }
        public Exceptions.ExceptionTarget ExceptionForTargetSurpassed(Int64 idException)
        {
            return new Handlers.SiteExceptionTargets().Item(idException);
        }
        

        #endregion

        #endregion
    }
}
