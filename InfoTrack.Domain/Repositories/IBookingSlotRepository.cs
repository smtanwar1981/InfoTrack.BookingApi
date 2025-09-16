using InfoTrack.Domain.Entities;

namespace InfoTrack.Domain.Repositories
{
    public interface IBookingSlotRepository
    {
        /// <summary>
        /// Asynchronously retrieves all available booking slots.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of  <see
        /// cref="BookingSlot"/> objects representing the available booking slots. The list will be empty  if no booking
        /// slots are available.</returns>
        Task<List<BookingSlot>> GetAllBookingSlotsAsync();

        /// <summary>
        /// Asynchronously adds a new booking slot to the system.
        /// </summary>
        /// <remarks>This method ensures that the booking slot is validated and stored persistently. 
        /// Callers should handle potential conflicts or validation errors.</remarks>
        /// <param name="bookingSlot">The booking slot to add. Must not be <see langword="null"/>.</param>
        /// <returns>A <see cref="Guid"/> representing the unique identifier of the newly added booking slot.</returns>
        Task AddBookingSlotsAsync(IEnumerable<BookingSlot> bookingSlots);

        // Other CRUD operations can be defined here as needed
    }
}
