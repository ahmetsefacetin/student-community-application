using Entities.DTOs;
using Entities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubRolesController : ControllerBase
    {

        private readonly IClubRolesService _clubRolesService;

        public ClubRolesController(IClubRolesService clubRolesService)
        {
            _clubRolesService = clubRolesService;
        }

        //  KULLANICININ KULÜP ROLÜNü GETIR
        [HttpGet("{id}/membership")]
        [Authorize]
        public async Task<ActionResult<UserClubRoleDto>> GetUserClubRole(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _clubRolesService.GetUserClubRoleAsync(id, userId);
            return Ok(result);
        }

        [HttpPut("{clubId}/make-officer/{userId}")]
        [Authorize]
        public async Task<IActionResult> MakeMemberOfficer(int clubId, int userId)
        {
            var userRole = await GetUserClubRole(clubId);
            if (userRole.Value.ClubRole != "Manager")
            {
                return Forbid();
            }
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            try
            {
                await _clubRolesService.MakeMemberOfficerAsync(clubId, userId, currentUserId);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
            return NoContent();
        }

        [HttpPut("{clubId}/demote-officer/{userId}")]
        [Authorize]
        public async Task<IActionResult> DemoteOfficer(int clubId, int UserId)
        {
            var userRole = await GetUserClubRole(clubId);
            if (userRole.Value.ClubRole != "Manager")
            {
                return Forbid();
            }
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            try
            {
                await _clubRolesService.DemoteOfficerAsync(clubId, UserId, currentUserId);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
            return NoContent();
        }
    }
}
