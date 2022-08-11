using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MaFormaPlusCoreMVC.Models
{
    public class Stagiaire : IdentityUser
    {
        //[Key]
        //public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string DateDeNaissance { get; set; } = string.Empty;
        public string Cv { get; set; } = string.Empty;

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
