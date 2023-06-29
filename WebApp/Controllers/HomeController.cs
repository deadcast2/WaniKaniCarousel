using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Lib;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SubjectDownloader _SubjectDownloader;

        public HomeController(SubjectDownloader subjectDownloader)
        {
            _SubjectDownloader = subjectDownloader;
        }

        public IActionResult Index()
        {
            _SubjectDownloader.Start();

            return View(SubjectSelector.GetNext());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}