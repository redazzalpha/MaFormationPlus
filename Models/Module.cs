using MaFormaPlusCoreMVC.Enums;
using System.ComponentModel.DataAnnotations;

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
        public ICollection<Parcours> Parcourses { get; set; } = new List<Parcours>();

    }
}
