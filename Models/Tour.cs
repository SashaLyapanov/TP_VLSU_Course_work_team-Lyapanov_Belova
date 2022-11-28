namespace TravelAgency_Prod.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int PersonCount { get; set; }

        //Будем указывать цену в долларах, поэтому short 
        public ushort Cost { get; set; }
        public DateTime DepartmentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Duration { get; set; } = 0;
        public string Hotel { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public bool Available { get; set; }
        public string img { get; set; }
        public bool isFavourite { get; set; }
    }
}