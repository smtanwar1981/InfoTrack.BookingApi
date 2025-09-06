namespace InfoTrack.Domain.Entities;

public record BookingSlot (Guid BookingSlotId, string BuyerName, DateTime StartTime, DateTime EndTime);
