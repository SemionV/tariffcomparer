using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using CostCalculator.Data;
using CostCalculator.Domain;
using NLog;

namespace CostCalculator.Logic
{
    public class TariffComparer
    {
        private static readonly Logger Logger = LogManager.GetLogger("MainLogger");

        public List<ComparisonItem> CompareTariffs(int consumption)
        {
            if (consumption < 0)
            {
                throw new Exception("Cansumption can not be less than 0");
            }

            var results = new List<ComparisonItem>();
            var tariffs = AppContext.Resolve<TariffRepository>().List();

            foreach (var tariff in tariffs)
            {
                try
                {
                    var calculator = AppContext.ResolveCalculator(tariff);
                    results.Add(new ComparisonItem
                    {
                        Tariff = tariff,
                        AnnualCost = calculator.CalculateAnnualCost(tariff, consumption)
                    });
                }
                catch (Exception e)
                {
                    Logger.Error($"Error occured: {e.Message}");
                }
            }

            return results.OrderBy(x => x.AnnualCost).ToList();
        }
    }
}