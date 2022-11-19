using AssignmentAsp.Net.Data;
using AssignmentAsp.Net.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentAsp.Net.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext _db;
        private readonly IHttpContextAccessor _sc;

        public AccountController(ApplicationDbContext db, IHttpContextAccessor sessionContext)
        {
            _db = db;
            _sc = sessionContext;
        }

        // Sign in
        public IActionResult Signin()
        {
            bool userAuthenticated = false;
            if (_sc.HttpContext.Session.GetString("UserId") != null) { userAuthenticated = true; }

            if(userAuthenticated)
            {
                return RedirectToAction("Index", "Tour");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signin(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = await _db.Accounts.FirstOrDefaultAsync(x => x.Username == model.Username && x.Password == model.Password);
                if (data != null)
                {
                    _sc.HttpContext.Session.SetString("UserId", data.Id.ToString());
                    _sc.HttpContext.Session.SetString("UserName", data.Username.ToString());
                    _sc.HttpContext.Session.SetInt32("UserIsSignedIn", 1);

                    return RedirectToAction("Index", "Tour");
                }

                model.LoginErrorMessage = "Invalid username or Password";
                return View(model);
            }
            return View(model);
        }

        public IActionResult Signout()
        {
            _sc.HttpContext.Session.Clear();
            return RedirectToAction("Signin", "Account");
        }
    }
}
