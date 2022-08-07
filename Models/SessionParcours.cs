namespace MaFormaPlusCoreMVC.Models
{
    public class SessionParcours
    {
        public Session Session { get; set; }
        public Parcours Parcours { get; set; }

        public SessionParcours(Session session, Parcours parcours)
        {
            Session = session;
            Parcours = parcours;
        }
    }
}
