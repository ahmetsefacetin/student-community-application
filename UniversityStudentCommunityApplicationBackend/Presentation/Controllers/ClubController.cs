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

        //  SADECE ADMIN KULÜP OLUÞTURABÝLÝR
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ClubResponseDto>> CreateClub([FromBody] CreateClubDto dto)
        {
            var result = await _clubService.CreateClubAsync(dto);
            return CreatedAtAction(nameof(GetClubById), new { id = result.Id }, result);
        }

        //  TÜM KULLANICILAR KULÜPLERÝ GÖREBÝLÝR (veya [Authorize] ile sadece login olanlar)
        [HttpGet]
        [AllowAnonymous] // veya [Authorize] - ihtiyaca göre
        public async Task<ActionResult<IEnumerable<ClubResponseDto>>> GetAllClubs()
        {
            var clubs = await _clubService.GetAllClubsAsync();
            return Ok(clubs);
        }

        //  KULÜP DETAYI
        [HttpGet("{id}")]
        [AllowAnonymous] // veya [Authorize]
        public async Task<ActionResult<ClubResponseDto>> GetClubById(int id)
        {
            var club = await _clubService.GetClubByIdAsync(id);
            return Ok(club);
        }


        [HttpPut("{id}")]
        [Authorize] 
        public async Task<IActionResult> UpdateClub(int id, [FromBody] UpdateClubDto dto)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId))
                return Unauthorized();

            await _clubService.UpdateClubAsync(id, dto, currentUserId);
            return NoContent();
        }

        //  KULLANICININ KULÜP ROLÜNü GETIR
        [HttpGet("{id}/membership")]
        [Authorize]
        public async Task<ActionResult<UserClubRoleDto>> GetUserClubRole(int id)
        {
            var userId = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _membershipService.GetUserClubRoleAsync(id, userId);
            return Ok(result);
        }

        //  SADECE ADMIN KULÜP SÝLEBÝLÝR
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            await _clubService.DeleteClubAsync(id);
            return NoContent();
        }
    }
}