namespace MaFormaPlusCoreMVC.Models
{
    public class SessionStagiaire
    {
        public int Id { get; set; }
        public int? SessionId { get; set; }
        public string? StagiaireId { get; set; }

        public Session? Session { get; set; } 
        public Stagiaire? Stagiaire { get; set; } 
    }
}
