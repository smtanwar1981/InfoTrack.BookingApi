using InfoTrack.Application.DTOs;

namespace InfoTrack.Application.Interfaces;

public interface IBookingSlotService
{
    //Task<IEnumerable<BookingSlotDTO>> GetAllBookingSlotsAsync();
    Task<ApiResponse<CreateBookingResponse>> CreateBookingSlotAsync(CreateBookingRequest request);
}
