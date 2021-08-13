using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePostgreSQLDockerApp.Controllers
{
    public class CustomersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}