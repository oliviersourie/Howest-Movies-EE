using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Howest_Movies_EE_Web.Models;
using Howest_Movies_EE_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Howest_Movies_EE_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MoviesContext db;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            db = new MoviesContext();
        }

        public IActionResult Index()
        {
            var result = db.Movies.Select(m => new
            {
                m.ImdbId,
                m.Title,
                m.Rating,
                genres = m.GenreMovie.Select(gm => new { gm.Genre.Name })

            });

            List<Movies> nogresult = db.Movies.Include(m => m.MovieRole).ToList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
