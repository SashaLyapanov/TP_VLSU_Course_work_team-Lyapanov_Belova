using Microsoft.AspNetCore.Mvc;

namespace TravelAgency_Prod.Models
{
    public class TourManager : User
    {
        public int Id { get; set; }
        public int RoleId { get; set; } = 2;
        public Role Role { get; set; }
        [Remote(action: "EmailValidation", controller: "AdminCRUDTourManagers", ErrorMessage = "Данный Email уже используется")]
        public string Email { get; set; }
    }
}
