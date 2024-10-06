using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using PredictPriceWeb.Models;
using System.Globalization;
using System.Text.Json;

namespace WebApi.Controllers
{
    [ApiController]
    
    [Route("api/[controller]/[action]")]
    public class PredictScoreController : ControllerBase
    {
        [HttpPost(Name = "GetDataPoints")]
        public IEnumerable<PredictStockPrice> GetDataPoints(IFormFile files)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ",", HasHeaderRecord = false };

            using (var reader = new StreamReader(files.OpenReadStream()))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<PredictStockMap>();
                    var records = csv.GetRecords<PredictStockPrice>().ToList();

                    var index = new Random().Next(0, records.Count - 10);
                    var selectedDataPoints = records.Skip(index).Take(10).ToList();
                    selectedDataPoints.ToList().ForEach(i => i.timeStamp = records.Last().timeStamp);

                    return selectedDataPoints;
                }

            }

        }

        [HttpPost(Name = "GetPredictionScore")]
        public IEnumerable<PredictStockPrice> GetPredictionScore([FromQuery] string json)
        {
            List<PredictStockPrice> predictions = new List<PredictStockPrice>();
            IEnumerable<PredictStockPrice> predictionsInput = JsonSerializer.Deserialize<IEnumerable<PredictStockPrice>>(json);
            if (predictions is null)
            {
                throw new ArgumentNullException(nameof(predictions));
            }
            else
            {
                var maxStock = predictionsInput.Select(s => s.stockPrice).Max();

                DateTime lastDate = DateTime.ParseExact(predictionsInput.Last().timeStamp, "dd-MM-yyyy",
                                      System.Globalization.CultureInfo.InvariantCulture);

                for (int i = 1; i < 4; i++)
                {
                    predictions.Add(new PredictStockPrice
                    {
                        stockId = predictionsInput.Last().stockId,
                        timeStamp = lastDate.AddDays(i).ToString(),
                        stockPrice = maxStock / i,
                    });
                }
            } 
            return predictions;
        }

        public sealed class PredictStockMap : ClassMap<PredictStockPrice>
        {
            public PredictStockMap()
            {
                Map(f => f.stockId).Index(0);
                Map(f => f.timeStamp).Index(1);
                Map(f => f.stockPrice).Index(2);
            }
        }
    }
}
