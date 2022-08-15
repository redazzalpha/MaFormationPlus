using MaFormaPlusCoreMVC.Data;
using MaFormaPlusCoreMVC.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<Utilisateur> _userManager;

        // constructors
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<Utilisateur> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
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

            string? userId = _userManager.GetUserId(HttpContext.User);
            List<int?> sessionIds = new();
            if(userId != null)
            {
                sessionIds = await (from s in _context.SessionStagiaire where s.StagiaireId == userId select s.SessionId).ToListAsync();
            }
            ViewBag.sessionIds = sessionIds;
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