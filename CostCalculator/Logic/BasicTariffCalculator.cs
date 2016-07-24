using System;
using CostCalculator.Domain;

namespace CostCalculator.Logic
{
    public class BasicTariffCalculator: TariffCalculator
    {
        public override decimal CalculateAnnualCost(Tariff tariff, int consumption)
        {
            var basicTariff = tariff as BasicTariff;
            if (basicTariff == null)
            {
                throw new Exception("Invalid tariff");
            }

            return ((decimal) 12*basicTariff.BaseCostPerMonth) + (basicTariff.ConsumptionCost*consumption);
        }
    }
}