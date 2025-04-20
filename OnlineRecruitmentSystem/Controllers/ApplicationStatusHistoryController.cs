using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineRecruitmentSystem.Data;
using OnlineRecruitmentSystem.Models;
using OnlineRecruitmentSystem.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace OnlineRecruitmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationStatusHistoryController : ControllerBase
    {
        private readonly RecruitmentDbContext _context;

        public ApplicationStatusHistoryController(RecruitmentDbContext context)
        {
            _context = context;
        }

        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateApplicationStatus([FromBody] ApplicationStatusUpdateDto dto)
        {
            var application = await _context.CandidateApplications
                .FirstOrDefaultAsync(a => a.Id == dto.CandidateApplicationId);

            if (application == null)
            {
                return NotFound("Application not found.");
            }

            // Add to history
            var statusHistory = new ApplicationStatusHistory
            {
                CandidateApplicationId = dto.CandidateApplicationId,
                Status = dto.Status,
                ChangedBy = dto.ChangedBy,
                ChangedAt = DateTime.UtcNow,
                Notes = dto.Notes
            };

            _context.ApplicationStatusHistories.Add(statusHistory);

            // Update current status
            application.CurrentStatus = dto.Status;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Application status updated successfully." });
        }

        [HttpPost("user-applications")]
        
        public async Task<IActionResult> GetUserApplications([FromBody] string userId)
        {
            var applications = await _context.CandidateApplications
                .Where(a => a.UserId == userId)
                .Include(a => a.Job)
                .ToListAsync();

            var result = applications.Select(a => new
            {
                ApplicationId = a.Id,
                JobId = a.JobId,
                JobTitle = a.Job.JobTitle,
                Salary = a.Job.Salary,
                Location = a.Job.Location,
                CurrentStatus = a.CurrentStatus
            });

            return Ok(result);
        }

        //[HttpPost("status-history")]
        //public async Task<IActionResult> GetStatusHistory([FromBody] int applicationId)
        //{
        //    // No user check here since authorization is not required
        //    var application = await _context.CandidateApplications
        //        .FirstOrDefaultAsync(a => a.Id == applicationId);

        //    if (application == null)
        //        return NotFound("Application not found.");

        //    var history = await _context.ApplicationStatusHistories
        //        .Where(h => h.CandidateApplicationId == applicationId)
        //        .OrderBy(h => h.ChangedAt)
        //        .ToListAsync();

        //    return Ok(history);
        //}
        [HttpPost("status-history")]
        public async Task<IActionResult> GetStatusHistory([FromBody] int applicationId)
        {
            var application = await _context.CandidateApplications
                .FirstOrDefaultAsync(a => a.Id == applicationId);

            if (application == null)
                return NotFound("Application not found.");

            var history = await _context.ApplicationStatusHistories
                .Where(h => h.CandidateApplicationId == applicationId)
                .OrderBy(h => h.ChangedAt)
                .Select(h => new ApplicationStatusDto
                {
                    Status = h.Status,
                    ChangedBy = h.ChangedBy,
                    ChangedAt = h.ChangedAt,
                    Notes = h.Notes
                })
                .ToListAsync();

            if (!history.Any())
                return Ok("No status updates yet.");

            return Ok(history);
        }



    }
}
