using Microsoft.AspNetCore.Mvc;

namespace PredictPriceWeb.Controllers
{
    public class PredictScoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
