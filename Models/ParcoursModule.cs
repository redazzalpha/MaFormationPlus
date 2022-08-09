namespace MaFormaPlusCoreMVC.Models
{
    public class ParcoursModule
    {

        public Parcours Parcours { get; set; } = new();
        public Module Module { get; set; } = new();
        public List<Parcours> Parcourses { get; set; } = new();
        public List<Module> Modules { get; set; } = new();
        public List<ModuleParcours> ModuleParcours { get; set; } = new();
    }
}
