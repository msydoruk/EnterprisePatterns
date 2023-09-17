using ClinicTool.DataAccess.Dto;
using ClinicTool.DataAccess.Entities;

namespace ClinicTool.DataAccess.Repositories.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        List<Appointment> Search(AppointmentCriteriaDto appointmentParameters);

        void Update(UpdateAppointmentDto appointmentParameters);
    }
}

