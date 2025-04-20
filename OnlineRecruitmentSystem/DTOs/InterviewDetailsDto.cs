namespace OnlineRecruitmentSystem.DTOs
{
    public class InterviewDetailsDto
    {
        public int CandidateApplicationId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string Mode { get; set; }
        public string LocationOrLink { get; set; }
        public int InterviewerId { get; set; }
        public string InterviewerFullName { get; set; }
        public string InterviewerEmail { get; set; }
        public string Status { get; set; }
        
    }
}
