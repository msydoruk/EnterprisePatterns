using ClinicTool.DataAccess.Entities;
using ClinicTool.DataAccess.Repositories.Interfaces;
using ClinicTool.DataAccess.UnitOfWork.Interfaces;
using ClinicTool.Domain.Dto;
using ClinicTool.Domain.Services.Interfaces;

namespace ClinicTool.Domain.Services.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public AppointmentDto Get(int id)
        {
            using var context = unitOfWork.Create();
            var appointment = context.Repositories?.AppointmentRepository.Get(id);

            return new AppointmentDto
            {
                Id = appointment.Id,
                DoctorId = appointment.Doctor.Id,
                PatientId = appointment.Patient.Id,
                AppointmentDate = appointment.AppointmentDate,
                Notes = appointment.Notes
            };
        }

        public void Update(AppointmentDto appointmentDto)
        {
            using var context = unitOfWork.Create();
            var appointment = context.Repositories.AppointmentRepository.Get(appointmentDto.Id);

            if (appointment == null)
                throw new ArgumentNullException();

            appointment.AppointmentDate = appointmentDto.AppointmentDate;
            appointment.Notes = appointmentDto.Notes;

            context.Repositories?.AppointmentRepository.Update(appointment);

            context.SaveChanges();
        }
    }
}

