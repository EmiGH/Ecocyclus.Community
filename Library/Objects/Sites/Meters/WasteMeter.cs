using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters
{
    public class WasteMeter : Meter
    {
        internal WasteMeter(Int64 idMeter, Int64 idSite, String identification, String description, Int64 idDefaultUnit, Security.Credential credential)
            : base(idMeter, idSite, identification, idDefaultUnit, credential)
        {
            _LanguageOption = new WasteMeterLanguageOption(credential.CurrentLanguage.IdLanguage, description);
        }

        #region Private Fields

        private WasteMeterLanguageOption _LanguageOption;

        #endregion

        #region Public Properties

        public String Description
        {
            get { return _LanguageOption.Description; }
        }
        public WasteMeterLanguageOption LanguageOption
        { get { return _LanguageOption; } }
        public Dictionary<String, WasteMeterLanguageOption> LanguageOptions
        { get { return new Handlers.WasteMeterLanguageOptions().Items(IdMeter); } }
        
        #endregion

        #region Public Methods

        #region Emission Factors

        public Dictionary<Int64, EmissionFactors.WasteMeterEmissionFactor> GetEmissionFactors()
        {
            return new Handlers.WasteMeterEmissionFactors().Items(IdMeter, Credential);
        }
        public EmissionFactors.WasteMeterEmissionFactor GetMeterEmissionFactor(Int64 idWasteType)
        {
            return new Handlers.WasteMeterEmissionFactors().Item(IdMeter, idWasteType, Credential);
        }
        public Auxiliaries.EmissionFactors.WasteTypeEmissionFactor GetWasteTypeEmissionFactor(Int64 idWasteType)
        {
            EmissionFactors.WasteMeterEmissionFactor _emissionFactor = GetMeterEmissionFactor(idWasteType);
            if (_emissionFactor != null)
                return _emissionFactor.WasteTypeEmissionFactor;

            Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _ef = new Handlers.WasteTypeEmissionFactors().Item(idWasteType, Site.Country.IdCountry, Credential);
            if (_ef != null)
                return _ef;
            else
                return new Handlers.WasteTypeEmissionFactors().ItemGlobal(idWasteType, Credential);
        }

        #endregion

        #region Series

        public Dictionary<Int64, Series.WasteSerie> GetSeries()
        { return new Handlers.WasteMeterSeries().Items(IdMeter, Credential); }
        public Dictionary<Int64, Series.WasteSerie> GetSeries(DateTime from, DateTime to)
        { return new Handlers.WasteMeterSeries().Items(IdMeter, from, to, Credential); }
        public Series.WasteSerie GetSerie(Int64 idSerie, DateTime from, DateTime to)
        { return new Handlers.WasteMeterSeries().Item(idSerie, from, to, Credential); }
        public Series.WasteSerie GetSerie(Int64 idSerie)
        { return new Handlers.WasteMeterSeries().Item(idSerie, Credential); }

        #endregion

        #region Metrics

        public override DateTime? GetLastDate()
        { return new Handlers.WasteMeters().LastDate(IdMeter); }

        public override Metrics.MetricPeriod GetStatistics(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadMeterStatistics(IdMeter, from, to, Credential); }
        public override Metrics.MetricPeriod GetStatistics()
        { return new Handlers.WasteSeries().ReadMeterStatistics(IdMeter, Credential); }
        public Metrics.MetricPeriod GetStatistics(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadMeterStatistics(IdMeter, idWasteType, from, to, Credential); }
        public Metrics.MetricPeriod GetStatistics(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadMeterStatistics(IdMeter, idWasteType, Credential); }
        public Metrics.MetricComposite GetStatisticsByTypes(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadMeterStatisticsByTypes(IdMeter, from, to, Credential); }
        public Metrics.MetricComposite GetStatisticsByTypes()
        { return new Handlers.WasteSeries().ReadMeterStatisticsByTypes(IdMeter, Credential); }
        public Metrics.MetricComposite GetStatisticsCO2ByTypes(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadMeterStatisticsCO2ByTypes(IdMeter, from, to, Credential); }
        public Metrics.MetricComposite GetStatisticsCO2ByTypes()
        { return new Handlers.WasteSeries().ReadMeterStatisticsCO2ByTypes(IdMeter, Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily()
        { return new Handlers.WasteSeries().ReadMeterDaily(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadMeterDaily(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadMeterDaily(IdMeter, idWasteType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadMeterDaily(IdMeter, idWasteType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly()
        { return new Handlers.WasteSeries().ReadMeterWeekly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadMeterWeekly(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadMeterWeekly(IdMeter, idWasteType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadMeterWeekly(IdMeter, idWasteType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly()
        { return new Handlers.WasteSeries().ReadMeterMonthly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadMeterMonthly(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadMeterMonthly(IdMeter, idWasteType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadMeterMonthly(IdMeter, idWasteType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly()
        { return new Handlers.WasteSeries().ReadMeterYearly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly(DateTime from, DateTime to)
        { return new Handlers.WasteSeries().ReadMeterYearly(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly(Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadMeterYearly(IdMeter, idWasteType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly(DateTime from, DateTime to, Int64 idWasteType)
        { return new Handlers.WasteSeries().ReadMeterYearly(IdMeter, idWasteType, from, to, Credential); }

        #endregion

        #endregion
    }
}
