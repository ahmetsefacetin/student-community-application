using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;
        private readonly IClubMembershipService _membershipService;

        public ClubController(IClubService clubService, IClubMembershipService membershipService)
        {
            _clubService = clubService;
            _membershipService = membershipService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ClubResponseDto>> CreateClub([FromBody] CreateClubDto dto)
        {
            var result = await _clubService.CreateClubAsync(dto);
            return CreatedAtAction(nameof(GetClubById), new { id = result.Id }, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ClubResponseDto>>> GetAllClubs()
        {
            var clubs = await _clubService.GetAllClubsAsync();
            return Ok(clubs);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ClubResponseDto>> GetClubById(int id)
        {
            var club = await _clubService.GetClubByIdAsync(id);
            return Ok(club);
        }

        [HttpGet("{id}/members")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ClubMemberDto>>> GetClubMembers(int id)
        {
            var members = await _membershipService.GetMembersAsync(id);
            return Ok(members);
        }

        [HttpPost("{id}/join")]
        [Authorize]
        public async Task<IActionResult> JoinClub(int id)
        {
            var currentUserId = User.FindFirstValue("sub");
            if (string.IsNullOrEmpty(currentUserId))
                return Unauthorized();

            await _membershipService.JoinClubAsync(id, currentUserId);
            return NoContent();
        }

        [HttpDelete("{id}/leave")]
        [Authorize]
        public async Task<IActionResult> LeaveClub(int id)
        {
            var currentUserId = User.FindFirstValue("sub"); 
            if (string.IsNullOrEmpty(currentUserId))
                return Unauthorized();

            await _membershipService.LeaveClubAsync(id, currentUserId);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateClub(int id, [FromBody] UpdateClubDto dto)
        {
            var currentUserId = User.FindFirstValue("sub");
            if (string.IsNullOrEmpty(currentUserId))
                return Unauthorized();

            await _clubService.UpdateClubAsync(id, dto, currentUserId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            await _clubService.DeleteClubAsync(id);
            return NoContent();
        }
    }
}
