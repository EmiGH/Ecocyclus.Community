using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Metrics
{
    public class Evolution
    {
        private Auxiliaries.Units.TimeRange _Dates;
        private Auxiliaries.Units.TimeUnit.Units _TimeUnit;
        private Int16 _Interval;

        private Auxiliaries.Units.Unit _ElectricityUnit;
        private Auxiliaries.Units.Unit _FuelUnit;
        private Auxiliaries.Units.Unit _TransportUnit;
        private Auxiliaries.Units.Unit _WasteUnit;
        private Auxiliaries.Units.Unit _WaterUnit;
        private Auxiliaries.Units.Unit _CO2Unit;

        private SortedDictionary<DateTime, EvolutionPoint> _Serie;

        internal Evolution(Auxiliaries.Units.TimeRange dates, Int16 interval, Auxiliaries.Units.TimeUnit.Units timeUnit, Auxiliaries.Units.Unit electricityUnit, Auxiliaries.Units.Unit fuelUnit, Auxiliaries.Units.Unit transportUnit, Auxiliaries.Units.Unit wasteUnit, Auxiliaries.Units.Unit waterUnit, Auxiliaries.Units.Unit co2Unit)
        {
            _Dates = dates;
            _TimeUnit = timeUnit;
            _Interval = interval;

            _ElectricityUnit = electricityUnit;
            _FuelUnit = fuelUnit;
            _TransportUnit = transportUnit;
            _WasteUnit = wasteUnit;
            _WaterUnit = waterUnit;
            _CO2Unit = co2Unit;

            _Serie = new SortedDictionary<DateTime, EvolutionPoint>();
            foreach (DateTime _item in _Dates.Milestones(interval, timeUnit, true))
		        _Serie.Add(_item, new EvolutionPoint());
        }

        internal void Update(DateTime date, Common.Data.Protocols protocol, Double sum, Double sumCO2)
        {
            DateTime _dateInitial = Auxiliaries.Units.TimeRange.GetNormalizedInitialDate(date, _Interval, _TimeUnit);
            switch (protocol)
            {
                case CSI.Library.Objects.Common.Data.Protocols.Electricity:
                    _Serie[_dateInitial].UpdateElectricity(sum, sumCO2);
                    break;
                case CSI.Library.Objects.Common.Data.Protocols.Fuel:
                    _Serie[_dateInitial].UpdateFuel(sum, sumCO2);
                    break;
                case CSI.Library.Objects.Common.Data.Protocols.Transport:
                    _Serie[_dateInitial].UpdateTransport(sum, sumCO2);
                    break;
                case CSI.Library.Objects.Common.Data.Protocols.Waste:
                    _Serie[_dateInitial].UpdateWaste(sum, sumCO2);
                    break;
                case CSI.Library.Objects.Common.Data.Protocols.Water:
                    _Serie[_dateInitial].UpdateWater(sum, sumCO2);
                    break;
                default:
                    break;
            }           

        }

        #region Public Methods

        public Auxiliaries.Units.Unit ElectricityUnit
        { get { return _ElectricityUnit; } }
        public Auxiliaries.Units.Unit FuelUnit
        { get { return _FuelUnit; } }
        public Auxiliaries.Units.Unit TransportUnit
        { get { return _TransportUnit; } }
        public Auxiliaries.Units.Unit WasteUnit
        { get { return _WasteUnit; } }
        public Auxiliaries.Units.Unit WaterUnit
        { get { return _WaterUnit; } }
        public Auxiliaries.Units.Unit CO2Unit
        { get { return _CO2Unit; } }

        public SortedDictionary<DateTime, EvolutionPoint> Serie
        { get { return _Serie; } }

        #endregion
    }
}
