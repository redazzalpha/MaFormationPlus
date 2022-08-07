using MaFormaPlusCoreMVC.Enums;
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

        public int? ParcoursId { get; set; }
        public Parcours? Parcours { get; set; }

        public Unite? Unite{ get; set; }
    }
}
            