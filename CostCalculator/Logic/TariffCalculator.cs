using CostCalculator.Domain;

namespace CostCalculator.Logic
{
    public abstract class TariffCalculator
    {
        public abstract decimal CalculateAnnualCost(Tariff tariff, int consumption);
    }
}