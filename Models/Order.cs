namespace TravelAgency_Prod.Models
{
    public class Order
    {
        public int Id { get; set; }
        public bool PaymentStatus { get; set; } = false;
        public int OrderNumber { get; set; }
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
