namespace ClinicTool.DataAccess.Dto
{
    public class AppointmentCriteriaDto
    {
        public int? DoctorId { get; set; }

        public int? PatientId { get; set; }

        public DateTime StartAppointmentDate { get; set; }

        public DateTime EndAppointmentDate { get; set; } = DateTime.Now;

        public bool ValidAppointmentDateRange => StartAppointmentDate < EndAppointmentDate;

        public string? NotesFilter { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }
    }
}

