using System.ComponentModel.DataAnnotations;

namespace MaFormaPlusCoreMVC.Models
{
    public class ModuleParcours
    {
        [Key]
        public int Id { get; set; }
        public int? ModuleId { get; set; }
        public Module? Module { get; set; }
        public int? ParcoursId { get; set; }
        public Parcours? Parcours { get; set; }


    }
}
