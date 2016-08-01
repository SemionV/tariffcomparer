using System;
using System.IO;
using System.Linq;
using CostCalculator;
using NLog;

namespace TariffComparer
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetLogger("MainLogger");

        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine($"Please pass path to Tariffs definition file argument to the program.");
                return;
            }

            string path = args[0];
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), path);
            }

            if (!File.Exists(path))
            {
                Console.WriteLine($"Tariffs declaration file does not exists at \"{path}\"");
                return;
            }

            AppContext.TariffsFile = path;
            AppContext.Container = AppContext.BuildContainer(AppContext.Configure);

            Logger.Info("Start");

            while (true)
            {
                Console.WriteLine("Please enter annual kWh consumption:");
                var input = Console.ReadLine();

                Logger.Info($"Consumption entered: {input}");

                if (input?.ToLower() == "exit")
                {
                    break;
                }

                int consumption = 0;
                if (!int.TryParse(input, out consumption))
                {
                    Console.WriteLine($"Entered value({input}) is not valid integer number. Please enter integer number grater or equal to 0.");
                }
                else
                {
                    try
                    {
                        var comparisonResult = AppContext.Resolve<CostCalculator.Logic.TariffComparer>().CompareTariffs(consumption);
                        foreach (var item in comparisonResult)
                        {
                            Console.WriteLine($"Tariff \"{item.Tariff.Name}\" annual cost: {item.AnnualCost}");
                        }
                    }
                    catch (Exception e)
                    {
                        var message = $"Error occured: {e.Message}";
                        Logger.Error(message);
                        Console.WriteLine(message);
                    }
                }
            }
        }
    }
}
