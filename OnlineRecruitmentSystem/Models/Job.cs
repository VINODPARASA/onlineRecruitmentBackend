namespace OnlineRecruitmentSystem.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Skills { get; set; }
        public string Experience { get; set; }
        public string Salary { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime PostedDate { get; set; }
       
    }
}
