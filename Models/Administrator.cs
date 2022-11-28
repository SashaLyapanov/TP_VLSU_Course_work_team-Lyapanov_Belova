using System.Data;

namespace TravelAgency_Prod.Models
{
    public class Administrator : User
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}

