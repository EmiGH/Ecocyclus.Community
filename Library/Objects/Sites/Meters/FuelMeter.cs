using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters
{
    public class FuelMeter : Meter
    {
        internal FuelMeter(Int64 idMeter, Int64 idSite, String identification, String description, Int64 idDefaultUnit, Security.Credential credential)
            : base(idMeter, idSite, identification, idDefaultUnit, credential)
        {
            _LanguageOption = new FuelMeterLanguageOption(credential.CurrentLanguage.IdLanguage, description);
        }

        #region Private Fields

        private FuelMeterLanguageOption _LanguageOption;

        #endregion

        #region Public Properties

        public String Description
        {
            get { return _LanguageOption.Description; }
        }
        public FuelMeterLanguageOption LanguageOption
        { get { return _LanguageOption; } }
        public Dictionary<String, FuelMeterLanguageOption> LanguageOptions
        { get { return new Handlers.FuelMeterLanguageOptions().Items(IdMeter); } }

        #endregion

        #region Public Methods

        #region Emission Factors 

        public Dictionary<Int64, EmissionFactors.FuelMeterEmissionFactor> GetEmissionFactors()
        {
            return new Handlers.FuelMeterEmissionFactors().Items(IdMeter, Credential);
        }
        public EmissionFactors.FuelMeterEmissionFactor GetMeterEmissionFactor(Int64 idFuelType)
        {
            return new Handlers.FuelMeterEmissionFactors().Item(IdMeter, idFuelType, Credential);
        }
        public Auxiliaries.EmissionFactors.FuelTypeEmissionFactor GetFuelTypeEmissionFactor(Int64 idFuelType)
        { 
            EmissionFactors.FuelMeterEmissionFactor _emissionFactor = GetMeterEmissionFactor(idFuelType);
            if (_emissionFactor != null)
                return _emissionFactor.FuelTypeEmissionFactor;

            Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor _ef = new Handlers.FuelTypeEmissionFactors().Item(idFuelType, Site.Country.IdCountry, Credential);
            if (_ef != null)
                return _ef;
            else
                return new Handlers.FuelTypeEmissionFactors().ItemGlobal(idFuelType, Credential);
        }

        #endregion

        #region Series

        public Dictionary<Int64, Series.FuelSerie> GetSeries()
        { return new Handlers.FuelMeterSeries().Items(IdMeter, Credential); }
        public Dictionary<Int64, Series.FuelSerie> GetSeries(DateTime from, DateTime to)
        { return new Handlers.FuelMeterSeries().Items(IdMeter, from, to, Credential); }
        public Series.FuelSerie GetSerie(Int64 idSerie, DateTime from, DateTime to)
        { return new Handlers.FuelMeterSeries().Item(idSerie, from, to, Credential); }
        public Series.FuelSerie GetSerie(Int64 idSerie)
        { return new Handlers.FuelMeterSeries().Item(idSerie, Credential); }

        #endregion

        #region Metrics

        public override DateTime? GetLastDate()
        { return new Handlers.FuelMeters().LastDate(IdMeter); }

        public override Metrics.MetricPeriod GetStatistics(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadMeterStatistics(IdMeter, from, to, Credential); }
        public override Metrics.MetricPeriod GetStatistics()
        { return new Handlers.FuelSeries().ReadMeterStatistics(IdMeter, Credential); }
        public Metrics.MetricPeriod GetStatistics(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadMeterStatistics(IdMeter, idFuelType, from, to, Credential); }
        public Metrics.MetricPeriod GetStatistics(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadMeterStatistics(IdMeter, idFuelType, Credential); }
        public Metrics.MetricComposite GetStatisticsByTypes(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadMeterStatisticsByTypes(IdMeter, from, to, Credential); }
        public Metrics.MetricComposite GetStatisticsByTypes()
        { return new Handlers.FuelSeries().ReadMeterStatisticsByTypes(IdMeter, Credential); }
        public Metrics.MetricComposite GetStatisticsCO2ByTypes(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadMeterStatisticsCO2ByTypes(IdMeter, from, to, Credential); }
        public Metrics.MetricComposite GetStatisticsCO2ByTypes()
        { return new Handlers.FuelSeries().ReadMeterStatisticsCO2ByTypes(IdMeter, Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily()
        { return new Handlers.FuelSeries().ReadMeterDaily(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadMeterDaily(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadMeterDaily(IdMeter, idFuelType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadMeterDaily(IdMeter, idFuelType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly()
        { return new Handlers.FuelSeries().ReadMeterWeekly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadMeterWeekly(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadMeterWeekly(IdMeter, idFuelType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadMeterWeekly(IdMeter, idFuelType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly()
        { return new Handlers.FuelSeries().ReadMeterMonthly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadMeterMonthly(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadMeterMonthly(IdMeter, idFuelType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadMeterMonthly(IdMeter, idFuelType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly()
        { return new Handlers.FuelSeries().ReadMeterYearly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly(DateTime from, DateTime to)
        { return new Handlers.FuelSeries().ReadMeterYearly(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly(Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadMeterYearly(IdMeter, idFuelType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly(DateTime from, DateTime to, Int64 idFuelType)
        { return new Handlers.FuelSeries().ReadMeterYearly(IdMeter, idFuelType, from, to, Credential); }

        #endregion

        #endregion
    }
}
