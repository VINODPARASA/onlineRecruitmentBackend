using Microsoft.AspNetCore.Mvc;
using OnlineRecruitmentSystem.Data;
using OnlineRecruitmentSystem.Models;
using OnlineRecruitmentSystem.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace OnlineRecruitmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostController : ControllerBase
    {
        private readonly RecruitmentDbContext _context;

        public JobPostController(RecruitmentDbContext context)
        {
            _context = context;
        }

        // Admin Post Job
        [HttpPost("post-job")]
       
        public async Task<IActionResult> PostJob([FromBody] JobDto jobDto)
        {
            if (jobDto == null)
                return BadRequest("Invalid job data");

            var job = new Job
            {
                JobTitle = jobDto.JobTitle,
                Description = jobDto.Description,
                Location = jobDto.Location,               // Adding location from the DTO
                Skills = jobDto.Skills,                   // Adding skills from the DTO
                Experience = jobDto.Experience,           // Adding experience from the DTO
                Salary = jobDto.Salary,                   // Adding salary from the DTO
                Deadline = jobDto.Deadline,               // Adding deadline from the DTO
                PostedDate = DateTime.Now             // Current time when job is posted
                           // Logged-in admin's name (or ID)
            };

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return Ok("Job posted successfully!");
        }

        // GET: api/JobPost/all-jobs
        //[HttpGet("all-jobs")]
        //public async Task<IActionResult> GetAllJobs()
        //{
        //    var jobs = await _context.Jobs
        //        .Select(job => new JobDto
        //        {
        //            JobTitle = job.JobTitle,
        //            Description = job.Description,
        //            Location = job.Location,
        //            Skills = job.Skills,
        //            Experience = job.Experience,
        //            Salary = job.Salary,
        //            Deadline = job.Deadline,
        //            PostedDate = job.PostedDate
        //        })
        //        .ToListAsync();

        //    return Ok(jobs);
        //}

        // GET: api/JobPost/all-jobs
        [HttpGet("all-jobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            var jobs = await _context.Jobs.ToListAsync();
            if (jobs == null || jobs.Count == 0)
            {
                return NotFound("No jobs available.");
            }
            return Ok(jobs); // Return the list of jobs as JSON
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return NotFound("Job not found.");

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return Ok("Job deleted successfully.");
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] Job updatedJob)
        {
            var existingJob = await _context.Jobs.FindAsync(id);
            if (existingJob == null) return NotFound("Job not found.");

            existingJob.JobTitle = updatedJob.JobTitle;
            existingJob.Description = updatedJob.Description;
            existingJob.Location = updatedJob.Location;
            existingJob.Skills = updatedJob.Skills;
            existingJob.Experience = updatedJob.Experience;
            existingJob.Salary = updatedJob.Salary;
            existingJob.Deadline = updatedJob.Deadline;

            await _context.SaveChangesAsync();
            return Ok("Job updated successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }





    }
}
