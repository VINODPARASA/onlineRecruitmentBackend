namespace OnlineRecruitmentSystem.Models
{
    public class Interviewer
    {
        public int Id { get; set; } // InterviewerId
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }

        // Navigation Property
        public List<Interview> Interviews { get; set; }
    }

}
