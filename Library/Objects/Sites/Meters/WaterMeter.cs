using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters
{
    public class WaterMeter : Meter
    {
        internal WaterMeter(Int64 idMeter, Int64 idSite, String identification, String description, Int64 idEmissionFactor, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBeforeDays, Int16 alertAfterDays, Boolean alertOnStart, Security.Credential credential)
            :base(idMeter, idSite, identification, idUnit, credential)
        {
        
            _IdEmissionFactor = idEmissionFactor;
            _IdLanguage = credential.CurrentLanguage.IdLanguage;

            _FrequencyQuantity = frequencyQuantity;
            _FrequencyUnit = frequencyUnit;
            _AlertBeforeInDays = alertBeforeDays;
            _AlertAfterInDays = alertAfterDays;
            _AlertOnStart = alertOnStart;

            _LanguageOption = new WaterMeterLanguageOption(_IdLanguage, description);

        }

        #region Private Fields

        private Int64 _IdEmissionFactor;
        private String _IdLanguage;

        private Int16 _FrequencyQuantity;
        private Int16 _FrequencyUnit;
        
        private Int16 _AlertBeforeInDays;
        private Int16 _AlertAfterInDays;
        private Boolean _AlertOnStart;

        private WaterMeterLanguageOption _LanguageOption;

        #endregion

        #region Public Properties

        public String Description
        {
            get { return _LanguageOption.Description; }
        }

        public Auxiliaries.EmissionFactors.WaterEmissionFactor EmissionFactor
        {
            get
            {
                if (_IdEmissionFactor > 0)
                    return new Handlers.WaterEmissionFactors().Item(_IdEmissionFactor, Credential);
                else
                {
                    Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _ef = new Handlers.WaterEmissionFactors().ItemDefault(Site.Country.IdCountry, Credential);
                    if (_ef != null)
                        return _ef;

                    return new Handlers.WaterEmissionFactors().ItemGlobal(Credential);
                }
            }
        }
        
        public Int16 FrequencyQuantity
        { get { return _FrequencyQuantity; } }
        public Int16 FrequencyUnit
        { get { return _FrequencyUnit; } }
        public Int16 AlertBeforeInDays
        { get { return _AlertBeforeInDays; } }
        public Int16 AlertAfterInDays
        { get { return _AlertAfterInDays; } }
        public Boolean AlertOnStart
        { get { return _AlertOnStart; } }

        public WaterMeterLanguageOption LanguageOption
        { get { return _LanguageOption; } }
        public Dictionary<String, WaterMeterLanguageOption> LanguageOptions
        { get { return new Handlers.WaterMeterLanguageOption().Items(IdMeter); } }

        #endregion

        #region Public Methods

        public Dictionary<Int64, EmissionFactors.WasteMeterEmissionFactor> GetEmissionFactors()
        {
            return new Handlers.WasteMeterEmissionFactors().Items(IdMeter, Credential);
        }

        #region Metrics


        public Boolean HasValue()
        { return new Handlers.WaterMeters().HasValues(IdMeter); }
        public Dictionary<Int64, Series.WaterLoad> GetLoads()
        { return new Handlers.WaterMeterLoads().ReadAll(IdMeter, Credential); }
        public Series.WaterLoad GetLoad(Int64 idLoad)
        { return new Handlers.WaterMeterLoads().ReadById(idLoad, Credential); }
        public DateTime? GetNextDate()
        { return new Handlers.WaterMeters().NextDate(IdMeter); }
        public override DateTime? GetLastDate()
        { return new Handlers.WaterMeters().LastDate(IdMeter); }

        public override Metrics.MetricPeriod GetStatistics(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadMeterStatistics(IdMeter, from, to, Credential); }
        public override Metrics.MetricPeriod GetStatistics()
        { return new Handlers.WaterSeries().ReadMeterStatistics(IdMeter, Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieLoaded()
        { return new Handlers.WaterSeries().ReadMeterLoaded(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieLoaded(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadMeterLoaded(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily()
        { return new Handlers.WaterSeries().ReadMeterDaily(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadMeterDaily(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly()
        { return new Handlers.WaterSeries().ReadMeterWeekly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadMeterWeekly(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly()
        { return new Handlers.WaterSeries().ReadMeterMonthly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadMeterMonthly(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly()
        { return new Handlers.WaterSeries().ReadMeterYearly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly(DateTime from, DateTime to)
        { return new Handlers.WaterSeries().ReadMeterYearly(IdMeter, from, to, Credential); }

        #endregion

        #endregion
    }
}
