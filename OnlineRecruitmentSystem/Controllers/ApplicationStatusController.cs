using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineRecruitmentSystem.Data;
using OnlineRecruitmentSystem.Models;
using OnlineRecruitmentSystem.DTOs;

namespace OnlineRecruitmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationStatusController : ControllerBase
    {
        private readonly RecruitmentDbContext _context;

        public ApplicationStatusController(RecruitmentDbContext context)
        {
            _context = context;
        }

        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusDto dto)
        {
            var application = await _context.CandidateApplications
                .FirstOrDefaultAsync(a => a.Id == dto.CandidateApplicationId);

            if (application == null)
                return NotFound("Application not found");

            application.CurrentStatus = dto.Status;

            var statusHistory = new ApplicationStatusHistory
            {
                CandidateApplicationId = dto.CandidateApplicationId,
                Status = dto.Status,
                ChangedBy = dto.ChangedBy,
                ChangedAt = DateTime.UtcNow,
                Notes = dto.Notes
            };

            _context.ApplicationStatusHistories.Add(statusHistory);
            await _context.SaveChangesAsync();

            return Ok("Status updated");
        }
    }
}
