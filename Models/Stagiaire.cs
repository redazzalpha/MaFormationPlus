namespace MaFormaPlusCoreMVC.Models
{
    public class Stagiaire : Utilisateur
    {
        public string? Nom { get; set; } = string.Empty;
        public string? Prenom { get; set; } = string.Empty;
        public string? Adresse { get; set; } = string.Empty;
        public string? DateDeNaissance { get; set; } = string.Empty;
        public string? Cv { get; set; } = string.Empty;

        public ICollection<SessionStagiaire> SessionStagiaires { get; set; } = new List<SessionStagiaire>();
    }
}
