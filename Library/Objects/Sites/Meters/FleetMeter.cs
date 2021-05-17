using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites.Meters
{
    public class FleetMeter : Meter
    {
        internal FleetMeter(Int64 idMeter, Int64 idSite, String identification, String description, Int64 idFleetType, Int64 idUnit, Int16 frequencyQuantity, Int16 frequencyUnit, Int16 alertBeforeDays, Int16 alertAfterDays, Boolean alertOnStart, Security.Credential credential)
            : base(idMeter, idSite, identification, idUnit, credential)
        {

            _IdFleetType = idFleetType;
            _IdLanguage = credential.CurrentLanguage.IdLanguage;

            _FrequencyQuantity = frequencyQuantity;
            _FrequencyUnit = frequencyUnit;
            _AlertBeforeInDays = alertBeforeDays;
            _AlertAfterInDays = alertAfterDays;
            _AlertOnStart = alertOnStart;

            _LanguageOption = new FleetMeterLanguageOption(_IdLanguage, description);

        }

        #region Private Fields

        private Int64 _IdFleetType;
        private String _IdLanguage;

        private Int16 _FrequencyQuantity;
        private Int16 _FrequencyUnit;

        private Int16 _AlertBeforeInDays;
        private Int16 _AlertAfterInDays;
        private Boolean _AlertOnStart;

        private FleetMeterLanguageOption _LanguageOption;

        #endregion

        #region Public Properties

        public String Description
        {
            get { return _LanguageOption.Description; }
        }

        public Auxiliaries.Types.FleetType FleetType
        {
            get
            {
                return new Handlers.FleetTypes().Item(_IdFleetType, Credential);
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

        public FleetMeterLanguageOption LanguageOption
        { get { return _LanguageOption; } }
        public Dictionary<String, FleetMeterLanguageOption> LanguageOptions
        { get { return new Handlers.FleetMeterLanguageOption().Items(IdMeter); } }

        #endregion

        #region Public Methods

        public Dictionary<Int64, EmissionFactors.FleetMeterEmissionFactor> GetEmissionFactors()
        {
            return new Handlers.FleetMeterEmissionFactors().Items(IdMeter, Credential);
        }
        public EmissionFactors.FleetMeterEmissionFactor GetMeterEmissionFactor(Int64 idFleetType)
        {
            return new Handlers.FleetMeterEmissionFactors().Item(IdMeter, idFleetType, Credential);
        }
        public Auxiliaries.EmissionFactors.FleetTypeEmissionFactor GetFleetTypeEmissionFactor(Int64 idFleetType)
        {
            EmissionFactors.FleetMeterEmissionFactor _emissionFactor = GetMeterEmissionFactor(idFleetType);
            if (_emissionFactor != null)
                return _emissionFactor.FleetTypeEmissionFactor;

            Objects.Auxiliaries.EmissionFactors.FleetTypeEmissionFactor _ef = new Handlers.FleetTypeEmissionFactors().Item(idFleetType, Site.Country.IdCountry, Credential);
            if (_ef != null)
                return _ef;
            else
                return new Handlers.FleetTypeEmissionFactors().ItemGlobal(idFleetType, Credential);
        }

        #region Metrics


        public Boolean HasValue()
        { return new Handlers.FleetMeters().HasValues(IdMeter); }
        public Dictionary<Int64, Series.FleetLoad> GetLoads()
        { return new Handlers.FleetMeterLoads().ReadAll(IdMeter, Credential); }
        public Series.FleetLoad GetLoad(Int64 idLoad)
        { return new Handlers.FleetMeterLoads().ReadById(idLoad, Credential); }
        public DateTime? GetNextDate()
        { return new Handlers.FleetMeters().NextDate(IdMeter); }
        public override DateTime? GetLastDate()
        { return new Handlers.FleetMeters().LastDate(IdMeter); }

        public override Metrics.MetricPeriod GetStatistics(DateTime from, DateTime to)
        { return new Handlers.FleetSeries().ReadMeterStatistics(IdMeter, from, to, Credential); }
        public override Metrics.MetricPeriod GetStatistics()
        { return new Handlers.FleetSeries().ReadMeterStatistics(IdMeter, Credential); }

        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieLoaded()
        { return new Handlers.FleetSeries().ReadMeterLoaded(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieLoaded(DateTime from, DateTime to)
        { return new Handlers.FleetSeries().ReadMeterLoaded(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily()
        { return new Handlers.FleetSeries().ReadMeterDaily(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieDaily(DateTime from, DateTime to)
        { return new Handlers.FleetSeries().ReadMeterDaily(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly()
        { return new Handlers.FleetSeries().ReadMeterWeekly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieWeekly(DateTime from, DateTime to)
        { return new Handlers.FleetSeries().ReadMeterWeekly(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly()
        { return new Handlers.FleetSeries().ReadMeterMonthly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieMonthly(DateTime from, DateTime to)
        { return new Handlers.FleetSeries().ReadMeterMonthly(IdMeter, from, to, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly()
        { return new Handlers.FleetSeries().ReadMeterYearly(IdMeter, Credential); }
        public Dictionary<DateTime, Metrics.MetricInstant> GetSerieYearly(DateTime from, DateTime to)
        { return new Handlers.FleetSeries().ReadMeterYearly(IdMeter, from, to, Credential); }

        #endregion

        #endregion
    }
}
