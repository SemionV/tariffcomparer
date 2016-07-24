using System.Collections;
using System.Collections.Generic;
using Autofac;
using CostCalculator;
using CostCalculator.Data;
using CostCalculator.Domain;
using CostCalculator.Logic;
using Moq;
using Xunit;

namespace XUnitTests
{
    public class ComparerTests
    {
        public class ComparisonItemComparer : IEqualityComparer<ComparisonItem>
        {
            public bool Equals(ComparisonItem x, ComparisonItem y)
            {
                return x.Tariff.Name == y.Tariff.Name && x.AnnualCost == y.AnnualCost;
            }

            public int GetHashCode(ComparisonItem obj)
            {
                return obj.GetHashCode();
            }
        }

        [Fact]
        public void CompareTariffs()
        {
            var tariffRepoMock = new Mock<TariffRepository>();
            var b1 = new BasicTariff {Name = "b1"};
            var b2 = new BasicTariff {Name = "b2"};
            var p1 = new PackagedTariff {Name = "p1"};
            var p2 = new PackagedTariff {Name = "p2"};
            tariffRepoMock.Setup(m => m.List()).Returns(new List<Tariff>{b1, b2, p1, p2});

            var basicCalculatorMock = new Mock<BasicTariffCalculator>();
            basicCalculatorMock.Setup(m => m.CalculateAnnualCost(It.Is<Tariff>(t => t.Name == b1.Name), It.IsAny<int>())).Returns(1);
            basicCalculatorMock.Setup(m => m.CalculateAnnualCost(It.Is<Tariff>(t => t.Name == b2.Name), It.IsAny<int>())).Returns(3);

            var packagedCalculatorMock = new Mock<PackagedTariffCalculator>();
            packagedCalculatorMock.Setup(m => m.CalculateAnnualCost(It.Is<Tariff>(t => t.Name == p1.Name), It.IsAny<int>())).Returns(2);
            packagedCalculatorMock.Setup(m => m.CalculateAnnualCost(It.Is<Tariff>(t => t.Name == p2.Name), It.IsAny<int>())).Returns(4);

            AppContext.Container = AppContext.BuildContainer(builder =>
            {
                AppContext.Configure(builder);

                builder.RegisterInstance(tariffRepoMock.Object).As<TariffRepository>().SingleInstance();
                builder.RegisterInstance(basicCalculatorMock.Object).Keyed<TariffCalculator>(typeof(BasicTariff)).SingleInstance();
                builder.RegisterInstance(packagedCalculatorMock.Object).Keyed<TariffCalculator>(typeof(PackagedTariff)).SingleInstance();
            });

            var comparisonResult = AppContext.Resolve<TariffComparer>().CompareTariffs(10);

            Assert.Equal(new List<ComparisonItem>
            {
                new ComparisonItem {Tariff = b1, AnnualCost = 1},
                new ComparisonItem {Tariff = p1, AnnualCost = 2},
                new ComparisonItem {Tariff = b2, AnnualCost = 3},
                new ComparisonItem {Tariff = p2, AnnualCost = 4},
            }, comparisonResult, new ComparisonItemComparer());
        } 
    }
}