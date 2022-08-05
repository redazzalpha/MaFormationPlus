using System.ComponentModel.DataAnnotations;

namespace MaFormaPlusCoreMVC.Models
{
    public class Conseiller
    {
        [Key]
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}