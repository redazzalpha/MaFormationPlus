using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaFormaPlusCoreMVC.Models
{
    public class Conseiller : Utilisateur
    {
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}