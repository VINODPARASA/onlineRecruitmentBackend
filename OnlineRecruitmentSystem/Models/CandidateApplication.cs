namespace OnlineRecruitmentSystem.Models
{
    public class CandidateApplication
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string UserId { get; set; }
        public string ResumeFileName { get; set; }
        public DateTime AppliedDate { get; set; }

        public Job Job { get; set; }
       public string CurrentStatus { get; set; } // e.g. "Submitted", "Shortlisted", etc.
        public List<ApplicationStatusHistory>? StatusHistory { get; set; } // Navigation property

    }
}
