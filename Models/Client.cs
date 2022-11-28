using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace TravelAgency_Prod.Models
{
    public class Client : User
    {
        public int Id { get; set; }
        public string Pasport { get; set; }
        public string ZagranPasport { get; set; }
        public string Phone { get; set; }
        public string Visa { get; set; }
        public Basket Basket { get; set; }
        public Favourite Favourite { get; set; }
        public int RoleId { get; set; } = 3;
        public virtual Role Role { get; set; }
        [Remote(action: "EmailValidation", controller: "Account", ErrorMessage = "Данный Email уже используется")]
        public string Email { get; set; }
    }
}
