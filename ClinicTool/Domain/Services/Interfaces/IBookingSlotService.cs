using ClinicTool.Domain.Dto;

namespace ClinicTool.Domain.Services.Interfaces
{
    public interface IBookingSlotService
    {
        List<BookingSlotDto> GetAll();

        BookingSlotDto Get(int id);

        void Add(BookingSlotDto doctor);

        void Update(BookingSlotDto doctor);

        bool Remove(BookingSlotDto doctor);
    }
}


