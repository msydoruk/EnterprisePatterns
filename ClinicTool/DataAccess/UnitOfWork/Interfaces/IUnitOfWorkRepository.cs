using ClinicTool.DataAccess.Repositories.Interfaces;

namespace ClinicTool.DataAccess.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        IAppointmentRepository AppointmentRepository { get; }

        IBookingSlotRepository BookingSlotRepository { get; }

        IDoctorRepository DoctorRepository { get; }

        IPatientRepository PatientRepository { get; }

        ISpecialtyRepository SpecialtyRepository { get; }
    }
}

