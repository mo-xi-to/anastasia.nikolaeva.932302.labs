using lab12.Models;
using lab12.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lab12.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CalculationService _calculationService;

        public HomeController(ILogger<HomeController> logger, CalculationService calculationService)
        {
            _logger = logger;
            _calculationService = calculationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Manual()
        {
            if (Request.Method == "GET")
            {
                return View("inputForm");
            }
            else
            {
                try
                {
                    int num1 = int.Parse(Request.Form["firstNumber"]);
                    int num2 = int.Parse(Request.Form["secondNumber"]);
                    string op = Request.Form["operation"];

                    var model = new CalculationExpressionModel(num1, num2, op);

                    ViewData["result"] = _calculationService.calc(model);
                    return View("result", model);
                }
                catch
                {
                    return View("result", new CalculationExpressionModel(0, 0, "Error"));
                }
            }
        }

        [HttpGet]
        [ActionName("ManualSeparate")]
        public IActionResult ManualSeparateGet()
        {
            return View("inputForm");
        }

        [HttpPost]
        [ActionName("ManualSeparate")]
        public IActionResult ManualSeparatePost()
        {
            try
            {
                int num1 = int.Parse(Request.Form["firstNumber"]);
                int num2 = int.Parse(Request.Form["secondNumber"]);
                string op = Request.Form["operation"];

                var model = new CalculationExpressionModel(num1, num2, op);

                ViewData["result"] = _calculationService.calc(model);
                return View("result", model);
            }
            catch
            {
                return View("result", new CalculationExpressionModel(0, 0, "Error"));
            }
        }

        [HttpGet]
        public IActionResult ModelBinding()
        {
            return View("inputForm");
        }

        [HttpPost]
        public IActionResult ModelBinding(CalculationExpressionModel model)
        {
            ViewData["result"] = _calculationService.calc(model);
            return View("result", model);
        }

        [HttpGet]
        public IActionResult ModelBindingSeperate()
        {
            return View("inputForm");
        }

        [HttpPost]
        public IActionResult ModelBindingSeperate(int firstNumber, int secondNumber, string operation)
        {
            var model = new CalculationExpressionModel(firstNumber, secondNumber, operation);

            ViewData["result"] = _calculationService.calc(model);

            return View("result", model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}