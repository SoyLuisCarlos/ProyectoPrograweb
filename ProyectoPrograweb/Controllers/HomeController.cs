using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPrograweb.Models;
using ProyectoPrograweb.Models.dbModels;
using ProyectoPrograweb.ViewModel;
using System.Diagnostics;

namespace ProyectoPrograweb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProyectoContext _context;

        public HomeController(ILogger<HomeController> logger, ProyectoContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult NewsFeed()
        {
            NewsViewModel nvm = new ()
            {
                Noticias = _context.News.ToList()
            };
            return View(nvm);
        }

        [Authorize]
        [HttpGet]
        public IActionResult MatchesFeed()
        {
            MatchesViewModel mvm = new()
            {
                Partidos = _context.Matches.ToList()
            };
            return View(mvm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}