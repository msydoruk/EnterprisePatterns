namespace ClinicTool.DataAccess.Entities
{
    public class BookingSlot : Entity
    {
        public DateTime StarDate { get; set; }

        public DateTime EndDate { get; set; }

        public BookingSlotStatus Status { get; set; }

        public Doctor Doctor { get; set; }
    }
}

