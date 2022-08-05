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
    public class ParcoursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParcoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Parcours
        public async Task<IActionResult> Index()
        {
              return _context.Parcours != null ? 
                          View(await _context.Parcours.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Parcours'  is null.");
        }

        // GET: Parcours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Parcours == null)
            {
                return NotFound();
            }

            var parcours = await _context.Parcours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcours == null)
            {
                return NotFound();
            }

            return View(parcours);
        }

        // GET: Parcours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parcours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Resume,Logo")] Parcours parcours)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parcours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parcours);
        }

        // GET: Parcours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Parcours == null)
            {
                return NotFound();
            }

            var parcours = await _context.Parcours.FindAsync(id);
            if (parcours == null)
            {
                return NotFound();
            }
            return View(parcours);
        }

        // POST: Parcours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Resume,Logo")] Parcours parcours)
        {
            if (id != parcours.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parcours);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcoursExists(parcours.Id))
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
            return View(parcours);
        }

        // GET: Parcours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Parcours == null)
            {
                return NotFound();
            }

            var parcours = await _context.Parcours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcours == null)
            {
                return NotFound();
            }

            return View(parcours);
        }

        // POST: Parcours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Parcours == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Parcours'  is null.");
            }
            var parcours = await _context.Parcours.FindAsync(id);
            if (parcours != null)
            {
                _context.Parcours.Remove(parcours);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParcoursExists(int id)
        {
          return (_context.Parcours?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
