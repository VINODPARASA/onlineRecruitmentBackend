using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineRecruitmentSystem.Data;
using OnlineRecruitmentSystem.DTOs;
using OnlineRecruitmentSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineRecruitmentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewersController : ControllerBase
    {
        private readonly RecruitmentDbContext _context;

        public InterviewersController(RecruitmentDbContext context)
        {
            _context = context;
        }

        // GET: api/interviewers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interviewer>>> GetInterviewers()
        {
            return await _context.Interviewers.ToListAsync();
        }

        // POST: api/interviews
        //[HttpPost]
        //public async Task<IActionResult> CreateInterview([FromBody] CreateInterviewDto interview)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    // Optional: You can validate CandidateApplicationId or InterviewerId existence here if needed

        //    _context.Interviews.Add(interview);
        //    await _context.SaveChangesAsync();

        //    return Ok(interview); // or CreatedAtAction if you want to return URI
        //}
        [HttpPost]
        public async Task<IActionResult> CreateInterview([FromBody] CreateInterviewDto interviewDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map DTO to Interview model
            var interview = new Interview
            {
                CandidateApplicationId = interviewDto.CandidateApplicationId,
                ScheduledAt = interviewDto.ScheduledAt,
                Mode = interviewDto.Mode,
                LocationOrLink = interviewDto.LocationOrLink,
                InterviewerId = interviewDto.InterviewerId,
                Status = interviewDto.Status
            };

            // Add interview to the database
            _context.Interviews.Add(interview);
            await _context.SaveChangesAsync();

            // Return the created interview with the auto-generated Id
            return Ok(interview);
        }


        [HttpGet("user/{userId}/scheduled-interviews")]
        public async Task<ActionResult<List<InterviewDetailsDto>>> GetScheduledInterviewsForUser(string userId)
        {
            // Step 1: Fetch CandidateApplications for the given userId
            var candidateApplications = await _context.CandidateApplications
                .Where(ca => ca.UserId == userId)
                .ToListAsync();

            if (candidateApplications == null || !candidateApplications.Any())
            {
                return NotFound("No applications found for this user.");
            }

            // Step 2: Get Interview records based on the CandidateApplicationId
            var interviewDetails = await _context.Interviews
                .Where(i => candidateApplications.Select(ca => ca.Id).Contains(i.CandidateApplicationId))
                .Include(i => i.Interviewer)  // Include Interviewer details
                .Select(i => new InterviewDetailsDto
                {
                    CandidateApplicationId = i.CandidateApplicationId,
                    ScheduledAt = i.ScheduledAt,
                    Mode = i.Mode,
                    LocationOrLink = i.LocationOrLink,
                    InterviewerId = i.InterviewerId,
                    InterviewerFullName = i.Interviewer.FullName,
                    InterviewerEmail = i.Interviewer.Email,
                    Status = i.Status,
                    
                })
                .ToListAsync();

            if (interviewDetails == null || !interviewDetails.Any())
            {
                return NotFound("No scheduled interviews found for this user.");
            }

            return Ok(interviewDetails);
        }


    }
}
