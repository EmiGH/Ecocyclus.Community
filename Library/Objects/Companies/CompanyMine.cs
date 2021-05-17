using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Companies
{
    public class CompanyMine : Company
    {
        internal CompanyMine(Int64 idCompany, String name, DateTime timestamp, Auxiliaries.Geographic.Contact contact, Int64 idLogo, Security.Credential credential)
            :base(idCompany, name, timestamp, contact, idLogo, credential)
        { }

        #region Public Methods

        public Dictionary<Int64, Users.UserOperatorCoworker> Operators
        { get { return new Handlers.Operators().Coworkers(IdCompany, _Credential); } }

        public Dictionary<Int64, Users.UserOperatorCoworker> Managers
        { get { return new Handlers.Operators().Managers(IdCompany, _Credential); } }

        #region Metrics

        public Double GetTotalArea()
        { return new Handlers.Companies().TotalArea(IdCompany); }
        public Auxiliaries.Units.Cost GetTotalCost()
        { return new Handlers.Companies().TotalCost(IdCompany, _Credential); }

        #region CO2

        public Metrics.MetricPeriod GetCO2Statistics(DateTime from, DateTime to)
        { return new Handlers.CO2Series().ReadCompanyStatistics(IdCompany, from, to, _Credential); }
        public Metrics.MetricPeriod GetCO2Statistics()
        { return new Handlers.CO2Series().ReadCompanyStatistics(IdCompany, _Credential); }
        public Metrics.MetricComposite GetCO2StatisticsByProtocols(DateTime from, DateTime to)
        { return new Handlers.CO2Series().ReadCompanyStatisticsByProtocols(IdCompany, from, to, _Credential); }
        public Metrics.MetricComposite GetCO2StatisticsByProtocols()
        { return new Handlers.CO2Series().ReadCompanyStatisticsByProtocols(IdCompany, _Credential); }
        public Metrics.MetricComposite GetCO2StatisticsBySites(DateTime from, DateTime to)
        { return new Handlers.CO2Series().ReadCompanyStatisticsBySites(IdCompany, from, to, _Credential); }
        public Metrics.MetricComposite GetCO2StatisticsBySites()
        { return new Handlers.CO2Series().ReadCompanyStatisticsBySites(IdCompany, _Credential); }
        
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Daily()
        { return new Handlers.CO2Series().ReadCompanyDaily(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Daily(DateTime from, DateTime to)
        { return new Handlers.CO2Series().ReadCompanyDaily(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Weekly()
        { return new Handlers.CO2Series().ReadCompanyWeekly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Weekly(DateTime from, DateTime to)
        { return new Handlers.CO2Series().ReadCompanyWeekly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Monthly()
        { return new Handlers.CO2Series().ReadCompanyMonthly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Monthly(DateTime from, DateTime to)
        { return new Handlers.CO2Series().ReadCompanyMonthly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Yearly()
        { return new Handlers.CO2Series().ReadCompanyYearly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetCO2Yearly(DateTime from, DateTime to)
        { return new Handlers.CO2Series().ReadCompanyYearly(IdCompany, from, to, _Credential); }

        #endregion

        #region Electricity

        public Metrics.MetricPeriod GetElectricityStatistics(DateTime from, DateTime to)
        { return new Handlers.ElectricitySeries().ReadCompanyStatistics(IdCompany, from, to, _Credential); }
        public Metrics.MetricPeriod GetElectricityStatistics()
        { return new Handlers.ElectricitySeries().ReadCompanyStatistics(IdCompany, _Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityDaily()
        { return new Handlers.ElectricitySeries().ReadCompanyDaily(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityDaily(DateTime from, DateTime to)
        { return new Handlers.ElectricitySeries().ReadCompanyDaily(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityWeekly()
        { return new Handlers.ElectricitySeries().ReadCompanyWeekly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityWeekly(DateTime from, DateTime to)
        { return new Handlers.ElectricitySeries().ReadCompanyWeekly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityMonthly()
        { return new Handlers.ElectricitySeries().ReadCompanyMonthly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityMonthly(DateTime from, DateTime to)
        { return new Handlers.ElectricitySeries().ReadCompanyMonthly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityYearly()
        { return new Handlers.ElectricitySeries().ReadCompanyYearly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetElectricityYearly(DateTime from, DateTime to)
        { return new Handlers.ElectricitySeries().ReadCompanyYearly(IdCompany, from, to, _Credential); }

        #endregion

        #region Fuels

        public Metrics.MetricPeriod GetFuelsStatistics(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadCompanyStatistics(IdCompany, from, to, _Credential); }
        public Metrics.MetricPeriod GetFuelsStatistics()
        { return new Handlers.FuelSeries().ReadCompanyStatistics(IdCompany, _Credential); }
        public Metrics.MetricPeriod GetFuelsStatistics(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadCompanyStatistics(IdCompany, idFuelType, from, to, _Credential); }
        public Metrics.MetricPeriod GetFuelsStatistics(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadCompanyStatistics(IdCompany, idFuelType, _Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsDaily()
        { return new Handlers.FuelSeries().ReadCompanyDaily(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsDaily(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadCompanyDaily(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsDaily(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadCompanyDaily(IdCompany, idFuelType, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsDaily(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadCompanyDaily(IdCompany, idFuelType, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsWeekly()
        { return new Handlers.FuelSeries().ReadCompanyWeekly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsWeekly(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadCompanyWeekly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsWeekly(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadCompanyWeekly(IdCompany, idFuelType, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsWeekly(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadCompanyWeekly(IdCompany, idFuelType, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsMonthly()
        { return new Handlers.FuelSeries().ReadCompanyMonthly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsMonthly(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadCompanyMonthly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsMonthly(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadCompanyMonthly(IdCompany, idFuelType, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsMonthly(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadCompanyMonthly(IdCompany, idFuelType, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsYearly()
        { return new Handlers.FuelSeries().ReadCompanyYearly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsYearly(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadCompanyYearly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsYearly(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadCompanyYearly(IdCompany, idFuelType, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFuelsYearly(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadCompanyYearly(IdCompany, idFuelType, from, to, _Credential); }

        #endregion

        #region Transport

        public Metrics.MetricPeriod GetTransportStatistics(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadCompanyStatistics(IdCompany, from, to, _Credential); }
        public Metrics.MetricPeriod GetTransportStatistics()
        { return new Handlers.TransportSeries().ReadCompanyStatistics(IdCompany, _Credential); }
        public Metrics.MetricPeriod GetTransportStatistics(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadCompanyStatistics(IdCompany, idTransportType, from, to, _Credential); }
        public Metrics.MetricPeriod GetTransportStatistics(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadCompanyStatistics(IdCompany, idTransportType, _Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportDaily()
        { return new Handlers.TransportSeries().ReadCompanyDaily(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportDaily(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadCompanyDaily(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportDaily(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadCompanyDaily(IdCompany, idTransportType, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportDaily(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadCompanyDaily(IdCompany, idTransportType, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportWeekly()
        { return new Handlers.TransportSeries().ReadCompanyWeekly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportWeekly(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadCompanyWeekly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportWeekly(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadCompanyWeekly(IdCompany, idTransportType, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportWeekly(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadCompanyWeekly(IdCompany, idTransportType, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportMonthly()
        { return new Handlers.TransportSeries().ReadCompanyMonthly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportMonthly(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadCompanyMonthly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportMonthly(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadCompanyMonthly(IdCompany, idTransportType, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportMonthly(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadCompanyMonthly(IdCompany, idTransportType, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportYearly()
        { return new Handlers.TransportSeries().ReadCompanyYearly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportYearly(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadCompanyYearly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportYearly(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadCompanyYearly(IdCompany, idTransportType, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetTransportYearly(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadCompanyYearly(IdCompany, idTransportType, from, to, _Credential); }

        #endregion

        #region Waste

        public Metrics.MetricPeriod GetWasteStatistics(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadCompanyStatistics(IdCompany, from, to, _Credential); }
        public Metrics.MetricPeriod GetWasteStatistics()
        { return new Handlers.WasteSeries().ReadCompanyStatistics(IdCompany, _Credential); }
        public Metrics.MetricPeriod GetWasteStatistics(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadCompanyStatistics(IdCompany, idWasteType, from, to, _Credential); }
        public Metrics.MetricPeriod GetWasteStatistics(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadCompanyStatistics(IdCompany, idWasteType, _Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteDaily()
        { return new Handlers.WasteSeries().ReadCompanyDaily(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteDaily(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadCompanyDaily(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteDaily(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadCompanyDaily(IdCompany, idWasteType, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteDaily(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadCompanyDaily(IdCompany, idWasteType, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteWeekly()
        { return new Handlers.WasteSeries().ReadCompanyWeekly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteWeekly(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadCompanyWeekly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteWeekly(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadCompanyWeekly(IdCompany, idWasteType, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteWeekly(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadCompanyWeekly(IdCompany, idWasteType, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteMonthly()
        { return new Handlers.WasteSeries().ReadCompanyMonthly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteMonthly(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadCompanyMonthly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteMonthly(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadCompanyMonthly(IdCompany, idWasteType, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteMonthly(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadCompanyMonthly(IdCompany, idWasteType, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteYearly()
        { return new Handlers.WasteSeries().ReadCompanyYearly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteYearly(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadCompanyYearly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteYearly(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadCompanyYearly(IdCompany, idWasteType, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWasteYearly(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadCompanyYearly(IdCompany, idWasteType, from, to, _Credential); }

        #endregion

        #region Water

        public Metrics.MetricPeriod GetWaterStatistics(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadCompanyStatistics(IdCompany, from, to, _Credential); }
        public Metrics.MetricPeriod GetWaterStatistics()
        { return new Handlers.WaterSeries().ReadCompanyStatistics(IdCompany, _Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterDaily()
        { return new Handlers.WaterSeries().ReadCompanyDaily(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterDaily(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadCompanyDaily(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterWeekly()
        { return new Handlers.WaterSeries().ReadCompanyWeekly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterWeekly(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadCompanyWeekly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterMonthly()
        { return new Handlers.WaterSeries().ReadCompanyMonthly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterMonthly(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadCompanyMonthly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterYearly()
        { return new Handlers.WaterSeries().ReadCompanyYearly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetWaterYearly(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadCompanyYearly(IdCompany, from, to, _Credential); }

        #endregion


        #region Fleet

        public Metrics.MetricPeriod GetFleetStatistics(DateTime from, DateTime to)
        { return new Handlers.FleetSeries().ReadCompanyStatistics(IdCompany, from, to, _Credential); }
        public Metrics.MetricPeriod GetFleetStatistics()
        { return new Handlers.FleetSeries().ReadCompanyStatistics(IdCompany, _Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetFleetDaily()
        { return new Handlers.FleetSeries().ReadCompanyDaily(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFleetDaily(DateTime from, DateTime to)
        { return new Handlers.FleetSeries().ReadCompanyDaily(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFleetWeekly()
        { return new Handlers.FleetSeries().ReadCompanyWeekly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFleetWeekly(DateTime from, DateTime to)
        { return new Handlers.FleetSeries().ReadCompanyWeekly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFleetMonthly()
        { return new Handlers.FleetSeries().ReadCompanyMonthly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFleetMonthly(DateTime from, DateTime to)
        { return new Handlers.FleetSeries().ReadCompanyMonthly(IdCompany, from, to, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFleetYearly()
        { return new Handlers.FleetSeries().ReadCompanyYearly(IdCompany, _Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetFleetYearly(DateTime from, DateTime to)
        { return new Handlers.FleetSeries().ReadCompanyYearly(IdCompany, from, to, _Credential); }

        #endregion

        #endregion

        #endregion

    }
}
