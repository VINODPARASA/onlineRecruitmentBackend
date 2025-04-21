namespace OnlineRecruitmentSystem.DTOs
{
    public class JobApplicationStatusSummaryDto
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public int TotalApplications { get; set; }
        public int ShortlistedCount { get; set; }
        public int RejectedCount { get; set; }
        public int ScheduledCount { get; set; }
        public int ReceivedCount { get; set; }
    }

}
