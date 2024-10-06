using CsvHelper.Configuration.Attributes;
using Microsoft.ML.Data;

namespace PredictPriceWeb.Models
{
    public class PredictStockPrice
    {


        [Index(0)]
        public string stockId { get; set; }
        [Index(1)]
        public string timeStamp { get; set; }

        [Index(3)]
        [VectorType(2)]
        public decimal stockPrice
        {
            get; set;
        }
    }

    public class Predict
    {
        public IEnumerable<IFormFile>? files { get; set; }
        public IEnumerable<PredictStockPrice>? stocks { get; set; }
    }

    public class ForecastResult
    {
        public PredictStockPrice[] foreCast { get; set; }
    }
}
