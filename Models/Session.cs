using System.ComponentModel.DataAnnotations;

namespace MaFormaPlusCoreMVC.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public string Debut { get; set; } = string.Empty;
        public string Fin { get; set; } = string.Empty;
        public int ParcoursId{ get; set; }
        public Parcours Parcours { get; set; } = new();
        public Conseiller? Conseiller { get; set; }
        public ICollection<SessionStagiaire>? sessionStagiaires{ get; set; } = new List<SessionStagiaire>();
    }
}
