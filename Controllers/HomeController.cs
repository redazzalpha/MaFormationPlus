using MaFormaPlusCoreMVC.Data;
using MaFormaPlusCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            List<SessionParcours> sessionParcours = new();
            List<Session> sessions = (from s in _context.Sessions select s).ToList();

            foreach (Session session in sessions)
            {
                Parcours parcours = (from p in _context.Parcours where p.Id == session.ParcoursId select p).First() ;
                sessionParcours.Add(new SessionParcours(session, parcours));
            }

            return View(sessionParcours);
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