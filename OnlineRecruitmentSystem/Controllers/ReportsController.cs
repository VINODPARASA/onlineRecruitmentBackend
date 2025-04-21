using Microsoft.AspNetCore.Mvc;
using OnlineRecruitmentSystem.DTOs;
using OnlineRecruitmentSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace OnlineRecruitmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly RecruitmentDbContext _context;

        public ReportsController(RecruitmentDbContext context)
        {
            _context = context;
        }

        [HttpGet("application-status-summary")]
        public async Task<IActionResult> GetApplicationStatusSummary()
        {
            var summary = await _context.CandidateApplications
                .Include(a => a.Job)
                .GroupBy(a => new { a.JobId, a.Job.JobTitle })
                .Select(g => new JobApplicationStatusSummaryDto
                {
                    JobId = g.Key.JobId,
                    JobTitle = g.Key.JobTitle,
                    TotalApplications = g.Count(),
                    ShortlistedCount = g.Count(a => a.CurrentStatus == "shortlisted"),
                    RejectedCount = g.Count(a => a.CurrentStatus == "rejected"),
                    ScheduledCount = g.Count(a => a.CurrentStatus == "scheduled"),
                    ReceivedCount = g.Count(a => a.CurrentStatus == "received")
                })
                .ToListAsync();

            return Ok(summary);
        }
    }

}
