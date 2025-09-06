using InfoTrack.Application.DTOs;
using InfoTrack.Application.Interfaces;
using InfoTrack.Domain.Entities;
using InfoTrack.Domain.Repositories;

namespace InfoTrack.Application.Services
{
    public class BookingSlotService(IBookingSlotRepository _repository) : IBookingSlotService
    {
        public async Task<ApiResponse<CreateBookingResponse>> CreateBookingSlotAsync(CreateBookingRequest request)
        {

            if (request == null) return ApiResponse<CreateBookingResponse>.Fail("What I am supposed to do with Empty request ?");

            if (string.IsNullOrWhiteSpace(request.BuyerName))
                return ApiResponse<CreateBookingResponse>.Fail($"Buyer name cannot be null or empty.");


            if (string.IsNullOrWhiteSpace(request.BookingTime) || !DateTime.TryParse(request.BookingTime, out var bookingTime))
                return ApiResponse<CreateBookingResponse>.Fail($"Invalid booking time format - {request.BookingTime}");

            var startOfDay = bookingTime.Date.AddHours(9);
            var lastValidStart = bookingTime.Date.AddHours(16);

            if (bookingTime < startOfDay || bookingTime > lastValidStart)
                return ApiResponse<CreateBookingResponse>.Fail($"Booking time must start between 09:00 and 16:00.");

            var existingBookingSlots = await _repository.GetAllBookingSlotsAsync();
            if (existingBookingSlots.Any(existingSlots => bookingTime >= existingSlots.StartTime && bookingTime <= existingSlots.EndTime))
                return ApiResponse<CreateBookingResponse>.Fail($"Requesting time slot ({bookingTime}) is conflicting with existing bookings. Please provide some other time slot.");

            var newBookingSlotId = await _repository.AddBookingSlotAsync(
                new BookingSlot(Guid.NewGuid(), request.BuyerName, bookingTime, bookingTime.AddHours(1).AddMinutes(-1)));

            return ApiResponse<CreateBookingResponse>.Ok(new CreateBookingResponse(Convert.ToString(newBookingSlotId)));

        }
    }
}
