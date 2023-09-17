namespace ClinicTool.DataAccess.Entities
{
    public class Doctor : Entity
    {
        public string FullName { get; set; }

        public Specialty Specialty { get; set; }

        private ValueHolder<List<BookingSlot>> bookingSlots;

        public void SetValueHolder(ValueHolder<List<BookingSlot>> bookingSlots)
        {
            this.bookingSlots = bookingSlots;
        }

        public List<BookingSlot> GetBookingSlots()
        {
            return bookingSlots.GetValue(Id);
        }
    }
}
