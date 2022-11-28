namespace TravelAgency_Prod.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Tour> Tours { get; set; }
    }
}
