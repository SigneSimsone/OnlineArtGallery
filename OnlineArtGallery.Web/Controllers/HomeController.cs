using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineArtGallery.Web.Data.Managers;
using OnlineArtGallery.Web.Models;
using System.Diagnostics;

namespace OnlineArtGallery.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ArtistDataManager _artistDataManager;

        public HomeController(ILogger<HomeController> logger, ArtistDataManager artistDataManager)
        {
            _logger = logger;
            _artistDataManager = artistDataManager;
        }


        public IActionResult Index(HomeModel model = null)
        {
            if (model == null)
            {
                model = new HomeModel();
            }

            return View(model);

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
