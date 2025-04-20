namespace OnlineRecruitmentSystem.DTOs
{
    public class ApplicationStatusUpdateDto
    {
        public int CandidateApplicationId { get; set; }
        public string Status { get; set; } // e.g. "Shortlisted", "Interview Scheduled"
        public string ChangedBy { get; set; } // Admin email or username
        public string Notes { get; set; }
    }

}
