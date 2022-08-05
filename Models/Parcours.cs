using System.ComponentModel.DataAnnotations;

namespace MaFormaPlusCoreMVC.Models
{
    public class Parcours
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Resume { get; set; } = string.Empty;
        public string Logo { get; set; } = string .Empty;

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
        public ICollection<Module> Modules{ get; set; } = new List<Module>();
    }
}
