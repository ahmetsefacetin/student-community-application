using Entities.DTOs;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new NotFoundException("User ID not found in token");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });


            var result = new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                CreatedAt = user.CreatedAt
            };
            return Ok(result);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users
                .Include(u => u.RefreshTokens)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            var result = new List<UserResponseDto>();
            foreach (var user in users)
            {

                result.Add(new UserResponseDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FullName = user.FullName,
                    CreatedAt = user.CreatedAt,
                });
            }

            return Ok(result);
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchUsers([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
                return BadRequest(new { message = "Search query must be at least 2 characters" });

            var users = await _userManager.Users
                .Where(u =>
                    u.UserName!.Contains(query) ||
                    u.Email!.Contains(query) ||
                    (u.FirstName != null && u.FirstName.Contains(query)) ||
                    (u.LastName != null && u.LastName.Contains(query))
                )
                .Take(20)
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    FullName = u.FullName,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users
                .OrderBy(u => u.UserName)
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    FullName = u.FullName,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userManager.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound(new { message = "User not found" });

            var result = new UserResponseDto
            {   
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FullName,
                CreatedAt = user.CreatedAt,
            };

            return Ok(result);
        }
    }
}