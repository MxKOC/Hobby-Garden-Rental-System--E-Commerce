using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FieldRent.Models;

namespace FieldRent.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

}
