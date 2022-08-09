using MaFormaPlusCoreMVC.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaFormaPlusCoreMVC.Models
{
    public class Module
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Resume { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;

        public Unite? Unite{ get; set; }
        public ICollection<Parcours>? Parcours { get; set; }
        public ICollection<ModuleParcours> ModuleParcours { get; set; } = new List<ModuleParcours>();
    }
}
            