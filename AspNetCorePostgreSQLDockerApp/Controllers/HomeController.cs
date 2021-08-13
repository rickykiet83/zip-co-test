using System.Threading.Tasks;
using AspNetCorePostgreSQLDockerApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePostgreSQLDockerApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDockerCommandsRepository _repo;

        public HomeController(IDockerCommandsRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Help()
        {
            //Call into PostgreSQL
            var commands = await _repo.GetDockerCommandsAsync();
            return View(commands);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Index()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        // public IActionResult Error()
        // {
        //     return View();
        // }
    }
}