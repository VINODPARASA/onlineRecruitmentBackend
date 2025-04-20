namespace OnlineRecruitmentSystem.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using OnlineRecruitmentSystem.Models;

    public class RecruitmentDbContext : IdentityDbContext<ApplicationUser>
    {
        public RecruitmentDbContext(DbContextOptions<RecruitmentDbContext> options) : base(options) { }

        // Add other DbSets like JobPostings, Applications, etc. later
        public DbSet<Job> Jobs { get; set; }
        public DbSet<CandidateApplication> CandidateApplications { get; set; }
        public DbSet<ApplicationStatusHistory> ApplicationStatusHistories { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Interviewer> Interviewers { get; set; }
        public DbSet<Profile> Profile{ get; set; }







    }

}
