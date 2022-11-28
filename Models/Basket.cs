namespace TravelAgency_Prod.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}