using MaFormaPlusCoreMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace MaFormaPlusCoreMVC.Models
{
    public class ParcoursModule
    {
        public Parcours Parcours { get; set; } = new();
        public Module Module { get; set; } = new();
        public ICollection<Parcours> Parcourses { get; set; } = new List<Parcours>();
        public ICollection<Module> Modules { get; set; } = new List<Module>();
        public ICollection<ModuleParcours> ModuleParcours { get; set; } = new List<ModuleParcours>();

        public async Task<bool> IsModuleAttached(ApplicationDbContext _contenxt, int moduleId)
        {
            if (_contenxt.Modules != null)
            {
                List<Module> attachedModules = await (from mp in _contenxt.ModuleParcours
                                                      where mp.ParcoursId == Parcours.Id
                                                      join m in _contenxt.Modules on mp.ModuleId equals moduleId
                                                      select m).ToListAsync();
                return attachedModules.Count() > 0;
            }
            return false;
        }
        public async Task<bool> IsParcoursAttached(ApplicationDbContext _contenxt, int parcoursId)
        {
            if (_contenxt.Parcours != null)
            {
                List<Parcours> attachedParcours = await (from mp in _contenxt.ModuleParcours
                                                         where mp.ModuleId == Module.Id
                                                         join p in _contenxt.Parcours on mp.ParcoursId equals parcoursId
                                                         select p).ToListAsync();
                return attachedParcours.Count() > 0;
            }
            return false;
        }
    }
}
