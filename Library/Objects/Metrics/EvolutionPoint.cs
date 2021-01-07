using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Metrics
{
    public class EvolutionPoint
    {
        private EvolutionPointProtocol _Electricity;
        private EvolutionPointProtocol _Fuel;
        private EvolutionPointProtocol _Transport;
        private EvolutionPointProtocol _Waste;
        private EvolutionPointProtocol _Water;

        private Double _TotalCO2;

        internal EvolutionPoint()
        {
            _Electricity = new EvolutionPointProtocol(Resources.Data.Electricity);
            _Fuel = new EvolutionPointProtocol(Resources.Data.Fuel);
            _Transport = new EvolutionPointProtocol(Resources.Data.Transport);
            _Waste = new EvolutionPointProtocol(Resources.Data.Waste);
            _Water = new EvolutionPointProtocol(Resources.Data.Water);

            _TotalCO2 = 0;
        }
        internal EvolutionPoint(Double electricitySum, Double electricitySumCO2, Double fuelSum, Double fuelSumCO2, Double transportSum, Double transportSumCO2, Double wasteSum, Double wasteSumCO2, Double waterSum, Double waterSumCO2)
        {
            _Electricity = new EvolutionPointProtocol(Resources.Data.Electricity);
            _Fuel = new EvolutionPointProtocol(Resources.Data.Fuel);
            _Transport = new EvolutionPointProtocol(Resources.Data.Transport);
            _Waste = new EvolutionPointProtocol(Resources.Data.Waste);
            _Water = new EvolutionPointProtocol(Resources.Data.Water);

            Recalculate(electricitySum, electricitySumCO2, fuelSum, fuelSumCO2, transportSum, transportSumCO2, wasteSum, wasteSumCO2, waterSum, waterSumCO2);
        }

        internal void Update(Double electricitySum, Double electricitySumCO2, Double fuelSum, Double fuelSumCO2, Double transportSum, Double transportSumCO2, Double wasteSum, Double wasteSumCO2, Double waterSum, Double waterSumCO2)
        {
            Recalculate(electricitySum, electricitySumCO2, fuelSum, fuelSumCO2, transportSum, transportSumCO2, wasteSum, wasteSumCO2, waterSum, waterSumCO2);
        }
        internal void UpdateElectricity(Double sum, Double sumCO2)
        {
            Recalculate(sum, sumCO2, Fuel.Sum, _Fuel.SumCO2, _Transport.Sum, _Transport.SumCO2, _Waste.Sum, _Waste.SumCO2, Water.Sum, Water.SumCO2);
        }
        internal void UpdateFuel(Double sum, Double sumCO2)
        {
            Recalculate(_Electricity.Sum, _Electricity.SumCO2, sum, sumCO2, _Transport.Sum, _Transport.SumCO2, _Waste.Sum, _Waste.SumCO2, Water.Sum, Water.SumCO2);
        }
        internal void UpdateTransport(Double sum, Double sumCO2)
        {
            Recalculate(_Electricity.Sum, _Electricity.SumCO2, Fuel.Sum, _Fuel.SumCO2, sum, sumCO2, _Waste.Sum, _Waste.SumCO2, Water.Sum, Water.SumCO2);
        }
        internal void UpdateWaste(Double sum, Double sumCO2)
        {
            Recalculate(_Electricity.Sum, _Electricity.SumCO2, Fuel.Sum, _Fuel.SumCO2, _Transport.Sum, _Transport.SumCO2, sum, sumCO2, Water.Sum, Water.SumCO2);
        }
        internal void UpdateWater(Double sum, Double sumCO2)
        {
            Recalculate(_Electricity.Sum, _Electricity.SumCO2, Fuel.Sum, _Fuel.SumCO2, _Transport.Sum, _Transport.SumCO2, _Waste.Sum, _Waste.SumCO2, sum, sumCO2);
        }

        private void Recalculate(Double electricitySum, Double electricitySumCO2, Double fuelSum, Double fuelSumCO2, Double transportSum, Double transportSumCO2, Double wasteSum, Double wasteSumCO2, Double waterSum, Double waterSumCO2)
        {
            Double _electricityShareCO2, _fuelShareCO2, _transportShareCO2, _wasteShareCO2, _waterShareCO2;

            _TotalCO2 = electricitySumCO2 + fuelSumCO2 + transportSumCO2 + wasteSumCO2 + waterSumCO2;

            _electricityShareCO2 = electricitySumCO2 / _TotalCO2 * 100;
            _fuelShareCO2 = fuelSumCO2 / _TotalCO2 * 100;
            _transportShareCO2 = transportSumCO2 / _TotalCO2 * 100;
            _wasteShareCO2 = wasteSumCO2 / _TotalCO2 * 100;
            _waterShareCO2 = waterSumCO2 / _TotalCO2 * 100;

            _Electricity.Update(electricitySum, electricitySumCO2, _electricityShareCO2);
            _Fuel.Update(fuelSum, fuelSumCO2, _fuelShareCO2);
            _Transport.Update(transportSum, transportSumCO2, _transportShareCO2);
            _Waste.Update(wasteSum, wasteSumCO2, _wasteShareCO2);
            _Water.Update(waterSum, waterSumCO2, _waterShareCO2);
        }

        public EvolutionPointProtocol Electricity
        { get { return _Electricity; } }
        public EvolutionPointProtocol Fuel
        { get { return _Fuel; } }
        public EvolutionPointProtocol Transport
        { get { return _Transport; } }
        public EvolutionPointProtocol Waste
        { get { return _Waste; } }
        public EvolutionPointProtocol Water
        { get { return _Water; } }

        public Double TotalCO2
        { get { return _TotalCO2; } }
    }
}
