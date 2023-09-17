using ClinicTool.DataAccess.Entities;
using ClinicTool.DataAccess.Repositories.Interfaces;
using ClinicTool.Domain.Dto;
using ClinicTool.Domain.Services.Interfaces;

namespace ClinicTool.Domain.Services.Implementation
{
    public class BookingSlotService : IBookingSlotService
    {
        private readonly IBookingSlotRepository bookingSlotRepository;

        public BookingSlotService(IBookingSlotRepository bookingSlotRepository)
        {
            this.bookingSlotRepository = bookingSlotRepository;
        }

        public List<BookingSlotDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public BookingSlotDto Get(int id)
        {
            var doctorEntityMap = new EntityMap<BookingSlot>(() => bookingSlotRepository.Get(id));
            var bookingSlot = doctorEntityMap.Get(id);

            return new BookingSlotDto
            {
                Id = bookingSlot.Id,
                EndDateTime = bookingSlot.StarDate,
                StarDateTime = bookingSlot.EndDate,
                BookingSlotStatus = bookingSlot.Status
            };
        }

        public void Add(BookingSlotDto doctor)
        {
            throw new NotImplementedException();
        }

        public void Update(BookingSlotDto doctor)
        {
            throw new NotImplementedException();
        }

        public bool Remove(BookingSlotDto doctor)
        {
            throw new NotImplementedException();
        }
    }
}
