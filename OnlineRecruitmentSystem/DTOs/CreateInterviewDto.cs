namespace OnlineRecruitmentSystem.DTOs
{
    public class CreateInterviewDto
    {
        public int CandidateApplicationId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string Mode { get; set; } // e.g., In-person, Zoom, Phone
        public string LocationOrLink { get; set; }
        public int InterviewerId { get; set; }
        public string Status { get; set; } // Scheduled, Completed, Cancelled
    }

}
