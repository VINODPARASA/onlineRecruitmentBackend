using Microsoft.AspNetCore.Mvc;
using OnlineRecruitmentSystem.Data;
using OnlineRecruitmentSystem.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace OnlineRecruitmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly RecruitmentDbContext _context;

        public ApplicationController(RecruitmentDbContext context)
        {
            _context = context;
        }

        [HttpPost("apply/{jobId}/{userId}")]
        public async Task<IActionResult> ApplyForJob(int jobId, string userId, IFormFile resumeFile)
        {
            if (resumeFile == null || resumeFile.Length == 0)
            {
                return BadRequest("Please upload a resume.");
            }

            // Ensure "wwwroot/resumes" directory exists
            var resumeFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "resumes");
            if (!Directory.Exists(resumeFolder))
            {
                Directory.CreateDirectory(resumeFolder);
            }

            // Save the resume file
            var filePath = Path.Combine(resumeFolder, resumeFile.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await resumeFile.CopyToAsync(fileStream);
            }

            // Create a new application record
            var application = new CandidateApplication
            {
                JobId = jobId,
                UserId = userId,
                CurrentStatus="applied",
                ResumeFileName = resumeFile.FileName,
                AppliedDate = DateTime.Now
            };

            _context.CandidateApplications.Add(application);
            await _context.SaveChangesAsync();

            return Ok("Application submitted successfully.");
        }

    }
}
