using MaFormaPlusCoreMVC.Data;
using MaFormaPlusCoreMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaFormaPlusCoreMVC.Controllers
{
    public class SessionsController : Controller
    {
        // fields
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Utilisateur> _userManager;


        // constructor
        public SessionsController(ApplicationDbContext context, UserManager<Utilisateur> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // actions
        public async Task<IActionResult> Index()
        {
            return _context.Sessions != null ?
                        View(await _context.Sessions.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Sessions'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            Parcours parcours = await (from p in _context.Parcours where p.Id == session.ParcoursId select p).FirstAsync();
            return View(new SessionParcours() { Session = session, Parcours = parcours });
        }

        public IActionResult Create()
        {
            List<Parcours> parcours = (from p in _context.Parcours select p).ToList();
            ViewBag.parcours = parcours;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Libelle,Debut, Fin")] Session session, int selectedParcours)
        {
            if (ModelState.IsValid)
            {
                InsertParcours(session, selectedParcours);
                _context.Add(session);
                await _context.SaveChangesAsync();

                string conseillerId = _userManager.GetUserId(HttpContext.User);
                Conseiller conseiller = await (from c in _context.Conseillers where c.Id == conseillerId select c).FirstAsync();
                conseiller.Sessions.Add(session);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            List<Parcours> parcours = (from p in _context.Parcours select p).ToList();
            ViewBag.parcours = parcours;

            return View(session);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Libelle,Debut, Fin")] Session session, int selectedParcours)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Parcours parcours = await (from p in _context.Parcours where p.Id == selectedParcours select p).FirstAsync();
                    session.ParcoursId = parcours.Id;
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sessions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sessions'  is null.");
            }
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(int id)
        {
            string? stagiaireId;
            if (ModelState.IsValid && IsConnected(out stagiaireId))
            {
                _context.SessionUtilisateurs?.Add(new SessionStagiaire() { SessionId = id, StagiaireId = stagiaireId });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }

        // methods
        private bool SessionExists(int id)
        {
            return (_context.Sessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private void InsertParcours(Session session, int? selectedParcours)
        {
            Parcours? parcours = (from p in _context.Parcours where p.Id == selectedParcours select p).FirstOrDefault();
            if (parcours != null)
                session.Parcours = parcours;
        }
        private bool IsConnected(out string? stagiaireId)
        {
            stagiaireId = _userManager.GetUserId(HttpContext.User);
            return stagiaireId != null;
        }
    }
}
