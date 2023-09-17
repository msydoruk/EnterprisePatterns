using ClinicTool.Domain.Dto;

namespace ClinicTool.Domain.Services.Interfaces
{
    public interface IAppointmentService
    {
        AppointmentDto Get(int id);

        void Update(AppointmentDto appointmentDto);
    }
}



