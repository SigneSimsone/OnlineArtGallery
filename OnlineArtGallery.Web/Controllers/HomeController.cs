using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineArtGallery.Web.Data.Managers;
using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult Index()
        {
            return View();
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
