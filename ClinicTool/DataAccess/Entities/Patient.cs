namespace ClinicTool.DataAccess.Entities
{
    public class Patient : Entity
    {
        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
