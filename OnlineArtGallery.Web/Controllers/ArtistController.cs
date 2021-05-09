using Microsoft.AspNetCore.Mvc;
using OnlineArtGallery.Web.Data.Managers;
using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArtGallery.Web.Controllers
{
    public class ArtistController : Controller
    {
        private readonly ArtistDataManager _artistDataManager;

        public ArtistController(ArtistDataManager artistDataManager)
        {
            _artistDataManager = artistDataManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ArtistModel[] model = _artistDataManager.GetArtists();

            return View(model);
        }

        [HttpPost]
        public IActionResult AddArtist(string name, string surname, string place)
        {
            _artistDataManager.AddArtist(name, surname, place);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Guid id)
        {
            _artistDataManager.Edit(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _artistDataManager.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
