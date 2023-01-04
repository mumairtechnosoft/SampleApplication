using Feedback360.DB;
using Feedback360.Models;
using Feedback360.Models.Enums;
using Feedback360.Models.Models.Account.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SampleApplication.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            ViewData["ReturnUrl"] = model.ReturnUrl;

            if (ModelState.IsValid)
            {

                //  Check here user from database

                var user = await _context.Users.Where(x => x.Username == model.Username).FirstOrDefaultAsync();
                if (user == null)
                {
                    return Ok(GenericResponse<bool>.Failure("Invalid Username or Password!", ApiStatusCode.RecordNotFound));
                }
                else
                {
                    if (user.Password != model.Password)
                    {
                        return Ok(GenericResponse<bool>.Failure("Invalid Username or Password!", ApiStatusCode.RecordNotFound));
                    }
                    else
                    {
                        var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, Enum.GetName(typeof(Roles), user.Role)),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

                        if (Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                    }
                }

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
