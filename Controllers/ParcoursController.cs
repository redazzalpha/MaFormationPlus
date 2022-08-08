using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MaFormaPlusCoreMVC.Data;
using MaFormaPlusCoreMVC.Models;
using System.Configuration;

namespace MaFormaPlusCoreMVC.Controllers
{
    public class ParcoursController : Controller
    {
        // fields
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _webHost;

        // constructors
        public ParcoursController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // actions
        public async Task<IActionResult> Index()
        {
            return _context.Parcours != null ?
                        View(await _context.Parcours.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Parcours'  is null.");
        }

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

            List<Module> modules = await (from m in _context.Modules where m.ParcoursId == parcours.Id select m).ToListAsync();
            return View(new ParcoursModule(parcours, modules));
        }

        public IActionResult Create()
        {
            List<Module> modules = (from m in _context.Modules select m).ToList();
            ViewBag.modules = modules;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile? file, [Bind("Id,Nom,Resume,Logo")] Parcours parcours, int? selectedModule)
        {
            if (ModelState.IsValid)
            {
                await InsertImg(file, parcours);
                await InsertModule(parcours, selectedModule);
                _context.Add(parcours);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parcours);
        }

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
                if(parcours.Logo != Defines.Defines.DEFAULT_IMG && parcours.Logo != null)
                {
                    string file = parcours.Logo.Split(Defines.Defines.SEVER_ADDRESS)[1];
                    System.IO.File.Delete(@"wwwroot"+ file);
                } 
                _context.Parcours.Remove(parcours);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delier(int id)
        {
            Module module = await (from m in _context.Modules where m.Id == id select m).FirstAsync();
            module.ParcoursId = null;
            _context.Update(module);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = id});
        }

        // methods
        private bool ParcoursExists(int id)
        {
            return (_context.Parcours?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private async Task InsertImg(IFormFile? file, Parcours parcours, string? logoStr = null)
        {
            string defaultImg = Defines.Defines.SEVER_ADDRESS + "/assets/generics/no-image.png";
            if (file != null)
            {
                string rootPath = _webHost.WebRootPath;
                string folder = "/assets/parcours/";
                string fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" +
                           Guid.NewGuid() +
                           Path.GetExtension(file.FileName);
                string path = rootPath + folder + fileName;
                using (FileStream fs = new(path, FileMode.Create)) {
                    await file.CopyToAsync(fs);
                }
                parcours.Logo = Defines.Defines.SEVER_ADDRESS + folder + fileName;
            }
            else if (logoStr != null) parcours.Logo = logoStr;
            else parcours.Logo = defaultImg;
        }
        private async Task InsertModule(Parcours parcours, int? selectedModule)
        {
            if (selectedModule != null)
            {
                Module? module = await (from m in _context.Modules where m.Id == selectedModule select m).FirstOrDefaultAsync();
                if (module != null)
                    parcours.Modules.Add(module);
            }
        }
    }
}
