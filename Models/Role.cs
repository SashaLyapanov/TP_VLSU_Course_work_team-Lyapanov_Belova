namespace TravelAgency_Prod.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Administrator Administrator { get; set; }
        public List<Client> Clients { get; set; } = new List<Client>();
        public List<TourManager> TourManagers { get; set; } = new List<TourManager>();
    }
}