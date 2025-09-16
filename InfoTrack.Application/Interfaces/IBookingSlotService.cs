using InfoTrack.Application.DTOs;

namespace InfoTrack.Application.Interfaces;

public interface IBookingSlotService
{
    //Task<IEnumerable<BookingSlotDTO>> GetAllBookingSlotsAsync();
    Task<ApiResponse<CreateBookingsResponse>> CreateBookingSlotAsync(CreateBookingsRequest request);
}
