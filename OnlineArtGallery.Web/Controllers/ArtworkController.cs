using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using OnlineArtGallery.Web.Data.Managers;
using OnlineArtGallery.Web.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArtGallery.Web.Controllers
{

    public class ArtworkController : Controller
    {
        private readonly AuditDataManager _auditDataManager;
        private readonly ArtworkDataManager _artworkDataManager;
        private readonly ArtistDataManager _artistDataManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly IStringLocalizer<Program> _localizer;

        public ArtworkController(ArtworkDataManager artworkDataManager, UserManager<UserModel> userManager, ArtistDataManager artistDataManager, IWebHostEnvironment hostingEnv, AuditDataManager auditDataManager, IStringLocalizer<Program> localizer)
        {
            _artworkDataManager = artworkDataManager;
            _userManager = userManager;
            _artistDataManager = artistDataManager;
            _hostingEnv = hostingEnv;
            _auditDataManager = auditDataManager;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ArtworkModel[] artworks = _artworkDataManager.GetArtworks();
            var userId = _userManager.GetUserId(User);

            ArtworkViewModel viewModel = new ArtworkViewModel();
            viewModel.Artworks = artworks;
            viewModel.UserId = userId;

            ArtistModel[] artists = _artistDataManager.GetArtists();
            var artistList = artists.Select(x => new { x.Id, x.FullName }).ToList();
            viewModel.ArtistDropdown = new SelectList(artistList, "Id", "FullName");


            StyleModel[] styles = _artworkDataManager.GetStyles();
            var styleList = styles.Select(x => new { x.Id, x.Style }).ToList();
            viewModel.StyleDropdown = new SelectList(styleList, "Id", "Style");

            var availablityList = new[] {
                new {Id = 0, Availability = "Unavailable"},
                new {Id = 1, Availability = "Available" },
                new {Id = 2, Availability = "Both" }
            }.ToList();

            viewModel.AvailabilityDropdown = new SelectList(availablityList, "Id", "Availability");


            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ShowFilteredArtworks(Guid[] artworksId)
        {
            var userId = _userManager.GetUserId(User);
            ArtworkModel[] artworks = _artworkDataManager.GetArtworks(artworksId.ToList());

            ArtworkViewModel viewModel = new ArtworkViewModel();
            viewModel.Artworks = artworks;
            viewModel.UserId = userId;

            ArtistModel[] artists = _artistDataManager.GetArtists();
            var artistList = artists.Select(x => new { x.Id, x.FullName }).ToList();
            viewModel.ArtistDropdown = new SelectList(artistList, "Id", "FullName");


            StyleModel[] styles = _artworkDataManager.GetStyles();
            var styleList = styles.Select(x => new { x.Id, x.Style }).ToList();
            viewModel.StyleDropdown = new SelectList(styleList, "Id", "Style");

            var availablityList = new[] {
                new {Id = 0, Availability = "Unavailable"},
                new {Id = 1, Availability = "Available" },
                new {Id = 2, Availability = "Both" }
            }.ToList();

            viewModel.AvailabilityDropdown = new SelectList(availablityList, "Id", "Availability");

            return View("Index", viewModel);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddArtwork(ArtworkViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index), model);
            }

            string relativeFilePath = null;
            ArtistModel artistmodel = _artistDataManager.GetOneArtist(model.SelectedArtist);
            StyleModel stylemodel = _artworkDataManager.GetOneStyle(model.SelectedStyle);

            if (model.File != null)
            {
                //upload files to wwwroot
                string fileName = $"{artistmodel.FullName}_{model.Title}_{stylemodel.Style}.jpg".Replace(" ", "");
                var absoluteFilePath = Path.Combine(_hostingEnv.WebRootPath, "images", fileName);

                using (var fileSteam = new FileStream(absoluteFilePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(fileSteam);
                }

                relativeFilePath = Path.Combine("\\images", fileName);
            }



            _artworkDataManager.AddArtwork(model.Title, artistmodel, stylemodel, model.Year, model.Description, model.Type, model.Price, model.Availability, relativeFilePath);

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid ArtworkId, ArtworkViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index), model);
            }

            string relativeFilePath = null;
            ArtistModel artistmodel = _artistDataManager.GetOneArtist(model.SelectedArtist);
            StyleModel stylemodel = _artworkDataManager.GetOneStyle(model.SelectedStyle);

            if (model.File != null)
            {
                //upload files to wwwroot
                string fileName = $"{artistmodel.FullName}_{model.Title}_{stylemodel.Style}.jpg".Replace(" ", "");
                var absoluteFilePath = Path.Combine(_hostingEnv.WebRootPath, "images", fileName);

                using (var fileSteam = new FileStream(absoluteFilePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(fileSteam);
                }

                relativeFilePath = Path.Combine("\\images", fileName);
            }




            _artworkDataManager.Edit(ArtworkId, model.Title, artistmodel, stylemodel, model.Year, model.Description, model.Type, model.Price, model.Availability, relativeFilePath);


            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(Guid ArtworkId)
        {
            // get artwork from database (ArtworkModel)
            ArtworkModel model = _artworkDataManager.GetOneArtwork(ArtworkId);
            ArtworkViewModel viewModel = new ArtworkViewModel();
            viewModel.Id = model.Id;
            viewModel.Title = model.Title;

            ArtistModel[] artists = _artistDataManager.GetArtists();
            var artistList = artists.Select(x => new { x.Id, x.FullName }).ToList();

            viewModel.ArtistDropdown = new SelectList(artistList, "Id", "FullName", model.Artist.FullName);


            StyleModel[] styles = _artworkDataManager.GetStyles();
            var styleList = styles.Select(x => new { x.Id, x.Style }).ToList();

            viewModel.StyleDropdown = new SelectList(styleList, "Id", "Style", model.Style.Style);

            viewModel.Year = model.Year;
            viewModel.Description = model.Description;
            viewModel.Type = model.Type;
            viewModel.Price = model.Price;
            viewModel.Availability = model.Availability;
            viewModel.FilePath = model.FilePath;

            // return view 

            return View(viewModel);
        }

        [Authorize(Roles = "RegisteredUser")]
        [HttpPost]
        public IActionResult Favorite(Guid ArtworkId)
        {
            var user = _userManager.GetUserAsync(User).Result;

            _artworkDataManager.FavoriteArtwork(ArtworkId, user);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "RegisteredUser")]
        [HttpPost]
        public IActionResult Unfavorite(Guid ArtworkId)
        {
            var user = _userManager.GetUserAsync(User).Result;

            _artworkDataManager.UnFavoriteArtwork(ArtworkId, user);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(Guid ArtworkId)
        {

            _artworkDataManager.Delete(ArtworkId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult OpenArtwork(Guid ArtworkId)
        {
            if (ArtworkId == Guid.Empty)
            {
                return RedirectToAction(nameof(Index));
            }


            // get artwork from database (ArtworkModel)
            ArtworkModel model = _artworkDataManager.GetOneArtwork(ArtworkId);

            return View("OneArtwork", model);
        }

        [Authorize(Roles = "RegisteredUser")]
        [HttpGet]
        public IActionResult BuyArtwork(Guid ArtworkId)
        {
            // get artwork from database (ArtworkModel)
            ArtworkModel model = _artworkDataManager.GetOneArtwork(ArtworkId);

            return View("OrderArtwork", model);
        }

        [Authorize(Roles = "RegisteredUser")]
        [HttpPost]
        public IActionResult AddOrder(string address, Guid ArtworkId)
        {
            ArtworkModel artworkmodel = _artworkDataManager.GetOneArtwork(ArtworkId);

            var userId = _userManager.GetUserId(User);
            UserModel usermodel = _artworkDataManager.GetOneUser(userId);

            _artworkDataManager.AddOrder(address, artworkmodel, usermodel);
            _artworkDataManager.EditAvailabilityAfterBuying(ArtworkId);

            HomeModel homemodel = new HomeModel();
            homemodel.Message = _localizer["Order accepted"];

            return RedirectToAction("Index", "Home", homemodel);
        }


        [Authorize(Roles = "RegisteredUser")]
        [HttpPost]
        public IActionResult AddFeedback(Guid ArtworkId, string comment)
        {
            var user = _userManager.GetUserAsync(User).Result;
            _artworkDataManager.AddFeedback(ArtworkId, comment, user);

            return RedirectToAction(nameof(OpenArtwork), new { ArtworkId });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteFeedback(Guid FeedbackId, Guid ArtworkId)
        {
            _artworkDataManager.DeleteFeedback(FeedbackId);

            return RedirectToAction(nameof(OpenArtwork), new { ArtworkId });
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddStyle(string style)
        {
            _artworkDataManager.AddStyle(style);

            return RedirectToAction(nameof(Index));
        }

    }
}

