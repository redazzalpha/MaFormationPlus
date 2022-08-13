using System.ComponentModel.DataAnnotations;

namespace MaFormaPlusCoreMVC.Models
{
    public class Parcours
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Resume { get; set; } = string.Empty;
        public string? Logo { get; set; } = string .Empty;
        public ICollection<ModuleParcours> ModuleParcours { get; set; } = new List<ModuleParcours>();
    }
}
