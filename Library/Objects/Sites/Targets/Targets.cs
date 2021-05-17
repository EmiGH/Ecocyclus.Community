using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Sites
{
    public class Targets
    {
        private Double _ElectricityConsumption;
        private Int64 _IdElectricityUnit;
        private Double _ElectricityCO2;
        private Double _FuelConsumption;
        private Int64 _IdFuelUnit;
        private Double _FuelCO2;
        private Double _TransportConsumption;
        private Int64 _IdTransportUnit;
        private Double _TransportCO2;
        private Double _WasteConsumption; 
        private Int64 _IdWasteUnit;
        private Double _WasteCO2;
        private Double _WaterConsumption;
        private Int64 _IdWaterUnit;
        private Double _WaterCO2;
        private Double _FleetConsumption;
        private Int64 _IdFleetUnit;
        private Double _FleetCO2;

        private Double _TotalCO2;

        Security.Credential _Credential;

        internal Targets(Double electricityConsumption, Int64 idElectricityUnit, Double electricityCO2, Double fuelConsumption, Int64 idFuelUnit, Double fuelCO2, Double transportConsumption, Int64 idTransportUnit, Double transportCO2, Double wasteConsumption, Int64 idWasteUnit, Double wasteCO2, Double waterConsumption, Int64 idWaterUnit, Double waterCO2, Double fleetConsumption, Int64 idFleetUnit, Double fleetCO2, Double totalCO2, Security.Credential credential)
        {
            _ElectricityConsumption = electricityConsumption;
            _IdElectricityUnit = idElectricityUnit;
            _ElectricityCO2 = electricityCO2;
            _FuelConsumption = fuelCO2;
            _FuelCO2 = fuelCO2;
            _IdFuelUnit = idFuelUnit;
            _FuelConsumption = fuelConsumption;
            _TransportConsumption = transportConsumption;
            _IdTransportUnit = idTransportUnit;
            _TransportCO2 = transportCO2;
            _WasteConsumption = wasteConsumption;
            _IdWasteUnit = idWasteUnit;
            _WasteCO2 = wasteCO2;
            _WaterConsumption = waterConsumption;
            _IdWaterUnit = idWaterUnit;
            _WaterCO2 = waterCO2;
            _FleetConsumption = fleetConsumption;
            _FleetCO2 = fleetCO2;
            _IdFleetUnit = idFleetUnit;
            _TotalCO2 = totalCO2;

            _Credential = credential;
        }

        public Double ElectricityConsumption { get { return _ElectricityConsumption; } }
        public Auxiliaries.Units.Unit ElectricityUnit
        { get { return new Handlers.Units().Item(_IdElectricityUnit, _Credential); } }
        public Double ElectricityCO2 { get { return _ElectricityCO2; } }
        public Double FuelConsumption { get { return _FuelConsumption; } }
        public Auxiliaries.Units.Unit FuelUnit
        { get { return new Handlers.Units().Item(_IdFuelUnit, _Credential); } }
        public Double FuelCO2 { get { return _FuelCO2; } }
        public Double TransportConsumption { get { return _TransportConsumption; } }
        public Auxiliaries.Units.Unit TransportUnit
        { get { return new Handlers.Units().Item(_IdTransportUnit, _Credential); } }
        public Double TransportCO2 { get { return _TransportCO2; } }
        public Double WasteConsumption { get { return _WasteConsumption; } }
        public Auxiliaries.Units.Unit WasteUnit
        { get { return new Handlers.Units().Item(_IdWasteUnit, _Credential); } }
        public Double WasteCO2 { get { return _WasteCO2; } }
        public Double WaterConsumption { get { return _WaterConsumption; } }
        public Auxiliaries.Units.Unit WaterUnit
        { get { return new Handlers.Units().Item(_IdWaterUnit, _Credential); } }
        public Double WaterCO2 { get { return _WaterCO2; } }
        public Double FleetConsumption { get { return _FleetConsumption; } }
        public Auxiliaries.Units.Unit FleetUnit
        { get { return new Handlers.Units().Item(_IdFleetUnit, _Credential); } }
        public Double FleetCO2 { get { return _FleetCO2; } }
        public Double TotalCO2 { get { return _TotalCO2; } } 
    }
}

