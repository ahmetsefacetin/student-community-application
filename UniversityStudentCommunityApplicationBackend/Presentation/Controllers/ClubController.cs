using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
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

        //  SADECE ADMIN KULÜP GÜNCELLEYEBÝLÝR (veya Manager'a da izin verilebilir)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // veya "Admin"
        public async Task<IActionResult> UpdateClub(int id, [FromBody] UpdateClubDto dto)
        {
            await _clubService.UpdateClubAsync(id, dto);
            return NoContent();
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