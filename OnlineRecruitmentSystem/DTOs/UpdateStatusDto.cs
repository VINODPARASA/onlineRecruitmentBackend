namespace OnlineRecruitmentSystem.DTOs
{
    public class UpdateStatusDto
    {
        public int CandidateApplicationId { get; set; }
        public string Status { get; set; }
        public string ChangedBy { get; set; }
        public string Notes { get; set; }
    }
}
