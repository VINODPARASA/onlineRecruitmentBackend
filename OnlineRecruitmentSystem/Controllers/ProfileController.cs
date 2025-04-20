using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OnlineRecruitmentSystem.Data;
using OnlineRecruitmentSystem.DTOs;
using OnlineRecruitmentSystem.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OnlineRecruitmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly RecruitmentDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProfileController(RecruitmentDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/Profile/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfile(string userId)
        {
            var profile = await _context.Profile.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
                return NotFound("Profile not found.");

            return Ok(profile);
        }

        // POST: api/Profile
        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromForm] ProfileDto dto)
        {
            var existing = await _context.Profile.FirstOrDefaultAsync(p => p.UserId == dto.UserId);
            if (existing != null)
                return BadRequest("Profile already exists.");

            var resumePath = await SaveFile(dto.Resume, "resume");
            var profilePicPath = await SaveFile(dto.ProfilePicture, "profile");

            var profile = new Profile
            {
                UserId = dto.UserId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Qualification = dto.Qualification,
                Skills = dto.Skills,
                CollegeName = dto.CollegeName,
                CGPA = dto.CGPA,
                Achievements = dto.Achievements,
                DateOfBirth = dto.DateOfBirth,
                ResumeFilePath = resumePath,
                ProfilePicturePath = profilePicPath
            };

            _context.Profile.Add(profile);
            await _context.SaveChangesAsync();

            return Ok(profile);
        }

        // PUT: api/Profile/{userId}
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateProfile(string userId, [FromForm] ProfileDto dto)
        {
            var profile = await _context.Profile.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
                return NotFound("Profile not found.");

            profile.FirstName = dto.FirstName;
            profile.LastName = dto.LastName;
            profile.Qualification = dto.Qualification;
            profile.Skills = dto.Skills;
            profile.CollegeName = dto.CollegeName;
            profile.CGPA = dto.CGPA;
            profile.Achievements = dto.Achievements;
            profile.DateOfBirth = dto.DateOfBirth;

            if (dto.Resume != null)
                profile.ResumeFilePath = await SaveFile(dto.Resume, "resume");

            if (dto.ProfilePicture != null)
                profile.ProfilePicturePath = await SaveFile(dto.ProfilePicture, "profile");

            await _context.SaveChangesAsync();

            return Ok(profile);
        }

        private async Task<string> SaveFile(IFormFile file, string prefix)
        {
            if (file == null || file.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(_environment.WebRootPath ?? "wwwroot", "profiles");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{prefix}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return relative path (can be used in frontend for image src etc.)
            return Path.Combine("profiles", fileName).Replace("\\", "/");
        }
    }
}
