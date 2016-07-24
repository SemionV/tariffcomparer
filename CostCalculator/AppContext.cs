using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CostCalculator.Data;
using CostCalculator.Domain;
using CostCalculator.Logic;

namespace CostCalculator
{
    public class AppContext
    {
        public static IContainer Container;

        public static string TariffsFile;

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        public static TariffCalculator ResolveCalculator(Tariff tariff)
        {
            return Container.ResolveKeyed<TariffCalculator>(tariff.GetType());
        }

        public static IContainer BuildContainer(Action<ContainerBuilder> registerComponents)
        {
            var builder = new ContainerBuilder();
            registerComponents(builder);
            return builder.Build();
        }

        /// <summary>
        /// Configures Autofac root scope.
        /// </summary>
        /// <param name="builder">The Autofac container builder.</param>
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<TariffRepository>().As<TariffRepository>().SingleInstance();
            builder.RegisterType<TariffComparer>().As<TariffComparer>().SingleInstance();

            builder.RegisterType<BasicTariffCalculator>().Keyed<TariffCalculator>(typeof(BasicTariff)).SingleInstance();
            builder.RegisterType<PackagedTariffCalculator>().Keyed<TariffCalculator>(typeof(PackagedTariff)).SingleInstance();
        }
    }
}
