using InfoTrack.Application.DTOs;
using InfoTrack.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingSlotController(IBookingSlotService _bookingSlotService) : ControllerBase
    {
        [HttpPost("createBooking")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBookingAsync(CreateBookingRequest request)
        {
            var result = await _bookingSlotService.CreateBookingSlotAsync(request);

            if (!result.Success) return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
