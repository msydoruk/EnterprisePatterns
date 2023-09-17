using System.Data.SqlClient;
using ClinicTool.DataAccess.Repositories.Implementation;
using ClinicTool.DataAccess.Repositories.Interfaces;
using ClinicTool.DataAccess.UnitOfWork.Interfaces;

namespace ClinicTool.DataAccess.UnitOfWork.Implementation
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        public IAppointmentRepository AppointmentRepository { get; }

        public IBookingSlotRepository BookingSlotRepository { get; }

        public IDoctorRepository DoctorRepository { get; }

        public IPatientRepository PatientRepository { get; }

        public ISpecialtyRepository SpecialtyRepository { get; }

        public UnitOfWorkRepository(SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            AppointmentRepository = new AppointmentRepository(sqlConnection, sqlTransaction);
            BookingSlotRepository = new BookingSlotRepository(sqlConnection, sqlTransaction);
            DoctorRepository = new DoctorRepository(sqlConnection, sqlTransaction);
            PatientRepository = new PatientRepository(sqlConnection, sqlTransaction);
            SpecialtyRepository = new SpecialtyRepository(sqlConnection, sqlTransaction);
        }
    }
}

