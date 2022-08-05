using System.ComponentModel.DataAnnotations;

namespace MaFormaPlusCoreMVC.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;

        public Conseiller? Conseiller { get; set; }
        public Parcours? Parcours { get; set; }
        public ICollection<Stagiaire> Stagiaires { get; set; } = new List<Stagiaire>();
    }
}
