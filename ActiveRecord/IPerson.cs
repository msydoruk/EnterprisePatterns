namespace ActiveRecord
{
    public interface IPerson
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        int Add();

        void Update();

        bool Delete();

        bool ValidateEmail();

        bool HasPermissions();

        void CorrectFirstName();
    }
}
