namespace TravelAgency_Prod.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public List<Tour> Tours { get; set; } = new List<Tour>();
        public int TourId { get; set; }
        public virtual Tour Tour{ get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}