using Microsoft.AspNetCore.Http;
using System;
namespace OnlineRecruitmentSystem.DTOs
{
    public class ProfileDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Qualification { get; set; }
        public string Skills { get; set; }
        public string CollegeName { get; set; }
        public double CGPA { get; set; }
        public string Achievements { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IFormFile Resume { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
