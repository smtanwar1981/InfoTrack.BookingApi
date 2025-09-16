namespace InfoTrack.Application.DTOs;


public record CreateBookingResponse(
    string BuyerName,
    string BookingTime,
    bool Success,
    string Error,
    string? BookingId
);
public record CreateBookingsResponse(List<CreateBookingResponse> Results);
