namespace jobsapi.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Location  { get; set; }

        public int? Budget { get; set; }

        public string CreatorId { get; set; }

        public Profile Creator { get; set; }
    }
}