namespace CostCalculator.Domain
{
    public class PackagedTariff: Tariff
    {
        public int Limit { get; set; }
        public decimal PriceBelowLimit{ get; set; }
        public decimal ExtraConsumptionCost { get; set; }
    }
}