using MaFormaPlusCoreMVC.Enums;
using System.ComponentModel.DataAnnotations;

namespace MaFormaPlusCoreMVC.Models
{
    public class Unite
    {
        [Key]
        public int Id { get; set; }
        public UniteEnum Nom { get; set; }
        public ICollection<Module> Modules{ get; set; } = new List<Module>();
    }
}
