namespace ClinicTool.DataAccess.Dto
{
    public class UpdateAppointmentDto
    {
        public int Id { get; set; }

        public int? DoctorId { get; set; }

        public bool DoctorIsChanged { get; set; }

        public int? PatientId { get; set; }

        public bool PatientIsChanged { get; set; }

        public DateTime? AppointmentDate { get; set; }

        public bool AppointmentDateIsChanged { get; set; }

        public string? Notes { get; set; }

        public bool NotesIsChanged { get; set; }

        public bool IsChanged => DoctorIsChanged || PatientIsChanged || AppointmentDateIsChanged || NotesIsChanged;
    }
}
