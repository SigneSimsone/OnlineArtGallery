using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineArtGallery.Web.Data.Managers;
using OnlineArtGallery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet]
        public IActionResult ShowFilteredArtists(Guid[] artistsId)
        {
            var userId = _userManager.GetUserId(User);
            ArtistModel[] artists = _artistDataManager.GetArtists(artistsId.ToList());

            ArtistViewModel viewModel = new ArtistViewModel();            
            viewModel.Artists = artists;
            viewModel.UserId = userId;

            return View("Index", viewModel);

        }

        [HttpPost]
        public IActionResult AddArtist(string name, string surname, string place)
        {
            _artistDataManager.AddArtist(name, surname, place);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult OpenArtist(Guid ArtistId)
        {
            // get artist from database (ArtistModel)
            ArtistModel model = _artistDataManager.GetOneArtist(ArtistId);
            // return view 

            return View("OneArtist", model);
        }

        [HttpPost]
        public IActionResult Edit(Guid ArtistId, string name, string surname, string place)
        {
            _artistDataManager.Edit(ArtistId, name, surname, place);


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(Guid ArtistId)
        {
            // get artist from database (ArtistModel)
            ArtistModel model = _artistDataManager.GetOneArtist(ArtistId);
            // return view 

            return View(model);
        }

        [HttpPost]
        public IActionResult Favorite(Guid ArtistId)
        {
            var user = _userManager.GetUserAsync(User).Result;

            _artistDataManager.FavoriteArtist(ArtistId, user);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Unfavorite(Guid ArtistId)
        {
            var user = _userManager.GetUserAsync(User).Result;

            _artistDataManager.UnFavoriteArtist(ArtistId, user);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Guid ArtistId)
        {

            _artistDataManager.Delete(ArtistId);

            return RedirectToAction(nameof(Index));
        }
    }
}
