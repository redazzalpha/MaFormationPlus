using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MaFormaPlusCoreMVC.Data;
using MaFormaPlusCoreMVC.Models;

namespace MaFormaPlusCoreMVC.Controllers
{
    public class ConseillersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConseillersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Conseillers
        public async Task<IActionResult> Index()
        {
              return _context.Conseillers != null ? 
                          View(await _context.Conseillers.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Conseillers'  is null.");
        }

        // GET: Conseillers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Conseillers == null)
            {
                return NotFound();
            }

            var conseiller = await _context.Conseillers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conseiller == null)
            {
                return NotFound();
            }

            return View(conseiller);
        }

        // GET: Conseillers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conseillers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom")] Conseiller conseiller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conseiller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conseiller);
        }

        // GET: Conseillers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Conseillers == null)
            {
                return NotFound();
            }

            var conseiller = await _context.Conseillers.FindAsync(id);
            if (conseiller == null)
            {
                return NotFound();
            }
            return View(conseiller);
        }

        // POST: Conseillers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom")] Conseiller conseiller)
        {
            if (id != conseiller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conseiller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConseillerExists(conseiller.Id))
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
            return View(conseiller);
        }

        // GET: Conseillers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Conseillers == null)
            {
                return NotFound();
            }

            var conseiller = await _context.Conseillers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conseiller == null)
            {
                return NotFound();
            }

            return View(conseiller);
        }

        // POST: Conseillers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Conseillers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Conseillers'  is null.");
            }
            var conseiller = await _context.Conseillers.FindAsync(id);
            if (conseiller != null)
            {
                _context.Conseillers.Remove(conseiller);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConseillerExists(int id)
        {
          return (_context.Conseillers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
