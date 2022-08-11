using MaFormaPlusCoreMVC.Data;
using MaFormaPlusCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MaFormaPlusCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        // fields
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        // constructors
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // actions
        public async Task<IActionResult> Index()
        {
            ICollection < SessionParcours > sessionParcourses = new List<SessionParcours>();
            if (_context.Parcours != null)
            {
                sessionParcourses = await (from s in _context.Sessions
                                           join p in _context.Parcours on s.ParcoursId equals p.Id
                                           select new SessionParcours() { Session = s, Parcours = p }).ToListAsync();
            }

            return View(sessionParcourses);
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