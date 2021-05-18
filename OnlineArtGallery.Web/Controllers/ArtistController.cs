using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineArtGallery.Web.Data.Managers;
using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArtGallery.Web.Controllers
{
    [Authorize]
    public class ArtistController : Controller
    {
        private readonly ArtistDataManager _artistDataManager;
        private readonly UserManager<UserModel> _userManager;

        public ArtistController(ArtistDataManager artistDataManager, UserManager<UserModel> userManager)
        {
            _artistDataManager = artistDataManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ArtistModel[] artists = _artistDataManager.GetArtists();
            var userId = _userManager.GetUserId(User);
            ArtistViewModel viewModel = new ArtistViewModel();
            viewModel.Artists = artists;
            viewModel.UserId = userId;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddArtist(string name, string surname, string place)
        {
            _artistDataManager.AddArtist(name, surname, place);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Guid id, string name, string surname, string place)
        {
            _artistDataManager.Edit(id, name, surname, place);


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            // get artist from database (ArtistModel)
            ArtistModel model = _artistDataManager.GetOneArtist(id);
            // return view 

            return View(model);
        }

        [HttpPost]
        public IActionResult Favorite(Guid id)
        {
            var user = _userManager.GetUserAsync(User).Result;

            _artistDataManager.FavoriteArtist(id, user);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Unfavorite(Guid id)
        {
            var user = _userManager.GetUserAsync(User).Result;

            _artistDataManager.UnFavoriteArtist(id, user);

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
