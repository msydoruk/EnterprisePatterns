using ClinicTool.DataAccess.Entities;

namespace ClinicTool.Domain.Dto
{
    public class BookingSlotDto
    {
        public int Id { get; set; }

        public DateTime StarDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public BookingSlotStatus BookingSlotStatus { get; set; }
    }
}
