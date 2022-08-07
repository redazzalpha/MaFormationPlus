namespace MaFormaPlusCoreMVC.Models
{
    public class ParcoursModule
    {
        public Parcours Parcours { get; set; }
        public List<Module> Modules { get; set; }

        public ParcoursModule(Parcours parcours, List<Module> modules)
        {
            Parcours = parcours;
            Modules = modules;
        }
    }
}
