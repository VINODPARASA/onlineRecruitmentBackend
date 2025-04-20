using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineRecruitmentSystem.Models;
using OnlineRecruitmentSystem.Data; // Use this namespace for the RecruitmentDbContext
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineRecruitmentSystem.DTOs;

namespace OnlineRecruitmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateApplicationsController : ControllerBase
    {
        private readonly RecruitmentDbContext _context;

        public CandidateApplicationsController(RecruitmentDbContext context)
        {
            _context = context;
        }

        // GET: api/CandidateApplications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateApplication>>> GetAllJobApplications()
        {
            var applications = await _context.CandidateApplications
                .Include(c => c.Job) // Include Job details
                .Include(c => c.StatusHistory) // Include application status history
                .Select(c => new
                {
                    c.Id,
                    c.JobId,
                    c.UserId,
                   // CandidateName = c.Job.JobTitle, // Assuming JobTitle in the Job table is the candidate's role/job title
                    c.ResumeFileName,
                    c.CurrentStatus,
                    StatusHistory = c.StatusHistory.Select(sh => new {
                        sh.Status,
                        sh.ChangedBy,
                        sh.ChangedAt,
                        sh.Notes
                    }).ToList()
                })
                .ToListAsync();

            if (applications == null || !applications.Any())
            {
                return NotFound("No applications found.");
            }

            return Ok(applications);
        }

        [HttpPost("check-application")]
        public async Task<IActionResult> CheckApplicationExists([FromBody] CheckApplicationDto dto)
        {
            var exists = await _context.CandidateApplications
                .AnyAsync(a => a.UserId == dto.UserId && a.JobId == dto.JobId);

            if (exists)
            {
                return Ok("found");
            }
            else
            {
                return Ok("not found");
            }
        }


        //// PUT: api/CandidateApplications/{id}/UpdateStatus
        //[HttpPut("{id}/UpdateStatus")]
        //public async Task<IActionResult> UpdateApplicationStatus(int id, [FromBody] UpdateStatusRequest statusRequest)
        //{
        //    var application = await _context.CandidateApplications
        //        .Include(c => c.StatusHistory) // Ensure StatusHistory is included for updating
        //        .FirstOrDefaultAsync(c => c.Id == id);

        //    if (application == null)
        //    {
        //        return NotFound($"No application found with ID {id}.");
        //    }

        //    // Create a new status history entry
        //    var statusHistory = new ApplicationStatusHistory
        //    {
        //        CandidateApplicationId = application.Id,
        //        Status = statusRequest.Status,
        //        ChangedBy = statusRequest.ChangedBy,
        //        ChangedAt = DateTime.Now,
        //        Notes = statusRequest.Notes
        //    };

        //    // Add the status history entry to the database
        //    _context.ApplicationStatusHistories.Add(statusHistory);

        //    // Update the current status of the application
        //    application.CurrentStatus = statusRequest.Status;

        //    // Save changes to the database
        //    await _context.SaveChangesAsync();

        //    return Ok(application);
        //}
    }

    // DTO to capture status update request

}
