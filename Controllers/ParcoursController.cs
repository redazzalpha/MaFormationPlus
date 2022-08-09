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
        private readonly IWebHostEnvironment _webHost;

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


            List<Module> modules = new();
            if (_context.ModuleParcours != null && _context.Modules != null)
            {
                modules = await (from m in _context.Modules
                                 join mp in _context.ModuleParcours on m.Id equals mp.ModuleId
                                 join p in _context.Parcours on mp.ParcoursId equals p.Id
                                 where p.Id == id
                                 select m).ToListAsync();
            }
            return View(new ParcoursModule() { Parcours = parcours, Modules = modules });
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
                _context.Add(parcours);
                await _context.SaveChangesAsync();
                await InsertModule(parcours, selectedModule);
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

            List<Module> modules = (from m in _context.Modules select m).ToList();
            List<ModuleParcours> moduleParcours = await (from mp in _context.ModuleParcours where mp.ParcoursId == id select mp).ToListAsync();

            return View(new ParcoursModule() { Parcours = parcours, Modules = modules, ModuleParcours = moduleParcours });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile? file, int id, [Bind("Id,Nom,Resume,Logo")] Parcours parcours, int? selectedModule)
        {
            if (id != parcours.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DeleteImg(parcours);
                    await InsertImg(file, parcours, parcours.Logo);
                    await InsertModule(parcours, selectedModule);
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
            ModuleParcours? moduleParcours = await (from mp in _context.ModuleParcours where mp.ParcoursId == id select mp).FirstOrDefaultAsync();
            if (moduleParcours != null)
                _context.Remove(moduleParcours);

            if (_context.Parcours == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Parcours'  is null.");
            }
            var parcours = await _context.Parcours.FindAsync(id);
            if (parcours != null)
            {
                DeleteImg(parcours);
                _context.Parcours.Remove(parcours);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delier(int id)
        {
            int parcoursId = 0;
            if (_context.ModuleParcours != null && _context.Parcours != null)
            {
                var moduleParcours = await (from m in _context.Modules
                                            where m.Id == id
                                            join mp in _context.ModuleParcours on m.Id equals mp.ModuleId
                                            join p in _context.Parcours on mp.ParcoursId equals p.Id
                                            select new { Parcours = p, Module = m, ModuleParcours = mp }).FirstAsync();

                _context.Remove(moduleParcours.ModuleParcours);
                await _context.SaveChangesAsync();
                parcoursId = moduleParcours.Parcours.Id;
            }
            return RedirectToAction("Details", new { id = parcoursId });
        }

        // methodsd
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
                using (FileStream fs = new(path, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                parcours.Logo = Defines.Defines.SEVER_ADDRESS + folder + fileName;
            }
            else if (logoStr == "")
            {
                string? logo = await (from p in _context.Parcours where p.Id == parcours.Id select p.Logo).FirstOrDefaultAsync();
                if (logo != null)
                    parcours.Logo = logo;
                else parcours.Logo = defaultImg;
            }
            else if (logoStr != null) parcours.Logo = logoStr;
            else parcours.Logo = defaultImg;
        }
        private static void DeleteImg(Parcours parcours)
        {
            if (parcours.Logo != Defines.Defines.DEFAULT_IMG && parcours.Logo != null && parcours.Logo != "")
            {
                string file = parcours.Logo.Split(Defines.Defines.SEVER_ADDRESS)[1];
                System.IO.File.Delete(@"wwwroot" + file);
            }
        }
        private async Task InsertModule(Parcours parcours, int? selectedModule)
        {
            if (selectedModule != null)
            {
                parcours.ModuleParcours.Add(new ModuleParcours() { ParcoursId = parcours.Id, ModuleId = selectedModule });
                _context.Update(parcours);
                await _context.SaveChangesAsync();
            }
        }
    }
}
