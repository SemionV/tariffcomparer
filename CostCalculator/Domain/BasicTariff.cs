namespace CostCalculator.Domain
{
    public class BasicTariff: Tariff
    {
        public decimal BaseCostPerMonth { get; set; }
        public decimal ConsumptionCost { get; set; }
    }
}