namespace MaFormaPlusCoreMVC.Models
{
    public class SessionParcours
    {
        public Session Session { get; set; } = new();
        public Parcours Parcours { get; set; } = new();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
        public ICollection<Parcours> parcourses{ get; set; } = new List<Parcours>();
    }
}
