using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Runtime.Remoting.Contexts;
using Autofac;
using CostCalculator;
using CostCalculator.Data;
using CostCalculator.Domain;
using CostCalculator.Logic;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace XUnitTests
{
    public class CalculationAssert
    {
        public static void Assert(Tariff tariff, int consumtion, decimal expectedAnnualCost)
        {
            var calculator = AppContext.ResolveCalculator(tariff);
            var annualCost = calculator.CalculateAnnualCost(tariff, consumtion);

            Xunit.Assert.Equal(expectedAnnualCost, annualCost);
        }
    }

    public class BasicCalculatorTest
    {
        [Theory]
        [InlineData(3500, 830)]
        [InlineData(4500, 1050)]
        [InlineData(6000, 1380)]
        public void Calculate(int consumtion, decimal expectedAnnualCost)
        {
            var tariff = new BasicTariff
            {
                ConsumptionCost = 0.22M,
                BaseCostPerMonth = 5,
                Name = "Basic Tariff"
            };

            AppContext.Container = AppContext.BuildContainer(AppContext.Configure);
            CalculationAssert.Assert(tariff, consumtion, expectedAnnualCost);
        }
    }

    public class PackagedCalculatorTest
    {
        [Theory]
        [InlineData(3500, 800)]
        [InlineData(4500, 950)]
        [InlineData(6000, 1400)]
        public void Calculate(int consumtion, decimal expectedAnnualCost)
        {
            var tariff = new PackagedTariff
            {
                Name = "Packaged Tariff",
                Limit = 4000,
                ExtraConsumptionCost = 0.3M,
                PriceBelowLimit = 800
            };

            AppContext.Container = AppContext.BuildContainer(AppContext.Configure);
            CalculationAssert.Assert(tariff, consumtion, expectedAnnualCost);
        }
    }
}