namespace OnlineRecruitmentSystem.Models
{
    public class Interview
    {
        public int Id { get; set; }
        public int CandidateApplicationId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string Mode { get; set; } // e.g., In-person, Zoom, Phone
        public string LocationOrLink { get; set; }

        // Replace plain Interviewer field with a relationship
        public int InterviewerId { get; set; }
        public Interviewer Interviewer { get; set; }

        public string Status { get; set; } // Scheduled, Completed, Cancelled
    }

}
