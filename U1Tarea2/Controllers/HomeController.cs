using Microsoft.AspNetCore.Mvc;
using U1Tarea2.Models.ViewModels;


namespace U1Tarea2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(ConversionViewModel conversionvm)
        {
            if(conversionvm.Moneda == "USD")
                conversionvm.Resultado = conversionvm.Cantidad / 18;
            else
                conversionvm.Resultado = conversionvm.Cantidad * 18;

            return View(conversionvm);
        }
    }
}
