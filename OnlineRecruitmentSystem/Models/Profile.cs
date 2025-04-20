namespace OnlineRecruitmentSystem.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Qualification { get; set; }
        public string Skills { get; set; }
        public string CollegeName { get; set; }
        public double CGPA { get; set; }
        public string Achievements { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ResumeFilePath { get; set; }
        public string ProfilePicturePath { get; set; }
    }
}
