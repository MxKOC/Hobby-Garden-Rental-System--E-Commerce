using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FieldRent.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using FieldRent.Data.Abstract;

namespace FieldRent.Controllers;

public class LoginController : Controller
{

    private IUserRepository _userRepository;
    public LoginController(IUserRepository userRepository)
    {
        _userRepository = userRepository;

    }



    public IActionResult Login()
    {
        if (User.Identity!.IsAuthenticated)    //user girişi yoksa
        {
            return RedirectToAction("IndexMap", "Admin");
        }
        return View();
    }




    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }




    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var isUser = _userRepository.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            if (isUser != null)
            {
                var userClaims = new List<Claim>();

                userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));

                if (isUser.Email == "info@muhammed.com")
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                }

                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);



/*    ! gün kala nereye gönderileceğine karar verilsin
                var list = new List<string>();  // Dinamik liste

                foreach (var item in isUser.Maps)
                {
                    TimeSpan? timeSpan = item.MapStop - DateTime.Now;
                    if (item.MapStop < DateTime.Now.AddHours(23) || item.MapStop < DateTime.Now.AddHours(24))
                    {
                        list.Add(item.MapId.ToString());
                    }
                }


                for (int i = 0; i < list.Count; i++)
                {
                    TempData["Item" + i] = "Sayın " + isUser.UserName + " --> " + list[i] + " Kordinatlı mapin süresine son 1 gün kaldı..."; ;
                }
*/

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
            }
        }

        return View(model);
    }





}
