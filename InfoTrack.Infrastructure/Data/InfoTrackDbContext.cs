using InfoTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoTrack.Infrastructure.Data;

public class InfoTrackDbContext : DbContext
{
    public InfoTrackDbContext(DbContextOptions<InfoTrackDbContext> options) : base(options)
    {
    }

    public DbSet<BookingSlot> BookingSlots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<BookingSlot>(entity =>
        {
            entity.HasKey(e => e.BookingSlotId);
            entity.Property(e => e.BuyerName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();
        });
    }   
}
