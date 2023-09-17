namespace ClinicTool.DataAccess.Entities
{
    public class Specialty : Entity
    {
        public string Name { get; set; }

        public List<Doctor> Doctors { get; set; }
    }
}
