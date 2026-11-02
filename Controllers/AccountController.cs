using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Cifrofoto.Coursework.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cifrofoto.Coursework.Controllers;

[AutoValidateAntiforgeryToken]
public class AccountController(IConfiguration configuration, IWebHostEnvironment environment) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Staff");

        return View(new LoginInput());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginInput input)
    {
        if (!ModelState.IsValid)
            return View(input);

        var expected = configuration["StaffPassword"];
        if (string.IsNullOrEmpty(expected) && environment.IsDevelopment())
            expected = "Study-Photo-2026!";
        if (string.IsNullOrEmpty(expected) || (!environment.IsDevelopment() && expected.Length < 16))
            return StatusCode(503, "Вход сотрудника пока не настроен.");

        var matches = CryptographicOperations.FixedTimeEquals(
            SHA256.HashData(Encoding.UTF8.GetBytes(input.Password)),
            SHA256.HashData(Encoding.UTF8.GetBytes(expected)));
        if (!matches)
        {
            ModelState.AddModelError("", "Неверный пароль");
            return View(input);
        }

        var identity = new ClaimsIdentity(
            new[] { new Claim(ClaimTypes.Name, "Сотрудник ателье") },
            CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        return RedirectToAction("Index", "Staff");
    }

    [Authorize, HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
