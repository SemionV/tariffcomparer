using System.Collections.Generic;
using System.IO;
using CostCalculator.Domain;
using Newtonsoft.Json;

namespace CostCalculator.Data
{
    public class TariffRepository
    {
        public virtual List<Tariff> List()
        {
            var json = File.ReadAllText(AppContext.TariffsFile);

            var tariffs = JsonConvert.DeserializeObject<List<Tariff>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            return tariffs;
        }
    }
}