using InfoTrack.Domain.Entities;
using InfoTrack.Domain.Repositories;
using InfoTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack.Infrastructure.Repositories;

public class BookingSlotRepository(InfoTrackDbContext _dbContext) : IBookingSlotRepository
{

    public async Task AddBookingSlotsAsync(IEnumerable<BookingSlot> bookingSlots)
    {
        _dbContext.BookingSlots.AddRange(bookingSlots);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<BookingSlot>> GetAllBookingSlotsAsync()
    {
        return await _dbContext.BookingSlots.AsNoTracking().ToListAsync();
    }
}
