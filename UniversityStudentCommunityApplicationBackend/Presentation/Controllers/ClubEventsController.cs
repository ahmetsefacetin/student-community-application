using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/Club/{clubId}/events")]
    public class ClubEventsController : ControllerBase
    {
        private readonly IClubEventService _clubEventService;

        public ClubEventsController(IClubEventService clubEventService)
        {
            _clubEventService = clubEventService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ClubEventResponseDto>>> GetEvents(int clubId)
        {
            var events = await _clubEventService.GetEventsAsync(clubId);
            return Ok(events);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ClubEventResponseDto>> CreateEvent(int clubId, [FromBody] CreateClubEventDto dto)
        {
            var userId = User.FindFirstValue("sub");
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var created = await _clubEventService.CreateEventAsync(clubId, dto, userId);
            return CreatedAtAction(nameof(GetEvents), new { clubId }, created);
        }

        [HttpPut("{eventId}")]
        [Authorize]
        public async Task<ActionResult<ClubEventResponseDto>> UpdateEvent(int clubId, int eventId, [FromBody] UpdateClubEventDto dto)
        {
            var userId = User.FindFirstValue("sub");
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var updated = await _clubEventService.UpdateEventAsync(clubId, eventId, dto, userId);
            return Ok(updated);
        }

        [HttpDelete("{eventId}")]
        [Authorize]
        public async Task<IActionResult> DeleteEvent(int clubId, int eventId)
        {
            var userId = User.FindFirstValue("sub");
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            await _clubEventService.DeleteEventAsync(clubId, eventId, userId);
            return NoContent();
        }
    }
}
