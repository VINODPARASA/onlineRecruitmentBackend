namespace OnlineRecruitmentSystem.Models
{
    public class ApplicationStatusHistory
    {
        public int Id { get; set; }
        public int CandidateApplicationId { get; set; }
        public string Status { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
        public string Notes { get; set; }

        public CandidateApplication CandidateApplication { get; set; }
    }
}

