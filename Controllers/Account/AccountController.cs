using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using dashboard.Interfaces;
using dashboard.DTOs;
using System.Text.Json;

namespace dashboard.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Error en los campos.";
                return View();
            }

            try
            {
                var user = await _accountService.Register(userDTO);

                if (user.Id != 0)
                {
                    return RedirectToAction("Login", "Account");
                }

                ViewData["Message"] = "No se pudo registrar el usuario, error fatal.";
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Error en los campos.";
                return View();
            }

            var user = await _accountService.Login(loginDTO);

            if (user == null)
            {
                ViewData["Message"] = "Correo o contraseña incorrectos.";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email!)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();  // Limpia la sesión completamente
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task LoginWithGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse", "Account")
            });
        }

        [HttpGet]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            var claims = result.Principal?.Identities.FirstOrDefault()?.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            }).ToList();

            if (claims != null)
            {
                var json = JsonSerializer.Serialize(claims, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine(json);

                var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var image = claims.FirstOrDefault(c => c.Type == "picture")?.Value;
                var phone = claims.FirstOrDefault(c => c.Type == "phone_number")?.Value;
                var address = claims.FirstOrDefault(c => c.Type == "address")?.Value;
                var country = claims.FirstOrDefault(c => c.Type == "country")?.Value;

                if (email != null)
                {
                    var user = await _accountService.GoogleLoginAsync(email, name);
                    var claimsIdentity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Email, user.Email!),
                        new Claim(ClaimTypes.Name, user.Name!)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    HttpContext.Session.SetString("UserId", user.Id.ToString()!);
                    HttpContext.Session.SetString("UserEmail", user.Email!);
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Login");
        }
    }
}
