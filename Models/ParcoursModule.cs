namespace MaFormaPlusCoreMVC.Models
{
    public class ParcoursModule
    {
        public Parcours Parcours { get; set; } = new();
        public Module Module { get; set; } = new();
        public ICollection<Parcours> Parcourses { get; set; } = new List<Parcours>();
        public ICollection<Module> Modules { get; set; } = new List<Module>();
        public ICollection<ModuleParcours> ModuleParcours { get; set; } = new List<ModuleParcours>();
    }
}
