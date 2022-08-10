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
    public class ModulesController : Controller
    {
        // fields
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        // constructors
        public ModulesController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // actions
        public async Task<IActionResult> Index()
        {
            return _context.Modules != null ?
                        View(await _context.Modules.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Modules'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        public  async Task<IActionResult> Create()
        {
            ICollection<Parcours> parcourses = await (from p in _context.Parcours select p).ToListAsync();
            return View(new ParcoursModule() { Parcourses = parcourses});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile? file, [Bind("Id,Nom,Resume,Info,Logo")] Module @module)
        {
            if (ModelState.IsValid)
            {
                await InsertImg(file, @module);

                _context.Add(@module);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@module);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }

            ICollection<Parcours> parcourses = await (from p in _context.Parcours select p).ToListAsync();
            List<ModuleParcours> moduleParcours = await (from mp in _context.ModuleParcours where mp.ModuleId == id select mp).ToListAsync();

            return View(new ParcoursModule() {Parcourses = parcourses, Module = @module, ModuleParcours = moduleParcours });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile? file, int id, [Bind("Id,Nom,Resume,Info,Logo")] Module @module)
        {
            if (id != @module.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DeleteImg(@module);
                    await InsertImg(file, @module, @module.Logo);
                    _context.Update(@module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(@module.Id))
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
            return View(@module);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (_context.Modules == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Modules'  is null.");
            }
            var @module = await _context.Modules.FindAsync(id);
            if (@module != null)
            {
                if (@module.Logo != Defines.Defines.DEFAULT_IMG && @module.Logo != null)
                {
                    string file = @module.Logo.Split(Defines.Defines.SEVER_ADDRESS)[1];
                    System.IO.File.Delete(@"wwwroot" + file);
                }
                _context.Modules.Remove(@module);
            }
            ICollection<ModuleParcours> moduleParcours = await (from mp in _context.ModuleParcours where mp.ModuleId == id select mp).ToListAsync();
            foreach (var item in moduleParcours)
            {
                _context.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // methods
        private bool ModuleExists(int id)
        {
            return (_context.Modules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private async Task InsertImg(IFormFile? file, Module module, string? logoStr = null)
        {
            string defaultImg = Defines.Defines.SEVER_ADDRESS + "/assets/generics/no-image.png";
            if (file != null)
            {
                string rootPath = _webHost.WebRootPath;
                string folder = "/assets/modules/";
                string fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" +
                           Guid.NewGuid() +
                           Path.GetExtension(file.FileName);
                string path = rootPath + folder + fileName;
                using (FileStream fs = new(path, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                module.Logo = Defines.Defines.SEVER_ADDRESS + folder + fileName;
            }
            else if (logoStr == "")
            {
                string? logo = await (from m in _context.Modules where m.Id == module.Id select m.Logo).FirstOrDefaultAsync();
                if (logo != null)
                    module.Logo = logo;
                else module.Logo = defaultImg;
            }
            else if (logoStr != null) module.Logo = logoStr;
            else module.Logo = defaultImg;
        }

        private static void DeleteImg(Module module)
        {
            if (module.Logo != Defines.Defines.DEFAULT_IMG && module.Logo != null && module.Logo != "")
            {
                string file = module.Logo.Split(Defines.Defines.SEVER_ADDRESS)[1];
                System.IO.File.Delete(@"wwwroot" + file);
            }
        }

    }
}
