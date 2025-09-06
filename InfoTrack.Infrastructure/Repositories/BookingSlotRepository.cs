using InfoTrack.Domain.Entities;
using InfoTrack.Domain.Repositories;
using InfoTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack.Infrastructure.Repositories;

public class BookingSlotRepository(InfoTrackDbContext _dbContext) : IBookingSlotRepository
{
   
    public async Task<Guid> AddBookingSlotAsync(BookingSlot bookingSlot)
    {
        var newSlot = _dbContext.BookingSlots.Add(bookingSlot);
        await _dbContext.SaveChangesAsync();
        return newSlot.Entity.BookingSlotId;
    }

    public async Task<IEnumerable<BookingSlot>> GetAllBookingSlotsAsync()
    {
        return await _dbContext.BookingSlots.AsNoTracking().ToListAsync();
    }
}
