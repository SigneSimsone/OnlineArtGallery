using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineArtGallery.Web.Data.Managers;
using OnlineArtGallery.Web.Models;
using System;

namespace OnlineArtGallery.Web.Controllers
{

    [Authorize]
    public class ArtworkController : Controller
    {
        private readonly ArtworkDataManager _artworkDataManager;
        private readonly UserManager<UserModel> _userManager;

        public ArtworkController(ArtworkDataManager artworkDataManager, UserManager<UserModel> userManager)
        {
            _artworkDataManager = artworkDataManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ArtworkModel[] artworks = _artworkDataManager.GetArtworks();
            var userId = _userManager.GetUserId(User);
            ArtworkViewModel viewModel = new ArtworkViewModel();
            viewModel.Artworks = artworks;
            viewModel.UserId = userId;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddArtwork(string title, int year, string description, string type, float price)
        {
            _artworkDataManager.AddArtwork(title, year, description, type, price);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Guid id, string title, int year, string description, string type, float price, bool availability)
        {
            _artworkDataManager.Edit(id, title, year, description, type, price, availability);


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            // get artwork from database (ArtworkModel)
            ArtworkModel model = _artworkDataManager.GetOneArtwork(id);
            // return view 

            return View(model);
        }

        [HttpPost]
        public IActionResult Favorite(Guid id)
        {
            var user = _userManager.GetUserAsync(User).Result;

            _artworkDataManager.FavoriteArtwork(id, user);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Unfavorite(Guid id)
        {
            var user = _userManager.GetUserAsync(User).Result;

            _artworkDataManager.UnFavoriteArtwork(id, user);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            _artworkDataManager.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}

