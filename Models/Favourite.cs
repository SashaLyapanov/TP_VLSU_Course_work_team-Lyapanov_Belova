namespace TravelAgency_Prod.Models
{
    public class Favourite
    {
        public int Id { get; set; }
        public List<Tour> Tours { get; set; } = new List<Tour>();
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}

