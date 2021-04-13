namespace jobsapi.Models
{
    public class Contractor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? Age { get; set; }

        public int? Salary { get; set; }

        public string CreatorId { get; set; }

        public Profile Creator { get; set; }


    }
}