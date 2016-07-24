using System;
using CostCalculator.Domain;

namespace CostCalculator.Logic
{
    public class PackagedTariffCalculator: TariffCalculator
    {
        public override decimal CalculateAnnualCost(Tariff tariff, int consumption)
        {
            var packagedTariff = tariff as PackagedTariff;
            if (packagedTariff == null)
            {
                throw new Exception("Invalid tariff");
            }

            return consumption <= packagedTariff.Limit
                ? packagedTariff.PriceBelowLimit
                : packagedTariff.PriceBelowLimit +
                  packagedTariff.ExtraConsumptionCost*(consumption - packagedTariff.Limit);
        }
    }
}