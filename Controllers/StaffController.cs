using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cifrofoto.Coursework.Controllers;

[Authorize]
public class StaffController : Controller
{
    public IActionResult Index() => View();
}
