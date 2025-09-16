using InfoTrack.Application.DTOs;
using InfoTrack.Application.Interfaces;
using InfoTrack.Domain.Entities;
using InfoTrack.Domain.Repositories;

namespace InfoTrack.Application.Services
{
    public class BookingSlotService(IBookingSlotRepository _repository) : IBookingSlotService
    {
        public async Task<ApiResponse<CreateBookingsResponse>> CreateBookingSlotAsync(CreateBookingsRequest request)
        {

            if (request == null || request.BookingRequests == null || request.BookingRequests.Count == 0) return ApiResponse<CreateBookingsResponse>.Fail("What I am supposed to do with Empty booking requests ?");

            if (request.BookingRequests.Count > 4) return ApiResponse<CreateBookingsResponse>.Fail("I am restricted to serve only 4 booking request simulataneously.");

            var existingBookingSlots = (await _repository.GetAllBookingSlotsAsync()).ToList();
            var results = new List<CreateBookingResponse>();
            var newValidBookings = new List<BookingSlot>();

            foreach (var bookingRequest in request.BookingRequests)
            {
                if (string.IsNullOrWhiteSpace(bookingRequest.BuyerName))
                {
                    results.Add(new CreateBookingResponse(bookingRequest.BuyerName, bookingRequest.BookingTime, false, "Buyer name cannot be empty.", null));
                    continue;
                }

                if (string.IsNullOrWhiteSpace(bookingRequest.BookingTime) || !DateTime.TryParse(bookingRequest.BookingTime, out var bookingTime))
                {
                    results.Add(new CreateBookingResponse(bookingRequest.BuyerName, bookingRequest.BookingTime, false, $"Invalid booking time format - {bookingRequest.BookingTime}", null));
                    continue;
                }

                var startOfDay = bookingTime.Date.AddHours(9);
                var lastValidStart = bookingTime.Date.AddHours(16);

                if (bookingTime < startOfDay || bookingTime > lastValidStart)
                {
                    results.Add(new CreateBookingResponse(bookingRequest.BuyerName, bookingRequest.BookingTime, false, "Booking time must start between 09:00 and 16:00.", null));
                    continue;
                }

                if (existingBookingSlots.Any(s => bookingTime >= s.StartTime && bookingTime <= s.EndTime))
                {
                    results.Add(new CreateBookingResponse(bookingRequest.BuyerName, bookingRequest.BookingTime, false, $"Booking request time {bookingTime} is conflicting with existing bookings. Please provide some other time slot.", null));
                    continue;
                }

                var newBookingSlot = new BookingSlot(Guid.NewGuid(), bookingRequest.BuyerName, bookingTime, bookingTime.AddHours(1).AddMinutes(-1));
                newValidBookings.Add(newBookingSlot);

                results.Add(new CreateBookingResponse(bookingRequest.BuyerName, bookingRequest.BookingTime, true, string.Empty, newBookingSlot.BookingSlotId.ToString()));
            }

            // saving only valid booking slots in one db call
            if (newValidBookings.Count > 0)
                await _repository.AddBookingSlotsAsync(newValidBookings);

            return ApiResponse<CreateBookingsResponse>.Ok(new CreateBookingsResponse(results));
        }
    }
}
