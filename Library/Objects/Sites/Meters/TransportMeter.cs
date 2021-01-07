using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters
{
    public class TransportMeter : Meter
    {
        internal TransportMeter(Int64 idMeter, Int64 idSite, String identification, String description, Int64 idDefaultUnit, Security.Credential credential)
            : base(idMeter, idSite, identification, idDefaultUnit, credential)
        {
            _LanguageOption = new TransportMeterLanguageOption(credential.CurrentLanguage.IdLanguage, description);
        }

        #region Private Fields

        private TransportMeterLanguageOption _LanguageOption;

        #endregion

        #region Public Properties

        public String Description
        {
            get { return _LanguageOption.Description; }
        }
        public TransportMeterLanguageOption LanguageOption
        { get { return _LanguageOption; } }
        public Dictionary<String, TransportMeterLanguageOption> LanguageOptions
        { get { return new Handlers.TransportMeterLanguageOptions().Items(IdMeter); } }
        
        #endregion

        #region Public Methods

        #region Emission Factors

        public Dictionary<Int64, EmissionFactors.TransportMeterEmissionFactor> GetEmissionFactors()
        {
            return new Handlers.TransportMeterEmissionFactors().Items(IdMeter, Credential);
        }
        public EmissionFactors.TransportMeterEmissionFactor GetMeterEmissionFactor(Int64 idTransportType)
        {
            return new Handlers.TransportMeterEmissionFactors().Item(IdMeter, idTransportType, Credential);
        }
        public Auxiliaries.EmissionFactors.TransportTypeEmissionFactor GetTransportTypeEmissionFactor(Int64 idTransportType)
        {
            EmissionFactors.TransportMeterEmissionFactor _emissionFactor = GetMeterEmissionFactor(idTransportType);
            if (_emissionFactor != null)
                return _emissionFactor.TransportTypeEmissionFactor;

            Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor _ef = new Handlers.TransportTypeEmissionFactors().Item(idTransportType, Site.Country.IdCountry, Credential);
            if (_ef != null)
                return _ef;
            else
                return new Handlers.TransportTypeEmissionFactors().ItemGlobal(idTransportType, Credential);
        }

        #endregion

        #region Series

        public Dictionary<Int64, Series.TransportSerie> GetSeries()
        { return new Handlers.TransportMeterSeries().Items(IdMeter, Credential); }
        public Dictionary<Int64, Series.TransportSerie> GetSeries(DateTime from, DateTime to)
        { return new Handlers.TransportMeterSeries().Items(IdMeter, from, to, Credential); }
        public Series.TransportSerie GetSerie(Int64 idSerie, DateTime from, DateTime to)
        { return new Handlers.TransportMeterSeries().Item(idSerie, from, to, Credential); }
        public Series.TransportSerie GetSerie(Int64 idSerie)
        { return new Handlers.TransportMeterSeries().Item(idSerie, Credential); }

        #endregion

        #region Metrics

        public override DateTime? GetLastDate()
        { return new Handlers.TransportMeters().LastDate(IdMeter); }

        public override Metrics.MetricPeriod GetStatistics(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadMeterStatistics(IdMeter, from, to, Credential); }
        public override Metrics.MetricPeriod GetStatistics()
        { return new Handlers.TransportSeries().ReadMeterStatistics(IdMeter, Credential); }
        public Metrics.MetricPeriod GetStatistics(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadMeterStatistics(IdMeter, idTransportType, from, to, Credential); }
        public Metrics.MetricPeriod GetStatistics(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadMeterStatistics(IdMeter, idTransportType, Credential); }
        public Metrics.MetricComposite GetStatisticsByTypes(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadMeterStatisticsByTypes(IdMeter, from, to, Credential); }
        public Metrics.MetricComposite GetStatisticsByTypes()
        { return new Handlers.TransportSeries().ReadMeterStatisticsByTypes(IdMeter, Credential); }
        public Metrics.MetricComposite GetStatisticsCO2ByTypes(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadMeterStatisticsCO2ByTypes(IdMeter, from, to, Credential); }
        public Metrics.MetricComposite GetStatisticsCO2ByTypes()
        { return new Handlers.TransportSeries().ReadMeterStatisticsCO2ByTypes(IdMeter, Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily()
        { return new Handlers.TransportSeries().ReadMeterDaily(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadMeterDaily(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadMeterDaily(IdMeter, idTransportType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadMeterDaily(IdMeter, idTransportType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly()
        { return new Handlers.TransportSeries().ReadMeterWeekly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadMeterWeekly(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadMeterWeekly(IdMeter, idTransportType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadMeterWeekly(IdMeter, idTransportType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly()
        { return new Handlers.TransportSeries().ReadMeterMonthly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadMeterMonthly(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadMeterMonthly(IdMeter, idTransportType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadMeterMonthly(IdMeter, idTransportType, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly()
        { return new Handlers.TransportSeries().ReadMeterYearly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly(DateTime from, DateTime to)
        { return new Handlers.TransportSeries().ReadMeterYearly(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly(Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadMeterYearly(IdMeter, idTransportType, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly(DateTime from, DateTime to, Int64 idTransportType)
        { return new Handlers.TransportSeries().ReadMeterYearly(IdMeter, idTransportType, from, to, Credential); }

        #endregion

        #endregion
    }
}
