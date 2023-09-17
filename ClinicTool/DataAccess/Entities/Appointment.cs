namespace ClinicTool.DataAccess.Entities
{
    public class Appointment : Entity
    {
        public Doctor Doctor { get; set; }

        public Patient Patient { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string Notes { get; set; }
    }
}
