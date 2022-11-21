using AssignmentAsp.Net.Models;
using AssignmentAsp.Net.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssignmentAsp.Net.Controllers
{
    public class TourController : Controller
    {
        private readonly ILogger<TourController> _logger;
        private readonly ITourService _services;
        private readonly IHttpContextAccessor _sc;

        public TourController(ILogger<TourController> logger, ITourService services, IHttpContextAccessor sessionContext)
        {
            _logger = logger;
            _services = services;
            _sc = sessionContext;
        }

        // GET TOUR
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery(Name = "search")] string search)
        {
            bool userAuthenticated = false;
            if (_sc.HttpContext.Session.GetString("UserId") != null) { userAuthenticated = true; }

            if (userAuthenticated)
            {
                var data = await _services.GetAll(search);
                return View(data);

            }
            return RedirectToAction("Signin", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var data = await _services.GetById(id);
            var viewData = new TourViewModel();

            if (data != null)
            {
                viewData.Id = data.Id;
                viewData.Name = data.Name;
                viewData.Address = data.Address;
                viewData.Rating = data.Rating;
                viewData.Description = data.Description;
                viewData.ImageUrl = data.ImageUrl;
                return View("~/Views/Tour/Details.cshtml", viewData);
            }

           return View("~/Views/Tour/Details.cshtml", viewData);
        }

        //  ADD NEW TOUR
        [HttpGet]
        public IActionResult Add()
        {
            bool userAuthenticated = false;
            if (_sc.HttpContext.Session.GetString("UserId") != null) { userAuthenticated = true; }

            if(userAuthenticated)
            {
                return View("~/Views/Tour/Add.cshtml");
            }
            return RedirectToAction("Signin", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Add(TourViewModel model)
        {
            // convert image file to base64 string
            string hexString = "";
            if (model.Image != null)
            {
                var bytes = await GetBytes(model);
                hexString = Convert.ToBase64String(bytes);
                model.ImageUrl = hexString;
            }

            // clear previous validation & rerun validation function
            ModelState.Clear();
            TryValidateModel(model);

            // check model validty
            if (ModelState.IsValid)
            {                
                var domainModel = new TourViewModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    ImageUrl = model.ImageUrl,
                    Address = model.Address,
                    Rating = model.Rating,
                    Description = model.Description,
                };

                await _services.Create(domainModel);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // Edit Existing tour
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            bool userAuthenticated = false;
            if (_sc.HttpContext.Session.GetString("UserId") != null) { userAuthenticated = true; }

            if (userAuthenticated)
            {
                var data = await _services.GetById(id);
                var viewData = new TourViewModel()
                {
                    Id = data.Id,
                    Name = data.Name,
                    Address = data.Address,
                    Rating = data.Rating,
                    Description = data.Description,
                    ImageUrl = data.ImageUrl
                };
                return View("~/Views/Tour/Edit.cshtml", viewData);
            }
            return RedirectToAction("Signin", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TourViewModel model)
        {
            string hexString = "";
            if (model.Image != null)
            {
                var bytes = await GetBytes(model);
                hexString = Convert.ToBase64String(bytes);
                model.ImageUrl = hexString;
            }

            ModelState.Clear();
            TryValidateModel(model);

            if (ModelState.IsValid)
            {
                var data = new TourViewModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    ImageUrl = model.ImageUrl,
                    Address = model.Address,
                    Rating = model.Rating,
                    Description = model.Description,
                };
                await _services.Edit(data);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // Delete A Tour
        public async Task<JsonResult> Delete(int id)
        {
            bool result = false;         
            if (id != null)
            {
                var data = await _services.GetById(id);
                await _services.Delete(data);
                result = true;
            }
            return Json(result);
        }

        // Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<byte[]> GetBytes(TourViewModel model)
        {
            await using var memoryStream = new MemoryStream();
            await model.Image.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}